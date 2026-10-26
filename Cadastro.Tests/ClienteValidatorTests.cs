using System;
using System.Collections.Generic;
using System.Text;
using Cadastro.Api.Domain;

namespace Cadastro.Tests
{
    public class ClienteValidatorTests
    {
        [Fact]
        public void Validar_QuandoDadosValidos_RetornaSemErros()
        {
            // Arrange
            CriarClienteRequest request = new CriarClienteRequest
            {
                Nome = "Maria Silva",
                Cpf = "529.982.247-25",
                Email = "maria@email.com",
                RendaMensal = 5000
            };

            // Act
            List<string> erros = ClienteValidator.Validar(request);

            // Assert
            Assert.Empty(erros);
        }

        [Fact]
        public void Validar_QuandoTodosCamposInvalidos_RetornaQuatroErros()
        {
            CriarClienteRequest request = new CriarClienteRequest();

            List<string> erros = ClienteValidator.Validar(request);

            Assert.Equal(4, erros.Count);
        }
    }
}
