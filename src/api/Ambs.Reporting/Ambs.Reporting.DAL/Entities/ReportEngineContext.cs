﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ambs.Reporting.DAL.Entities
{
    public partial class ReportEngineContext : DbContext
    {
        public ReportEngineContext()
        {
        }

        public ReportEngineContext(DbContextOptions<ReportEngineContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dashboard> Dashboards { get; set; } = null!;
        public virtual DbSet<Filter> Filters { get; set; } = null!;
        public virtual DbSet<GraphType> GraphTypes { get; set; } = null!;
        public virtual DbSet<GraphicalFeature> GraphicalFeatures { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<ReportFilter> ReportFilters { get; set; } = null!;
        public virtual DbSet<TabularFeature> TabularFeatures { get; set; } = null!;
        public virtual DbSet<Widget> Widgets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ReportDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dashboard>(entity =>
            {
                entity.ToTable("Dashboard", "config");

                entity.Property(e => e.CreatedBy).HasMaxLength(20);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UpdatedBy).HasMaxLength(20);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Filter>(entity =>
            {
                entity.ToTable("Filter", "config");

                entity.Property(e => e.CreatedBy).HasMaxLength(20);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DependentParameters).HasMaxLength(500);

                entity.Property(e => e.Label).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Parameter).HasMaxLength(50);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedBy).HasMaxLength(20);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<GraphType>(entity =>
            {
                entity.ToTable("GraphType", "config");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<GraphicalFeature>(entity =>
            {
                entity.ToTable("GraphicalFeature", "config");

                entity.Property(e => e.CreatedBy).HasMaxLength(20);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ShowFilterInfo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ShowLegend)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SubTitle).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.UpdatedBy).HasMaxLength(20);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.XaxisTitle)
                    .HasMaxLength(100)
                    .HasColumnName("XAxisTitle");

                entity.Property(e => e.YaxisTitle)
                    .HasMaxLength(100)
                    .HasColumnName("YAxisTitle");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.GraphicalFeatures)
                    .HasForeignKey(d => d.ReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Graphical__Repor__36B12243");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report", "config");

                entity.Property(e => e.CreatedBy).HasMaxLength(20);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedBy).HasMaxLength(20);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Dashboard)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.DashboardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__Dashboar__2A4B4B5E");
            });

            modelBuilder.Entity<ReportFilter>(entity =>
            {
                entity.ToTable("ReportFilter", "config");

                entity.HasOne(d => d.Filter)
                    .WithMany(p => p.ReportFilters)
                    .HasForeignKey(d => d.FilterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ReportFil__Filte__3D5E1FD2");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportFilters)
                    .HasForeignKey(d => d.ReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ReportFil__Repor__3C69FB99");
            });

            modelBuilder.Entity<TabularFeature>(entity =>
            {
                entity.ToTable("TabularFeature", "config");

                entity.Property(e => e.CreatedBy).HasMaxLength(20);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Exportable)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ShowFilterInfo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SubTitle).HasMaxLength(100);

                entity.Property(e => e.Template).HasMaxLength(200);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.UpdatedBy).HasMaxLength(20);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.TabularFeatures)
                    .HasForeignKey(d => d.ReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TabularFe__Repor__31EC6D26");
            });

            modelBuilder.Entity<Widget>(entity =>
            {
                entity.ToTable("Widget", "config");

                entity.Property(e => e.CreatedBy).HasMaxLength(20);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UpdatedBy).HasMaxLength(20);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Dashboard)
                    .WithMany(p => p.Widgets)
                    .HasForeignKey(d => d.DashboardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Widget__Dashboar__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
