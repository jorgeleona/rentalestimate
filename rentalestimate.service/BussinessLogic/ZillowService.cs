using System;
using rentalestimate.service.Interfaces;
using rentalestimate.model.Users;
using rentalestimate.model.Zillow;
using rentalestimate.dataaccess.Interfaces;

namespace rentalestimate.service.BussinessLogic
{
	public class ZillowService : IZillowService
    {
		private IZillowRepository _zillowRepository;

		public ZillowService(IZillowRepository zillowRepository)
        {
			_zillowRepository = zillowRepository;
        }

		public SearchResult GetSearchResults(UserInformation userInformation){

			return _zillowRepository.GetSearchResults(userInformation);
		}
    }
}
