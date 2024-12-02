using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PWIII_Gestion_Defensa_Tesis.Models;

public partial class DefenseActivity
{
    [Key]
    public int Id { get; set; }

    [StringLength(150, MinimumLength = 10, ErrorMessage = "The Description must be between 10 and 150 characters.")]
    [RegularExpression(@"^(?!.*\s\s)(?!.*\s$)[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\s\.,;!?&()'\-\""\[\]<>]*$", ErrorMessage = "Only letters, numbers, spaces, and common punctuation marks are allowed, and no consecutive or trailing spaces.")]
    [Required(ErrorMessage = "Description is required")]
    [Display(Name = "DESCRIPTION")]
    public string Description { get; set; } = null!;

    public byte Status { get; set; }

    [Required(ErrorMessage = "Defense date is required")]
    [Display(Name = "DEFENSE DATE")]
    [DataType(DataType.DateTime)]
    public DateTime DefenseDate { get; set; }

    [Required(ErrorMessage = "Thesis is required")]
    [Display(Name = "PROYECTO")]
    public int IdThesis { get; set; }

    [Required(ErrorMessage = "Auditorium is required")]
    [Display(Name = "AUDITORIUM")]
    public int IdAudience { get; set; }

    [Required(ErrorMessage = "Student is required")]
    [Display(Name = "STUDENT")]
    public short IdStudent { get; set; }
    [Display(Name = "STATUS ACTIVITY")]
    public string StatusThesis { get; set; } = "Pendiente";

	public DateTime registerDate { get; set; } = DateTime.Now;
    [Required(ErrorMessage = "Profesional is required")]
    [Display(Name = "PROFESIONALS")]
    public virtual ICollection<ActivityProfessional> ActivityProfessionals { get; set; } = new List<ActivityProfessional>();

    public virtual Audience IdAudienceNavigation { get; set; } = null!;

    public virtual Student IdStudentNavigation { get; set; } = null!;

    public virtual Thesis IdThesisNavigation { get; set; } = null!;
}
