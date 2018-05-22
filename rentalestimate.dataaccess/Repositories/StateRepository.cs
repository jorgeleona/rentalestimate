using System;
using rentalestimate.dataaccess.Interfaces;
using rentalestimate.dataaccess.Base;
using rentalestimate.model;
using System.Collections.Generic;
using rentalestimate.model.Catalogs;

namespace rentalestimate.dataaccess.Repositories
{
	public class StateRepository : BaseRepository<State>, IStateRepository
    {
       
		public List<State> GetStates(){
			return new List<State>(base.Get());
		}

		
    }
}
