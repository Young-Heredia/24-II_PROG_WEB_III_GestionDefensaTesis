using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PWIII_Gestion_Defensa_Tesis.Models;

public partial class TypeThesis
{
    [Key]
    public byte Id { get; set; }

    [Required(ErrorMessage = "Name Thesis is required")]
    [Display(Name = "NAME")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Thesis> Theses { get; set; } = new List<Thesis>();
}
