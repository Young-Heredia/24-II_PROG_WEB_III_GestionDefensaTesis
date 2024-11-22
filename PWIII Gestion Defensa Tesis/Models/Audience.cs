using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PWIII_Gestion_Defensa_Tesis.Models;

public partial class Audience
{
    [Key]
    public byte Id { get; set; }

    [Required(ErrorMessage = "Latitude is required")]
    [Display(Name = "LATITUDE")]
    public double Latitude { get; set; }

    [Required(ErrorMessage = "Longitude is required")]
    [Display(Name = "LONGITUDE")]
    public double Longitude { get; set; }

    [Required(ErrorMessage = "Audience is required")]
    [Display(Name = "AUDIENCE")]
    public string Name { get; set; } = null!;

    public byte Status { get; set; }

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();
}
