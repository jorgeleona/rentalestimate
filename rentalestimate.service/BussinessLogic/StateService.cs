using System;
using System.Collections.Generic;
using rentalestimate.dataaccess.Interfaces;
using rentalestimate.model.Users;
using rentalestimate.service.Interfaces;

namespace rentalestimate.service.BussinessLogic
{
	public class UserInformationService : IUserInformationService
    {
		private IUserInformationRepository _userInformationRepository;

		public UserInformationService(IUserInformationRepository userInformationRepository)
        {
			_userInformationRepository = userInformationRepository;
        }

       
        public UserInformation AddUser(UserInformation newUser)
        {
			return _userInformationRepository.AddUser(newUser);
        }

		public List<UserInformation> GetUsers()
        {

			return _userInformationRepository.GetUsers();
        }


    }
}
