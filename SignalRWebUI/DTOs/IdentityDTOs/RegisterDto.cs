namespace SignalRWebUI.DTOs.IdentityDTOs
{
    public class RegisterDto
    {
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Mail { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
