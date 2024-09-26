using Microsoft.AspNetCore.Identity;

namespace Convite.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Sobrenome { get; set; }
        public DateTime? Nascimento { get; set; }
        public string? Sexo { get; set; }
        public string? Cor { get; set; }
        public string? Nacionalidade { get; set; }
        public bool? CadastroCompleto { get; set; } = false;  
        public bool? CriaEvento { get; set; } = false;
        // Coleção de endereços associados ao usuário
        public ICollection<Endereco>? Enderecos { get; set; }

    }
}
