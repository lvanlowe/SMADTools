using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InformationService.Models
{
    public partial class PwsodbContext : DbContext
    {
        public PwsodbContext()
        {
        }

        public PwsodbContext(DbContextOptions<PwsodbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Carrier> Carrier { get; set; }
        public virtual DbSet<Coach> Coach { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<PhoneTypes> PhoneTypes { get; set; }
        public virtual DbSet<RegisteredAthlete> RegisteredAthlete { get; set; }
        public virtual DbSet<Registrant> Registrant { get; set; }
        public virtual DbSet<RegistrantEmail> RegistrantEmail { get; set; }
        public virtual DbSet<RegistrantPhone> RegistrantPhone { get; set; }
        public virtual DbSet<Programs> Programs { get; set; }
        public virtual DbSet<Sports> Sports { get; set; }
        public virtual DbSet<SportTypes> SportTypes { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }
        public virtual DbSet<Uniforms> Uniforms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=RSD109021162554;Database=PWSODB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<Carrier>(entity =>
            {
                entity.ToTable("Carrier", "pwso");

                entity.Property(e => e.Domain)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Coach>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Coach)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Coach_Location");
            });

            //modelBuilder.Entity<Location>(entity =>
            //{
            //    entity.ToTable("Location", "pwso");

            //    entity.Property(e => e.Id).HasColumnName("id");

            //    entity.Property(e => e.Name)
            //        .HasMaxLength(25)
            //        .IsUnicode(false);

            //    entity.HasOne(d => d.Sport)
            //        .WithMany(p => p.Location)
            //        .HasForeignKey(d => d.SportId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Location_Sport");
            //});

            modelBuilder.Entity<PhoneTypes>(entity =>
            {
                entity.ToTable("PhoneTypes", "pwso");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RegisteredAthlete>(entity =>
            {
                entity.ToTable("RegisteredAthlete", "pwso");

                entity.HasOne(d => d.Registrant)
                    .WithMany(p => p.RegisteredAthlete)
                    .HasForeignKey(d => d.RegistrantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegisteredAthlete_Registrant");

                entity.HasOne(d => d.Uniforms)
                    .WithMany(p => p.RegisteredAthlete)
                    .HasForeignKey(d => d.UniformsId)
                    .HasConstraintName("FK_RegisteredAthlete_Uniforms");
            });

            modelBuilder.Entity<Registrant>(entity =>
            {
                entity.ToTable("Registrant", "pwso");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NickName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Size)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Year)
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RegistrantEmail>(entity =>
            {
                entity.ToTable("RegistrantEmail", "pwso");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Registrant)
                    .WithMany(p => p.RegistrantEmail)
                    .HasForeignKey(d => d.RegistrantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegistrantEmail_Registrant");
            });

            modelBuilder.Entity<RegistrantPhone>(entity =>
            {
                entity.ToTable("RegistrantPhone", "pwso");

                entity.Property(e => e.CarrierId).HasColumnName("CarrierID");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Carrier)
                    .WithMany(p => p.RegistrantPhone)
                    .HasForeignKey(d => d.CarrierId)
                    .HasConstraintName("FK_RegistrantPhone_Carrier");

                entity.HasOne(d => d.PhoneTypeNavigation)
                    .WithMany(p => p.RegistrantPhone)
                    .HasForeignKey(d => d.PhoneTypeId)
                    .HasConstraintName("FK_RegistrantPhone_PhoneTypes");

                entity.HasOne(d => d.Registrant)
                    .WithMany(p => p.RegistrantPhone)
                    .HasForeignKey(d => d.RegistrantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegistrantPhone_Registrant");
            });


            modelBuilder.Entity<Uniforms>(entity =>
            {
                entity.ToTable("Uniforms", "pwso");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comments)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.UniqueId)
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Programs>(entity =>
            {
                entity.ToTable("Programs", "pwso");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.SportNavigation)
                    .WithMany(p => p.Programs)
                    .HasForeignKey(d => d.Sport)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Programs_Sport");
            });

            modelBuilder.Entity<SportTypes>(entity =>
            {
                entity.ToTable("SportTypes", "pwso");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.SportTypes)
                    .HasForeignKey(d => d.SportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SportTypes_Sport");
            });

            modelBuilder.Entity<Sports>(entity =>
            {
                entity.ToTable("Sports", "pwso");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Teams>(entity =>
            {
                entity.ToTable("Teams", "pwso");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teams_Program");

                entity.HasOne(d => d.SportTypeNavigation)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.SportType)
                    .HasConstraintName("FK_Teams_SportType");
            });
        }
    }
}
