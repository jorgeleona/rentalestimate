using System;
using rentalestimate.model.Base;

namespace rentalestimate.model.Catalogs
{
	public class State : BaseEntity
    {
		public string Name { get; set; }

		public string StateCode { get; set; }
    }
}
