﻿// <auto-generated />
using System;
using AsturianuTV.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AsturianuTV.Migrations
{
    [DbContext(typeof(AsturianuTVDbContext))]
    [Migration("20231102151630_AddColumnDateToTransfers")]
    partial class AddColumnDateToTransfers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.BaseKnowledges.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Armor")
                        .HasColumnType("int");

                    b.Property<int>("Attribute")
                        .HasColumnType("int");

                    b.Property<int>("Damage")
                        .HasColumnType("int");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.Property<int>("HealthRegeneration")
                        .HasColumnType("int");

                    b.Property<string>("History")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MagicResist")
                        .HasColumnType("int");

                    b.Property<int>("Mana")
                        .HasColumnType("int");

                    b.Property<int>("ManaRegeneration")
                        .HasColumnType("int");

                    b.Property<int>("MoveSpeed")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RangeType")
                        .HasColumnType("int");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.BaseKnowledges.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CoolDown")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ItemCategory")
                        .HasColumnType("int");

                    b.Property<int>("ItemType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.BaseKnowledges.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int?>("CoolDown")
                        .HasColumnType("int");

                    b.Property<int?>("Damage")
                        .HasColumnType("int");

                    b.Property<int?>("DamageType")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ManaCost")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SkillType")
                        .HasColumnType("int");

                    b.Property<int?>("TimeDuration")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.ContentNews.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ChildId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LastModifiedBy")
                        .HasColumnType("int");

                    b.Property<int?>("NewsId")
                        .HasColumnType("int");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NewsId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.ContentNews.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsReady")
                        .HasColumnType("bit");

                    b.Property<int?>("LeagueId")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TeamId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.ContentNews.NewsMaterial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MaterialId")
                        .HasColumnType("int");

                    b.Property<int?>("NewsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("NewsId");

                    b.ToTable("NewsMaterial");
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.ContentNews.NewsTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("NewsId")
                        .HasColumnType("int");

                    b.Property<int?>("TagId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NewsId");

                    b.HasIndex("TagId");

                    b.ToTable("NewsTags");
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.Cybersports.League", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PriceFound")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.Cybersports.LeagueTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LeagueId")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.HasIndex("TeamId");

                    b.ToTable("LeagueTeams");
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.Cybersports.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Bday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullPrice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Matches")
                        .HasColumnType("int");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.Cybersports.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FullPrice")
                        .HasColumnType("int");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Matches")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.Cybersports.Transfer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("NewTeamId")
                        .HasColumnType("int");

                    b.Property<int?>("OldTeamId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TransferDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("NewTeamId");

                    b.HasIndex("OldTeamId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Transfers");
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsNewsMaterial")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.Subscriptions.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlanId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.Subscriptions.BlogMaterial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BlogId")
                        .HasColumnType("int");

                    b.Property<int?>("MaterialId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.HasIndex("MaterialId");

                    b.ToTable("BlogMaterial");
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.Subscriptions.Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SubscriptionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.Subscriptions.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsReady")
                        .HasColumnType("bit");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Administrator"
                        },
                        new
                        {
                            Id = 2,
                            Name = "DefaultUser"
                        });
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<int?>("SubscriptionId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@gmail.com",
                            Password = "Asturianu#2001",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.BaseKnowledges.Skill", b =>
                {
                    b.HasOne("AsturianuTV.Infrastructure.Data.Models.BaseKnowledges.Character", "Character")
                        .WithMany("Skills")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.ContentNews.Comment", b =>
                {
                    b.HasOne("AsturianuTV.Infrastructure.Data.Models.ContentNews.News", "News")
                        .WithMany("Comments")
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.ContentNews.News", b =>
                {
                    b.HasOne("AsturianuTV.Infrastructure.Data.Models.Cybersports.League", "League")
                        .WithMany("News")
                        .HasForeignKey("LeagueId");

                    b.HasOne("AsturianuTV.Infrastructure.Data.Models.Cybersports.Player", "Player")
                        .WithMany("News")
                        .HasForeignKey("PlayerId");

                    b.HasOne("AsturianuTV.Infrastructure.Data.Models.Cybersports.Team", "Team")
                        .WithMany("News")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.ContentNews.NewsMaterial", b =>
                {
                    b.HasOne("AsturianuTV.Infrastructure.Data.Models.Material", "Material")
                        .WithMany("NewsMaterials")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AsturianuTV.Infrastructure.Data.Models.ContentNews.News", "News")
                        .WithMany("NewsMaterials")
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.ContentNews.NewsTag", b =>
                {
                    b.HasOne("AsturianuTV.Infrastructure.Data.Models.ContentNews.News", "News")
                        .WithMany("NewsTags")
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AsturianuTV.Infrastructure.Data.Models.Tag", "Tag")
                        .WithMany("NewsTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.Cybersports.LeagueTeam", b =>
                {
                    b.HasOne("AsturianuTV.Infrastructure.Data.Models.Cybersports.League", "League")
                        .WithMany("LeagueTeams")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AsturianuTV.Infrastructure.Data.Models.Cybersports.Team", "Team")
                        .WithMany("LeagueTeams")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.Cybersports.Player", b =>
                {
                    b.HasOne("AsturianuTV.Infrastructure.Data.Models.Cybersports.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.Cybersports.Transfer", b =>
                {
                    b.HasOne("AsturianuTV.Infrastructure.Data.Models.Cybersports.Team", "NewTeam")
                        .WithMany()
                        .HasForeignKey("NewTeamId");

                    b.HasOne("AsturianuTV.Infrastructure.Data.Models.Cybersports.Team", "OldTeam")
                        .WithMany()
                        .HasForeignKey("OldTeamId");

                    b.HasOne("AsturianuTV.Infrastructure.Data.Models.Cybersports.Player", "Player")
                        .WithMany("Transfers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.Subscriptions.Blog", b =>
                {
                    b.HasOne("AsturianuTV.Infrastructure.Data.Models.Subscriptions.Plan", "Plan")
                        .WithMany("Blogs")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.Subscriptions.BlogMaterial", b =>
                {
                    b.HasOne("AsturianuTV.Infrastructure.Data.Models.Subscriptions.Blog", "Blog")
                        .WithMany("BlogMaterials")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AsturianuTV.Infrastructure.Data.Models.Material", "Material")
                        .WithMany("BlogMaterials")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.Models.Subscriptions.Plan", b =>
                {
                    b.HasOne("AsturianuTV.Infrastructure.Data.Models.Subscriptions.Subscription", "Subscription")
                        .WithMany("Plans")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("AsturianuTV.Infrastructure.Data.User", b =>
                {
                    b.HasOne("AsturianuTV.Infrastructure.Data.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.HasOne("AsturianuTV.Infrastructure.Data.Models.Subscriptions.Subscription", "Subscription")
                        .WithMany("Users")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
