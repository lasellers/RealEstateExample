﻿using System;
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                List<Listing> listingsList = _context.Listings.ToList();
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            try
            {
                // no id number given? then redirect to listings index
                if (id == null)
                    return RedirectToAction("Index", "Listings");

                //
                Listing listing = _context.Listings.SingleOrDefault(c => c.Id == id);

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
                Debug.WriteLine("InvalidCastException {0}", e.Message);

                var viewModel = new ListingViewModel
                {
                    Listing = new Listing()
                };
                return View(viewModel);
            }
            catch (ArgumentException e)
            {
                Debug.WriteLine("ArgumentException {0}", e.Message);

                return HttpNotFound();
            }
        }



        // GET: Listings/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            // if we are not logged in, do not allow deletes
            bool loggedIn = (System.Web.HttpContext.Current.User != null) &&
                        System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!loggedIn)
                return RedirectToAction("Index", "Listings", new { Error = @"Must be logged in" });

            //
            try
            {
                // no id number given? then redirect to listings index
                if (id == null)
                    return RedirectToAction("Index", "Listings");

                //
                //var listing = new Listing { Id = id ?? default(int) };
                Listing listing = new Listing { Id = id.GetValueOrDefault() };
                _context.Listings.Attach(listing);
                _context.Listings.Remove(listing);
                _context.SaveChanges();
            }
            catch (ArgumentException e)
            {
                Debug.WriteLine("ArgumentException {0}", e.Message);

                return HttpNotFound();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception {0}", e.Message);

                return View(id);
            }

            return RedirectToAction("Index", "Listings", new
            {
                Success = $"Listing {id} deleted"
            });
        }

      

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult New()
        {
            var realtors = _context.Realtors.ToList();
            var types = _context.ListingScheduleTypes.ToList();
            var photographs = _context.ListingPhotographs.ToList();

            var slRealtors = GetSelectListRealtors(realtors);
            var slTypes = GetSelectListListingScheduleTypes(types);
            var slPhotographs = GetSelectListingPhotographs(photographs);

            var viewModel = new ListingEditViewModel()
            {
                Listing = new Listing()
                {
                    RealtorId = 0,
                    ListingScheduleTypeId = 0
                },
                Realtors = realtors,
                ListingScheduleTypes = types,
                ListingPhotographs = photographs,
                SelectListRealtors = slRealtors,
                SelectListListingScheduleTypes = slTypes,
                SelectListListingPhotographs = slPhotographs
            };

            return View("Edit", viewModel);
        }


      

        /// <summary>
        /// GET: Listing/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            // if we are not logged in, do not allow edits
            bool loggedIn = (System.Web.HttpContext.Current.User != null) &&
                        System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!loggedIn)
                return RedirectToAction("Index", "Listings", new { Error = @"Must be logged in" });

            //
            try
            {
                // no id number given? then redirect to listings index
                if (id == null)
                    return RedirectToAction("Index", "Listings");

                Listing listing = _context.Listings.SingleOrDefault(r => r.Id == id);

                // if record doesn't exist 404
                if (listing == null)
                    return HttpNotFound();

                var realtors = _context.Realtors.ToList();
                var types = _context.ListingScheduleTypes.ToList();
                var photographs = _context.ListingPhotographs.ToList();

                var slRealtors = GetSelectListRealtors(realtors);
                var slTypes = GetSelectListListingScheduleTypes(types);
                var slPhotographs = GetSelectListingPhotographs(photographs);

                var viewModel = new ListingEditViewModel()
                {
                    Listing = listing,
                    Realtors = realtors,
                    ListingScheduleTypes = types,
                    ListingPhotographs = photographs,
                    SelectListRealtors = slRealtors,
                    SelectListListingScheduleTypes = slTypes,
                    SelectListListingPhotographs = slPhotographs
                };
                return View("Edit", viewModel);
            }
            catch (ArgumentException e)
            {
                Debug.WriteLine("ArgumentException {0}", e.Message);

                return HttpNotFound();
            }
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
                viewModel.Listing.Created = DateTime.Now;
                _context.Listings.Add(viewModel.Listing);
            }
            else
            {
                Listing listingInDb = _context.Listings.SingleOrDefault(m => m.Id == viewModel.Listing.Id);

                try
                {
                    listingInDb.Created = System.DateTime.Now;
                    listingInDb.Name = viewModel.Listing.Name??"No Name";
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
                catch (NullReferenceException e)
                {
                    Debug.WriteLine("NullReferenceException: {0}", e.Message);
                }

            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                Debug.WriteLine("DbEntityValidationException: {0}", e.Message);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                Debug.WriteLine("DbUpdateException: {0}", e.Message);
            }

            //return RedirectToAction("Edit", "Listings", new { Id = viewModel.Listing.Id });
            return RedirectToAction("Index", "Listings", new
            {
                Success = $"Listing {viewModel.Listing.Id} saved"
            });
        }


      
        /// <summary>
        /// Note: SelectListItem is predefined.
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetSelectListRealtors(List<Realtor> elements)
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<SelectListItem>();

            // For each string in the 'elements' variable, create a new SelectListItem object
            // that has both its Value and Text properties set to a particular value.
            // This will result in MVC rendering each item as:
            //     <option value="State Name">State Name</option>
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.Id.ToString(),
                    Text = element.Name
                });
            }

            return selectList;
        }

        /// <summary>
        /// Note: SelectListItem is predefined.
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetSelectListListingScheduleTypes(List<ListingScheduleType> elements)
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<SelectListItem>();

            // For each string in the 'elements' variable, create a new SelectListItem object
            // that has both its Value and Text properties set to a particular value.
            // This will result in MVC rendering each item as:
            //     <option value="State Name">State Name</option>
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.Id.ToString(),
                    Text = element.Cost.ToString()
                });
            }

            return selectList;
        }

        /// <summary>
        /// Note: SelectListItem is predefined.
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetSelectListingPhotographs(List<ListingPhotograph> elements)
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<SelectListItem>();

            // For each string in the 'elements' variable, create a new SelectListItem object
            // that has both its Value and Text properties set to a particular value.
            // This will result in MVC rendering each item as:
            //     <option value="State Name">State Name</option>
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.Id.ToString(),
                    Text = element.Name
                });
            }

            return selectList;
        }
    }
}