using System.ComponentModel.DataAnnotations;

namespace Convite.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        //[Required(ErrorMessage = "O campo Logradouro é obrigatório.")]
        public string? Logradouro { get; set; }
        //[Required(ErrorMessage = "O campo Numero é obrigatório.")]
        public string? Numero { get; set; }
        //[Required(ErrorMessage = "O campo Cidade é obrigatório.")]
        public string? Cidade { get; set; }
        //[Required(ErrorMessage = "O campo Estado é obrigatório.")]
        public string? Estado { get; set; }
        //[Required(ErrorMessage = "O campo CEP é obrigatório.")]
        //[RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "CEP inválido.")]
        public string? CEP { get; set; }

        // Chave estrangeira para o usuário
        public string UserId { get; set; }

        // Propriedade de navegação de volta para o usuário
        public ApplicationUser User { get; set; }

    }
}
