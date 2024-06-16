using System;
using System.Collections.Generic;

namespace DrawerPos.Shared;

public partial class Shift
{
    public int ShiftId { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? ShiftDate { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public virtual Employee? Employee { get; set; }
}
