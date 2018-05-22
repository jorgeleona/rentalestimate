using System;
using rentalestimate.model.Zillow;
using rentalestimate.model.Users;

namespace rentalestimate.dataaccess.Interfaces
{
	public interface IZillowRepository
    {      
		SearchResult GetSearchResults(UserInformation userInformation);
    }
}
