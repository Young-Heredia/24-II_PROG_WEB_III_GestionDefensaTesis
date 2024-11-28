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
	[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name cannot contain special characters or spaces.")]
	public string Name { get; set; } = null!;

	[Required(ErrorMessage = "Description is required")]
	[Display(Name = "DESCRIPTION")]
	[RegularExpression(@"^[a-zA-Z0-9.]+$", ErrorMessage = "Description cannot contain special characters or spaces.")]
	public string Description { get; set; } = null!;
    [Required(ErrorMessage = "Note is required")]
    [Display(Name = "NOTE")]
    [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "The note must be a valid number (no letters or special characters).")]
    [Range(0, double.MaxValue, ErrorMessage = "The note must be a positive number.")]
    public double Note { get; set; }
    public byte IdTypeThesis { get; set; }
	public byte Status { get; set; }
	public DateTime registerDate { get; set; } = DateTime.Now;
	public virtual ICollection<DefenseActivity> DefenseActivities { get; set; } = new List<DefenseActivity>();

	public virtual TypeThesis IdTypeThesisNavigation { get; set; } = null!;
}
