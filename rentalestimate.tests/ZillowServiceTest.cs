using System;
using rentalestimate.dataaccess.Repositories;
using rentalestimate.model.Users;
using rentalestimate.model.Zillow;
using rentalestimate.service.BussinessLogic;
using rentalestimate.service.Interfaces;
using Xunit;

namespace rentalestimate.tests
{
    public class ZillowServiceTests
    {

		private IZillowService _zillowService;

		public ZillowServiceTests(){

			_zillowService = new ZillowService(new ZillowRepository());
		}

        [Fact]
		public void ZillowService_GetSearchResultsTest()
        {
			UserInformation userInfo = new UserInformation()
			{
				Address = "337 Lippert Ave",
				City = "Fremont",
				ZipCode = "94539",
				StateCode = "CA"
			};
			SearchResult result = _zillowService.GetSearchResults(userInfo);
			Assert.NotNull(result);
			Assert.True(result.zestimateAmount != 0);
        }
    }
}
