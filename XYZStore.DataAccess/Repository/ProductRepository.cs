using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZStore.DataAccess.Repository.IRepository;
using XYZStore.Models;
using XYZStore.Models.Models;

namespace XYZStore.DataAccess.Repository
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private ApplicationDbContext _db;

		public ProductRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(Product obj)
		{
			var objFromDb = _db.Products.FirstOrDefault(u=>u.Id== obj.Id);
			if(objFromDb != null)
			{
				objFromDb.Title = obj.Title;
				objFromDb.Year = obj.Year;
				objFromDb.IMDB = obj.IMDB;
				objFromDb.Price = obj.Price;
				objFromDb.Price5 = obj.Price5;
				objFromDb.Price10 = obj.Price10;
				objFromDb.ListPrice = obj.ListPrice;
				objFromDb.Description = obj.Description;
				objFromDb.CategoryId = obj.CategoryId;
				objFromDb.Director = obj.Director;
				objFromDb.CoverTypeId = obj.CoverTypeId;
				if(obj.ImageUrl != null)
				{
					objFromDb.ImageUrl = obj.ImageUrl;
				}
			}
		}
	}
}
