using System;
using rentalestimate.model.Users;
using rentalestimate.model.Zillow;

namespace rentalestimate.service.Interfaces
{
    public interface IZillowService
    {
		SearchResult GetSearchResults(UserInformation userInformation);
    }
}
