using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Reflection;

namespace Domain.Models
{
	public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [ForeignKey("Gender")]
        public int GenderId { get; set; }

        [Phone]
        public string Phone { get; set; }


        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public DateTime LastUpdateDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}

