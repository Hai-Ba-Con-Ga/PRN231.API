namespace BusinessObject.Dto.Account;

public class AccountResponse
{
    public int AccountId { get; set; }
    public string Username { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public int? ParentAccountId { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
}