using LabWork_pt2.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabWork_pt2.Entity
{
    public class Role
    {
        public int Id { get; set; }

        public RoleEnum role { get; set; }    
        public Role(RoleEnum role)
        {
            this.role = role;    
        }
        public Role()
        {

        }

    }
}
