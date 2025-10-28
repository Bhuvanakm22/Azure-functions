namespace WebApplication1.Models
{
    public class SalesRequest
    {
        public string Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? Status { get; set; } = string.Empty;

    }
}
