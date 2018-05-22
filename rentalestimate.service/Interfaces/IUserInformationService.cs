using System;
using System.Collections.Generic;
using rentalestimate.model.Users;

namespace rentalestimate.service.Interfaces
{
    public interface IUserInformationService
    {
		
        UserInformation AddUser(UserInformation newUser);
		List<UserInformation> GetUsers();
    }
}
