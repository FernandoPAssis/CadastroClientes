using System.Net.Mail;

namespace Cadastro.Api.Domain
{
    public static class EmailValidator
    {
        public static bool IsValid(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email && addr.Host.Contains('.'); //verifica se tem o . depois do @dominio
            }
            catch
            {
                return false;
            }
        }
    }
}
