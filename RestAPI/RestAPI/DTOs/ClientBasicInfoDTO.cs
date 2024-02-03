using System.ComponentModel.DataAnnotations;

namespace RestAPI.DTOs
{
    public class ClientBasicInfoDTO
    {
        [Required(ErrorMessage = "Client's FirstName is required.")]
        [StringLength(120, ErrorMessage = "Client's FirstName cannot be longer than 120 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Client's LastName is required.")]
        [StringLength(120, ErrorMessage = "Client's LastName cannot be longer than 120 characters.")]
        public string LastName { get; set; }
    }
}
