namespace SignalRWebUI.ErrorResponseModels
{
    public class ApiValidationErrorResponse
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int status { get; set; }
        public Dictionary<string,List<string>> Errors { get; set; }
        public string TraceId { get; set; }
    }
}
