using System;
using rentalestimate.model.Catalogs;
using System.Collections.Generic;

namespace rentalestimate.dataaccess.Interfaces
{
	public interface IStateRepository
    {

		List<State> GetStates();

    }
}
