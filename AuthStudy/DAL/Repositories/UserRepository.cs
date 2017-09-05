using System.Collections.Generic;
using AuthStudy.DAL.Models;

namespace AuthStudy.DAL.Repositories
{
    public class UserRepository
    {
        public UserRepository()
        {

        }

        public User GetUser(string UserName, string Password)
        {
            return new User
            {
                Id = 1,
                Username = "Areas",
                GroupAccess = new List<GroupAccess>
                {
                    new GroupAccess
                    {
                        GroupId = 1,
                        AccessId = 1,
                        Access = new Access
                        {

                            Id = 1,
                            Name = "Home"

                        }
                    }
                },
                UserAccess = new List<UserAccess>
                {
                    new UserAccess
                    {
                        AccessId = 2,
                        UserId = 1,
                        Access = new Access
                        {

                            Id = 2,
                            Name = "Contact"

                        }
                    }
                }
            };
        }
    }
}
