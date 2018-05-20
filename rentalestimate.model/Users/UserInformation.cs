using System;

namespace rentalestimate.model
{
	public class UserInformation
    {
		public int UserInformationId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public string EMail { get; set; }
		public string IPAddress { get; set; }
		public double AnnualEstimate { get; set; }
    }
}
