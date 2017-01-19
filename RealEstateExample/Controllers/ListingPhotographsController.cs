

using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RealEstateExample.Models;
using RealEstateExample.ViewModels;
using System.Diagnostics;
using System.IO;

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

        // GET: ListingPhotographs
        public ActionResult Index()
        {
            try
            {
                var photographs = _context.ListingPhotographs.ToList();

                //string savedFileName = GetPhotographPath(viewModel.ListingPhotograph.Id);

                return View(photographs);
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("InvalidCastException: {0}", e.Message);

                return View(new List<ListingPhotograph>());
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine("InvalidOperationException: {0}", e.Message);

                return View(new List<ListingPhotograph>());
            }
        }

        // GET: ListingPhotographs/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                // no id number given? then 404
                if (id == null)
                    return RedirectToAction("Index", "ListingPhotographs");

                //
                var photograph = _context.ListingPhotographs.SingleOrDefault(c => c.Id == id);

                if (photograph == null)
                    return HttpNotFound();

                return View(new ListingPhotographEditViewModel()
                {
                    ListingPhotograph = photograph
                });

            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("Debug: {0}", e.Message);

                return View(new ListingPhotographEditViewModel()
                {
                    ListingPhotograph = new ListingPhotograph()
                });
            }
            catch (ArgumentException ex)
            {
                return HttpNotFound();
            }
        }



        // GET: ListingPhotographs/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                // no id number given? then 404
                if (id == null)
                    return RedirectToAction("Index", "ListingPhotographs");

                //
                // var listing = new Listing { Id = id ?? default(int) };
                var listing = new ListingPhotograph { Id = id.GetValueOrDefault() };
                _context.ListingPhotographs.Attach(listing);
                _context.ListingPhotographs.Remove(listing);
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

            return RedirectToAction("Index", "ListingPhotographs", new
            {
                Success = string.Format("Listing photograph {0} deleted", id)
            });
        }

        // POST: ListingPhotographs/Delete/5
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
            var viewModel = new ListingPhotographEditViewModel()
            {
                ListingPhotograph = new ListingPhotograph()
                {
                    Id = 0
                }
            };

            return View("Edit", viewModel);
        }

        /// <summary>
        /// POST: ListingPhotographs/Edit/5
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
        /// GET: ListingPhotographs/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            try
            {
                // no id number given? then 404
                if (id == null)
                    return RedirectToAction("Index", "ListingPhotographs");

                //
                var photograph = _context.ListingPhotographs.SingleOrDefault(r => r.Id == id);
                if (photograph == null)
                    return HttpNotFound();

                var listings = _context.Listings.ToList();
                var slListings = GetSelectListings(listings);

                var viewModel = new ListingPhotographEditViewModel()
                {
                    ListingPhotograph = photograph,
                    SelectListListings = slListings
                };
                return View("Edit", viewModel);
            }
            catch (ArgumentException ex)
            {
                return HttpNotFound();
            }
        }

        /*
        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, int? id)
        {
            try
            {
                // no id number given? then 404
                if (id == null)
                    return RedirectToAction("Index", "ListingPhotographs");

                //
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    file.SaveAs(path);
                }

                //
                var photograph = _context.ListingPhotographs.SingleOrDefault(r => r.Id == id);
                if (photograph == null)
                    return HttpNotFound();

                var listings = _context.Listings.ToList();
                var slListings = GetSelectListings(listings);

                var viewModel = new ListingPhotographEditViewModel()
                {
                    ListingPhotograph = photograph,
                    SelectListListings = slListings
                };
                return View("Edit", viewModel);

            }
            catch (ArgumentException ex)
            {
                return HttpNotFound();
            }
        }
        */

        /// <summary>
        /// Note: SelectListItem is predefined.
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetSelectListings(List<Listing> elements)
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
        /// Handle no id passed situation.
        /// </summary>
        /// <returns></returns>
        /* [HttpGet]
         public ActionResult Edit()
         {
             return RedirectToAction("Index");
         }*/

        public class ViewDataUploadFilesResult
        {
            public string Name { get; set; }
            public int Length { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(ListingPhotographEditViewModel viewModel)
        {
            if (viewModel.ListingPhotograph.Id == 0)
            {
                viewModel.ListingPhotograph.Created = System.DateTime.Now;
                _context.ListingPhotographs.Add(viewModel.ListingPhotograph);
            }
            else
            {

                try
                {
                    var photographInDb =
                        _context.ListingPhotographs.SingleOrDefault(m => m.Id == viewModel.ListingPhotograph.Id);

                    photographInDb.Created = DateTime.Now;
                    photographInDb.Name = viewModel.ListingPhotograph.Name;
                    photographInDb.Description = viewModel.ListingPhotograph.Description;
                    photographInDb.ListingId = viewModel.ListingPhotograph.ListingId;

                    //
                    var uploadedFiles = new List<ViewDataUploadFilesResult>();

                    foreach (string file in Request.Files)
                    {
                        HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                        if (hpf == null)
                        {
                            continue;
                        }
                        if (hpf.ContentLength == 0)
                        {
                            continue;
                        }
                        if (hpf.FileName.Length == 0)
                        {
                            continue;
                        }

                        string savedFileName = GetPhotographPath(viewModel.ListingPhotograph.Id);
                        /*
                        string savedFolder = Path.Combine(
                            AppDomain.CurrentDomain.BaseDirectory,
                            "Content/Photographs"
                        );
                        
                        try
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath(savedFolder));
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e);
                        }

                        string savedFileName = Path.Combine(
                            savedFolder,
                            String.Format("{0}.jpg", viewModel.ListingPhotograph.Id)
                        );*/
                        /*  Path.GetFileName(hpf.FileName) */
                        hpf.SaveAs(savedFileName);

                        uploadedFiles.Add(new ViewDataUploadFilesResult()
                        {
                            Name = savedFileName,
                            Length = hpf.ContentLength
                        });
                    }

                }
                catch (NullReferenceException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                catch (DirectoryNotFoundException ex)
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

            return RedirectToAction("Index", "ListingPhotographs", new
            {
                Success = string.Format("Listing photograph {0} saved", viewModel.ListingPhotograph.Id)
            });
        }


        private string GetPhotographPath(int id)
        {
            string savedFolder = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Content/Photographs"
            );
            try
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(savedFolder));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            string savedFileName = Path.Combine(
            savedFolder,
            String.Format("{0}.jpg", id)
            );

            return savedFileName;
        }

    }

}