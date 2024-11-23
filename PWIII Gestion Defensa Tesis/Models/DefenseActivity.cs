using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PWIII_Gestion_Defensa_Tesis.Models;

public partial class DefenseActivity
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(100, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 10)]
    [Display(Name = "DESCRIPTION")]
    public string Description { get; set; } = null!;

    public byte Status { get; set; }

    [Required(ErrorMessage = "Defense date is required")]
    [Display(Name = "DEFENSE DATE")]
    [DataType(DataType.DateTime)]
    public DateTime DefenseDate { get; set; }

    public int IdThesis { get; set; }

    public byte IdAudience { get; set; }

    public short IdStudent { get; set; }

    public virtual ICollection<ActivityProfessional> ActivityProfessionals { get; set; } = new List<ActivityProfessional>();

    public virtual Audience IdAudienceNavigation { get; set; } = null!;

    public virtual Student IdStudentNavigation { get; set; } = null!;

    public virtual Thesis IdThesisNavigation { get; set; } = null!;
}
