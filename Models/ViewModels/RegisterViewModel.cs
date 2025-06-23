namespace AuthRoleManager.Models.ViewModels;

public class RegisterViewModel
{
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}
