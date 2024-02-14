namespace FlightDocs_System.Model
{
    public class ResponseModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string>? Errors { get; set; }
        public object Data {  get; set; } 
    }
}
