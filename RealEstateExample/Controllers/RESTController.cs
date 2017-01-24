using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using RealEstateExample.Models;
using RealEstateExample.JSONModels;
using System.Diagnostics;
using System.Net.Http;
using System.Net;

namespace RealEstateExample.Controllers
{
    [RoutePrefix("api")]
    public class RESTController : ApiController
    {
        private ApplicationDbContext _context;
        private bool json = true;

        /// <summary>
        /// 
        /// </summary>
        public RESTController()
        {
            _context = new ApplicationDbContext();
        }

        public System.Net.Http.Formatting.MediaTypeFormatter GetFormatter()
        {
            if (json)
            {
                return Configuration.Formatters.JsonFormatter;
            }
            else
            {
                return Configuration.Formatters.XmlFormatter;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            //base.Dispose(disposing);
            _context.Dispose();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("listings")]
        [HttpGet]
        public HttpResponseMessage GetListings()
        {
            try
            {
                List<Listing> listingsList = _context.Listings.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, listingsList, GetFormatter());
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("InvalidCastException: {0}", e.Message);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetFormatter());
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine("InvalidOperationException: {0}", e.Message);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetFormatter());
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listingId"></param>
        /// <returns></returns>
        [Route("listing/{listingId}")]
        [HttpGet]
        public HttpResponseMessage GetListing(int listingId)
        {
            try
            {
                Listing listing = _context.Listings.SingleOrDefault(m => m.Id == listingId);
                return Request.CreateResponse(HttpStatusCode.OK, listing, GetFormatter());
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("InvalidCastException: {0}", e.Message);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetFormatter());
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine("InvalidOperationException: {0}", e.Message);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetFormatter());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("realtors")]
        [HttpGet]
        public HttpResponseMessage GetRealtors()
        {
            try
            {
                var realtorsList = _context.Realtors.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, realtorsList, GetFormatter());
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("InvalidCastException: {0}", e.Message);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetFormatter());
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine("InvalidOperationException: {0}", e.Message);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetFormatter());
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="realtorId"></param>
        /// <returns></returns>
        [Route("realtor/{realtorId}")]
        [HttpGet]
        public HttpResponseMessage GetRealtor(int realtorId)
        {
            try
            {
                Realtor realtor = _context.Realtors.SingleOrDefault(m => m.Id == realtorId);

                return Request.CreateResponse(HttpStatusCode.OK, realtor, GetFormatter());
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("InvalidCastException: {0}", e.Message);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetFormatter());
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine("InvalidOperationException: {0}", e.Message);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetFormatter());
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("listingscheduletypes")]
        [HttpGet]
        public HttpResponseMessage GetListingScheduleTypes()
        {
            try
            {
                List<ListingScheduleType> typesList = _context.ListingScheduleTypes.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, typesList, GetFormatter());
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("InvalidCastException: {0}", e.Message);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetFormatter());
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine("InvalidOperationException: {0}", e.Message);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetFormatter());
            }

        }


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        [Route("listingscheduletype/{typeId}")]
        [HttpGet]
        public HttpResponseMessage GetListingScheduleTypes(int typeId)
        {
            try
            {
                ListingScheduleType type = _context.ListingScheduleTypes.SingleOrDefault(m => m.Id == typeId);

                return Request.CreateResponse(HttpStatusCode.OK, type, GetFormatter());
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("InvalidCastException: {0}", e.Message);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetFormatter());
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine("InvalidOperationException: {0}", e.Message);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetFormatter());
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("listingphotographs")]
        [HttpGet]
        public HttpResponseMessage GetListingPhotographs()
        {
            try
            {
                List<ListingPhotograph> photographsList = _context.ListingPhotographs.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, photographsList, GetFormatter());
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("InvalidCastException: {0}", e.Message);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetFormatter());
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine("InvalidOperationException: {0}", e.Message);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetFormatter());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listingId"></param>
        /// <returns></returns>
        [Route("listingphotograph/{listingId}")]
        [HttpGet]
        public HttpResponseMessage GetListingPhotographs(int listingId)
        {
            try
            {
                List<ListingPhotograph> photographsList = _context.ListingPhotographs.Where(x => x.Id == listingId).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, photographsList, GetFormatter());
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("InvalidCastException: {0}", e.Message);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetFormatter());
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine("InvalidOperationException: {0}", e.Message);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetFormatter());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="photographId"></param>
        /// <param name="listingId"></param>
        /// <returns></returns>
        [Route("listingphotograph/{listingId}/{photographId}")]
        [HttpGet]
        public HttpResponseMessage GetListingPhotograph(int photographId, int listingId)
        {
            try
            {
                List<ListingPhotograph> photographsList = _context.ListingPhotographs.Where(x => x.Id == photographId && x.ListingId == listingId).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, photographsList, GetFormatter());
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine("InvalidCastException: {0}", e.Message);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetFormatter());
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine("InvalidOperationException: {0}", e.Message);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetFormatter());
            }
        }



        /*
[Route("available")]
[HttpGet]
public IEnumerable<ListingsJSONModel> GetListings()
{
    try
    {

        var listingsList = _context.Listings.Select(x => new { x.Id, x.Name, x.Description }).ToList();
        return listingsList as List<ListingsJSONModel>;

        //  var listingsList = _context.Listings.ToList();
        // return listingsList;
    }
    catch (InvalidCastException e)
    {
        Debug.WriteLine("InvalidCastException: {0}", e.Message);

        return new List<ListingsJSONModel>();
    }
    catch (InvalidOperationException e)
    {
        Debug.WriteLine("InvalidOperationException: {0}", e.Message);

        return new List<ListingsJSONModel>();
    }

}
*/

        /*
    [Route("listings2")]
    [HttpGet]
    public IEnumerable<Listing> GetListings2()
    {
        try
        {
            List<Listing> listingsList = _context.Listings.ToList();
            return listingsList;
        }
        catch (InvalidCastException e)
        {
            Debug.WriteLine("InvalidCastException: {0}", e.Message);

            return new List<Listing>();
        }
        catch (InvalidOperationException e)
        {
            Debug.WriteLine("InvalidOperationException: {0}", e.Message);

            return new List<Listing>();
        }

    }
    */

    }
}
