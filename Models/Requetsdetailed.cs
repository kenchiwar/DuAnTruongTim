using System;
using System.Collections.Generic;

namespace DuAnTruongTim.Models;

public partial class Requetsdetailed
{
    public int Id { get; set; }

    public DateTime? Sentdate { get; set; }

    public DateTime? Payday { get; set; }

    public string? Reason { get; set; }

    public short? Status { get; set; }

    public string? Reply { get; set; }

    public int IdRequest { get; set; }

    public virtual Requet IdRequestNavigation { get; set; } = null!;
}
