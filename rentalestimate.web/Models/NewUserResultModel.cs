using System;
using rentalestimate.model.Users;

namespace rentalestimate.web.Models
{
	public class NewUserResultModel
	{
		public NewUserResultModel() { }

		public NewUserResultModel(UserInformation user)
		{
			Success = user.Id > 0;
			Message = user.Id > 0 ? "Your rental has been estimated, we are sending you an EMail with this information" : 
			              "Ups! we could not save your information, please try again";
			MonthlyRangeLow = user.MonthlyRangeLow.ToString("C2");
            MonthlyRangeHigh = user.MonthlyRangeHigh.ToString("C2");
		}

		public bool Success { get; private set; }
		public string Message { get; private set; }
		public string MonthlyRangeLow { get; private set; }
		public string MonthlyRangeHigh { get; private set; }
	}
}
