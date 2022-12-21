using ShopAPI.Features.DataAccess.Models;

namespace ShopAPI.Features.Notifications.Model;

public class Notification : BaseEntity
{
    public string Title { get; set; }

    public string Message { get; set; }

    public string TargetEmail { get; set; }
}