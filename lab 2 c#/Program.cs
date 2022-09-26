using LabWork_pt2;
using Microsoft.EntityFrameworkCore;
using LabWork_pt2.Entity;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using FileModel = LabWork_pt2.Entity.FileModel;

internal class Program
{
    
    private static void Main(string[] args)
    {
   
        using (DataBaseContext db = new DataBaseContext())
        {
           /* User user1 = new User("Dima", "ZXC", new Role(LabWork_pt2.Enum.RoleEnum.ROLE_USER));
            User user2 = new User("Dimas", "Dimas", new Role(LabWork_pt2.Enum.RoleEnum.ROLE_ADMIN));
            *//* FileModel file = new FileModel("ZOMBIE", "PATH", "A LOT OF ZOMBIES");
             FileModel file2 = new FileModel("ZOMBIESDASDAD", "PATH", "A LOT OF ZOMBIES");
             FileModel file1 = new FileModel("ZOMBIE_asdasds", "PATH", "A LOT OF ZOMBIES");
             FileModel file3 = new FileModel("War thunder ", "PATH", "GAME WITH 1000 tanks");*//*


            db.Users.AddRange(user1, user2);
            // db.Files.AddRange(file, file1, file2, file3);
            db.SaveChanges();*/

        }
        CreateHostBuilder(args).Build().Run();  

    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               });
}