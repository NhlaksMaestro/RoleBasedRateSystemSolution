using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Entities.Mapping
{
    public class EmployeeLoginMap : EntityTypeConfiguration<EmployeeLogin>
    {
        public EmployeeLoginMap()
        {
            // Primary Key
            this.HasKey(t => t.EmployeeId);

            // Properties
            //this.Property(t => t.EmployeeId)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("EmployeeLogins");
            this.Property(t => t.EmployeeId).HasColumnName("EmployeeId");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Password).HasColumnName("Password");

            // Relationships
            this.HasRequired(t => t.Employee)
                .WithRequiredDependent(t => t.EmployeeLogin);

        }
    }
}
