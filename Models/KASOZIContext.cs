using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace JobInterview.Models
{
    public partial class KASOZIContext : DbContext
    {
        public KASOZIContext()
        {
        }

        public KASOZIContext(DbContextOptions<KASOZIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Rate> Rates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=KASOZI;user=root;pwd=kasozi", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb3")
                .UseCollation("utf8_general_ci");

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.ToTable("rates");

                entity.HasIndex(e => e.Id, "ID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Currency)
                    .HasMaxLength(45)
                    .HasColumnName("CURRENCY");

                entity.Property(e => e.Date)
                    .HasMaxLength(45)
                    .HasColumnName("DATE");

                entity.Property(e => e.From).HasColumnName("FROM");

                entity.Property(e => e.To).HasColumnName("TO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
