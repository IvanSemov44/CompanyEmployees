namespace Shared.DataTransferObjects
{
    using System.ComponentModel.DataAnnotations;

    public abstract record CompanyForManipulationDto
    {
        [Required(ErrorMessage = "Company name is required field.")]
        [MaxLength(60, ErrorMessage = "Maximum lenght for Name is 60 characters.")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "Address name is required field.")]
        [MaxLength(60, ErrorMessage = "Address lenght for Name is 60 characters.")]
        public string? Address { get; init; }

        [Required(ErrorMessage = "Country name is required field.")]
        [MaxLength(60, ErrorMessage = "Maximum Country for Name is 60 characters.")]
        public string? Country { get; init; }

        public IEnumerable<EmployeeForCreationDto>? Employees;
    }
}
