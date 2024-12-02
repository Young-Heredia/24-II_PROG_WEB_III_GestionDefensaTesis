using System.ComponentModel.DataAnnotations;

namespace PWIII_Gestion_Defensa_Tesis.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Role User is required")]
        [Display(Name = "ROLE")]
        public string RolName { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
