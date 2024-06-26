namespace Shared.DataTransferObjects
{
    using System.ComponentModel.DataAnnotations;

    public record UserForRegistrationDto
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        [Required(ErrorMessage = "UserName is required")]
        public string? UserName { get; init; }
        [Required(ErrorMessage = "Password is required")]
        public string? PassWord { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public ICollection<string>? Roles { get; init; }
    }
}
