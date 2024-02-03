using System.ComponentModel.DataAnnotations;

namespace RestAPI.DTOs
{
    public class CountryBasicInfoDTO
    {
        [Required(ErrorMessage = "Country's Name is required.")]
        [StringLength(120, ErrorMessage = "Country's Name cannot be longer than 120 characters.")]
        public string Name { get; set; }
    }
}
