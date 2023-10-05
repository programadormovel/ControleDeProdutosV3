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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ClienteModel>()
				.HasMany(e => e.Enderecos)
				.WithOne(e => e.Cliente)
				.HasForeignKey(e => e.ClienteId)
				.IsRequired(false);
		}

		public DbSet<EnderecoModel> Endereco { get; set; }
		public DbSet<ProdutoModel> Produto { get; set; }
        public DbSet<ClienteModel> Cliente { get; set; }
        public DbSet<LoginModel> Login { get; set; }
        
	}
}
