using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Entities.Mapping
{
    public class ProjectRoleMap : EntityTypeConfiguration<ProjectRole>
    {
        public ProjectRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.ProjectRoleId);

            // Properties
            this.Property(t => t.ProjectRoleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("ProjectRoles");
            this.Property(t => t.ProjectRoleId).HasColumnName("ProjectRoleId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.DateTimeCreated).HasColumnName("DateTimeCreated");
        }
    }
}
