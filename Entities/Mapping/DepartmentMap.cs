using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Entities.Mapping
{
    public class DepartmentMap : EntityTypeConfiguration<Department>
    {
        public DepartmentMap()
        {
            // Primary Key
            this.HasKey(t => t.DepartmentId);

            // Properties
            this.Property(t => t.DepartmentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.MailStop)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Departments");
            this.Property(t => t.DepartmentId).HasColumnName("DepartmentId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.MailStop).HasColumnName("MailStop");
            this.Property(t => t.DateTimeCreated).HasColumnName("DateTimeCreated");
        }
    }
}
