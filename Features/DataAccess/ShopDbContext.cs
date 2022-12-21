using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopAPI.Features.DataAccess.Models;
using ShopAPI.Features.Deliveries.Model;
using ShopAPI.Features.Notifications.Model;
using ShopAPI.Features.Orders.Model;
using ShopAPI.Features.Payments.Model;
using ShopAPI.Features.Products.Model;

namespace ShopAPI.Features.DataAccess;

public class ShopDbContext : DbContext
{
    /// <inheritdoc />
    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
    {
    }

    // Add DbSet of entities here

    public DbSet<Delivery> Deliveries { get; set; }

    public DbSet<Notification> Notifications { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Payment> Payments { get; set; }

    public DbSet<Product> Products { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Enum to string converter

        var orderStatusConverter = new ValueConverter<OrderStatus?, string>(
            v => v != null ? v.ToString() : null,
            v => v != null ? (OrderStatus?)Enum.Parse(typeof(OrderStatus), v) : null);

        var deliveryStatusConverter = new ValueConverter<DeliveryStatus?, string>(
            v => v != null ? v.ToString() : null,
            v => v != null ? (DeliveryStatus?)Enum.Parse(typeof(DeliveryStatus), v) : null);

        var paymentStatusConverter = new ValueConverter<PaymentStatus?, string>(
            v => v != null ? v.ToString() : null,
            v => v != null ? (PaymentStatus?)Enum.Parse(typeof(PaymentStatus), v) : null);

        modelBuilder.Entity<Order>()
            .Property(j => j.Status)
            .HasConversion(orderStatusConverter);

        modelBuilder.Entity<Delivery>()
            .Property(j => j.Status)
            .HasConversion(deliveryStatusConverter);

        modelBuilder.Entity<Payment>()
            .Property(j => j.Status)
            .HasConversion(paymentStatusConverter);


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

        modelBuilder.HasPostgresExtension("hstore");

        // TODO: Seed some data to DB

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