using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductProvider.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public int IdProduct { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string NameProduct { get; set; }
        [Required]
        [Display(Name="List Price")]
        public decimal ListPrice { get; set; }
        [Required]
public string Color { get; set; }
        public string Sku { get; set; }
        public string Upc { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [Display(Name="Is Available?")]
        public bool IsAvailable { get; set; }

        public IList<string> SelectedProviders { get; set; }
        public IList<SelectListItem> AvailableProviders { get; set; }

        public ProductViewModel()
        {
            SelectedProviders = new List<string>();
            AvailableProviders = new List<SelectListItem>();
        }

    }

   
}