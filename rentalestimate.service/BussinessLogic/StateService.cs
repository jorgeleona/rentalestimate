using System;
using System.Collections.Generic;
using rentalestimate.dataaccess.Interfaces;
using rentalestimate.model.Catalogs;
using rentalestimate.service.Interfaces;

namespace rentalestimate.service.BussinessLogic
{
	public class StatesService : IStateService
    {
		private IStateRepository _stateRepository;

		public StatesService(IStateRepository stateRepository)
        {
			_stateRepository = stateRepository;
        }
        
		public List<State> GetStates()
        {

			return _stateRepository.GetStates();
        }


    }
}
