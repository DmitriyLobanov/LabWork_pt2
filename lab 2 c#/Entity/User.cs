using Microsoft.AspNetCore.Identity;
using LabWork_pt2.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabWork_pt2.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }


        public User()
        {

        }

        public User(string userName, string password, Role role)
        {
            UserName = userName;
            Password = password;
            Role = role;
        }
    }
}
