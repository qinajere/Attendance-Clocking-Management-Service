using AttendanceClockingManagementSystem.API.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace AttendanceClockingManagementSystem.API.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<QRCode> QRCodes { get; set; }
        public DbSet<OfficeTiming> OfficeTimings { get; set; }
        public DbSet<Absent> Absents { get; set; }
        public DbSet<Leave> Leaves { get; set; }

        protected override void  OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Attendance>()
                .HasOne<QRCode>(q => q.QRCode)
                .WithOne(u => u.Attendance)
                .HasForeignKey<Attendance>(x => x.QRCodeID);

            base.OnModelCreating(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            builder.Properties<DateOnly>()
               .HaveConversion<DateOnlyConverter>()
               .HaveColumnType("date");

            builder.Properties<DateOnly?>()
                .HaveConversion<NullableDateOnlyConverter>()
                .HaveColumnType("date");
        }
    }
}

