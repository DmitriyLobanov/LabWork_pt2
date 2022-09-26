namespace LabWork_pt2.Entity
{
    public class FileModel
    {
        public int Id { get; set; }

        public string Name { get; set; }    

        public string Path { get; set; }    

        public string FileDescription { get; set; }

        public int downloadedCounter { get; set; }  

        
        public FileModel(string name, string path, string fileDescription)
        {
            Name = name;
            Path = path;
            FileDescription = fileDescription;
        }
        public FileModel( string fileDescription)
        {
            
            FileDescription = fileDescription;
        }
    }
}
