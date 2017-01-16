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
                // var realtors = GetRealtors();
         
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

                var realtors2 = new List<Realtor>();
                return View(realtors2);
            }

        }

        // GET: Realtors/Details/5
        public ActionResult Details(int id)
        {
            try
            {

                //var realtor = GetRealtors().SingleOrDefault(c => c.Id == id);
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
                    Realtor = new Realtor() { }
                };
                return View(viewModel2);

                //var listings2 = new List<Listing> { };
                //return View(listings2);
            }
        }


        /*  // GET: Realtors/Create
          public ActionResult Create()
          {
              return View();
          }

          // POST: Realtors/Create
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

        // GET: Realtors/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
              //  var id32 = Convert.ToByte(id);
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

            return RedirectToAction("Index", "Realtors");
        }

        /// <summary>
        /// POST: Realtors/Delete/5
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


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult New()
        {
            var scheduleTypes = _context.ListingScheduleTypes.ToList();

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
        public ActionResult Save(RealtorViewModel viewModel)
        {
            if (viewModel.Realtor.Id == 0)
            {
                viewModel.Realtor.Created = System.DateTime.Now;
                _context.Realtors.Add(viewModel.Realtor);
            }
            else
            {
                var realtorInDb = _context.Realtors.SingleOrDefault(m => m.Id == viewModel.Realtor.Id);

                try
                {
                    realtorInDb.Created = System.DateTime.Now;
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

            return RedirectToAction("Index", "Realtors");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /*private IEnumerable<Realtor> GetRealtors()
        {
            return new List<Realtor>
            {
                   new Realtor {Id = 1, Name = "Rascal", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus sed condimentum enim. Mauris eleifend pretium blandit. Nullam eleifend in nibh ac accumsan. Sed ac arcu ac velit fringilla rhoncus. Nunc iaculis malesuada justo, vitae finibus lorem aliquam nec. Cras maximus enim malesuada, molestie augue eget, viverra nulla. Nulla sollicitudin pharetra nibh quis ultricies. In faucibus ex id ultricies fringilla. Ut imperdiet quam nec justo volutpat dictum. Donec convallis libero in sem molestie, in vehicula lectus consequat. Sed eros purus, eleifend vel ligula eget, dictum imperdiet metus. Duis eu congue ipsum. Curabitur eu porta nulla."},
                   new Realtor {Id = 2, Name = "Muffin Head", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus sed condimentum enim. Mauris eleifend pretium blandit. Nullam eleifend in nibh ac accumsan. Sed ac arcu ac velit fringilla rhoncus. Nunc iaculis malesuada justo, vitae finibus lorem aliquam nec. Cras maximus enim malesuada, molestie augue eget, viverra nulla. Nulla sollicitudin pharetra nibh quis ultricies. In faucibus ex id ultricies fringilla. Ut imperdiet quam nec justo volutpat dictum. Donec convallis libero in sem molestie, in vehicula lectus consequat. Sed eros purus, eleifend vel ligula eget, dictum imperdiet metus. Duis eu congue ipsum. Curabitur eu porta nulla."},
                   new Realtor {Id = 3, Name = "Machine Head X", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus sed condimentum enim. Mauris eleifend pretium blandit. Nullam eleifend in nibh ac accumsan. Sed ac arcu ac velit fringilla rhoncus. Nunc iaculis malesuada justo, vitae finibus lorem aliquam nec. Cras maximus enim malesuada, molestie augue eget, viverra nulla. Nulla sollicitudin pharetra nibh quis ultricies. In faucibus ex id ultricies fringilla. Ut imperdiet quam nec justo volutpat dictum. Donec convallis libero in sem molestie, in vehicula lectus consequat. Sed eros purus, eleifend vel ligula eget, dictum imperdiet metus. Duis eu congue ipsum. Curabitur eu porta nulla."},
                   new Realtor {Id = 4, Name = "Fallout Boy",  Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus sed condimentum enim. Mauris eleifend pretium blandit. Nullam eleifend in nibh ac accumsan. Sed ac arcu ac velit fringilla rhoncus. Nunc iaculis malesuada justo, vitae finibus lorem aliquam nec. Cras maximus enim malesuada, molestie augue eget, viverra nulla. Nulla sollicitudin pharetra nibh quis ultricies. In faucibus ex id ultricies fringilla. Ut imperdiet quam nec justo volutpat dictum. Donec convallis libero in sem molestie, in vehicula lectus consequat. Sed eros purus, eleifend vel ligula eget, dictum imperdiet metus. Duis eu congue ipsum. Curabitur eu porta nulla."}
            };
        }*/


    }
}
