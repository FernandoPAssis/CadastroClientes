namespace Cadastro.Api.Domain
{
    public class CriarClienteRequest
    {
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public string? Email { get; set; }
        public decimal RendaMensal { get; set; }
    }
}
