using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PWIII_Gestion_Defensa_Tesis.Models;

public partial class TypeThesis
{
    [Key]
    public int Id { get; set; }

    [StringLength(30, MinimumLength = 4, ErrorMessage = "The Name must be between 4 and 30 characters.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Only letters, including accented characters and single spaces between words are allowed.")]
    [Required(ErrorMessage = "Name Thesis is required")]
    [Display(Name = "NAME")]
    public string Name { get; set; } = null!;

    [StringLength(200, MinimumLength = 4, ErrorMessage = "The Description must be between 4 and 200 characters.")]
    [RegularExpression(@"^(?!.*\s\s)(?!.*\s$)[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\s\.,;!?&()'\-\""\[\]<>]*$", ErrorMessage = "Only letters, numbers, spaces, and common punctuation marks are allowed, and no consecutive or trailing spaces.")]
    [Required(ErrorMessage = "Description is required")]
    [Display(Name = "DESCRIPTION")]
    public string Description { get; set; }
    public byte Status { get; set; } = 1;
    [Display(Name = "REGISTER DATE")]
    public DateTime registerDate { get; set; } = DateTime.Now;
	public virtual ICollection<Thesis> Theses { get; set; } = new List<Thesis>();
}
