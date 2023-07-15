using Newtonsoft.Json;

namespace GiphyWithDotNet.Models
{
    public class GifViewModel
    {
        public class Gif
        {
            public string Url { get; set; }
        }

        public async Task<GifViewModel> GetTrendingGifs(string GiphyApiKey, HttpClient httpClient)
        {
            var response = await httpClient.GetAsync($"https://api.giphy.com/v1/gifs/trending?api_key={GiphyApiKey}&limit=3");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var giphyResponse = JsonConvert.DeserializeObject<GifViewModel>(jsonString);
            return giphyResponse;
        }

        public async Task<GifViewModel> SearchGifs(string GiphyApiKey, HttpClient httpClient, string searchWord)
        {
            var encodedSearchWord = Uri.EscapeDataString(searchWord ?? "");
            var response = await httpClient.GetAsync($"https://api.giphy.com/v1/gifs/search?q={encodedSearchWord}&api_key={GiphyApiKey}&limit=3");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var giphyResponse = JsonConvert.DeserializeObject<GifViewModel>(jsonString);
            return giphyResponse;
        }
    }
}
