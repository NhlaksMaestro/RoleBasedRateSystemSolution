using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Entities.Mapping
{
    public class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            // Primary Key
            this.HasKey(t => t.EmployeeId);

            // Properties
            this.Property(t => t.EmployeeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Address)
                .HasMaxLength(100);

            this.Property(t => t.Phone)
                .IsFixedLength()
                .HasMaxLength(24);

            // Table & Column Mappings
            this.ToTable("Employees");
            this.Property(t => t.EmployeeId).HasColumnName("EmployeeId");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.DateTimeCreated).HasColumnName("DateTimeCreated");
            this.Property(t => t.DepartmentRoleId).HasColumnName("DepartmentRoleId");
            this.Property(t => t.DepartmentId).HasColumnName("DepartmentId");

            // Relationships
            this.HasRequired(t => t.DepartmentRole)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.DepartmentRoleId);
            this.HasRequired(t => t.Department)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.DepartmentId);
            //this.HasRequired(t => t.EmployeeLogin)
            //    .WithRequiredDependent(t => t.Employee);
        }
    }
}
