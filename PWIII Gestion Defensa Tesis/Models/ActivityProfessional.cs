using System;
using System.Collections.Generic;

namespace PWIII_Gestion_Defensa_Tesis.Models;

public partial class ActivityProfessional
{
    public int IdActivity { get; set; }

    public short IdProfessional { get; set; }

    public virtual Activity IdActivityNavigation { get; set; } = null!;

    public virtual Professional IdProfessionalNavigation { get; set; } = null!;
}
