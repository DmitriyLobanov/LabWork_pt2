using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace LabWork_pt2.DTO.Request
{
    public class FileChangeDescriptionDto
    {
        [Required(ErrorMessage = "Please a new file description")]
        public string newFileDescription { get; set; }

        public string id { get; set; }   
    }
}
