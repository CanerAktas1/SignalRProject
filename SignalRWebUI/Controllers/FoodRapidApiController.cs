using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SignalRWebUI.DTOs.RapidApiDTOs;
using System.Drawing.Printing;
using System.Net.Http.Headers;
using X.PagedList.Extensions;


namespace SignalRWebUI.Controllers
{
    public class FoodRapidApiController : Controller
    {
        public async Task<IActionResult> Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://tasty.p.rapidapi.com/recipes/list?from=0&size=20&tags=under_30_minutes"),
                Headers =
                {
                    { "x-rapidapi-key", "b4deb6a935msh7c738041746736fp169ad5jsne6e8101f6317" },
                    { "x-rapidapi-host", "tasty.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                //var values = JsonConvert.DeserializeObject<List<ResultTastyApi>>(body);
                //return View(values.ToList());
                var root = JsonConvert.DeserializeObject<RootTastyApi>(body);
                var values = root.Results.ToList().ToPagedList(pageNumber, pageSize);
                ViewBag.Recipes = values;
                return View(values);
            }
        }

        public async Task<IActionResult> BeerList(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://beer9.p.rapidapi.com/?brewery=Berkshire%20brewing%20company"),
                Headers =
    {
        { "x-rapidapi-key", "b4deb6a935msh7c738041746736fp169ad5jsne6e8101f6317" },
        { "x-rapidapi-host", "beer9.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var responseMessage = JsonConvert.DeserializeObject<ApiResponse>(body);
                var beers = responseMessage.Data;
                var pagedBeers = beers.ToPagedList(pageNumber, pageSize);
                ViewBag.Beers = pagedBeers;
                return View(pagedBeers);
            }
        }

        public async Task<IActionResult> Coctails(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://cocktail-recipe-api-apiverve.p.rapidapi.com/v1/cocktailingredient?ingredient=gin"),
                Headers =
                {
                    { "x-rapidapi-key", "b4deb6a935msh7c738041746736fp169ad5jsne6e8101f6317" },
                    { "x-rapidapi-host", "cocktail-recipe-api-apiverve.p.rapidapi.com" },
                    { "Accept", "application/json" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var responseMessage=JsonConvert.DeserializeObject<Data>(body);
                var cocktails = responseMessage.cocktails.ToList();
                var pagedCocktails = cocktails.ToPagedList(pageNumber,pageSize);
                ViewBag.Cocktails = pagedCocktails;
                return View(pagedCocktails);
            }
        }
    }
}
