using Microsoft.EntityFrameworkCore;

namespace Studnet_Management_System.Model
{
    public  class StudMgtContext :DbContext
    {
        public StudMgtContext(DbContextOptions<StudMgtContext> opt):base(opt)
        { }
        public DbSet<Student> Students { get; set; }    

       public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RToken> RTokens { get; set; }

    }
}
