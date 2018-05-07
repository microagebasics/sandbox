﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using ShaunApi.Data;
using System;

namespace ShaunApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("ShaunApi.Data.Attendee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmailAddress")
                        .HasMaxLength(256);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("ID");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Attendees");
                });

            modelBuilder.Entity("ShaunApi.Data.Conference", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("ID");

                    b.ToTable("Conferences");
                });

            modelBuilder.Entity("ShaunApi.Data.ConferenceAttendee", b =>
                {
                    b.Property<int>("ConferenceID");

                    b.Property<int>("AttendeeID");

                    b.HasKey("ConferenceID", "AttendeeID");

                    b.HasIndex("AttendeeID");

                    b.ToTable("ConferenceAttendee");
                });

            modelBuilder.Entity("ShaunApi.Data.Session", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abstract")
                        .HasMaxLength(4000);

                    b.Property<int?>("AttendeeID");

                    b.Property<int>("ConferenceID");

                    b.Property<DateTimeOffset?>("EndTime");

                    b.Property<DateTimeOffset?>("StartTime");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int?>("TrackId");

                    b.HasKey("ID");

                    b.HasIndex("AttendeeID");

                    b.HasIndex("ConferenceID");

                    b.HasIndex("TrackId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("ShaunApi.Data.SessionSpeaker", b =>
                {
                    b.Property<int>("SessionId");

                    b.Property<int>("SpeakerId");

                    b.HasKey("SessionId", "SpeakerId");

                    b.HasIndex("SpeakerId");

                    b.ToTable("SessionSpeaker");
                });

            modelBuilder.Entity("ShaunApi.Data.SessionTag", b =>
                {
                    b.Property<int>("SessionID");

                    b.Property<int>("TagID");

                    b.HasKey("SessionID", "TagID");

                    b.HasIndex("TagID");

                    b.ToTable("SessionTag");
                });

            modelBuilder.Entity("ShaunApi.Data.Speaker", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bio")
                        .HasMaxLength(4000);

                    b.Property<int?>("ConferenceID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Website")
                        .HasMaxLength(1000);

                    b.HasKey("ID");

                    b.HasIndex("ConferenceID");

                    b.ToTable("Speakers");
                });

            modelBuilder.Entity("ShaunApi.Data.Tag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("ID");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("ShaunApi.Data.Track", b =>
                {
                    b.Property<int>("TrackID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ConferenceID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("TrackID");

                    b.HasIndex("ConferenceID");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("ShaunApi.Data.ConferenceAttendee", b =>
                {
                    b.HasOne("ShaunApi.Data.Attendee", "Attendee")
                        .WithMany("ConferenceAttendees")
                        .HasForeignKey("AttendeeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ShaunApi.Data.Conference", "Conference")
                        .WithMany("ConferenceAttendees")
                        .HasForeignKey("ConferenceID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ShaunApi.Data.Session", b =>
                {
                    b.HasOne("ShaunApi.Data.Attendee")
                        .WithMany("Sessions")
                        .HasForeignKey("AttendeeID");

                    b.HasOne("ShaunApi.Data.Conference", "Conference")
                        .WithMany("Sessions")
                        .HasForeignKey("ConferenceID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ShaunApi.Data.Track", "Track")
                        .WithMany("Sessions")
                        .HasForeignKey("TrackId");
                });

            modelBuilder.Entity("ShaunApi.Data.SessionSpeaker", b =>
                {
                    b.HasOne("ShaunApi.Data.Session", "Session")
                        .WithMany("SessionSpeakers")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ShaunApi.Data.Speaker", "Speaker")
                        .WithMany("SessionSpeakers")
                        .HasForeignKey("SpeakerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ShaunApi.Data.SessionTag", b =>
                {
                    b.HasOne("ShaunApi.Data.Session", "Session")
                        .WithMany("SessionTags")
                        .HasForeignKey("SessionID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ShaunApi.Data.Tag", "Tag")
                        .WithMany("SessionTags")
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ShaunApi.Data.Speaker", b =>
                {
                    b.HasOne("ShaunApi.Data.Conference")
                        .WithMany("Speakers")
                        .HasForeignKey("ConferenceID");
                });

            modelBuilder.Entity("ShaunApi.Data.Track", b =>
                {
                    b.HasOne("ShaunApi.Data.Conference", "Conference")
                        .WithMany("Tracks")
                        .HasForeignKey("ConferenceID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
