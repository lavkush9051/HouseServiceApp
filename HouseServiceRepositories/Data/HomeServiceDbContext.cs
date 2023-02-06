using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HouseServiceRepositories.Data
{
    public partial class HomeServiceDbContext : DbContext
    {
        public HomeServiceDbContext()
        {
        }

        public HomeServiceDbContext(DbContextOptions<HomeServiceDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomersRequest> CustomersRequests { get; set; } = null!;
        public virtual DbSet<Owner> Owners { get; set; } = null!;
        public virtual DbSet<ServicesList> ServicesLists { get; set; } = null!;
        public virtual DbSet<ServicesTable> ServicesTables { get; set; } = null!;
        public virtual DbSet<Admin> Admin { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=localhost\\SQLEXPRESS; database=HomeServiceDb; integrated security=true");
            }*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e=> e.AdminName) 
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e=> e.AdminPassword)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomersRequest>(entity =>
            {
                entity.HasKey(e => e.AppointmentId)
                    .HasName("PK__Customer__8ECDFCC288787494");

                entity.ToTable("CustomersRequest");

                entity.Property(e => e.DateOfAppointment).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomersRequests)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Customers__Custo__3D5E1FD2");
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.Property(e => e.OwnerName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Owners)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__Owners__ServiceI__3A81B327");
            });

            modelBuilder.Entity<ServicesList>(entity =>
            {
                entity.HasKey(e => e.AppointId)
                    .HasName("PK__Services__DCC1C939D1C83916");

                entity.ToTable("ServicesList");

                entity.Property(e => e.DateofAppointment).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ServicesLists)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__ServicesL__Custo__403A8C7D");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.ServicesLists)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK__ServicesL__Owner__412EB0B6");
            });

            modelBuilder.Entity<ServicesTable>(entity =>
            {
                entity.HasKey(e => e.ServiceId)
                    .HasName("PK__Services__C51BB00A233D5078");

                entity.ToTable("ServicesTable");

                entity.Property(e => e.ServiceName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
