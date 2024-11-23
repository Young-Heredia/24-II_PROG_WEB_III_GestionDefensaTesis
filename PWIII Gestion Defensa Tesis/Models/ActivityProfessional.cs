using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PWIII_Gestion_Defensa_Tesis.Models;

public partial class ActivityProfessional
{
    [Key]
    public int IdActivityProfessional { get; set; }

    public int IdActivity { get; set; }

    public short IdProfessional { get; set; }

    public virtual DefenseActivity IdActivityNavigation { get; set; } = null!;

    public virtual Professional IdProfessionalNavigation { get; set; } = null!;
}
