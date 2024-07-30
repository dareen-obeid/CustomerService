using System;
namespace Application.DTOs
{
	public class CustomerDto
	{
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool IsActive { get; set; }

        public string GenderName { get; set; }
        public List<AddressDto> Addresses { get; set; }
    }


    public class CreateCustomerDto
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool IsActive { get; set; }

        public string GenderName { get; set; }
    }
}

