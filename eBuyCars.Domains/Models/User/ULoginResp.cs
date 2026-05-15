namespace BuyCars.Domain.Models.User
{
    public class ULoginResp
    {
        public bool Status { get; set; }
        public string StatusMsg { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
