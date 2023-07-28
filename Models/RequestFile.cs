using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DuAnTruongTim.Models;

public partial class RequestFile
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int IdRequest { get; set; }

    [JsonIgnore]
    public virtual Requet IdRequestNavigation { get; set; } = null!;
}
