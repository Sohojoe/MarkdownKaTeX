using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
// using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Octokit;

namespace MarkdownKaTeX
{
    public class MdKaTeXService
    {
        string appName = "MarkdownKaTeX";
        string clientID = "d18caf90d72937793d29";
        string clientSecret = "7a0bd7b2fd793ee41c445d40347c1700b5b3c129";
        HttpClient _http;
        public HttpClient Http {get {
            if (_http==null)
            {
                _http = new HttpClient();
                // _http.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(appName));
                // _http.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue(appName));
                // _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(clientID, clientSecret);
            };
            return _http;
        }}
        GitHubClient __gitHubClient;
        GitHubClient _gitHubClient {get {
            if (__gitHubClient==null)
            {
               __gitHubClient = new Octokit.GitHubClient(new Octokit.ProductHeaderValue(appName));
               var basicAuth = new Credentials(clientID, clientSecret);
                __gitHubClient.Credentials = basicAuth;
            }
            return __gitHubClient;
        }}
        public string GitUser {get;set;} = "sohojoe";
        public string ReproName {get;set;} = "AccessibleRL";
        // public string GitUrl {get;set;} = "https://github.com/lilianweng/lil-log/";
        public string GitUrl { get {
            return $"https://github.com/{GitUser}/{ReproName}/";
        }}
        async Task<string> DownloadTextFileAsync(string path)
        {
            try
            {
                string text = string.Empty;
                text = await Http.GetStringAsync(path);
                return text;                
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> GetPageAsync(string localPath)
        {
            var path = new Uri(new Uri(GitUrl), localPath);
            try
            {
                var content = await _gitHubClient.Repository.Content.GetRawContent(GitUser, ReproName, localPath);
                if (content == null)
                    return null;
                var str = Encoding.Default.GetString(content);
                return str;
            }
            catch (Octokit.NotFoundException)
            {
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }  

        public async Task<string> FileToMarkdownAsync(string path)
        {
            string text = await DownloadTextFileAsync(path);
            var markup = new MarkupString(text);
            return markup.Value;
        }
        public async Task<List<GithubPost>> GetPostsAsync()
        {
            string gitUrl;
            string[] split;
            string[] pathElements;
            string gitUser;
            string reproName;
            string postsUrl;
            try
            {
                gitUrl = GitUrl.ToLowerInvariant();
                split = gitUrl.Split("github.com/");
                pathElements = split[1].Split("/");
                gitUser = pathElements[0];
                reproName = pathElements[1];
                postsUrl = $"http://api.github.com/repos/{gitUser}/{reproName}/contents/_posts?ref=master";

                // var user = await _gitHubClient.User.Get("sohojoe");
                // var repro = await _gitHubClient.Repository.Get(gitUser, reproName);
                var gitPosts = await _gitHubClient.Repository.Content.GetAllContents(gitUser, reproName, "_posts");
                // // var aa = await _gitHubClient.Search.SearchCode.`
                // var posts = await Http.GetFromJsonAsync<GithubPost[]>(postsUrl);
                var posts = new List<GithubPost>();
                foreach (var gp in gitPosts)
                {
                    var p = new GithubPost();
                    p.Name = gp.Name;
                    p.GitUser = gitUser;
                    p.ReproName = reproName;
                    p.NavUrl = $"{gitUser}/{reproName}/{gp.Path}";
                    p.RepositoryContent = gp;
                    posts.Add(p);
                }
                return posts.ToList();                
            }
            catch (Octokit.NotFoundException)
            {
                return null;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}