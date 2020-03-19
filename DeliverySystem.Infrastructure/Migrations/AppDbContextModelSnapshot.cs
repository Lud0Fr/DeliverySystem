﻿// <auto-generated />
using System;
using DeliverySystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DeliverySystem.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DeliverySystem.Domain.Deliveries.Delivery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("CreatedBy");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("PartnerId");

                    b.Property<int>("State");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<int?>("UpdatedBy");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("DeliverySystem.Domain.Identities.Identity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("CreatedBy");

                    b.Property<string>("Email");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("PasswordHash");

                    b.Property<int>("Role");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<int?>("UpdatedBy");

                    b.HasKey("Id");

                    b.ToTable("Identities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@admin.com",
                            IsDeleted = false,
                            PasswordHash = "5BAA61E4C9B93F3F0682250B6CF8331B7EE68FD8",
                            Role = 0
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "partner@partner.com",
                            IsDeleted = false,
                            PasswordHash = "5BAA61E4C9B93F3F0682250B6CF8331B7EE68FD8",
                            Role = 2
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "user@user.com",
                            IsDeleted = false,
                            PasswordHash = "5BAA61E4C9B93F3F0682250B6CF8331B7EE68FD8",
                            Role = 1
                        });
                });

            modelBuilder.Entity("DeliverySystem.Domain.Deliveries.Delivery", b =>
                {
                    b.OwnsOne("DeliverySystem.Domain.Deliveries.AccessWindow", "AccessWindow", b1 =>
                        {
                            b1.Property<int>("DeliveryId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTime>("EndTime")
                                .HasColumnName("EndTime");

                            b1.Property<DateTime>("StartTime")
                                .HasColumnName("AccessWindow");

                            b1.HasKey("DeliveryId");

                            b1.ToTable("Deliveries");

                            b1.HasOne("DeliverySystem.Domain.Deliveries.Delivery")
                                .WithOne("AccessWindow")
                                .HasForeignKey("DeliverySystem.Domain.Deliveries.AccessWindow", "DeliveryId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("DeliverySystem.Domain.Deliveries.Order", "Order", b1 =>
                        {
                            b1.Property<int>("DeliveryId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("OrderNumber")
                                .HasColumnName("OrderNumber");

                            b1.Property<string>("Sender")
                                .HasColumnName("Sender");

                            b1.HasKey("DeliveryId");

                            b1.ToTable("Deliveries");

                            b1.HasOne("DeliverySystem.Domain.Deliveries.Delivery")
                                .WithOne("Order")
                                .HasForeignKey("DeliverySystem.Domain.Deliveries.Order", "DeliveryId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("DeliverySystem.Domain.Deliveries.Recipient", "Recipient", b1 =>
                        {
                            b1.Property<int>("DeliveryId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Address")
                                .HasColumnName("Address");

                            b1.Property<string>("Email")
                                .HasColumnName("Email");

                            b1.Property<string>("Name")
                                .HasColumnName("Name");

                            b1.Property<string>("PhoneNumber")
                                .HasColumnName("PhoneNumber");

                            b1.HasKey("DeliveryId");

                            b1.ToTable("Deliveries");

                            b1.HasOne("DeliverySystem.Domain.Deliveries.Delivery")
                                .WithOne("Recipient")
                                .HasForeignKey("DeliverySystem.Domain.Deliveries.Recipient", "DeliveryId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
