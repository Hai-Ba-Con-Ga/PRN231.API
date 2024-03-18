namespace BusinessObject.Dto.Account;

public class SignupRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    
}