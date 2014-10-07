using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SE.Models.Mapping
{
    public class MainStepMap : EntityTypeConfiguration<MainStep>
    {
        public MainStepMap()
        {
            // Primary Key
            this.HasKey(t => t.MainStepID);

            // Properties
            this.Property(t => t.MainStepName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.MainStepText)
                .HasMaxLength(255);

            this.Property(t => t.AudioFilename)
                .HasMaxLength(255);

            this.Property(t => t.AudioPath)
                .HasMaxLength(255);

            this.Property(t => t.VideoFilename)
                .HasMaxLength(255);

            this.Property(t => t.VideoPath)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("MainSteps");
            this.Property(t => t.MainStepID).HasColumnName("MainStepID");
            this.Property(t => t.TaskID).HasColumnName("TaskID");
            this.Property(t => t.MainStepName).HasColumnName("MainStepName");
            this.Property(t => t.MainStepText).HasColumnName("MainStepText");
            this.Property(t => t.MainStepTime).HasColumnName("MainStepTime");
            this.Property(t => t.AudioFilename).HasColumnName("AudioFilename");
            this.Property(t => t.AudioPath).HasColumnName("AudioPath");
            this.Property(t => t.VideoFilename).HasColumnName("VideoFilename");
            this.Property(t => t.VideoPath).HasColumnName("VideoPath");
            this.Property(t => t.CreatedTime).HasColumnName("CreatedTime");
            this.Property(t => t.ListOrder).HasColumnName("ListOrder");

            // Relationships
            this.HasRequired(t => t.Task)
                .WithMany(t => t.MainSteps)
                .HasForeignKey(d => d.TaskID);

        }
    }
}
