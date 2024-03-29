﻿// <auto-generated />
using System;
using Balea.EntityFrameworkCore.Store.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Assets.Management.Migrations
{
    [DbContext(typeof(BaleaDbContext))]
    [Migration("20220609152202_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.ApplicationEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.DelegationEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("From")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Selected")
                        .HasColumnType("bit");

                    b.Property<DateTime>("To")
                        .HasColumnType("datetime2");

                    b.Property<int>("WhoId")
                        .HasColumnType("int");

                    b.Property<int>("WhomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("WhoId");

                    b.HasIndex("WhomId");

                    b.ToTable("Delegations");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.MappingEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Mappings");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.PermissionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("Name", "ApplicationId")
                        .IsUnique();

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.PermissionTagEntity", b =>
                {
                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("PermissionId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("PermissionTags");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.PolicyEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("Name", "ApplicationId")
                        .IsUnique();

                    b.ToTable("Policies");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("Name", "ApplicationId")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.RoleMappingEntity", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("MappingId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "MappingId");

                    b.HasIndex("MappingId");

                    b.ToTable("RoleMappings");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.RolePermissionEntity", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.RoleSubjectEntity", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("RoleSubjects");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.RoleTagEntity", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("RoleTags");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.SubjectEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Sub")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Sub")
                        .IsUnique();

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.TagEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.DelegationEntity", b =>
                {
                    b.HasOne("Balea.EntityFrameworkCore.Store.Entities.ApplicationEntity", "Application")
                        .WithMany("Delegations")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Balea.EntityFrameworkCore.Store.Entities.SubjectEntity", "Who")
                        .WithMany("WhoDelegations")
                        .HasForeignKey("WhoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Balea.EntityFrameworkCore.Store.Entities.SubjectEntity", "Whom")
                        .WithMany("WhomDelegations")
                        .HasForeignKey("WhomId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Application");

                    b.Navigation("Who");

                    b.Navigation("Whom");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.PermissionEntity", b =>
                {
                    b.HasOne("Balea.EntityFrameworkCore.Store.Entities.ApplicationEntity", "Application")
                        .WithMany("Permissions")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.PermissionTagEntity", b =>
                {
                    b.HasOne("Balea.EntityFrameworkCore.Store.Entities.PermissionEntity", "Permission")
                        .WithMany("Tags")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Balea.EntityFrameworkCore.Store.Entities.TagEntity", "Tag")
                        .WithMany("Permissions")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.PolicyEntity", b =>
                {
                    b.HasOne("Balea.EntityFrameworkCore.Store.Entities.ApplicationEntity", "Application")
                        .WithMany("Policies")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Application");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.RoleEntity", b =>
                {
                    b.HasOne("Balea.EntityFrameworkCore.Store.Entities.ApplicationEntity", "Application")
                        .WithMany("Roles")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.RoleMappingEntity", b =>
                {
                    b.HasOne("Balea.EntityFrameworkCore.Store.Entities.MappingEntity", "Mapping")
                        .WithMany("Roles")
                        .HasForeignKey("MappingId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Balea.EntityFrameworkCore.Store.Entities.RoleEntity", "Role")
                        .WithMany("Mappings")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Mapping");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.RolePermissionEntity", b =>
                {
                    b.HasOne("Balea.EntityFrameworkCore.Store.Entities.PermissionEntity", "Permission")
                        .WithMany("Roles")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Balea.EntityFrameworkCore.Store.Entities.RoleEntity", "Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.RoleSubjectEntity", b =>
                {
                    b.HasOne("Balea.EntityFrameworkCore.Store.Entities.RoleEntity", "Role")
                        .WithMany("Subjects")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Balea.EntityFrameworkCore.Store.Entities.SubjectEntity", "Subject")
                        .WithMany("Roles")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.RoleTagEntity", b =>
                {
                    b.HasOne("Balea.EntityFrameworkCore.Store.Entities.RoleEntity", "Role")
                        .WithMany("Tags")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Balea.EntityFrameworkCore.Store.Entities.TagEntity", "Tag")
                        .WithMany("Roles")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.ApplicationEntity", b =>
                {
                    b.Navigation("Delegations");

                    b.Navigation("Permissions");

                    b.Navigation("Policies");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.MappingEntity", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.PermissionEntity", b =>
                {
                    b.Navigation("Roles");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.RoleEntity", b =>
                {
                    b.Navigation("Mappings");

                    b.Navigation("Permissions");

                    b.Navigation("Subjects");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.SubjectEntity", b =>
                {
                    b.Navigation("Roles");

                    b.Navigation("WhoDelegations");

                    b.Navigation("WhomDelegations");
                });

            modelBuilder.Entity("Balea.EntityFrameworkCore.Store.Entities.TagEntity", b =>
                {
                    b.Navigation("Permissions");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
