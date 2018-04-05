//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProductProvider.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Provider = new HashSet<Provider>();
        }
    
        public int idProduct { get; set; }
        public string nameProduct { get; set; }
        public decimal listPrice { get; set; }
        public string color { get; set; }
        public string sku { get; set; }
        public string upc { get; set; }
        public int quantity { get; set; }
        public bool isAvailable { get; set; }
        public string urlImage { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Provider> Provider { get; set; }
    }
}
