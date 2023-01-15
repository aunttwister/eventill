﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Reservations.Persistance;

#nullable disable

namespace Reservations.Persistance.Migrations
{
    [DbContext(typeof(ReservationDbContext))]
    [Migration("20230114195218_AddActivePropEventQuestion")]
    partial class AddActivePropEventQuestion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Reservations.Domain.Audit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Action")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("KeyValues")
                        .HasColumnType("longtext");

                    b.Property<string>("NewValues")
                        .HasColumnType("longtext");

                    b.Property<string>("OldValues")
                        .HasColumnType("longtext");

                    b.Property<string>("TableName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Audits");
                });

            modelBuilder.Entity("Reservations.Domain.Event", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("EventTypeId")
                        .HasColumnType("int");

                    b.Property<ulong>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(0ul);

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<long>("Length")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("EventTypeId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Reservations.Domain.EventOccurrence", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("EventId")
                        .HasColumnType("bigint");

                    b.Property<ulong>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(0ul);

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("EventOccurrences");
                });

            modelBuilder.Entity("Reservations.Domain.EventQuestion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<sbyte>("Active")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<long>("EventId")
                        .HasColumnType("bigint");

                    b.Property<ulong>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(0ul);

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<long>("QuestionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("QuestionId");

                    b.ToTable("EventQuestions");
                });

            modelBuilder.Entity("Reservations.Domain.EventType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<ulong>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(0ul);

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.HasKey("Id");

                    b.ToTable("EventTypes");
                });

            modelBuilder.Entity("Reservations.Domain.Extension", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<ulong>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(0ul);

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("Extension");
                });

            modelBuilder.Entity("Reservations.Domain.LoginDetails", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<ulong>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(0ul);

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("LoginDetails");
                });

            modelBuilder.Entity("Reservations.Domain.Question", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<ulong>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(0ul);

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Reservations.Domain.Reservation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<ulong>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(0ul);

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Reservations.Domain.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<ulong>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(0ul);

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = 0ul,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = 0ul,
                            Name = "Guest"
                        });
                });

            modelBuilder.Entity("Reservations.Domain.Ticket", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<long?>("EventOccurenceId")
                        .HasColumnType("bigint");

                    b.Property<ulong>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(0ul);

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<long?>("ReservationId")
                        .HasColumnType("bigint");

                    b.Property<string>("TicketState")
                        .IsRequired()
                        .HasColumnType("enum('Available', 'Unavailable', 'Reserved', 'Sold')");

                    b.HasKey("Id");

                    b.HasIndex("EventOccurenceId");

                    b.HasIndex("ReservationId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Reservations.Domain.TicketExtension", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<long>("ExtensionId")
                        .HasColumnType("bigint");

                    b.Property<ulong>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(0ul);

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<long>("TicketId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ExtensionId");

                    b.HasIndex("TicketId");

                    b.ToTable("TicketExtension");
                });

            modelBuilder.Entity("Reservations.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<ulong>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(0ul);

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Reservations.Domain.Event", b =>
                {
                    b.HasOne("Reservations.Domain.EventType", "EventType")
                        .WithMany("Events")
                        .HasForeignKey("EventTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventType");
                });

            modelBuilder.Entity("Reservations.Domain.EventOccurrence", b =>
                {
                    b.HasOne("Reservations.Domain.Event", "Event")
                        .WithMany("EventOccurences")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Reservations.Domain.EventQuestion", b =>
                {
                    b.HasOne("Reservations.Domain.Event", "Event")
                        .WithMany("EventQuestions")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Reservations.Domain.Question", "Question")
                        .WithMany("EventQuestions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Reservations.Domain.LoginDetails", b =>
                {
                    b.HasOne("Reservations.Domain.User", "User")
                        .WithOne("LoginDetails")
                        .HasForeignKey("Reservations.Domain.LoginDetails", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Reservations.Domain.Reservation", b =>
                {
                    b.HasOne("Reservations.Domain.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Reservations.Domain.Ticket", b =>
                {
                    b.HasOne("Reservations.Domain.EventOccurrence", "EventOccurence")
                        .WithMany("Tickets")
                        .HasForeignKey("EventOccurenceId");

                    b.HasOne("Reservations.Domain.Reservation", "Reservation")
                        .WithMany("Tickets")
                        .HasForeignKey("ReservationId");

                    b.Navigation("EventOccurence");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("Reservations.Domain.TicketExtension", b =>
                {
                    b.HasOne("Reservations.Domain.Extension", "Extension")
                        .WithMany("TicketExtensions")
                        .HasForeignKey("ExtensionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Reservations.Domain.Ticket", "Ticket")
                        .WithMany("TicketExtensions")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Extension");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("Reservations.Domain.User", b =>
                {
                    b.HasOne("Reservations.Domain.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Reservations.Domain.Event", b =>
                {
                    b.Navigation("EventOccurences");

                    b.Navigation("EventQuestions");
                });

            modelBuilder.Entity("Reservations.Domain.EventOccurrence", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Reservations.Domain.EventType", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("Reservations.Domain.Extension", b =>
                {
                    b.Navigation("TicketExtensions");
                });

            modelBuilder.Entity("Reservations.Domain.Question", b =>
                {
                    b.Navigation("EventQuestions");
                });

            modelBuilder.Entity("Reservations.Domain.Reservation", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Reservations.Domain.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Reservations.Domain.Ticket", b =>
                {
                    b.Navigation("TicketExtensions");
                });

            modelBuilder.Entity("Reservations.Domain.User", b =>
                {
                    b.Navigation("LoginDetails");

                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
