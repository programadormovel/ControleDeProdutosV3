using ControleDeProdutosAula.Data;
using ControleDeProdutosAula.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeProdutosAula.Repository
{
	public class ProdutoRepositorio : IProdutoRepositorio
	{
		private readonly BancoContext _bancoContext;

		public ProdutoRepositorio(BancoContext bancoContext)
		{
			_bancoContext = bancoContext;
		}

		public async Task<ProdutoModel> Adicionar(ProdutoModel produto)
		{
			await _bancoContext.Produto.AddAsync(produto);
			await _bancoContext.SaveChangesAsync();

			return produto;
		}

		public async Task<List<ProdutoModel>> BuscarTodos()
		{
			return await _bancoContext.Produto.ToListAsync();
		}

		public async Task<ProdutoModel> ListarPorId(long id)
		{
			Task<ProdutoModel> produtoDB;

			try
			{
				produtoDB = _bancoContext.Produto.FirstOrDefaultAsync(x => x.Id == id);
			}
			catch (System.Exception e)
			{
				throw new System.Exception($"{e.Message}Houve um erro na busca do produto");
			}

			return await produtoDB;
		}

		public async Task<ProdutoModel> Atualizar(ProdutoModel produto)
		{
			ProdutoModel produtoDB = await ListarPorId(produto.Id);

			if (produtoDB == null) throw new System.Exception("Houve um erro na atualização do produto");

			produtoDB.Descricao = produto.Descricao;
			produtoDB.CodigoDeBarras = produto.CodigoDeBarras;
			produtoDB.DataDeValidade = produto.DataDeValidade;
			//produtoDB.DataDeRegistro = produto.DataDeRegistro;
			produtoDB.Quantidade = produto.Quantidade;
			produtoDB.Valor = produto.Valor;
			#pragma warning disable CS8601 // Possible null reference assignment.
			produtoDB.NomeDaFoto = produto.NomeDaFoto;
			produtoDB.Foto = produto.Foto;
			#pragma warning restore CS8601 // Possible null reference assignment.
			produtoDB.Ativo = produto.Ativo;

			_bancoContext.Produto.Update(produtoDB);
			await _bancoContext.SaveChangesAsync();

			return await Task.FromResult(produtoDB);
		}

		public async Task<bool> Apagar(long id)
		{
			ProdutoModel produtoDB = await ListarPorId(id);

			if (produtoDB == null) throw new System.Exception("Houve um erro na exclusão do produto");
			_bancoContext.Produto.Remove(produtoDB);
			await _bancoContext.SaveChangesAsync();

			return await Task.FromResult(true);
		}
	}
}
