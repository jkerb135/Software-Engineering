using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SE.Models.Mapping
{
    public class CompletedTaskMap : EntityTypeConfiguration<CompletedTask>
    {
        public CompletedTaskMap()
        {
            // Primary Key
            this.HasKey(t => t.TaskID);

            // Properties
            this.Property(t => t.TaskID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TaskName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.AssignedUser)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("CompletedTasks");
            this.Property(t => t.TaskID).HasColumnName("TaskID");
            this.Property(t => t.TaskName).HasColumnName("TaskName");
            this.Property(t => t.AssignedUser).HasColumnName("AssignedUser");
            this.Property(t => t.DateTimeCompleted).HasColumnName("DateTimeCompleted");
            this.Property(t => t.TotalTime).HasColumnName("TotalTime");

            // Relationships
            this.HasRequired(t => t.MemberAssignment)
                .WithMany(t => t.CompletedTasks)
                .HasForeignKey(d => d.AssignedUser);
            this.HasRequired(t => t.Task)
                .WithOptional(t => t.CompletedTask);

        }
    }
}
