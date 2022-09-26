using LabWork_pt2.Entity;
using Microsoft.EntityFrameworkCore;
using FileModel = LabWork_pt2.Entity.FileModel;

namespace LabWork_pt2;


public class DataBaseContext:DbContext
{
    public DbSet<User> Users { get; set; } 
    public DbSet<Role> Roles { get; set; } 
    public DbSet<FileModel> Files { get; set; } 
    public DataBaseContext()
    {
        
        Database.EnsureCreated();   
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("server=localhost; Port = 5432; Database=postgres; username=postgres; password=LEDsa300");
        
    }
    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();
    }

}
