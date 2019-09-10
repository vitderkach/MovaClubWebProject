using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovaClubWebApp.MovaClubDb.Models;
namespace MovaClubWebApp.MovaClubDb
{
    public class MovaClubDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguageLevel> LanguageLevels { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<GroupStudent> GroupStudents { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<OfficeTeacher> OfficeTeachers { get; set; }
        public DbSet<GroupTeacher> GroupTeachers { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<LanguageTeacher> LanguageTeachers { get; set; }
        public DbSet<Lesson> Lessons {get; set; }
        public DbSet<ExceptLesson> ExceptLessons { get; set; }
        public DbSet<OccurLesson> OccurLessons { get; set; }

        public MovaClubDbContext(DbContextOptions<MovaClubDbContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Language>().HasAlternateKey(l => l.Title);
            modelBuilder.Entity<LanguageLevel>().HasAlternateKey(ll => ll.Title);
            modelBuilder.Entity<LanguageTeacher>().HasKey(lt => new { lt.TeacherId, lt.LanguageId });
            modelBuilder.Entity<GroupStudent>().HasKey(gs => new { gs.GroupId, gs.StudentId });
            modelBuilder.Entity<OfficeTeacher>().HasKey(ot => new { ot.OfficeId, ot.TeacherId });
            modelBuilder.Entity<GroupTeacher>().HasKey(gt => new { gt.GroupId, gt.TeacherId });
            modelBuilder.Entity<ClassRoom>().HasIndex(cr => new { cr.OfficeId, cr.Title }).IsUnique();
        }
    }
}
