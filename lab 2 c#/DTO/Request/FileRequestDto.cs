namespace LabWork_pt2.DTO.Request
{
    public class FileRequestDto
    {
        public IFormFile formFile { get; set; }
        public string fileDescription { get; set; }

        public FileRequestDto(IFormFile formFile, string fileDescription)
        {
            this.formFile = formFile;
            this.fileDescription = fileDescription;
        }
    }
}
