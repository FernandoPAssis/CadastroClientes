namespace Cadastro.Api.Domain
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal RendaMensal { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
