namespace SignalRWebUI.DTOs.RapidApiDTOs
{

    public class Data
    {
        public int count { get; set; }
        public string filteredOn { get; set; }
        public ResultCocktailApi[] cocktails { get; set; }
    }

    public class ResultCocktailApi
    {
        public string name { get; set; }
        public string glass { get; set; }
        public string category { get; set; }
        public string garnish { get; set; }
        public string preparation { get; set; }
    }

    public class Ingredient
    {
        public string unit { get; set; }
        public float amount { get; set; }
        public string ingredient { get; set; }
        public string label { get; set; }
        public string special { get; set; }
    }
}