using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProductProvider.Models;
using ProductProvider.ViewModels;

namespace ProductProvider
{
    public class ProductsController : Controller
    {
        private ProductProviderEntities db = new ProductProviderEntities();

        
        
        private ProductViewModel ViewModelConstructor(Product data)
        {
            try { 
            return new ProductViewModel()
            {
                IdProduct = data.idProduct,
                NameProduct = data.nameProduct,
                Color = data.color,
                IsAvailable = data.isAvailable,
                ListPrice = data.listPrice,
                Quantity = data.quantity,
                Sku = data.sku,
                Upc = data.upc,
                
            };
            }catch(Exception ex)
            {
                return null;
            }
        }

        private Product ProductConstructor(ProductViewModel data)
        {
            try
            {
                List<Provider> providers = new List<Provider>();
                foreach(var s in data.SelectedProviders)
                {
                    providers.Add(db.Provider.Find(int.Parse(s)));
                }
                return new Product() {
                    color = data.Color,
                    isAvailable = data.IsAvailable,
                    idProduct = data.IdProduct,
                    listPrice = data.ListPrice,
                    nameProduct = data.NameProduct,
                    quantity = data.Quantity,
                    sku = data.Sku,
                    upc = data.Upc,
                    Provider = providers,
                    
                };
            }catch(Exception ex)
            {
                return null;
            }
        }

        // GET: Products
        public ActionResult Index()
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            foreach (var data in db.Product.ToList())
            {
                products.Add(ViewModelConstructor(data));
            }
            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            ProductViewModel productViewModel = ViewModelConstructor(db.Product.Find(id));
            if (productViewModel == null)
            {
                return HttpNotFound();
            }
            return View(productViewModel);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ProductViewModel pvm = new ProductViewModel();
            pvm.AvailableProviders = GetAvailableProviders();
            pvm.Color = "#123AB6";        
            return View(pvm);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(ProductViewModel productViewModel, string[] Providers)
        {
            if (ModelState.IsValid)
            {
            
                //Add a new product 
                Product product = ProductConstructor(productViewModel);
                db.Product.Add(product);
                db.SaveChanges();

                //Add all selected providers 
                foreach ( var p in productViewModel.SelectedProviders)
                {
                    int providerid;
                    int.TryParse(p,out providerid);
                    Provider provider = db.Provider.Where(s => s.idProvider == providerid).First();
                    product.Provider.Add(provider);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            productViewModel.AvailableProviders = GetAvailableProviders();
            return View(productViewModel);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            ProductViewModel productViewModel = ViewModelConstructor(product);
            productViewModel.AvailableProviders = GetAvailableProviders();
            foreach (var p in product.Provider) {

                productViewModel.AvailableProviders.Where(s => s.Value == p.idProvider.ToString()).First().Selected = true;
            }
            if (productViewModel == null)
            {
                return HttpNotFound();
            }
            return View(productViewModel);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Find the product to edit
                    var existingProduct = db.Product.Find(productViewModel.IdProduct);
                    var modifiedProduct = ProductConstructor(productViewModel);
                    if (existingProduct != null)
                    {
                        db.Entry(existingProduct).CurrentValues.SetValues(modifiedProduct);

                        foreach (var deleteProvider in existingProduct.Provider.ToList())
                        {
                            //Delete old providers that are no longer associated with the product
                            if (!modifiedProduct.Provider.Any(s => s.idProvider == deleteProvider.idProvider))
                                existingProduct.Provider.Remove(deleteProvider);
                        }
                        foreach (var providerModel in modifiedProduct.Provider)
                        {
                            //Add new providers to the product
                            var eProvider = existingProduct.Provider
                                .Where(c => c.idProvider == providerModel.idProvider)
                                .SingleOrDefault();

                            if (eProvider == null)
                                existingProduct.Provider.Add(db.Provider.Find(providerModel.idProvider));

                        }

                        db.SaveChanges();
                    }
                }
                catch (Exception ex) {
                    productViewModel.AvailableProviders = GetAvailableProviders();
                    foreach (var p in productViewModel.SelectedProviders)
                    {
                        productViewModel.AvailableProviders.Where(s => s.Value == p.ToString()).First().Selected = true;
                    }
                    return View(productViewModel);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductViewModel productViewModel = ViewModelConstructor(db.Product.Find(id));
            if (productViewModel == null)
            {
                return HttpNotFound();
            }
            return View(productViewModel);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Find the product and delete the info in table ProductProvider
            Product product = db.Product.Find(id);
            var providers = product.Provider.ToList();
            foreach(var pro in providers)
            {
                product.Provider.Remove(pro);
            }
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Function to fill with avialable providers for products
        private IList<SelectListItem> GetAvailableProviders() {

            List<SelectListItem> list = new List<SelectListItem>();
            foreach(var a in db.Provider.Where(s=> s.isAviable == true))
            {
                list.Add(new SelectListItem() {Value = a.idProvider.ToString(), Text=a.nameProvider, Selected = false });
            }

            return list;
        }
    }
}
