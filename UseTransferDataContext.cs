using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace ORAGO_CC_Planning.Models;

public class UseTransferDataContext : DbContext{
    public UseTransferDataContext(DbContextOptions<UseTransferDataContext> options)
        :base(options) 
    {
        Debug.WriteLine($"{ContextId} context created.");
    }
    
    public DbSet<Customer>? Kunden { get; set; }
    public DbSet<Article>? Artikel { get; set; }
    public DbSet<Currency>? Currencies { get; set; }
    public DbSet<ExchangeRate>? ExchangeRates { get; set; }
    public DbSet<Transaction>? Bewegungsdaten { get; set; }
    public DbSet<BudgetEntry>? BudgetEntries { get; set; }
    public DbSet<YearlyLock>? YearlyLocks { get; set; }
    public DbSet<BudgetCurrency>? BudgetCurrencies { get; set; }
    public DbSet<VolumeTemplate>? VolumeTemplates { get; set; }
    public DbSet<EntityLocalCurrency>? EntityLocalCurrencies { get; set; }
    public DbSet<TransactionMonthlyLock>? TransactionMonthlyLocks { get; set; }
    public DbSet<ArticleFinal>? ArtikelFinals { get; set; }
    public DbSet<CustomerFinal>? CustomerFinals { get; set; }
    public DbSet<UploadLogHeader>? UploadLogHeaders { get; set; }
    public DbSet<UploadLogItem>? UploadLogItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Transfer");

        // modelBuilder.Entity<Article>(entity => { 
        //     entity.HasIndex(e => e.Entity).IsUnique();
        // });
        // modelBuilder.Entity<Customer>(entity => { 
        //     entity.HasIndex(e => e.Entity).IsUnique();
        // });
        // modelBuilder.Entity<Transaction>(entity => { 
        //     entity.HasIndex(e => e.Entity).IsUnique();
        // });

          modelBuilder.Entity<Article>().HasIndex(u => new { u.Id, u.Entity }).IsUnique();
          modelBuilder.Entity<Customer>().HasIndex(u => new {u.Id, u.Entity }).IsUnique();
          modelBuilder.Entity<Transaction>().HasIndex(u => new {u.Id, u.Entity }).IsUnique();
          modelBuilder.Entity<EntityLocalCurrency>().HasIndex(u => new {u.Id, u.Entity}).IsUnique();
    }

    /// <summary>
    /// Dispose pattern.
    /// </summary>
    public override void Dispose()
    {
        Debug.WriteLine($"{ContextId} context disposed.");
        base.Dispose();
    }

        /// <summary>
    /// Dispose pattern.
    /// </summary>
    /// <returns>A <see cref="ValueTask"/></returns>
    public override ValueTask DisposeAsync()
    {
        Debug.WriteLine($"{ContextId} context disposed async.");
        return base.DisposeAsync();
    }
}