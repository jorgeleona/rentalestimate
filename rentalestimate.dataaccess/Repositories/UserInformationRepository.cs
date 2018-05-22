using System;
using rentalestimate.dataaccess.Interfaces;
using rentalestimate.dataaccess.Base;
using rentalestimate.model;
using System.Collections.Generic;
using rentalestimate.model.Users;

namespace rentalestimate.dataaccess.Repositories
{
	public class UserInformationRepository : BaseRepository<UserInformation>, IUserInformationRepository
    {
      
		public List<UserInformation> GetUsers(){
			
			return new List<UserInformation>(base.Get());
		}

		public UserInformation AddUser(UserInformation newUser){
			return base.Add(newUser);
		}

		public UserInformation GetUser(int userId){
			return base.Get(userId);
		}
    }
}
