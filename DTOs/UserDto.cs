using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public record UserDto
    {
        public int Id { get; init; }

        [Required(ErrorMessage = "שדה שם פרטי הוא חובה")]
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;

        [Required(ErrorMessage = "אימייל הוא שדה חובה")]
        [EmailAddress(ErrorMessage = "אימייל לא תקין - חסר @ או דומיין")]
        public string Email { get; init; } = string.Empty;

        public string? Password { get; init; }

        public string? Phone { get; init; }
        public string? Address { get; init; }

        public UserDto() { }

        public UserDto(int id, string firstName, string lastName, string email, string? password, string? phone, string? address)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Phone = phone;
            Address = address;
        }
    }
}