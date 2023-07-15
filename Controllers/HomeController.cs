using GiphyWithDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace GiphyWithDotNet.Controllers
{
    public class HomeController : Controller
    {
        private const string _GiphyApiKey = "QsfRn4Ng0eSY2TwgXJvb3s0y7TBmJN45";
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly GifViewModel _gifViewModel;

        public HomeController(IMemoryCache cache)
        {
            _httpClient = new HttpClient();
            _gifViewModel = new GifViewModel();
            _cache = cache;
        }

        public IActionResult Index()
        {
            return View();
        }        

        public async Task<ActionResult> TrendingGifs()
        {
            if (_cache.TryGetValue("trendingGifs", out GifViewModel cachedGifs))
            {
                return View(cachedGifs);
            }

            var giphyResponse = await _gifViewModel.GetTrendingGifs(_GiphyApiKey, _httpClient);


            _cache.Set("trendingGifs", giphyResponse, DateTime.Today.AddDays(1));

            return View(giphyResponse);
        }
        
        public IActionResult SearchGifs()
        {
            return View();
        }

        public async Task<ActionResult> Search(string searchWord)
        {
            GifViewModel cachedGifs;
            if (_cache.TryGetValue(searchWord, out cachedGifs))
            {
                return View("SearchGifs", cachedGifs);
            }

            var giphyResponse = await _gifViewModel.SearchGifs(_GiphyApiKey, _httpClient, searchWord);
            
            _cache.Set(searchWord, giphyResponse, DateTime.Today.AddDays(1));

            return View("SearchGifs", giphyResponse);
        }
    }
}