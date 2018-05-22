using System;
using System.Collections.Generic;
using rentalestimate.model.Users;
using rentalestimate.model.Catalogs;

namespace rentalestimate.web.Models
{
	public class UserInformationForm
    {
		public UserInformationForm(){}
        
		public UserInformationForm(UserInformation user){

			Id = user.Id;
			FirstName = user.FirstName;
			LastName = user.LastName;
			PhoneNumber = user.PhoneNumber;
			EMail = user.EMail;
			IPAddress = user.IPAddress;
			Address = user.Address;
			City = user.City;
			ZipCode = user.ZipCode;
			StateCode = user.StateCode;
			MonthlyRangeLow = user.MonthlyRangeLow.ToString("C2");
			MonthlyRangeHigh =  user.MonthlyRangeHigh.ToString("C2");
		}

		public UserInformation ToUserInformation(string requestIPAddress){
			return new UserInformation()
			{
                Id = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                PhoneNumber = this.PhoneNumber,
                EMail = this.EMail,
				Address = this.Address,
				City = this.City,
				ZipCode = this.ZipCode,
				StateCode = this.StateCode,
				IPAddress = requestIPAddress
				                
			};
		}

        public int Id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string EMail { get; set; }
        
		public string IPAddress { get; set; }

		public string Address { get; set; }

		public string City { get; set; }

		public string ZipCode { get; set; }

		public string StateCode { get; set; }

		public string RequestIPAddress { get; set; }

		public string MonthlyRangeLow { get; set; }

		public string MonthlyRangeHigh { get; set; }
    }
    
}
