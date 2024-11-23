// <auto-generated />
using System;
using HomeAnalytica.DataRegistry.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HomeAnalytica.DataRegistry.Data.Migrations
{
    [DbContext(typeof(DataRegistryDbContext))]
    partial class DataRegistryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HomeAnalytica.DataRegistry.Data.Entities.SensorMetadata", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("DeviceId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("device_id");

                    b.Property<DateTime?>("InstallationDate")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("installation_date");

                    b.Property<DateTime?>("LastMaintenance")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_maintenance");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("location");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.ToTable("SensorMetadata");
                });
#pragma warning restore 612, 618
        }
    }
}
