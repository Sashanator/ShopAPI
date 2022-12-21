namespace ShopAPI.Features.DataAccess.Models;

public interface IBaseEntity
{
    /// <summary>
    ///     The unique id of this entity
    /// </summary>
    Guid Id { set; get; }

    /// <summary>
    ///     The UTC date and time when this entity was created
    /// </summary>
    DateTime CreatedAt { set; get; }

    /// <summary>
    ///     The UTC date and time when this entity was last modificed
    /// </summary>
    DateTime? LastModifiedAt { set; get; }

    /// <summary>
    ///     Flag is deleted
    /// </summary>
    bool IsDeleted { get; set; }

    /// <summary>
    ///     A concurrency check value
    /// </summary>
    long Version { set; get; }
}