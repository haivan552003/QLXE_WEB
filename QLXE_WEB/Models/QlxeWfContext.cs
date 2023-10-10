using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLXE_WEB.Models;

public partial class QlxeWfContext : DbContext
{
    public QlxeWfContext()
    {
    }

    public QlxeWfContext(DbContextOptions<QlxeWfContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;port=3306;database=qlxe_wf;uid=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdP).HasName("PRIMARY");

            entity.ToTable("product");

            entity.HasIndex(e => e.IdU, "fk_user_procduct");

            entity.Property(e => e.IdP)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("ID_P");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.IdU)
                .HasColumnType("int(11)")
                .HasColumnName("ID_U");
            entity.Property(e => e.ProcductName)
                .HasMaxLength(50)
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            entity.HasOne(d => d.IdUNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdU)
                .HasConstraintName("fk_user_procduct");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdU).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.IdU)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("ID_U");
            entity.Property(e => e.Account).HasMaxLength(50);
            entity.Property(e => e.Age).HasColumnType("int(11)");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
