using System;
using System.Collections.Generic;

namespace DuAnTruongTim.Models;

public partial class Requet
{
    public int Id { get; set; }

    public int IdComplain { get; set; }

    public int IdDepartment { get; set; }

    public int? IdHandle { get; set; }

    public string? Title { get; set; }

    public short? Status { get; set; }

    public short Level { get; set; }

    public DateTime? Sentdate { get; set; }

    public DateTime? Enddate { get; set; }

    public short? Priority { get; set; }

    public virtual Account IdComplainNavigation { get; set; } = null!;

    public virtual Department IdDepartmentNavigation { get; set; } = null!;

    public virtual Account? IdHandleNavigation { get; set; }

    public virtual ICollection<RequestFile> RequestFiles { get; set; } = new List<RequestFile>();

    public virtual ICollection<Requetsdetailed> Requetsdetaileds { get; set; } = new List<Requetsdetailed>();
}
