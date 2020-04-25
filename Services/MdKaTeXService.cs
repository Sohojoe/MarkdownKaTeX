using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace MarkdownKaTeX
{
    public class MdKaTeXService
    {
        async Task<string> DownloadTextFileAsync(string path)
        {
            HttpClient http = new HttpClient();
            string text = string.Empty;
            text = await http.GetStringAsync(path);
            return text;
        } 

        public async Task<string> FileToMarkdownAsync(string path)
        {
            string text = await DownloadTextFileAsync(path);
            var markup = new MarkupString(text);
            return markup.Value;
        }        
    }
}