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
    public class RealtorsController : Controller
    {
        private ApplicationDbContext _context;

        public RealtorsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            //base.Dispose(disposing);
            _context.Dispose();
        }

        // GET: Realtors
        public ActionResult Index()
        {
            // realtors == null || realtors.Count()==0

            try
            {
                var realtorsList = _context.Realtors.ToList();
   
                return View(realtorsList);
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("Debug: {0}", e.Message);

                return View(new List<Realtor>());
            }

        }

        // GET: Realtors/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                // no id number given? then 404
                if (id == null)
                    return RedirectToAction("Index", "Realtors");

                var realtor = _context.Realtors.SingleOrDefault(c => c.Id == id);

                if (realtor == null)
                    return HttpNotFound();

                var viewModel = new RealtorViewModel
                {
                    Realtor = realtor
                };
                return View(viewModel);

            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("Debug: {0}", e.Message);

                var viewModel2 = new RealtorViewModel
                {
                    Realtor = new Realtor()
                };
                return View(viewModel2);
            }
            catch (ArgumentException e)
            {
                Debug.WriteLine(e.Message);

                return HttpNotFound();
            }
        }

        // GET: Realtors/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                // no id number given? then 404
                if (id == null)
                    return RedirectToAction("Index", "Realtors");

                //
                //var realtor = new Realtor { Id = id ?? default(int) };
                var realtor = new Realtor { Id = id.GetValueOrDefault() };
                _context.Realtors.Attach(realtor);
                _context.Realtors.Remove(realtor);
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

            return RedirectToAction("Index", "Realtors", new
            {
                Success = string.Format("Realtor {0} deleted", id)
            });
        }

        /* [HttpPost]
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
            var viewModel = new RealtorViewModel()
            {
                Realtor = new Realtor()
            };
            return View("Edit", viewModel);
        }


        // GET: Realtors/Edit/5
        /* public ActionResult Edit(int id)
         {
             return View();
         }*/

        // POST: Realtors/Edit/5
        /*   [HttpPost]
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
        /// GET: Realtors/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            try
            {
                // no id number given? then 404
                if (id == null)
                    return RedirectToAction("Index", "Realtors");

                //
                var realtor = _context.Realtors.SingleOrDefault(r => r.Id == id);

                if (realtor == null)
                    return HttpNotFound();

                var viewModel = new RealtorViewModel()
                {
                    Realtor = realtor
                };
                return View("Edit", viewModel);

            }
            catch (ArgumentException e)
            {
                Debug.WriteLine(e.Message);

                return HttpNotFound();
            }
        }


        /// <summary>
        /// Handle no id passed situation.
        /// </summary>
        /// <returns></returns>
      /*  [HttpGet]
        public ActionResult Edit()
        {
            return RedirectToAction("Index");
        }*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(RealtorViewModel viewModel)
        {
            if (viewModel.Realtor.Id == 0)
            {
                viewModel.Realtor.Created = DateTime.Now;
                _context.Realtors.Add(viewModel.Realtor);
            }
            else
            {
                var realtorInDb = _context.Realtors.SingleOrDefault(m => m.Id == viewModel.Realtor.Id);

                try
                {
                    realtorInDb.Created = DateTime.Now;
                    realtorInDb.Name = viewModel.Realtor.Name??"No Name";
                    realtorInDb.Description = viewModel.Realtor.Description;
                    realtorInDb.Phone = viewModel.Realtor.Phone;
                    realtorInDb.Address = viewModel.Realtor.Address;
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

            //return RedirectToAction("Edit", "Realtors", new {Id = viewModel.Realtor.Id });
            return RedirectToAction("Index", "Realtors", new
            {
                Success = $"Realtor {viewModel.Realtor.Id} saved"
            });
        }

    }
}
