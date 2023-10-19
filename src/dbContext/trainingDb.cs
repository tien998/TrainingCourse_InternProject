using Microsoft.EntityFrameworkCore;
using Model;

public class TraniningDb : DbContext
{
    public TraniningDb(DbContextOptions<TraniningDb> options) : base(options) { }
    public DbSet<TrainingYear> TrainingYear { get; set; }
    public DbSet<SubjectDepartment> SubjectDepartment { get; set; }
    public DbSet<DepartmentPerTrainingYear> DepartmentPerTrainingYear { get; set; }

    public DbSet<Course> Course { get; set; }
    public DbSet<Class> Class { get; set; }
    public DbSet<ClassSchedule> ClassSchedule { get; set; }
    public DbSet<Register> Register { get; set; }
    public DbSet<Subject> Subject { get; set; }
    public DbSet<CourseSubject> CourseSubject { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TrainingYear>().HasKey(e => e.Id);
        modelBuilder.Entity<SubjectDepartment>().HasKey(e => e.Id);
        modelBuilder.Entity<Course>().HasKey(e => e.Id);

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e._course).WithMany(e => e._class).HasForeignKey(e => e.CourseId);
        });

        modelBuilder.Entity<DepartmentPerTrainingYear>(entity =>
        {
            entity.HasKey(e => new { e.TrainingYearId, e.DepartmentId });
            entity.HasOne(e => e._trainingYear).WithMany(e => e._departmentPerTraining).HasForeignKey(e => e.TrainingYearId);
            entity.HasOne(e => e._subjectDepartment).WithMany(e => e._departmentPerTraining).HasForeignKey(e => e.DepartmentId);
        });

        modelBuilder.Entity<ClassSchedule>(entity =>
        {
            entity.HasKey(e => new { e.TeacherId, e.ClassId });
            entity.HasOne(e => e._class).WithMany(e => e._classSchedule).HasForeignKey(e => e.ClassId);
        });

        modelBuilder.Entity<Register>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ClassId });
            entity.HasOne(e => e._class).WithMany(e => e._register).HasForeignKey(e => e.ClassId);
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e._subjectDepartment).WithMany(e => e._subjects).HasForeignKey(e => e.DepartmentId);
        });

        modelBuilder.Entity<CourseSubject>(entity =>
        {
            entity.HasKey(e => new { e.CourseId, e.SubjectId });
            entity.HasOne(e => e._course).WithMany(e => e._courseSubject).HasForeignKey(e => e.CourseId);
            entity.HasOne(e => e._subject).WithMany(e => e._courseSubject).HasForeignKey(e => e.SubjectId);
        });



    }
}