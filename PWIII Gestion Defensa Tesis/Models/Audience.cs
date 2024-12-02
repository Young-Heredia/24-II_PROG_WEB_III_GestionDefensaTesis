using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PWIII_Gestion_Defensa_Tesis.Models;

public partial class Audience
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Audience is required")]
    [Display(Name = "NAME AUDIENCE")]
    [StringLength(40, MinimumLength = 4, ErrorMessage = "The Name must be between 4 and 40 characters.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Only letters, including accented characters and single spaces between words are allowed.")]
    public string Name { get; set; } = null!;

    [Display(Name = "DIRECTION")]
    [Required(ErrorMessage = "Direction is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "The Direction must be between 6 and 100 characters.")]
    [RegularExpression(@"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s,.\-#()]+$", ErrorMessage = "Invalid characters in the address.")]
    public string Direction { get; set; }

    [Required(ErrorMessage = "Latitude is required")]
    [Display(Name = "LATITUDE")]
    public string Latitude { get; set; }

    [Required(ErrorMessage = "Longitude is required")]
    [Display(Name = "LONGITUDE")]
    public string Longitude { get; set; }

    [Display(Name = "AUDITORIUM IMAGE")]
    public string? ImagePath { get; set; }
    public byte Status { get; set; } = 1;
    public DateTime registerDate { get; set; } = DateTime.Now;

    public virtual ICollection<DefenseActivity> DefenseActivities { get; set; } = new List<DefenseActivity>();
}
