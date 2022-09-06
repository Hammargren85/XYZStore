using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZStore.Models.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
		[Required]
		public int Year { get; set; }
		public string Description { get; set; }
        [Required]
        public string IMDB { get; set; }
		[Required]
		public string Director { get; set; }
        [Required]
        [Range(1,10000)]
		[DisplayName("Price for 1!")]
		public double Price { get; set; }
		[Required]
		[Range(1, 10000)]
		[DisplayName("Price for 5!")]
		public double Price5 { get; set; }
		[Required]
		[Range(1, 10000)]
		[DisplayName("Price for 10!")]
		public double Price10 { get; set; }
		[Required]
		[Range(1, 10000)]
		[DisplayName("List Price")]
		public double ListPrice { get; set; }
		[ValidateNever]
		public string ImageUrl { get; set; }
		[Required]
		[Display(Name = "Category")]
		public int CategoryId { get; set; }
		[ForeignKey("CategoryId")]
		[ValidateNever]
		public Category Category { get; set; }
		[Required]
		[Display(Name = "Cover Type")]
		public int CoverTypeId { get; set; }
		[ValidateNever]
		public CoverType CoverType { get; set; }
	}
}
