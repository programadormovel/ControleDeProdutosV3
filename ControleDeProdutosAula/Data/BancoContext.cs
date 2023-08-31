using ControleDeProdutosAula.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Transactions;

namespace ControleDeProdutosAula.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {

        }

        public DbSet<ProdutoModel> Produto { get; set; }
        public DbSet<ClienteModel> Cliente { get; set; }
        public DbSet<LoginModel> Login { get; set; }
	}
}
