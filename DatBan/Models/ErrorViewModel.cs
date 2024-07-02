namespace DatBan.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    public class WeatherForecast
    {
        public DateTime Date { get; set; }
    }
}
