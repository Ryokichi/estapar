using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace estapar_web_api;

public partial class EstaparDbContext : DbContext
{
    public DbSet<FormaPagamento> FormaPagamento { get; set; }
    public DbSet<Garagem> Garagem { get; set; }
    public DbSet<Passagem> Passagem { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FormaPagamento>().HasKey(fp => fp.Codigo);
    }

    public EstaparDbContext(DbContextOptions<EstaparDbContext> options) : base(options)
    {
    }

    // public EstaparDbContext(DbContextOptions<EstaparDbContext> options)
    //     : base(options)
    // {
    // }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseSqlServer("Name=ConnectionStrings:ConnectToEstaparDB");

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
