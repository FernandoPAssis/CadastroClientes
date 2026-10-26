using Cadastro.Api.Domain;

namespace Cadastro.Tests
{
    public class EmailValidatorTests
    {
        [Theory]
        [InlineData("maria@email.com")]
        [InlineData("joao.silva@empresa.com.br")]
        public void IsValid_QuandoEmailValido_RetornaTrue(string email)
        {
            bool resultado = EmailValidator.IsValid(email);

            Assert.True(resultado);
        }

        [Theory]
        [InlineData("maria@email")]               // sem ponto no domínio
        [InlineData("mariaemail.com")]            // sem @
        [InlineData("Maria <maria@email.com>")]   // formato com nome
        [InlineData("")]
        [InlineData(null)]
        public void IsValid_QuandoEmailInvalido_RetornaFalse(string? email)
        {
            bool resultado = EmailValidator.IsValid(email);

            Assert.False(resultado);
        }
    }
}
