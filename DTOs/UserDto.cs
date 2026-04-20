using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    
    public record UserResponseDto(
        int Id,
        string FirstName,
        string LastName,
        string Email,
        string? Phone,
        string? Address
    );

    
    public record UserRegisterDto
    {
        public int Id { get; init; }

        [Required(ErrorMessage = "שדה שם פרטי הוא חובה")]
        public string FirstName { get; init; } = string.Empty;

        public string LastName { get; init; } = string.Empty;

        [Required(ErrorMessage = "אימייל הוא שדה חובה")]
        [EmailAddress(ErrorMessage = "אימייל לא תקין")]
        public string Email { get; init; } = string.Empty;

        [Required(ErrorMessage = "סיסמה היא שדה חובה")]
        public string Password { get; init; } = string.Empty;

        public string? Phone { get; init; }
        public string? Address { get; init; }
    }

    
    public record UserLoginDto(
        [Required, EmailAddress] string Email,
        [Required] string Password
    );
}