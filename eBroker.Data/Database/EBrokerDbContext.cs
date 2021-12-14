using System;
using eBroker.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace eBroker.Data.Database
{
    public partial class EBrokerDbContext : DbContext
    {
        public EBrokerDbContext()
        {
        }

        public EBrokerDbContext(DbContextOptions<EBrokerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<TradeHistory> TradeHistory { get; set; }
        public virtual DbSet<TradeType> TradeType { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserPortfolio> UserPortfolio { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var settings = new AppConfiguration();
                optionsBuilder.UseSqlServer(settings.SqlConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Account__UserID__2C3393D0");
            });

            modelBuilder.Entity<TradeHistory>(entity =>
            {
                entity.HasKey(e => e.TradeId)
                    .HasName("PK__TradeHis__3028BABB0091A85E");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.TradeHistory)
                    .HasForeignKey(d => d.StockId)
                    .HasConstraintName("FK__TradeHist__Stock__34C8D9D1");

                entity.HasOne(d => d.TradeTypeNavigation)
                    .WithMany(p => p.TradeHistory)
                    .HasForeignKey(d => d.TradeType)
                    .HasConstraintName("FK__TradeHist__Trade__33D4B598");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TradeHistory)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__TradeHist__UserI__32E0915F");
            });

            modelBuilder.Entity<TradeType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK__TradeTyp__516F039598CB335B");
            });

            modelBuilder.Entity<UserPortfolio>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK__UserPort__FBDF78C96AE8F39B");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.UserPortfolio)
                    .HasForeignKey(d => d.StockId)
                    .HasConstraintName("FK__UserPortf__Stock__38996AB5");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPortfolio)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserPortf__UserI__37A5467C");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__UserRole__RoleID__29572725");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserRole__UserID__286302EC");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
