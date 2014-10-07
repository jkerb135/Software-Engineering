using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SE.Models.Mapping
{
    public class DetailedStepMap : EntityTypeConfiguration<DetailedStep>
    {
        public DetailedStepMap()
        {
            // Primary Key
            this.HasKey(t => t.DetailedStepID);

            // Properties
            this.Property(t => t.DetailedStepName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.DetailedStepText)
                .HasMaxLength(255);

            this.Property(t => t.ImageFilename)
                .HasMaxLength(255);

            this.Property(t => t.ImagePath)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("DetailedSteps");
            this.Property(t => t.DetailedStepID).HasColumnName("DetailedStepID");
            this.Property(t => t.MainStepID).HasColumnName("MainStepID");
            this.Property(t => t.DetailedStepName).HasColumnName("DetailedStepName");
            this.Property(t => t.DetailedStepText).HasColumnName("DetailedStepText");
            this.Property(t => t.ImageFilename).HasColumnName("ImageFilename");
            this.Property(t => t.ImagePath).HasColumnName("ImagePath");
            this.Property(t => t.CreatedTime).HasColumnName("CreatedTime");
            this.Property(t => t.ListOrder).HasColumnName("ListOrder");

            // Relationships
            this.HasRequired(t => t.MainStep)
                .WithMany(t => t.DetailedSteps)
                .HasForeignKey(d => d.MainStepID);

        }
    }
}
