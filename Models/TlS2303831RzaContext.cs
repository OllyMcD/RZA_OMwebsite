using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace RZA_OMwebsite.Models;

public partial class TlS2303831RzaContext : DbContext
{
    public TlS2303831RzaContext()
    {
    }

    public TlS2303831RzaContext(DbContextOptions<TlS2303831RzaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Animal> Animals { get; set; }

    public virtual DbSet<Animallocation> Animallocations { get; set; }

    public virtual DbSet<Attraction> Attractions { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Matchanimal> Matchanimals { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Roombooking> Roombookings { get; set; }

    public virtual DbSet<Stat> Stats { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Ticketbooking> Ticketbookings { get; set; }

    public virtual DbSet<Tracking> Trackings { get; set; }

    public virtual DbSet<Zoo> Zoos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=MYSqlConnection", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasKey(e => e.AnimalId).HasName("PRIMARY");

            entity.ToTable("animal");

            entity.Property(e => e.AnimalId).HasColumnName("AnimalID");
            entity.Property(e => e.IndividualName).HasMaxLength(255);
            entity.Property(e => e.Sex).HasColumnType("enum('Male','Female')");
            entity.Property(e => e.Species).HasMaxLength(255);
        });

        modelBuilder.Entity<Animallocation>(entity =>
        {
            entity.HasKey(e => new { e.AnimalId, e.ZooName, e.DateArrived })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("animallocation");

            entity.HasIndex(e => e.ZooName, "ZooName");

            entity.Property(e => e.AnimalId).HasColumnName("AnimalID");

            entity.HasOne(d => d.Animal).WithMany(p => p.Animallocations)
                .HasForeignKey(d => d.AnimalId)
                .HasConstraintName("animallocation_ibfk_1");

            entity.HasOne(d => d.ZooNameNavigation).WithMany(p => p.Animallocations)
                .HasForeignKey(d => d.ZooName)
                .HasConstraintName("animallocation_ibfk_2");
        });

        modelBuilder.Entity<Attraction>(entity =>
        {
            entity.HasKey(e => e.AttractionId).HasName("PRIMARY");

            entity.ToTable("attraction");

            entity.Property(e => e.AttractionId).HasColumnName("attractionId");
            entity.Property(e => e.Desc)
                .HasMaxLength(500)
                .HasColumnName("Desc.");
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("customers");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "phoneNumber").IsUnique();

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .HasColumnName("lastName");
            entity.Property(e => e.LoyaltyPoints).HasColumnName("loyaltyPoints");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Postcode)
                .HasMaxLength(8)
                .HasColumnName("postcode");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Matchanimal>(entity =>
        {
            entity.HasKey(e => new { e.AnimalFemaleId, e.AnimalMaleId, e.DateOfMatch })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("matchanimal");

            entity.HasIndex(e => e.AnimalMaleId, "AnimalMaleID");

            entity.Property(e => e.AnimalFemaleId).HasColumnName("AnimalFemaleID");
            entity.Property(e => e.AnimalMaleId).HasColumnName("AnimalMaleID");

            entity.HasOne(d => d.AnimalFemale).WithMany(p => p.MatchanimalAnimalFemales)
                .HasForeignKey(d => d.AnimalFemaleId)
                .HasConstraintName("matchanimal_ibfk_1");

            entity.HasOne(d => d.AnimalMale).WithMany(p => p.MatchanimalAnimalMales)
                .HasForeignKey(d => d.AnimalMaleId)
                .HasConstraintName("matchanimal_ibfk_2");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomNumber).HasName("PRIMARY");

            entity.ToTable("rooms");

            entity.Property(e => e.RoomNumber)
                .ValueGeneratedNever()
                .HasColumnName("roomNumber");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.RoomType)
                .HasMaxLength(20)
                .HasColumnName("roomType");
        });

        modelBuilder.Entity<Roombooking>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.RoomNumber, e.StartDate })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("roombookings");

            entity.HasIndex(e => e.RoomNumber, "roomNumber");

            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.RoomNumber).HasColumnName("roomNumber");
            entity.Property(e => e.StartDate).HasColumnName("startDate");
            entity.Property(e => e.EndDate).HasColumnName("endDate");

            entity.HasOne(d => d.Customer).WithMany(p => p.Roombookings)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("roombooking_ibfk_1");

            entity.HasOne(d => d.Room).WithMany(p => p.Roombookings)
                .HasForeignKey(d => d.RoomNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("roombookings_ibfk_2");
        });

        modelBuilder.Entity<Stat>(entity =>
        {
            entity.HasKey(e => e.PageUrl).HasName("PRIMARY");

            entity.ToTable("stats");

            entity.HasIndex(e => e.PageUrl, "Page_url_UNIQUE").IsUnique();

            entity.Property(e => e.PageUrl).HasColumnName("Page_url");
            entity.Property(e => e.PageViews).HasColumnName("Page_views");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PRIMARY");

            entity.ToTable("ticket");

            entity.HasIndex(e => e.AttractionId, "attractionID_idx");

            entity.HasIndex(e => e.Ticketbookingid, "ticket_fk2_idx");

            entity.Property(e => e.TicketId).HasColumnName("ticketId");
            entity.Property(e => e.AttractionId).HasColumnName("attractionId");
            entity.Property(e => e.Ticketbookingid).HasColumnName("ticketbookingid");

            entity.HasOne(d => d.Attraction).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.AttractionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ticket_fk1");

            entity.HasOne(d => d.Ticketbooking).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.Ticketbookingid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ticket_fk2");
        });

        modelBuilder.Entity<Ticketbooking>(entity =>
        {
            entity.HasKey(e => e.TicketbookingId).HasName("PRIMARY");

            entity.ToTable("ticketbooking");

            entity.HasIndex(e => e.TicketbookingId, "ticketId_idx");

            entity.HasIndex(e => e.CustomerId, "ticketbooking_fk1_idx");

            entity.Property(e => e.TicketbookingId).HasColumnName("ticketbookingId");
            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DateBooked).HasColumnName("dateBooked");

            entity.HasOne(d => d.Customer).WithMany(p => p.Ticketbookings)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ticketbooking_fk1");
        });

        modelBuilder.Entity<Tracking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tracking");

            entity.Property(e => e.Action).HasMaxLength(255);
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("IP_Address");
            entity.Property(e => e.Timestamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<Zoo>(entity =>
        {
            entity.HasKey(e => e.ZooName).HasName("PRIMARY");

            entity.ToTable("zoo");

            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.Town).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
