using LabWork_pt2.DTO.Request;
using LabWork_pt2.Entity;
using LabWork_pt2.Enum;
using LabWork_pt2.Repository;

namespace LabWork_pt2.Service
{
    public class FileService
    {
        private readonly DataBaseContext _dataBaseContext = new DataBaseContext();
        public IQueryable<FileModel> Search(string searchString)
        {

            var files = from m in _dataBaseContext.Files
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                files = files.Where(s => s.FileDescription.Contains(searchString));

            }
            return files;
        }

    }
}
