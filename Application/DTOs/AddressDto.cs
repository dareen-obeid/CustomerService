using System;
namespace Application.DTOs
{
	public class AddressDto
	{
        public int AddressId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string AddressLine { get; set; }
        public string PostalCode { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool IsActive { get; set; }

        public string CountryName { get; set; }

    }
}

