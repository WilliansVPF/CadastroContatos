namespace CadastroContatos.Models
{
    public class ContaModel
    {
        public int? IdConta { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string? Salt { get; set; }
        public int? IdUsuario { get; set; }
    }
}