using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PWIII_Gestion_Defensa_Tesis.Models;

public partial class Thesis
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name thesis is required")]
    [Display(Name = "NAME")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Description is required")]
    [Display(Name = "DESCRIPTION")]
    public string Description { get; set; } = null!;

    public byte IdTypeThesis { get; set; }

    public byte Status { get; set; }

    public virtual ICollection<DefenseActivity> DefenseActivities { get; set; } = new List<DefenseActivity>();

    public virtual TypeThesis IdTypeThesisNavigation { get; set; } = null!;
}
