using System;
namespace rentalestimate.model.Base
{
    public abstract class BaseEntity
    {
		public int Id { get; set; }
        
		public DateTime CreatedDate { get; set; }

        public string CreatedUser { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedUser { get; set; }
    }
}
