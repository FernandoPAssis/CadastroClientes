using Cadastro.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Api.Data
{
    public class CadastroDbContext : DbContext
    {
        public CadastroDbContext(DbContextOptions<CadastroDbContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes 
        { 
            get{ return Set<Cliente>(); } 
        }
    }
}
