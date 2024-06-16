using System;
using System.Collections.Generic;

namespace DrawerPos.Shared;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Position { get; set; }

    public int? StoreId { get; set; }

    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();

    public virtual Store? Store { get; set; }
}
