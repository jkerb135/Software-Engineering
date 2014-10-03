using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iPaws.Models.Mapping
{
    public class MemberAssignmentMap : EntityTypeConfiguration<MemberAssignment>
    {
        public MemberAssignmentMap()
        {
            // Primary Key
            this.HasKey(t => t.AssignedUser);

            // Properties
            this.Property(t => t.AssignedUser)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.AssignedSupervisor)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("MemberAssignments");
            this.Property(t => t.AssignedUser).HasColumnName("AssignedUser");
            this.Property(t => t.AssignedSupervisor).HasColumnName("AssignedSupervisor");
        }
    }
}
