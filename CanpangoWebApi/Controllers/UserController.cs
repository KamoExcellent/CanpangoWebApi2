using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogic;


namespace CanpangoWebApi.Controllers
{
    public class UserController : ApiController
    {
        ChomiDBEntities entities = new ChomiDBEntities();

        public IEnumerable<User> Get()
        {
            using(ChomiDBEntities entities = new ChomiDBEntities())
            {
                return entities.Users.ToList();
            }
        }

        public User Get(int id)
        {
            using(ChomiDBEntities entities = new ChomiDBEntities())
            {
                return entities.Users.FirstOrDefault(u => u.UserId == id);
            }
        }

        //POST Method to add new user 
        public HttpResponseMessage Post([FromBody] User _user)
        {
            try
            {
                entities.Users.Add(_user);
                entities.SaveChanges();

                //returns response status code as created
                var msg = Request.CreateResponse(HttpStatusCode.Created, _user);

                //Response msg with requestUri for checking purpose
                msg.Headers.Location = new Uri(Request.RequestUri + _user.UserId.ToString());
                return msg;
                
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //PUT Method for updating records
        public HttpResponseMessage Put(int id,[FromBody] User _user)
        {
            //fetch and filter record with id
            var userDetail = (from a in entities.Users where a.UserId == id select a).FirstOrDefault();

            if(userDetail != null)
            {
                //set fetch record object property with userDetails
                userDetail.FirstName = _user.FirstName;
                userDetail.MobileNumber = _user.MobileNumber;

                //save Changes
                entities.SaveChanges();

                //return response status as Updated
                return Request.CreateResponse(HttpStatusCode.OK, userDetail);
            }
            else
            {
                //return response error code as NOT FOUND message
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Code or User Not Found");
            }
        }

        //Delete method with parameter value
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                //fetch and delete user with userId
                var _deleteUser = (from x in entities.Users where x.UserId == id select x).FirstOrDefault();

                if(_deleteUser!=null)
                {
                    entities.Users.Remove(_deleteUser);
                    entities.SaveChanges();

                    //RETURN response status code as Delelted with userId

                    return Request.CreateResponse(HttpStatusCode.OK,id);
                }
                else
                {
                    //return response status code as User Not Found or invalid
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not found or Invalid" + id.ToString());
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
        }






















       
    }
}
