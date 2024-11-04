// <auto-generated />
using HomeAnalytica.DataCollection.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HomeAnalytica.DataCollection.Data.Migrations
{
    [DbContext(typeof(HomeAnalyticaDbContext))]
    partial class HomeAnalyticaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HomeAnalytica.DataCollection.Data.Entities.SensorData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<double>("EnergyConsumption")
                        .HasColumnType("double precision")
                        .HasColumnName("energy_consumption");

                    b.Property<double>("Humidity")
                        .HasColumnType("double precision")
                        .HasColumnName("humidity");

                    b.Property<double>("Temperature")
                        .HasColumnType("double precision")
                        .HasColumnName("temperature");

                    b.HasKey("Id");

                    b.ToTable("SensorData");
                });
#pragma warning restore 612, 618
        }
    }
}