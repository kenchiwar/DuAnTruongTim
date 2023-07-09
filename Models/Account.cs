using System;
using System.Collections.Generic;

namespace DuAnTruongTim.Models;

public partial class Account
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int IdRole { get; set; }

    public int IdDepartment { get; set; }

    public string? Fullname { get; set; }

    public string? Emailaddress { get; set; }

    public string? Phonenumber { get; set; }

    public string? Address { get; set; }

    public string? Citizenidentification { get; set; }

    public DateTime? Dateofbirth { get; set; }

    public bool? Sex { get; set; }

    public bool? Status { get; set; }

    public string? Role { get; set; }

    public string? Class { get; set; }

    public string? Schoolyear { get; set; }

    public string? Degree { get; set; }

    public string? Academicrank { get; set; }

    public virtual Department IdDepartmentNavigation { get; set; } = null!;

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual ICollection<Requet> RequetIdComplainNavigations { get; set; } = new List<Requet>();

    public virtual ICollection<Requet> RequetIdHandleNavigations { get; set; } = new List<Requet>();

    public virtual ICollection<RoleClaim> IdRoleClaims { get; set; } = new List<RoleClaim>();
}
