using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using XYZStore.Models.ViewModels;
using XYZStore.Models.Models;
using XYZStore.DataAccess.Repository.IRepository;

namespace XYZStore.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public ShoppingCartVM ShoppingCartVM { get; set; }
		public int OrderTotal { get; set; }
		public CartController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			ShoppingCartVM = new ShoppingCartVM()
			{
				ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,
				includeProperties: "Product")
			};
			foreach(var cart in ShoppingCartVM.ListCart)
			{
				cart.Price = GetPriceBasedOnQuntity(cart.Count,cart.Product.Price,
				cart.Product.Price5,cart.Product.Price10);
				ShoppingCartVM.CartTotal += (cart.Price * cart.Count); 
			}
			return View(ShoppingCartVM);
	    }
		public IActionResult Summary()
		{
			//var claimsIdentity = (ClaimsIdentity)User.Identity;
			//var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			//ShoppingCartVM = new ShoppingCartVM()
			//{
			//	ListCart = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,
			//	includeProperties: "Product")
			//};
			//foreach (var cart in ShoppingCartVM.ListCart)
			//{
			//	cart.Price = GetPriceBasedOnQuntity(cart.Count, cart.Product.Price,
			//	cart.Product.Price5, cart.Product.Price10);
			//	ShoppingCartVM.CartTotal += (cart.Price * cart.Count);
			//}
			//return View(ShoppingCartVM);
			return View();
		}

		public IActionResult Plus(int cartId)
		{
			var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
			_unitOfWork.ShoppingCart.IncrementCount(cart, 1);
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}
		public IActionResult Minus(int cartId)
		{
			var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
			if(cart.Count <= 1) 
			{
				_unitOfWork.ShoppingCart.Remove(cart);
			}
			else
			{
				_unitOfWork.ShoppingCart.DecrementCount(cart, 1);
			}
			
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}
		public IActionResult Remove(int cartId)
		{
			var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
			_unitOfWork.ShoppingCart.Remove(cart);
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}

		private double GetPriceBasedOnQuntity(double quantity,double price,double price5,double price10)
		{	
			if(quantity <= 5)
			{
				return price;
			}
			else
			{

			if(quantity <=10)
			{
				return price5;
			}
				return price10;
			}
		}	
    }
}
