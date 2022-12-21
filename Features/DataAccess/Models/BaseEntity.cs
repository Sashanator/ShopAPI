using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopAPI.Features.DataAccess.Models;

/// <inheritdoc />
[Serializable]
public class BaseEntity : IBaseEntity
{
    /// <inheritdoc />
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <inheritdoc />
    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <inheritdoc />
    [Column(TypeName = "timestamp without time zone")]
    public DateTime? LastModifiedAt { get; set; }

    /// <inheritdoc />
    public bool IsDeleted { get; set; } = false;

    /// <inheritdoc />
    [ConcurrencyCheck]
    public long Version { get; set; } = 0;
}