

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
    public class ListingPhotographsController : Controller
    {
        private ApplicationDbContext _context;

        public ListingPhotographsController()
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
                var listingsList = _context.Listings.ToList();
                return View(listingsList);
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("InvalidCastException: {0}", e.Message);

                return View(new List<Listing>());
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine("InvalidOperationException: {0}", e.Message);

                return View(new List<Listing>());
            }
        }

        // GET: Listings/Details/5
        public ActionResult Details(int id)
        {
            try
            {
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



        // GET: Listings/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
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

            return RedirectToAction("Index", "ListingPhotographs");
        }

        // POST: Listings/Delete/5
        /*  [HttpPost]
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
          }*/


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult New()
        {
            var scheduleTypes = _context.ListingScheduleTypes.ToList();

            var viewModel = new ListingEditViewModel()
            {
                Listing = new Listing(),
                ListingScheduleTypes = scheduleTypes
            };
            return View("Edit", viewModel);
        }

        /// <summary>
        /// POST: Listing/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
      /*  [HttpPost]
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
        }*/

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
            var types = _context.ListingScheduleTypes.ToList();
            var photographs = _context.ListingPhotographs.ToList();

            var viewModel = new ListingEditViewModel()
            {
                Listing = listing,
                Realtors = realtors,
                ListingScheduleTypes = types,
                ListingPhotographs = photographs
            };
            return View("Edit", viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
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

    }

}