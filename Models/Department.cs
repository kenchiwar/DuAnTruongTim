﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DuAnTruongTim.Models;

public partial class Department
{
    public int Id { get; set; }

    public string? TenDepartment { get; set; }

    public string? Describe { get; set; }

    public string? Address { get; set; }

    public bool? Status { get; set; }

    [JsonIgnore]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
    [JsonIgnore]
    public virtual ICollection<Requet> Requets { get; set; } = new List<Requet>();
}
