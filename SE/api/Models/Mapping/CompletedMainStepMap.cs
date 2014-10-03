using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iPaws.Models.Mapping
{
    public class CompletedMainStepMap : EntityTypeConfiguration<CompletedMainStep>
    {
        public CompletedMainStepMap()
        {
            // Primary Key
            this.HasKey(t => new { t.MainStepID, t.TaskID, t.MainStepName, t.AssignedUser, t.DateTimeComplete, t.TotalTime });

            // Properties
            this.Property(t => t.MainStepID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TaskID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MainStepName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.AssignedUser)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("CompletedMainSteps");
            this.Property(t => t.MainStepID).HasColumnName("MainStepID");
            this.Property(t => t.TaskID).HasColumnName("TaskID");
            this.Property(t => t.MainStepName).HasColumnName("MainStepName");
            this.Property(t => t.AssignedUser).HasColumnName("AssignedUser");
            this.Property(t => t.DateTimeComplete).HasColumnName("DateTimeComplete");
            this.Property(t => t.TotalTime).HasColumnName("TotalTime");

            // Relationships
            this.HasRequired(t => t.MainStep)
                .WithMany(t => t.CompletedMainSteps)
                .HasForeignKey(d => d.MainStepID);
            this.HasRequired(t => t.MemberAssignment)
                .WithMany(t => t.CompletedMainSteps)
                .HasForeignKey(d => d.AssignedUser);
            this.HasRequired(t => t.Task)
                .WithMany(t => t.CompletedMainSteps)
                .HasForeignKey(d => d.TaskID);

        }
    }
}
