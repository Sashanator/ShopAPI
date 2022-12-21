using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Features.RequestHandling.Base;

namespace ShopAPI.Features.Common;

/// <inheritdoc />
[ApiController]
[ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.BadRequest)]
[ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.InternalServerError)]
public class BaseApiController : ControllerBase { }