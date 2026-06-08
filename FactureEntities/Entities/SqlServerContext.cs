using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FactureEntities.Entities;

public partial class SqlServerContext : DbContext
{
    public SqlServerContext(DbContextOptions<SqlServerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Facture> Factures { get; set; }

    public virtual DbSet<LigneFacture> LigneFactures { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<VFacture> VFactures { get; set; }

    public virtual DbSet<Vendeur> Vendeurs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Article__3214EC070916CBAF");

            entity.ToTable("Article");

            entity.Property(e => e.Actif).HasDefaultValue(true);
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasDefaultValue("-");
            entity.Property(e => e.Nom)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("-");
            entity.Property(e => e.Prix)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(6, 2)");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Client__3214EC07DB13E63C");

            entity.ToTable("Client");

            entity.HasIndex(e => e.Id, "UQ__Client__3214EC061C32BF4C").IsUnique();

            entity.Property(e => e.Adresse).IsRequired();
            entity.Property(e => e.Localite).IsRequired();
            entity.Property(e => e.Nom)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("...");
            entity.Property(e => e.Npa).IsRequired();
            entity.Property(e => e.Prenom).IsRequired();
        });

        modelBuilder.Entity<Facture>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Facture__3214EC07BA676094");

            entity.ToTable("Facture");

            entity.HasIndex(e => e.Id, "UQ__Facture__3214EC06DEA08855").IsUnique();

            entity.Property(e => e.Numero).IsRequired();

            entity.HasOne(d => d.Client).WithMany(p => p.Factures)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Facture_fk3");

            entity.HasOne(d => d.Vendeur).WithMany(p => p.Factures)
                .HasForeignKey(d => d.VendeurId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Facture_fk4");
        });

        modelBuilder.Entity<LigneFacture>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LigneFac__3214EC07765305C4");

            entity.ToTable("LigneFacture");

            entity.HasIndex(e => e.Id, "UQ__LigneFac__3214EC06B7810FFD").IsUnique();

            entity.Property(e => e.PrixUnitaire).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Article).WithMany(p => p.LigneFactures)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LigneFacture_fk2");

            entity.HasOne(d => d.Facture).WithMany(p => p.LigneFactures)
                .HasForeignKey(d => d.FactureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LigneFacture_fk1");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Log__3214EC079D19DDED");

            entity.ToTable("Log");

            entity.Property(e => e.Datelog)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("datelog");
            entity.Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.Message)
                .IsRequired()
                .HasMaxLength(2000)
                .HasColumnName("message");
            entity.Property(e => e.Typelog)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("typelog");
        });

        modelBuilder.Entity<VFacture>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_Facture");

            entity.Property(e => e.Nom)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Prenom).IsRequired();
            entity.Property(e => e.PrixUnitaire).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Vendeur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vendeur__3214EC07E234E11E");

            entity.ToTable("Vendeur");

            entity.HasIndex(e => e.Id, "UQ__Vendeur__3214EC06C5E83CBE").IsUnique();

            entity.Property(e => e.Identifiant).IsRequired();
            entity.Property(e => e.MotDePasse).IsRequired();
            entity.Property(e => e.Nom).IsRequired();
            entity.Property(e => e.Prenom).IsRequired();
            entity.Property(e => e.Role)
                .IsRequired()
                .HasDefaultValue("user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
