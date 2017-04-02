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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                List<ListingPhotograph> photographs = _context.ListingPhotographs.ToList();
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            try
            {
                // no id number given? then redirect to photographs index
                if (id == null)
                    return RedirectToAction("Index", "ListingPhotographs");

                // get the photo 
                ListingPhotograph photograph = _context.ListingPhotographs.SingleOrDefault(c => c.Id == id);
                if (photograph == null)
                    return HttpNotFound();

                // get listing fields id, name, description atm
                var listingSelect = _context.Listings.Select(x => new { x.Id, x.Name, x.Description }).SingleOrDefault(c => c.Id == photograph.ListingId);

                //
                ListingPhotographEditViewModel viewModel = new ListingPhotographEditViewModel()
                {
                    ListingPhotograph = photograph,
                    FilePath = GetPhotographFilePathIfExists(id.GetValueOrDefault()),
                    FileURL = GetPhotographUrlIfExists(id.GetValueOrDefault()),
                    Listing = new Listing()
                    {
                        Id = listingSelect.Id,
                        Name = listingSelect.Name,
                        Description = listingSelect.Description
                    }
                };

                return View(viewModel);

            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("InvalidCastException: {0}", e.Message);

                return View(new ListingPhotographEditViewModel()
                {
                    ListingPhotograph = new ListingPhotograph()
                });
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine("NullReferenceException: {0}", e.Message);

                return HttpNotFound();
            }
            catch (ArgumentException e)
            {
                Debug.WriteLine("ArgumentException: {0}", e.Message);

                return HttpNotFound();
            }

        }



        // GET: ListingPhotographs/Delete/5
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
                return RedirectToAction("Index", "ListingPhotographs", new { Error = @"Must be logged in" });

            //
            try
            {
                // no id number given? then redirect to photographs index
                if (id == null)
                    return RedirectToAction("Index", "ListingPhotographs");

                //
                // var listing = new Listing { Id = id ?? default(int) };
                ListingPhotograph listing = new ListingPhotograph { Id = id.GetValueOrDefault() };
                _context.ListingPhotographs.Attach(listing);
                _context.ListingPhotographs.Remove(listing);
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

            return RedirectToAction("Index", "ListingPhotographs", new
            {
                Success = $"Listing photograph {id} deleted"
            });
        }

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
        /// GET: ListingPhotographs/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            // if we are not logged in, do not allow edits
            bool loggedIn = (System.Web.HttpContext.Current.User != null) &&
                        System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!loggedIn)
                return RedirectToAction("Index", "ListingPhotographs", new { Error = @"Must be logged in" });

            //
            try
            {
                // no id number given? then redirect to photographs index
                if (id == null)
                    return RedirectToAction("Index", "ListingPhotographs");

                //
                ListingPhotograph photograph = _context.ListingPhotographs.SingleOrDefault(r => r.Id == id);
                if (photograph == null)
                    return HttpNotFound();

                var listings = _context.Listings.ToList();
                var slListings = GetSelectListings(listings);

                var viewModel = new ListingPhotographEditViewModel()
                {
                    ListingPhotograph = photograph,
                    SelectListListings = slListings,
                    FilePath = GetPhotographFilePathIfExists(id.GetValueOrDefault()),
                    FileURL = GetPhotographUrlIfExists(id.GetValueOrDefault())
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
        /// 
        /// </summary>
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
                viewModel.ListingPhotograph.Created = DateTime.Now;
                _context.ListingPhotographs.Add(viewModel.ListingPhotograph);
            }
            else
            {

                try
                {
                    ListingPhotograph photographInDb =
                        _context.ListingPhotographs.SingleOrDefault(m => m.Id == viewModel.ListingPhotograph.Id);

                    photographInDb.Name = viewModel.ListingPhotograph.Name ?? "No Name";
                    photographInDb.Description = viewModel.ListingPhotograph.Description;
                    photographInDb.ListingId = viewModel.ListingPhotograph.ListingId;
                    photographInDb.Created = DateTime.Now;

                    //
                    CheckPhotographPath();

                    //
                   // var uploadedFiles = new List<ViewDataUploadFilesResult>();

                    foreach (string file in Request.Files)
                    {
                        HttpPostedFileBase hpf = Request.Files[file];
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

                        //
                        string savedFileName = GetPhotographFilePath(viewModel.ListingPhotograph.Id);

                        /*  Path.GetFileName(hpf.FileName) */
                        if (savedFileName.Length > 0)
                        {
                            hpf.SaveAs(savedFileName);
                        }

                       /* uploadedFiles.Add(new ViewDataUploadFilesResult()
                        {
                            Name = savedFileName,
                            Length = hpf.ContentLength
                        });*/
                    }

                }
                catch (NullReferenceException e)
                {
                    Debug.WriteLine(e.Message);
                }
                catch (DirectoryNotFoundException e)
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

            return RedirectToAction("Index", "ListingPhotographs", new
            {
                Success = $"Listing photograph {viewModel.ListingPhotograph.Id} saved"
            });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetPhotographPath()
        {
            //string savedFolder =
            //  AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "Content" + Path.DirectorySeparatorChar + "Photographs";
            // System.IO.Directory.CreateDirectory(Server.MapPath("/Content/Photographs/"));
            string[] paths =
            {
                AppDomain.CurrentDomain.BaseDirectory,
                "Content",
                "Photographs"
            };
            string savedFolder = Path.Combine(paths);
            return savedFolder;
        }


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        private void CheckPhotographPath()
        {
            string savedFolder = GetPhotographPath();

            try
            {
                Directory.CreateDirectory(savedFolder);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetPhotographFilePath(int id)
        {
            string savedFileName = Path.Combine(
             GetPhotographPath(),
            id + ".jpg"
            );

            return savedFileName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetPhotographUrl(int id)
        {
            string savedFileName = $"/Content/Photographs/{id}.jpg";
            return savedFileName;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetPhotographFilePathIfExists(int id)
        {
            string savedFileName = Path.Combine(
             GetPhotographPath(),
            id + ".jpg"
            );

            if (System.IO.File.Exists(savedFileName))
            {
                return savedFileName;
            }
            else
            {
                return @"";
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetPhotographUrlIfExists(int id)
        {
            string filePath = GetPhotographFilePathIfExists(id);
            if (filePath == "")
            {
                return @"";
            }
            else
            {
                string savedFileName = "/Content/Photographs/" + id + ".jpg";
                return savedFileName;
            }
        }

    }
}