﻿// <auto-generated />
using System;
using Application.Entiries.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Application.Migrations
{
    [DbContext(typeof(AnalysisDbContext))]
    partial class AnalysisDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Application.Entiries.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Aggression")
                        .HasColumnType("float");

                    b.Property<double>("Arousal")
                        .HasColumnType("float");

                    b.Property<double>("Atmosphere")
                        .HasColumnType("float");

                    b.Property<string>("CallDestination")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("CallDuration")
                        .HasColumnType("float");

                    b.Property<string>("CallSource")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ClStress")
                        .HasColumnType("float");

                    b.Property<double>("Concentration")
                        .HasColumnType("float");

                    b.Property<DateTime>("Creation")
                        .HasColumnType("datetime2");

                    b.Property<double>("Discomfort")
                        .HasColumnType("float");

                    b.Property<double>("DiscomfortEnd")
                        .HasColumnType("float");

                    b.Property<double>("DiscomfortStart")
                        .HasColumnType("float");

                    b.Property<double>("Excitement")
                        .HasColumnType("float");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Finish")
                        .HasColumnType("datetime2");

                    b.Property<double>("Hesitation")
                        .HasColumnType("float");

                    b.Property<double>("Imagination")
                        .HasColumnType("float");

                    b.Property<double>("Joy")
                        .HasColumnType("float");

                    b.Property<double>("MentalEffort")
                        .HasColumnType("float");

                    b.Property<string>("NewTagsString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Sad")
                        .HasColumnType("float");

                    b.Property<DateTime?>("StartCall")
                        .HasColumnType("datetime2");

                    b.Property<double>("Stress")
                        .HasColumnType("float");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Uncertainty")
                        .HasColumnType("float");

                    b.Property<double>("Uneasy")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Application.Entiries.Segment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AnalyzeWholeJSONObj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Creation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeadersJSONObj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeadersPositionsJSONObj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SegmentsJSONObj")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Segments");
                });
#pragma warning restore 612, 618
        }
    }
}
