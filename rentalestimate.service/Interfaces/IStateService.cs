using System;
using System.Collections.Generic;
using rentalestimate.model.Catalogs;

namespace rentalestimate.service.Interfaces
{
	public interface IStateService
    {
		
		List<State> GetStates();
    }
}
