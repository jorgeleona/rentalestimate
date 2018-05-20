using System;
using rentalestimate.model.Base;

namespace rentalestimate.model.Users
{
	public class UserInformation : BaseEntity
    {
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string PhoneNumber { get; set; }

		public string EMail { get; set; }

		public string IPAddress { get; set; }

		public double AnnualRentEstimate { get; set; }

		public string Address { get; set; }

		public string City { get; set; }

		public string ZipCode { get; set; }

		public string StateCode { get; set; }
    }
}
