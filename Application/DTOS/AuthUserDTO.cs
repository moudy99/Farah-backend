namespace Application.DTOS
{
    public class AuthUserDTO
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public DateTime ExpireTIme { get; set; }
        public List<string> Errors { get; set; }
    }
}
