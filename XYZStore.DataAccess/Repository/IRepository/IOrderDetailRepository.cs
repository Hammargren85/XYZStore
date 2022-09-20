﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZStore.Models;
using XYZStore.Models.Models;

namespace XYZStore.DataAccess.Repository.IRepository
{
	public interface IOrderDetailRepository : IRepository<OrderDetail>
	{
	 void Update(OrderDetail obj);
	}
}
