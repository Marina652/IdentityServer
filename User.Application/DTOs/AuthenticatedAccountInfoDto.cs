namespace User.Application.DTOs;

public class AuthenticatedAccountInfoDto
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public IList<string> UserRoles { get; set; }
}
