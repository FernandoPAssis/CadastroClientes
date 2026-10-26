using Cadastro.Api.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cadastro.Tests
{
    public class CpfValidatorTests
    {
        [Theory]
        [InlineData("52998224725")]
        [InlineData("529.982.247-25")]
        public void IsValid_QuandoCpfValido_RetornaTrue(string cpf)
        {
            // Arrange: o CPF vem do InlineData

            // Act
            var resultado = CpfValidator.IsValid(cpf);

            // Assert
            Assert.True(resultado);
        }

        [Theory]
        [InlineData("52998224724")]   // dígito verificador errado
        [InlineData("11111111111")]   // todos os dígitos iguais
        [InlineData("123")]           // tamanho errado
        [InlineData("")]
        [InlineData(null)]
        public void IsValid_QuandoCpfInvalido_RetornaFalse(string? cpf)
        {
            var resultado = CpfValidator.IsValid(cpf);

            Assert.False(resultado);
        }
    }
}
