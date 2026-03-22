using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Seguridad
{
    public class LoginBase
    {
        public string? NombreUsuario { get; set; }
        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string CorreoElectronico { get; set; }
        public string? PasswordHash { get; set; }
    }
    public class Login : LoginBase
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class LoginRequest : Login
    {
        [Required]
        public string Password { get; set; }
    }
}
