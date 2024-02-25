using DataAccessLayer;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLogicLayer
{
    public class UserManager
    {
        public List<UserDTO> GetListOfUsers()
        {
            SkillTrackerDBEntities db = new SkillTrackerDBEntities();
            DbSet<User> dalUser = db.Users; // return User from DAL.
            List<UserDTO> users = new List<UserDTO>();
            foreach (var item in dalUser)
            {
                UserDTO user = new UserDTO();
                user.Id = item.Id;
                user.EmailId = item.EmailId;
                //user.Password = item.Password;
                user.Password = this.HashPassword(item.Password);
                user.Is_Admin = item.Is_Admin;
                user.FullName = item.FullName;
                user.DateOfBirth = item.DateOfBirth;
                user.ContactNo = item.ContactNo;
                user.Gender = item.Gender;

                users.Add(user);
            }
            return users;
        }

        public string HashPassword(string password) // methods that takes a plain text password 
        {
            using (SHA512 sha256Hash = SHA512.Create()) // using Create() method of SHA256 class to create an instance of the class.
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)); // Computing hash using UTF8 encoding 

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder(); // using "StringBuilder" class, to build strings from the bytes obtained above.
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // Converting byte to String using "x2" format specifier.
                }
                return builder.ToString();
            }
        }


        public UserDTO GetUserWithSkills(int userId)
        {
            using (var db = new SkillTrackerDBEntities())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == userId); // getting all the details of the User

                if (user != null)
                {
                    var userDTO = new UserDTO
                    {
                        Id = user.Id,
                        EmailId = user.EmailId,
                        Password = user.Password,
                        Is_Admin = user.Is_Admin, // not needed
                        FullName = user.FullName,
                        DateOfBirth = user.DateOfBirth,
                        ContactNo = user.ContactNo,
                        Gender = user.Gender
                    };

                    // Fetch UserSkills for the user
                    var userSkills = db.UserSkills
                                        .Where(us => us.UserID == userId)
                                        .Include(us => us.Skill) // Include Skill to fetch SkillName
                                        .ToList();

                    foreach (var userSkill in userSkills)
                    {
                        userDTO.UserSkills.Add(new UserSkillDTO
                        {
                            Id = userSkill.Id,
                            UserID = userSkill.UserID,
                            SkillID = userSkill.SkillID,
                            Proficiency = userSkill.Proficiency,
                            Skill = new SkillDTO
                            {
                                //Id = userSkill.Skill.Id,
                                Name = userSkill.Skill.Name // Set the SkillName from the Skill object
                            }
                        });
                    }

                    return userDTO;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}














        //public List<UserDTO> GetAllUsersWithSkillsAndProficiencies()
        //{
        //    using (var context = new SkillTrackerDBEntities())
        //    {
        //        // Retrieve users from the database along with their skills and proficiencies
        //        var users = context.Users.Include("UserSkills.Skill");

        //        // Mapping User to UserDTO along with Skills and Proficiencies
        //        var userDTOs = new List<UserDTO>();
        //        foreach (var user in users)
        //        {
        //            var userDTO = new UserDTO
        //            {
        //                Id = user.Id,
        //                EmailId = user.EmailId,
        //                Password = user.Password,
        //                Is_Admin = user.Is_Admin,
        //                FullName = user.FullName,
        //                DateOfBirth = user.DateOfBirth,
        //                ContactNo = user.ContactNo,
        //                Gender = user.Gender,
        //                Skills = new List<SkillDTOWithProficiency>()
        //            };

        //            // Mapping skills and proficiencies for the user
        //            foreach (var userSkill in user.UserSkills)
        //            {
        //                // Adding the skill and proficiency to the userDTO
        //                userDTO.Skills.Add(new SkillDTOWithProficiency
        //                {
        //                    Skill = new SkillDTO
        //                    {
        //                        Id = userSkill.Skill.Id,
        //                        Name = userSkill.Skill.Name
        //                    },
        //                    Proficiency = userSkill.Proficiency
        //                });
        //            }

        //            userDTOs.Add(userDTO);
        //        }

        //        return userDTOs;
        //    }