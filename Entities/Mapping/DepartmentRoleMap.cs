using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Entities.Mapping
{
    public class DepartmentRoleMap : EntityTypeConfiguration<DepartmentRole>
    {
        public DepartmentRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.DepartmentRoleId);

            // Properties
            this.Property(t => t.DepartmentRoleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("DepartmentRoles");
            this.Property(t => t.DepartmentRoleId).HasColumnName("DepartmentRoleId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.DateTimeCreated).HasColumnName("DateTimeCreated");
            this.Property(t => t.RatePerHour).HasColumnName("RatePerHour");
        }
    }
}
