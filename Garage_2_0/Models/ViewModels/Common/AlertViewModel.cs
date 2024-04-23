using Garage_2_0.Models.Enums;

namespace Garage_2_0.Models.ViewModels.Common
{
    public class AlertViewModel
    {
        public bool IsActive { get; set; }
        public string? Message { get; set; }
        public AlertType Type { get; set; }
    }
}
