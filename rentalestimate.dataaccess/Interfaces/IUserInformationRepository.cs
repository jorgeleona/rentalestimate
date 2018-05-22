using System;
using rentalestimate.model.Users;
using System.Collections.Generic;

namespace rentalestimate.dataaccess.Interfaces
{
    public interface IUserInformationRepository
    {

		List<UserInformation> GetUsers();
		UserInformation AddUser(UserInformation newUser);
		UserInformation GetUser(int userId);
    }
}
