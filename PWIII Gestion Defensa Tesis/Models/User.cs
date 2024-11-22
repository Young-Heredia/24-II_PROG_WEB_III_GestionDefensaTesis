using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PWIII_Gestion_Defensa_Tesis.Models;

public partial class User
{
    [Key]
    public short Id { get; set; }

    [Required(ErrorMessage = "Role User is required")]
    [Display(Name = "ROLE")]
    public string Role { get; set; } = null!;

    public byte Status { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [Display(Name = "PASSWORD")]
    public byte[] Password { get; set; } = null!;

    [Required(ErrorMessage = "username is required")]
    [Display(Name = "USERNAME")]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [Display(Name = "EMAIL")]
    public string Email { get; set; } = null!;
}
