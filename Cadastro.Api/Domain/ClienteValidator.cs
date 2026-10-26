namespace Cadastro.Api.Domain
{
    public static class ClienteValidator
    {
        public static List<string> Validar(CriarClienteRequest request)
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Nome))
                erros.Add("Nome é obrigatório.");

            if (!CpfValidator.IsValid(request.Cpf))
                erros.Add("CPF inválido.");

            if (!EmailValidator.IsValid(request.Email))
                erros.Add("E-mail inválido.");

            if (request.RendaMensal <= 0)
                erros.Add("Renda mensal deve ser maior que zero.");

            return erros;
        }
    }
}
