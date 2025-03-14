using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SchoolProject.Models;

public partial class SchoolContext : DbContext
{
    public SchoolContext()
    {
    }

    public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=Database\\school.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("department");

            entity.Property(e => e.Id)
                .HasColumnType("INT")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("student");

            entity.Property(e => e.Age).HasColumnType("INT");
            entity.Property(e => e.DepartmentId)
                .HasColumnType("INT")
                .HasColumnName("DepartmentID");
            entity.Property(e => e.Enrolled)
                .HasColumnType("INT")
                .HasColumnName("Enrolled ");
            entity.Property(e => e.Id)
                .HasColumnType("INT")
                .HasColumnName("ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
