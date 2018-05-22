using System;
using System.Collections.Generic;
using rentalestimate.model.Base;

namespace rentalestimate.dataaccess.Base
{
	public interface IBaseRepository<T> where T : BaseEntity
    {
        IEnumerable<T> Get();
		T Add(T newEntity);
		T Get(int entityId);
    }
}
