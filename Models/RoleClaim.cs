using System;
using System.Collections.Generic;

namespace DuAnTruongTim.Models;

public partial class RoleClaim
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Describe { get; set; }

    public int? Claim { get; set; }

    public virtual ICollection<Account> IdAccounts { get; set; } = new List<Account>();
}
