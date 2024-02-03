using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.DTOs
{
    public class TripsDTO
    {
        [Required(ErrorMessage = "Trip's Name is required.")]
        [StringLength(120, ErrorMessage = "Trip's Name cannot be longer than 120 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Trip's Description is required.")]
        [StringLength(220, ErrorMessage = "Trip's Description cannot be longer than 220 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Trip's DateFrom is required.")]
        public DateTime DateFrom { get; set; }

        [Required(ErrorMessage = "Trip's DateTo is required.")]
        public DateTime DateTo { get; set; }

        [Required(ErrorMessage = "Trip's MaxPeople is required.")]
        public int MaxPeople { get; set; }

        [Required(ErrorMessage = "Trip's list of countries is required")]
        public List<CountryBasicInfoDTO> Countries { get; set; }

        [Required(ErrorMessage = "Trip's list of clients is required")]
        public List<ClientBasicInfoDTO> Clients { get; set; }
    }
}
