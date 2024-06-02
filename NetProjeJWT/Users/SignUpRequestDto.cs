namespace NetProjeJWT.Users
{
    public record SignUpRequestDto(
        string UserName,
        string Email,
        string Password,
        string Name,
        string Lastname,
        DateTime? BirthDate);
}
