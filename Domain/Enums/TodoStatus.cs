using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Enums
{
    public enum TodoStatus
    {
        [Display(Name = "Pending")]
        Pending = 0,
        [Display(Name = "In-Progress")]
        InProgress = 1,
        [Display(Name = "Done")]
        Completed = 2,
        [Display(Name = "Inactive")]
        Inactive = 3
    }
}
