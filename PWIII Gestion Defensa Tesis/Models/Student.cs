using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PWIII_Gestion_Defensa_Tesis.Models;

public partial class Student
{
    [Key]
    public short Id { get; set; }

    [Required(ErrorMessage = "Name Student is required")]
    [Display(Name = "NAME")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Lastname Student is required")]
    [Display(Name = "LASTNAME")]
    public string LastName { get; set; } = null!;

    [Display(Name = "SECOND LASTNAME")]
    public string? SecondLastName { get; set; }

    public byte Status { get; set; }

    public virtual ICollection<DefenseActivity> DefenseActivities { get; set; } = new List<DefenseActivity>();
}
