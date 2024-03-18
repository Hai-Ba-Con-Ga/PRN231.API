namespace BusinessObject.Dto.Account;

public class CreateAccountRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public int? ParentAccountId { get; set; }
    public string Role { get; set; } = null!;
}