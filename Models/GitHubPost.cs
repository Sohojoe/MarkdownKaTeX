using System.Text.Json.Serialization;
using Octokit;

namespace MarkdownKaTeX
{

    public class GithubPost
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        // [JsonPropertyName("path")]
        // public string Path { get; set; }

        // [JsonPropertyName("sha")]
        // public string Sha { get; set; }

        // [JsonPropertyName("size")]
        // public int Size { get; set; }

        // [JsonPropertyName("url")]
        // public string Url { get; set; }

        // [JsonPropertyName("html_url")]
        // public string HtmlUrl { get; set; }

        // [JsonPropertyName("git_url")]
        // public string GitUrl { get; set; }

        // [JsonPropertyName("download_url")]
        // public string DownloadUrl { get; set; }
        public string GitUser { get; set; }
        public string ReproName { get; set; }
        public string NavUrl { get; set; }
        public RepositoryContent RepositoryContent { get; set; }

    }
}