﻿using System;
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

                return View(new List<ListingScheduleType>());
            }

        }

        // GET: ListingScheduleTypes/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                // no id number given? then 404
                if (id == null)
                    return RedirectToAction("Index", "ListingScheduleTypes");

                //
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
            catch (ArgumentException ex)
            {
                return HttpNotFound();
            }
        }



        // GET: ListingScheduleTypes/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                // no id number given? then 404
                if (id == null)
                    return RedirectToAction("Index", "ListingScheduleTypes");

                //
                var id32 = Convert.ToByte(id.GetValueOrDefault());
                var type = new ListingScheduleType { Id = id32 };
                _context.ListingScheduleTypes.Attach(type);
                _context.ListingScheduleTypes.Remove(type);
                _context.SaveChanges();
            }
            catch (ArgumentException ex)
            {
                return HttpNotFound();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

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
            try
            {
                // no id number given? then 404
                if (id == null)
                    return RedirectToAction("Index", "ListingScheduleTypes");

                //
                var type = _context.ListingScheduleTypes.SingleOrDefault(r => r.Id == id);

                // if record doesn't exist 404
                if (type == null)
                    return HttpNotFound();

                var viewModel = new ListingScheduleTypeViewModel()
                {
                    ListingScheduleType = type
                };
                return View("Edit", viewModel);
            }
            catch (ArgumentException ex)
            {
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

            //return RedirectToAction("Edit", "ListingScheduleTypes", new { Id = viewModel.ListingScheduleType.Id });
            return RedirectToAction("Index", "ListingScheduleTypes", new
            {
                Success = $"Listing Schedule Type {viewModel.ListingScheduleType.Id} saved"
            });
        }


    }
}
