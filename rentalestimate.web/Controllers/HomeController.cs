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

namespace rentalestimate.web.Controllers
{
    public class HomeController : Controller
    {
        
		private IUserInformationService _userInformationService;
		private List<CatalogModel> _states = new List<CatalogModel>();
		private IHttpContextAccessor _accessor;
		private IStateService _stateService;

		public HomeController(
			IHttpContextAccessor accessor,
			IUserInformationService userInformationService, 
			IStateService stateService){

			_accessor = accessor;
			_userInformationService = userInformationService;
			_stateService = stateService;
			SetStates();
		}      
  
        public ActionResult Index()
        {
			ViewBag.States = this._states;

			List<UserInformationForm> users = new List<UserInformationForm>();
            _userInformationService.GetUsers().ForEach(user => users.Add(new UserInformationForm(user)));         
            
			return View(users);
        }

        [Route("users")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Users()
        {
			List<UserInformationForm> users = new List<UserInformationForm>();

			_userInformationService.GetUsers().ForEach(user => users.Add(new UserInformationForm(user)));         

			return Json(users);
        }

        [Route("users/adduser")]
        [HttpPost]
		public ActionResult AddUser(UserInformationForm newUser)
        {
			string requestIPAdress = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

			_userInformationService.AddUser(newUser.ToUserInformation(requestIPAdress));
            return Content("Success :)");
        }

		public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		private void SetStates()
        {
			_states.Add(new CatalogModel()
            {
                Id = 0,
                Code = "0",
                Name = "Select your state"
            });
            _stateService.GetStates().
                ForEach(state => _states.Add(
                   new CatalogModel()
                   {
                       Id = state.Id,
                       Code = state.StateCode,
                       Name = state.Name
                   }));
        }
    }
}
