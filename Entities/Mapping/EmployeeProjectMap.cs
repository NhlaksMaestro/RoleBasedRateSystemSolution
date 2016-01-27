using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Entities.Mapping
{
    public class EmployeeProjectMap : EntityTypeConfiguration<EmployeeProject>
    {
        public EmployeeProjectMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EmployeeId, t.ProjectId, t.ProjectRoleId });

            // Properties
            this.Property(t => t.EmployeeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProjectId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProjectRoleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("EmployeeProjects");
            this.Property(t => t.EmployeeId).HasColumnName("EmployeeId");
            this.Property(t => t.ProjectId).HasColumnName("ProjectId");
            this.Property(t => t.ProjectRoleId).HasColumnName("ProjectRoleId");

            // Relationships
            this.HasRequired(t => t.Employee)
                .WithMany(t => t.EmployeeProjects)
                .HasForeignKey(d => d.EmployeeId);
            this.HasRequired(t => t.ProjectRole)
                .WithMany(t => t.EmployeeProjects)
                .HasForeignKey(d => d.ProjectRoleId);
            this.HasRequired(t => t.Project)
                .WithMany(t => t.EmployeeProjects)
                .HasForeignKey(d => d.ProjectId);

        }
    }
}
