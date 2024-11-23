using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PWIII_Gestion_Defensa_Tesis.Models;

public partial class Professional
{
    [Key]
    public short Id { get; set; }

    [Required(ErrorMessage = "Name Professional is required")]
    [Display(Name = "NAME")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "LastName Professional is required")]
    [Display(Name = "LASTNAME")]
    public string LastName { get; set; } = null!;

    [Display(Name = "SECOND LASTNAME")]
    public string? SecondLastName { get; set; }

    [Required(ErrorMessage = "Career Professional is required")]
    [Display(Name = "CAREER")]
    public string Career { get; set; } = null!;

    public byte Status { get; set; }

    public virtual ICollection<ActivityProfessional> ActivityProfessionals { get; set; } = new List<ActivityProfessional>();
}
