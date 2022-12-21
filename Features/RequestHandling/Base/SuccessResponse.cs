namespace ShopAPI.Features.RequestHandling.Base
{
    /// <summary>
    ///     Success response
    /// </summary>
    public class SuccessResponse : Response
    {
        public SuccessResponse(Guid id, object payload = null, int statusCode = 200) : base(id)
        {
            StatusCode = statusCode;
            Payload = payload;
            HasError = false;
        }
    }
}