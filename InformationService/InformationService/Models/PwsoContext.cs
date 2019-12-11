using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InformationService.Models
{
    public partial class PwsoContext : DbContext
    {
        public PwsoContext()
        {
        }

        public PwsoContext(DbContextOptions<PwsoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Athlete> Athlete { get; set; }
        public virtual DbSet<AthleteEvent> AthleteEvent { get; set; }
        public virtual DbSet<Athletes> Athletes { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Teams> Team { get; set; }
        public virtual DbSet<Newsletter> Newsletter { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=RSD109021162554;Database=TrackMeet;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<Area>(entity =>
            {
                entity.ToTable("Area", "track");
            });

            modelBuilder.Entity<Athlete>(entity =>
            {
                entity.ToTable("Athlete", "track");

                entity.Property(e => e.AgeGroup)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<AthleteEvent>(entity =>
            {
                entity.ToTable("AthleteEvent", "track");

                entity.Property(e => e.FieldDistance).HasColumnType("decimal(6, 2)");

                entity.HasOne(d => d.Athlete)
                    .WithMany(p => p.AthleteEvent)
                    .HasForeignKey(d => d.AthleteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AthleteEvent_Athlete");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.AthleteEvent)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AthleteEvent_Event");
            });

            modelBuilder.Entity<Athletes>(entity =>
            {
                entity.ToTable("Athletes", "admin");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Father)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Guardian)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HomePhone)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MF)
                    .HasColumnName("M/F")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.MedicalDate).HasColumnType("date");

                entity.Property(e => e.MedicalExpirationDate).HasColumnType("date");

                entity.Property(e => e.MiddleInitial)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Mother)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParentWorkPhone)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TeeShirtSize)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Event", "track");

                entity.Property(e => e.EventCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EventName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EventType)
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

        }
    }
}
