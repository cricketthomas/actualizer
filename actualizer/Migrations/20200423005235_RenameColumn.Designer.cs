﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using actualizer.Infastructure.Data.Actualizer.db;

namespace actualizer.Migrations
{
    [DbContext(typeof(ActualizerContext))]
    [Migration("20200423005235_RenameColumn")]
    partial class RenameColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("actualizer.Models.ActualizerModels+SearchResultsMetadata", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("keywordsExtracted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("sentimentAPIRequests")
                        .HasColumnType("INTEGER");

                    b.Property<int>("totalCommentsSearched")
                        .HasColumnType("INTEGER");

                    b.Property<int>("totalSearches")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("SearchResultsMetadata");
                });
#pragma warning restore 612, 618
        }
    }
}
