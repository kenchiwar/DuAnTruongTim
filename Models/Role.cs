using System;
using System.Collections.Generic;

namespace DuAnTruongTim.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Describe { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
