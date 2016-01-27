using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Entities;
using Entities.Mapping;
using Repository.Pattern.Ef6;

namespace DAL
{
    public partial class RoleBasedSystemDBContext : DataContext
    {
        static RoleBasedSystemDBContext()
        {
            //Database.SetInitializer<RoleBasedSystemDBContext>(new RoleBasedSystemDBContextSeeder());
            Database.SetInitializer<RoleBasedSystemDBContext>(new RoleBasedSystemDBContextSeeder());
            using (var db = new RoleBasedSystemDBContext())
            {
                db.Database.Initialize(true);
            }
        }

        public RoleBasedSystemDBContext()
            : base("Name=RoleBasedSystemDBContext")
        {
        }

        public DbSet<DepartmentRole> DepartmentRoles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeLogin> EmployeeLogins { get; set; }
        public DbSet<ProjectRole> ProjectRoles { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DepartmentRoleMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new EmployeeProjectMap());
            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new ProjectRoleMap());
            modelBuilder.Configurations.Add(new ProjectMap());
            modelBuilder.Configurations.Add(new EmployeeLoginMap());
        }
    }
}