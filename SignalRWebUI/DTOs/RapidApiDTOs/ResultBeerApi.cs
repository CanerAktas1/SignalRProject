using Newtonsoft.Json;

namespace SignalRWebUI.DTOs.RapidApiDTOs
{
    public class ApiResponse
    {
        public List<ResultBeerApi> Data { get; set; }
    }

    public class ResultBeerApi
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string abv { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string Image { get; set; }
    }
}
