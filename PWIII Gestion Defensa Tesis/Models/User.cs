using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PWIII_Gestion_Defensa_Tesis.Models;

public partial class User
{
    [Key]
    public short UserId { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [Display(Name = "EMAIL")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [Display(Name = "PASSWORD")]
    public byte[] Password { get; set; } = null!;

    [Required(ErrorMessage = "Username is required")]
    [Display(Name = "USERNAME")]
    public string UserName { get; set; } = null!;

    public byte Status { get; set; }


    public int RoleId { get; set; }

    public Role? Role { get; set; }

}
