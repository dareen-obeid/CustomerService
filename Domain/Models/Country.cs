using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
	public class Country
	{
        [Key]
        public int CountryId { get; set; }

        [Required]
        [StringLength(100)]
        public string CountryName { get; set; }




        public virtual ICollection<Address> Addresses { get; set; }

    }
}

