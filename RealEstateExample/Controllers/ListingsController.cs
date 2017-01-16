using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateExample.Models;
using RealEstateExample.ViewModels;
using System.Diagnostics;

namespace RealEstateExample.Controllers
{
    public class ListingsController : Controller
    {
        private ApplicationDbContext _context;

        public ListingsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            //base.Dispose(disposing);
            _context.Dispose();
        }

        // GET: Listings
        public ActionResult Index()
        {
            try
            {
               // var listings = GetListings();

                var listingsList = _context.Listings.ToList();
                return View(listingsList);
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("Debug: {0}", e.Message);

                var listings2 = new List<Listing>();
                return View(listings2);
            }
        }

        // GET: Listings/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                // var listing = GetListings().SingleOrDefault(c => c.Id == id);

                var listing = _context.Listings.SingleOrDefault(c => c.Id == id);

                if (listing == null)
                    return HttpNotFound();

                var viewModel = new ListingViewModel
                {
                    Listing = listing
                };
                return View(viewModel);

            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("Debug: {0}", e.Message);

                var viewModel = new ListingViewModel
                {
                    Listing = new Listing()
                };
                return View(viewModel);
            }
        }

     /*   // GET: Listings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Listings/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/


        // GET: Listings/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                //  var id32 = Convert.ToByte(id);
                var listing = new Listing { Id = id };
                _context.Listings.Attach(listing);
                _context.Listings.Remove(listing);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View(id);
            }

            return RedirectToAction("Index","Listings");
        }

        // POST: Listings/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Listings/Random
        /*  public ActionResult Random()
          {
              var listing = new Listing() { Name = "Shrek" };

              var realtor = new Realtor { Name = "Customer 1" };

              var viewModel = new ListingViewModel
              {
                  Listing = listing
              };

              return View(viewModel);
          }*/



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult New()
        {
            var scheduleTypes = _context.ListingScheduleTypes.ToList();

            var viewModel = new ListingViewModel()
            {
                Listing = new Listing()/*,
                ListingScheduleTypes = scheduleTypes*/
            };
            return View("Edit", viewModel);
        }

        /// <summary>
        /// POST: Listing/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// GET: Listing/Edit/5
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Edit(int Id)
        {
            var listing = _context.Listings.SingleOrDefault(r => r.Id == Id);

            if (listing == null)
                return HttpNotFound();

            var realtors = _context.Realtors.ToList();

            var viewModel = new ListingEditViewModel()
            {
                Listing = listing,
                Realtors = realtors
            };
            return View("Edit", viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult Save(ListingViewModel viewModel)
        {
            if (viewModel.Listing.Id == 0)
            {
                viewModel.Listing.Created = System.DateTime.Now;
                _context.Listings.Add(viewModel.Listing);
            }
            else
            {
                var listingInDb = _context.Listings.SingleOrDefault(m => m.Id == viewModel.Listing.Id);

                try
                {
                    listingInDb.Created = System.DateTime.Now;
                    listingInDb.Name = viewModel.Listing.Name;
                    listingInDb.Description = viewModel.Listing.Description;
                    listingInDb.Phone = viewModel.Listing.Phone;
                    listingInDb.Address = viewModel.Listing.Address;

                    listingInDb.Lat = viewModel.Listing.Lat;
                    listingInDb.Lng = viewModel.Listing.Lng;
                    listingInDb.Cost = viewModel.Listing.Cost;
                    listingInDb.BuildYear = viewModel.Listing.BuildYear;

                    listingInDb.RealtorId = viewModel.Listing.RealtorId;

                    listingInDb.State = viewModel.Listing.State;
                    
                    listingInDb.ListingScheduleTypeId = viewModel.Listing.ListingScheduleTypeId;
                }
                catch (NullReferenceException ex)
                {
                    Debug.WriteLine(ex.Message);
                }

            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                Debug.WriteLine(ex);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                Debug.WriteLine(ex);
            }

            return RedirectToAction("Index", "Listings");
        }


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
      /*  private IEnumerable<Listing> GetListings()
        {
            return new List<Listing>
            {
                   new Listing {Id = 1, Name = "House 1", BuildYear=1990, Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus sed condimentum enim. Mauris eleifend pretium blandit. Nullam eleifend in nibh ac accumsan. Sed ac arcu ac velit fringilla rhoncus. Nunc iaculis malesuada justo, vitae finibus lorem aliquam nec. Cras maximus enim malesuada, molestie augue eget, viverra nulla. Nulla sollicitudin pharetra nibh quis ultricies. In faucibus ex id ultricies fringilla. Ut imperdiet quam nec justo volutpat dictum. Donec convallis libero in sem molestie, in vehicula lectus consequat. Sed eros purus, eleifend vel ligula eget, dictum imperdiet metus. Duis eu congue ipsum. Curabitur eu porta nulla."},
                   new Listing {Id = 2, Name = "House 2",BuildYear=1960, Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus sed condimentum enim. Mauris eleifend pretium blandit. Nullam eleifend in nibh ac accumsan. Sed ac arcu ac velit fringilla rhoncus. Nunc iaculis malesuada justo, vitae finibus lorem aliquam nec. Cras maximus enim malesuada, molestie augue eget, viverra nulla. Nulla sollicitudin pharetra nibh quis ultricies. In faucibus ex id ultricies fringilla. Ut imperdiet quam nec justo volutpat dictum. Donec convallis libero in sem molestie, in vehicula lectus consequat. Sed eros purus, eleifend vel ligula eget, dictum imperdiet metus. Duis eu congue ipsum. Curabitur eu porta nulla."},
                   new Listing {Id = 3, Name = "My House 3",BuildYear=2003, Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus sed condimentum enim. Mauris eleifend pretium blandit. Nullam eleifend in nibh ac accumsan. Sed ac arcu ac velit fringilla rhoncus. Nunc iaculis malesuada justo, vitae finibus lorem aliquam nec. Cras maximus enim malesuada, molestie augue eget, viverra nulla. Nulla sollicitudin pharetra nibh quis ultricies. In faucibus ex id ultricies fringilla. Ut imperdiet quam nec justo volutpat dictum. Donec convallis libero in sem molestie, in vehicula lectus consequat. Sed eros purus, eleifend vel ligula eget, dictum imperdiet metus. Duis eu congue ipsum. Curabitur eu porta nulla."},
                   new Listing {Id = 4, Name = "Expensive House 4",BuildYear=1990,  Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus sed condimentum enim. Mauris eleifend pretium blandit. Nullam eleifend in nibh ac accumsan. Sed ac arcu ac velit fringilla rhoncus. Nunc iaculis malesuada justo, vitae finibus lorem aliquam nec. Cras maximus enim malesuada, molestie augue eget, viverra nulla. Nulla sollicitudin pharetra nibh quis ultricies. In faucibus ex id ultricies fringilla. Ut imperdiet quam nec justo volutpat dictum. Donec convallis libero in sem molestie, in vehicula lectus consequat. Sed eros purus, eleifend vel ligula eget, dictum imperdiet metus. Duis eu congue ipsum. Curabitur eu porta nulla."}
            };
        }
        */
    }

}