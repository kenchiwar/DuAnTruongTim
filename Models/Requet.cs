using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

    [JsonIgnore]
    public virtual Account IdComplainNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Department IdDepartmentNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Account? IdHandleNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<RequestFile> RequestFiles { get; set; } = new List<RequestFile>();

    [JsonIgnore]
    public virtual ICollection<Requetsdetailed> Requetsdetaileds { get; set; } = new List<Requetsdetailed>();
}
