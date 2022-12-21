namespace ShopAPI.Features.RequestHandling.Base
{
    /// <summary>
    ///     Interface for api request
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        ///     Request Id
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        ///     Metadata for request
        /// </summary>
        Dictionary<string, object> Meta { get; set; }

        /// <summary>
        ///     Request datetime
        /// </summary>
        DateTime RequestDate { get; }
    }

    /// <summary>
    ///     Typed request
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRequest<T> : IRequest
    {
        /// <summary>
        ///     Request payload
        /// </summary>
        T Payload { get; set; }
    }
}