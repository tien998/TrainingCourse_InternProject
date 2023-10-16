using Microsoft.EntityFrameworkCore;
using Model;

public class TraniningDb : DbContext
{
    public TraniningDb(DbContextOptions<TraniningDb> options) : base(options) {}
    public DbSet<Training> Training { get; set; }
    public DbSet<SubjectDepartment> SubjectDepartment { get; set; }
    public DbSet<DepartmentPerTraining> DepartmentPerTraining { get; set; }

    public DbSet<Class> Class { get; set; }
    public DbSet<Register> Register { get; set; }
    public DbSet<Subject> Subject { get; set; }
    public DbSet<TrainingProcess> TrainingProcess { get; set; }
}