namespace BusinessObject.Dto.Account;

public class UpdateAccountRequest
{
    public string? Email { get; set; }
    public string Role { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public int? ParentAccountId { get; set; }
}