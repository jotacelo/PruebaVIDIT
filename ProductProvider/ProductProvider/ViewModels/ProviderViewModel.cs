using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductProvider.ViewModels
{
    public class ProviderViewModel
    {
        [Key]
        public int IdProvider { get; set; }
        [Required]
        [Display(Name ="Provider")]
        public string NameProvider { get; set; }
        [Display(Name = "Address")]
        public string AddressProvider { get; set; }
        [Required]
        [Display(Name = "Is Available?")]
        public bool IsAviable { get; set; }
        public string Website { get; set; }

        public virtual ICollection<ProductViewModel> Product { get; set; }
    }
}