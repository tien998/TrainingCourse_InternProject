using Microsoft.EntityFrameworkCore;
using Model;

public class TraniningDb : DbContext
{
    public TraniningDb(DbContextOptions<TraniningDb> options) : base(options) { }
    public DbSet<Training> Training { get; set; }
    public DbSet<SubjectDepartment> SubjectDepartment { get; set; }
    public DbSet<DepartmentPerTraining> DepartmentPerTraining { get; set; }

    public DbSet<Class> Class { get; set; }
    public DbSet<ClassSchedule> ClassSchedule { get; set; }
    public DbSet<Register> Register { get; set; }
    public DbSet<Subject> Subject { get; set; }
    public DbSet<TrainingProcess> TrainingProcess { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Training>().HasKey(e => e.Id);
        modelBuilder.Entity<SubjectDepartment>().HasKey(e => e.Id);
        modelBuilder.Entity<Class>().HasKey(e => e.Id);

        modelBuilder.Entity<DepartmentPerTraining>(entity =>
        {
            entity.HasKey(e => new { e.TrainingId, e.DepartmentId });
            entity.HasOne(e => e._training).WithMany(e => e._departmentPerTraining).HasForeignKey(e => e.TrainingId);
            entity.HasOne(e => e._subjectDepartment).WithMany(e => e._departmentPerTraining).HasForeignKey(e => e.DepartmentId);
        });

        modelBuilder.Entity<ClassSchedule>(entity =>
                {
                    entity.HasKey(e => new { e.TeacherUserId, e.ClassId });
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

        modelBuilder.Entity<TrainingProcess>(entity =>
        {
            entity.HasKey(e => new { e.ClassId, e.SubjectId });
            entity.HasOne(e => e._class).WithMany(e => e._trainingProcesses).HasForeignKey(e => e.ClassId);
            entity.HasOne(e => e._subject).WithMany(e => e._trainingProcesses).HasForeignKey(e => e.SubjectId);
        });



    }
}