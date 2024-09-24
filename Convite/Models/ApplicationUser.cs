using Microsoft.AspNetCore.Identity;

namespace Convite.Models
{
    public class ApplicationUser : IdentityUser
    {
        //public string Sobrenome { get; set; }
        //public DateTime MyProperty { get; set; }
        public bool CadastroCompleto { get; set; } = false;  
        public bool CriaEvento { get; set; } = false;
    }
}
