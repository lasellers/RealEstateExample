using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateExample.Models;
using RealEstateExample.ViewModels;

namespace RealEstateExample.Controllers
{
    public class ListingScheduleTypesController : Controller
    {
        private ApplicationDbContext _context;

        public ListingScheduleTypesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            //base.Dispose(disposing);
            _context.Dispose();
        }

        // GET: ListingScheduleTypes
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                List<ListingScheduleType> types = _context.ListingScheduleTypes.ToList();

                return View(types);
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("InvalidCastException: {0}", e.Message);

                return View(new List<ListingScheduleType>());
            }

        }

        // GET: ListingScheduleTypes/Details/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            try
            {
                // no id number given? then redirect to types index
                if (id == null)
                    return RedirectToAction("Index", "ListingScheduleTypes");

                //
                ListingScheduleType type = _context.ListingScheduleTypes.SingleOrDefault(c => c.Id == id);

                if (type == null)
                    return HttpNotFound();

                var viewModel = new ListingScheduleTypeViewModel
                {
                    ListingScheduleType = type
                };
                return View(viewModel);
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("InvalidCastException: {0}", e.Message);

                var viewModel2 = new ListingScheduleTypeViewModel
                {
                    ListingScheduleType = new ListingScheduleType()
                };
                return View(viewModel2);
            }
            catch (ArgumentException e)
            {
                Debug.WriteLine("ArgumentException: {0}", e.Message);

                return HttpNotFound();
            }
        }



        // GET: ListingScheduleTypes/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            // if we are not logged in, do not allow deletes
            bool loggedIn = (System.Web.HttpContext.Current.User != null) &&
                        System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!loggedIn)
                return RedirectToAction("Index", "ListingScheduleTypes", new { Error = @"Must be logged in" });

            //
            try
            {
                // no id number given? then redirect to types index
                if (id == null)
                    return RedirectToAction("Index", "ListingScheduleTypes");

                //
                var id32 = Convert.ToByte(id.GetValueOrDefault());
                ListingScheduleType type = new ListingScheduleType { Id = id32 };
                _context.ListingScheduleTypes.Attach(type);
                _context.ListingScheduleTypes.Remove(type);
                _context.SaveChanges();
            }
            catch (ArgumentException e)
            {
                Debug.WriteLine(e.Message);

                return HttpNotFound();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

                return View(id);
            }

            return RedirectToAction("Index", "ListingScheduleTypes", new
            {
                Success = $"List Schedule Type {id} deleted"
            });
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult New()
        {
            var viewModel = new ListingScheduleTypeViewModel()
            {
                ListingScheduleType = new ListingScheduleType()
            };

            return View("Edit", viewModel);
        }



        /// <summary>
        /// GET: ListingScheduleTypes/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            // if we are not logged in, do not allow edits
            bool loggedIn = (System.Web.HttpContext.Current.User != null) &&
                        System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!loggedIn)
                return RedirectToAction("Index", "ListingScheduleTypes", new { Error = @"Must be logged in" });

            //
            try
            {
                // no id number given? then redirect to types index
                if (id == null)
                    return RedirectToAction("Index", "ListingScheduleTypes");

                //
                ListingScheduleType type = _context.ListingScheduleTypes.SingleOrDefault(r => r.Id == id);

                // if record doesn't exist 404
                if (type == null)
                    return HttpNotFound();

                var viewModel = new ListingScheduleTypeViewModel()
                {
                    ListingScheduleType = type
                };
                return View("Edit", viewModel);
            }
            catch (ArgumentException e)
            {
                Debug.WriteLine("ArgumentException: {0}", e.Message);

                return HttpNotFound();
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(ListingScheduleTypeViewModel viewModel)
        {
            if (viewModel.ListingScheduleType.Id == 0)
            {
                //  viewModel.ListingScheduleType.Created = System.DateTime.Now;
                _context.ListingScheduleTypes.Add(viewModel.ListingScheduleType);
            }
            else
            {
                ListingScheduleType typeInDb = _context.ListingScheduleTypes.SingleOrDefault(m => m.Id == viewModel.ListingScheduleType.Id);

                try
                {
                    //   typeInDb.Id = viewModel.ListingScheduleType.Id;
                    typeInDb.Cost = viewModel.ListingScheduleType.Cost;
                    typeInDb.DiscountRate = viewModel.ListingScheduleType.DiscountRate;
                }
                catch (NullReferenceException e)
                {
                    Debug.WriteLine(e.Message);
                }

            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                Debug.WriteLine(e);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                Debug.WriteLine(e);
            }

            //return RedirectToAction("Edit", "ListingScheduleTypes", new { Id = viewModel.ListingScheduleType.Id });
            return RedirectToAction("Index", "ListingScheduleTypes", new
            {
                Success = $"Listing Schedule Type {viewModel.ListingScheduleType.Id} saved"
            });
        }


    }
}
