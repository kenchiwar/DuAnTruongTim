using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DuAnTruongTim.Models;

public partial class RequestFile
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int IdRequest { get; set; }

    [JsonIgnore]
    public virtual Requet IdRequestNavigation { get; set; } = null!;
}
