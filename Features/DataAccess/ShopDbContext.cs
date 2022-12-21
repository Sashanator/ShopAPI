using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopAPI.Features.DataAccess.Models;

namespace ShopAPI.Features.DataAccess;

public class ShopDbContext : DbContext
{
    /// <inheritdoc />
    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
    {
    }

    // Add DbSet of entities here

    /// <summary>
    ///     Test entities for context
    /// </summary>
    public DbSet<TestEntity> TestEntities { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Examples

        //Enum to string converter

        //var converter = new ValueConverter<EnyEnum?, string>(
        //    v => v != null ? v.ToString() : null,
        //    v => v != null ? (EnyEnum?)Enum.Parse(typeof(EnyEnum), v) : null);

        //modelBuilder.Entity<AnyEntity>()
        //    .Property(j => j.AnyEnum)
        //    .HasConversion(converter); 


        //One to Many relations

        //modelBuilder.Entity<AnyEntity>()
        //    .HasMany(o => o.AnyOneToManyEntity)
        //    .WithOne(h => h.AnyEntity)
        //    .HasForeignKey(h => h.AnyForeignKey)
        //    .OnDelete(DeleteBehavior.Cascade);


        //Add indexes
        //modelBuilder.Entity<AnyEntity>()
        //    .HasIndex(i => i.AnyProperty)
        //    .HasName("IX_EntityName_PropertyName");

        #endregion

        modelBuilder.HasPostgresExtension("hstore");

        // Seed data

        //modelBuilder.Entity<TestEntity>().HasData(new List<TestEntity>
        //{
        //    new()
        //    {
        //        Id = new Guid("dc561644-df90-446f-88c4-12facf27d626"),
        //        Description = "Test Entity for Admin"
        //    }
        //});
    }


    /// <inheritdoc />
    public override int SaveChanges()
    {
        foreach (var entity in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
            if (entity.Entity is BaseEntity baseEntity)
                baseEntity.Version++;

        return base.SaveChanges();
    }

    /// <inheritdoc />
    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        foreach (var entity in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
            if (entity.Entity is BaseEntity baseEntity)
                baseEntity.Version++;

        return await base.SaveChangesAsync(cancellationToken);
    }
}