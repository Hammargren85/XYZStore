using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZStore.DataAccess.Repository.IRepository;
using XYZStore.Models;

namespace XYZStore.DataAccess.Repository
{
	public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
	{
		private ApplicationDbContext _db;

		public CoverTypeRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(CoverType obj)
		{
			_db.CoverType.Update(obj);
		}
	}
}
