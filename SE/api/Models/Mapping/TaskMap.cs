using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iPaws.Models.Mapping
{
    public class TaskMap : EntityTypeConfiguration<Task>
    {
        public TaskMap()
        {
            // Primary Key
            this.HasKey(t => t.TaskID);

            // Properties
            this.Property(t => t.AssignedUser)
                .HasMaxLength(255);

            this.Property(t => t.TaskName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Tasks");
            this.Property(t => t.TaskID).HasColumnName("TaskID");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.AssignedUser).HasColumnName("AssignedUser");
            this.Property(t => t.TaskName).HasColumnName("TaskName");
            this.Property(t => t.TaskTime).HasColumnName("TaskTime");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CreatedTime).HasColumnName("CreatedTime");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");

            // Relationships
            this.HasRequired(t => t.Category)
                .WithMany(t => t.Tasks)
                .HasForeignKey(d => d.CategoryID);
            this.HasOptional(t => t.MemberAssignment)
                .WithMany(t => t.Tasks)
                .HasForeignKey(d => d.AssignedUser);

        }
    }
}
