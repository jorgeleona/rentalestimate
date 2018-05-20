using System;
using System.Collections.Generic;
using rentalestimate.model.Users;
using rentalestimate.model.Catalogs;

namespace rentalestimate.web.Models
{
	public class CatalogModel
    {
        public int Id { get; set; }      
        
		public string Code { get; set; }

		public string Name { get; set; }
    }
    
}
