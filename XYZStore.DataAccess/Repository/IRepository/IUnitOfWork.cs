﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZStore.DataAccess.Repository.IRepository
{
	public interface IUnitOfWork
	{
		ICategoryRepository Category { get; }
		ICoverTypeRepository CoverType { get; }
		IProductRepository Product { get; }
		ICompanyRepository Company { get; }
		void Save();
	}
}
