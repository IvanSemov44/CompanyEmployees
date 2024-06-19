namespace Shared.DataTransferObjects
{
    using System.ComponentModel.DataAnnotations;

    public abstract record EmployeeForManipulation
    {
        [Required(ErrorMessage = "Employee name is required field.")]
        [MaxLength(30, ErrorMessage = "Maximum lenght for the Name is 30 character.")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "Age is required field.")]
        [Range(18, int.MaxValue, ErrorMessage = "Age is required and it can't be lower than 18.")]
        public int Age { get; init; }

        [Required(ErrorMessage = "Position is required field")]
        [MaxLength(20, ErrorMessage = "Maximum lenght for the Position is 20 character")]
        public string? Position { get; init; }
    }
}
