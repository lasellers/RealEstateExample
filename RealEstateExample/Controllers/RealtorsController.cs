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
                //var realtorsList2 = _context.Realtors.Include();

                return View(realtorsList);

                /* var viewModel = new RealtorsViewModel
                 {
                     Realtors = realtorsList
                 };
                 return View(viewModel);*/
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("Debug: {0}", e.Message);

                return View(new List<Realtor>());
            }

        }

        // GET: Realtors/Details/5
        public ActionResult Details(int id)
        {
            try
            {
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
        }

        // GET: Realtors/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var realtor = new Realtor { Id = id };
                _context.Realtors.Attach(realtor);
                _context.Realtors.Remove(realtor);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
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
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Edit(int Id)
        {
            var realtor = _context.Realtors.SingleOrDefault(r => r.Id == Id);

            if (realtor == null)
                return HttpNotFound();

            var viewModel = new RealtorViewModel()
            {
                Realtor = realtor
            };
            return View("Edit", viewModel);
        }

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
                    realtorInDb.Name = viewModel.Realtor.Name;
                    realtorInDb.Description = viewModel.Realtor.Description;
                    realtorInDb.Phone = viewModel.Realtor.Phone;
                    realtorInDb.Address = viewModel.Realtor.Address;
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

            //return RedirectToAction("Edit", "Realtors", new {Id = viewModel.Realtor.Id });
            return RedirectToAction("Index", "Realtors", new
            {
                Success = string.Format("Realtor {0} saved", viewModel.Realtor.Id)
            });
        }

    }
}
