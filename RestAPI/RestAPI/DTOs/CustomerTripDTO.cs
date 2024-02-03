using System;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.DTOs
{
    public class CustomerTripDTO
    {
        [Required(ErrorMessage = "Client's FirstName is required.")]
        [StringLength(120, ErrorMessage = "Client's FirstName cannot be longer than 120 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Client's LastName is required.")]
        [StringLength(120, ErrorMessage = "Client's LastName cannot be longer than 120 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Client's Email is required.")]
        [StringLength(120, ErrorMessage = "Client's Email cannot be longer than 120 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Client's Telephone is required.")]
        [StringLength(120, ErrorMessage = "Client's Telephone cannot be longer than 120 characters.")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Client's Pesel is required.")]
        [StringLength(120, ErrorMessage = "Client's Pesel cannot be longer than 120 characters.")]
        public string Pesel { get; set; }

        [Required(ErrorMessage = "Trip's ID is required.")]
        public int IdTrip { get; set; }

        [Required(ErrorMessage = "Trip's Name is required.")]
        [StringLength(120, ErrorMessage = "Trip's Name cannot be longer than 120 characters.")]
        public string TripName { get; set; }

        public DateTime PaymentDate { get; set; }

    }
}
