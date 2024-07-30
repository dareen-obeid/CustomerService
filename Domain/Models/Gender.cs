using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
	public class Gender
	{
        [Key]
        public int GenderId { get; set; }

        [Required]
        [StringLength(20)]
        public string GenderName { get; set; }


        public virtual ICollection<Customer> Customers { get; set; }
    }
}

