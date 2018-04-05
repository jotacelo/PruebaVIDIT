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
    public class ProvidersController : Controller
    {
        private ProductProviderEntities db = new ProductProviderEntities();



        private ProviderViewModel ViewModelConstructor(Provider data)
        {
            try
            {
                return new ProviderViewModel()
                {
                    IdProvider = data.idProvider,
                    AddressProvider = data.addressProvider,
                    IsAviable = data.isAviable,
                    NameProvider = data.nameProvider,
                    Website = data.website
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private Provider ProviderConstructor(ProviderViewModel data)
        {
            try
            {
                return new Provider()
                {
                    addressProvider = data.AddressProvider,
                    isAviable = data.IsAviable,
                    idProvider = data.IdProvider,
                    nameProvider = data.NameProvider,
                    website = data.Website
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        // GET: Provider
        public ActionResult Index()
        {
            List<ProviderViewModel> provider = new List<ProviderViewModel>();
            foreach (var data in db.Provider.ToList())
            {
                provider.Add(ViewModelConstructor(data));
            }
            return View(provider);
        }

        // GET: Providers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProviderViewModel providerViewModel = ViewModelConstructor(db.Provider.Find(id));
            if (providerViewModel == null)
            {
                return HttpNotFound();
            }
            return View(providerViewModel);
        }

        // GET: Providers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Providers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(ProviderViewModel providerViewModel)
        {
            if (ModelState.IsValid)
            {
                
                Provider provider= ProviderConstructor(providerViewModel);
                db.Provider.Add(provider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(providerViewModel);
        }

        // GET: Providers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProviderViewModel providerViewModel = ViewModelConstructor(db.Provider.Find(id));
            if (providerViewModel == null)
            {
                return HttpNotFound();
            }
            return View(providerViewModel);
        }

        // POST: Providers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProviderViewModel providerViewModel)
        {
            if (ModelState.IsValid)
            {
                        Provider provider = ProviderConstructor(providerViewModel);
                db.Entry(provider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(providerViewModel);
        }

        // GET: Providers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProviderViewModel providerViewModel = ViewModelConstructor(db.Provider.Find(id));
            if (providerViewModel == null)
            {
                return HttpNotFound();
            }
            return View(providerViewModel);
        }

        // POST: Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Provider provider= db.Provider.Find(id);
            var products = provider.Product.ToList();
            foreach(var a in products)
            {
                provider.Product.Remove(a);
            }
            db.Provider.Remove(provider);
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
    }
}