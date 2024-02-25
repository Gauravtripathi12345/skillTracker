using BusinessLogicLayer;
using SkillTrackerApi.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SkillTrackerApi.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        [Route("api/User")]
        public List<UserModel> GetAllUsers()
        {
            UserManager usermanager = new UserManager();
            List<UserDTO> users = usermanager.GetListOfUsers();
            List<UserModel> userModelList = new List<UserModel>();
            foreach (var item in users)
            {
                UserModel user = new UserModel();
                user.Id = item.Id;
                user.EmailId = item.EmailId;
                user.Password = item.Password;
                user.Is_Admin = item.Is_Admin;
                user.FullName = item.FullName;
                user.DateOfBirth = item.DateOfBirth;
                user.ContactNo = item.ContactNo;
                user.Gender = item.Gender;

                userModelList.Add(user);
            }
            return userModelList;
        }

        //[HttpGet("api/User/{userId}")]

        [HttpGet]
        [Route("api/User/{userId}")]
        public IHttpActionResult GetUserWithSkills(int userId)
        {
            try
            {
                UserManager usermanager = new UserManager();
                var userDto = usermanager.GetUserWithSkills(userId);

                if (userDto == null)
                {
                    return NotFound();
                }

                // Map UserDTO to UserModel if necessary
                var userModel = new UserModel
                {
                    Id = userDto.Id,
                    EmailId = userDto.EmailId,
                    Password = userDto.Password,
                    Is_Admin = userDto.Is_Admin,
                    FullName = userDto.FullName,
                    DateOfBirth = userDto.DateOfBirth,
                    ContactNo = userDto.ContactNo,
                    Gender = userDto.Gender,
                    UserSkills= userDto.UserSkills,
                };

                return Ok(userModel); 
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


    }
}
