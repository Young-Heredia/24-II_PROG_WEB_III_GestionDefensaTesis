using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PWIII_Gestion_Defensa_Tesis.Models;

public partial class Professional
{
    [Key]
    public short Id { get; set; }

    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed without spaces or special characters.")]
    [Required(ErrorMessage = "Name Professional is required")]
    [Display(Name = "NAME")]
    public string Name { get; set; } = null!;

    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed without spaces or special characters.")]
    [Required(ErrorMessage = "LastName Professional is required")]
    [Display(Name = "LASTNAME")]
    public string LastName { get; set; } = null!;

    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed without spaces or special characters.")]
    [Display(Name = "SECOND LASTNAME")]
    public string? SecondLastName { get; set; }

    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed without spaces or special characters.")]
    [Required(ErrorMessage = "Career Professional is required")]
    [Display(Name = "CAREER")]
    public string Career { get; set; } = null!;

    [Required(ErrorMessage = "CI is required")]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Only letters and numbers are allowed without spaces or special characters.")]
    [Display(Name = "CI")]
    public string ci { get; set; }

    public byte Status { get; set; }
    public DateTime registerDate { get; set; }= DateTime.Now;

    public virtual ICollection<ActivityProfessional> ActivityProfessionals { get; set; } = new List<ActivityProfessional>();
}
