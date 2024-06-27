namespace Shared.DataTransferObjects
{
    using System.ComponentModel.DataAnnotations;

    public record UserForAuthenticationDto
    {
        [Required(ErrorMessage = "User name is required.")]
        public string? Username { get; init; }
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; init; }
    }
}
