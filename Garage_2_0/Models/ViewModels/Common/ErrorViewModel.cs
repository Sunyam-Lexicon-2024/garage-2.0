﻿namespace Garage_2_0.Models.ViewModels.Common;


public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
