using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rentalestimate.web.Models;
using rentalestimate.service.Interfaces;
using rentalestimate.model.Catalogs;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace rentalestimate.web.Controllers
{
	[Route("api/[controller]")]
    public class HomeController : Controller
    {
        
		private IUserInformationService _userInformationService;
		private IHttpContextAccessor _accessor;
		private IStateService _stateService;

		public HomeController(
			IHttpContextAccessor accessor,
			IUserInformationService userInformationService, 
			IStateService stateService){

			_accessor = accessor;
			_userInformationService = userInformationService;
			_stateService = stateService;

		}        

        [Route("users")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Users()
        {
			List<UserInformationForm> users = new List<UserInformationForm>();

			_userInformationService.GetUsers().ForEach(user => users.Add(new UserInformationForm(user)));         

			return Json(users);
        }

		[Route("states")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult States()
        {
			List<CatalogModel> states = new List<CatalogModel>();
			states.Add(new CatalogModel()
            {
                Id = 0,
                Code = "0",
                Name = "Select your state"
            });
            _stateService.GetStates().
                ForEach(state => states.Add(
                   new CatalogModel()
                   {
                       Id = state.Id,
                       Code = state.StateCode,
                       Name = state.Name
                   }));

            return Json(states);
        }

        [Route("users/adduser")]
        [HttpPost]
		public ActionResult AddUser([FromBody]UserInformationForm newUser)
        {
			string requestIPAdress = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

			var userAdded = _userInformationService.AddUser(newUser.ToUserInformation(requestIPAdress));

			NewUserResultModel resultModel = new NewUserResultModel( userAdded);

			return Content(JsonConvert.SerializeObject(resultModel));
        }             
    }
}
