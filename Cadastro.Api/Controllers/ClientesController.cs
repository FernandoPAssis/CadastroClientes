using Cadastro.Api.Data;
using Cadastro.Api.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Api.Controllers
{
    [ApiController]
    [Route("api/v1/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly CadastroDbContext _db;

        public ClientesController(CadastroDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(CriarClienteRequest request)
        {
            // 1. valida os dados
            List<string> erros = ClienteValidator.Validar(request);
            if (erros.Count > 0)
                return BadRequest(new { erros = erros });                       // 400

            // 2. verifica CPF duplicado
            string cpf = CpfValidator.OnlyDigits(request.Cpf!);
            bool cpfJaExiste = await _db.Clientes.AnyAsync(c => c.Cpf == cpf);
            if (cpfJaExiste)
                return Conflict(new { erros = new List<string> { "CPF já cadastrado." } });   // 409

            // 3. grava no banco
            Cliente cliente = new Cliente
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome!.Trim(),
                Cpf = cpf,
                Email = request.Email!.Trim(),
                RendaMensal = request.RendaMensal,
                DataCadastro = DateTime.UtcNow
            };

            _db.Clientes.Add(cliente);
            await _db.SaveChangesAsync();





            return CreatedAtAction(nameof(ObterPorId), new { id = cliente.Id }, cliente);   // 201
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            Cliente? cliente = await _db.Clientes.FindAsync(id);

            if (cliente == null)
                return NotFound();                                              // 404

            return Ok(cliente);                                                 // 200
        }
    }
}
