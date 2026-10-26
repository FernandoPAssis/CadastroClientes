using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Cadastro.Api.Messaging
{
    public class RabbitMqPublisher
    {
        public const string ExchangeClienteCadastrado = "cliente-cadastrado";

        private readonly IConnection _conexao;

        public RabbitMqPublisher(IConnection conexao)
        {
            _conexao = conexao;
        }

        public async Task PublicarClienteCadastradoAsync(ClienteCadastradoEvento evento)
        {
            // abre um canal (fecha sozinho no fim do método)
            await using IChannel canal = await _conexao.CreateChannelAsync();

            // garante que o exchange existe; se já existir, não faz nada
            await canal.ExchangeDeclareAsync(ExchangeClienteCadastrado, ExchangeType.Fanout, durable: true);

            // converte o objeto em JSON e depois em bytes (o RabbitMQ trafega bytes)
            string json = JsonSerializer.Serialize(evento);
            byte[] corpo = Encoding.UTF8.GetBytes(json);

            BasicProperties propriedades = new BasicProperties
            {
                Persistent = true,                 // grava em disco: não se perde se o RabbitMQ reiniciar
                ContentType = "application/json"
            };

            // publica no exchange 
            await canal.BasicPublishAsync(ExchangeClienteCadastrado, string.Empty, false, propriedades, corpo);
        }
    }
}
