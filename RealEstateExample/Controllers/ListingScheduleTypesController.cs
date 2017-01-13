using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateExample.Models;
using RealEstateExample.ViewModels;
using System.Diagnostics;

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
        public ActionResult Index()
        {
            // realtors == null || realtors.Count()==0

            try
            {
                var types = _context.ListingScheduleTypes.ToList();
    
                return View(types);
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("Debug: {0}", e.Message);

                var types2 = new List<ListingScheduleType> ();
                return View(types2);
            }

        }

        // GET: ListingScheduleTypes/Details/5
        public ActionResult Details(int id)
        {
            try
            {
              var type = _context.ListingScheduleTypes.SingleOrDefault(c => c.Id == id);

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
                Debug.WriteLine("Debug: {0}", e.Message);

                var viewModel2 = new ListingScheduleTypeViewModel
                {
                    ListingScheduleType = new ListingScheduleType()
                };
                return View(viewModel2);
            }
        }



        // GET: ListingScheduleTypes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        /// <summary>
        /// POST: ListingScheduleTypes/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
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


        /*    // GET: ListingScheduleTypes/Create
            public ActionResult Create()
            {
                return View();
            }

            // POST: ListingScheduleTypes/Create
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
            }
            */

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


        // POST: ListingScheduleTypes/Edit/5
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
        /// GET: ListingScheduleTypes/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var type = _context.ListingScheduleTypes.SingleOrDefault(r => r.Id == id);

            if (type == null)
                return HttpNotFound();

            var viewModel = new ListingScheduleTypeViewModel()
            {
                ListingScheduleType = type
            };
            return View("Edit", viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public ActionResult Save(ListingScheduleTypeViewModel viewModel)
        {
            if (viewModel.ListingScheduleType.Id == 0)
            {
              //  viewModel.ListingScheduleType.Created = System.DateTime.Now;
                _context.ListingScheduleTypes.Add(viewModel.ListingScheduleType);
            }
            else
            {
                var typeInDb = _context.ListingScheduleTypes.SingleOrDefault(m => m.Id == viewModel.ListingScheduleType.Id);

                try
                {
                 //   typeInDb.Id = viewModel.ListingScheduleType.Id;
                    typeInDb.Cost = viewModel.ListingScheduleType.Cost;
                    typeInDb.DiscountRate = viewModel.ListingScheduleType.DiscountRate;
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

            return RedirectToAction("Index", "ListingScheduleTypes");
        }


    }
}
