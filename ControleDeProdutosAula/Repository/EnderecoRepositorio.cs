using ControleDeProdutosAula.Data;
using ControleDeProdutosAula.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeProdutosAula.Repository
{
	public class EnderecoRepositorio : IEnderecoRepositorio
	{
		private readonly BancoContext _bancoContext;

		public EnderecoRepositorio(BancoContext bancoContext)
		{
			_bancoContext = bancoContext;
		}

		public async Task<EnderecoModel> Adicionar(EnderecoModel endereco)
		{
			await _bancoContext.Endereco.AddAsync(endereco);
			await _bancoContext.SaveChangesAsync();

			return endereco;
		}

		public async Task<List<EnderecoModel>> BuscarTodos()
		{
			return await _bancoContext.Endereco.ToListAsync();
		}

		public async Task<EnderecoModel> ListarPorId(long id)
		{
			Task<EnderecoModel> enderecoDB;

			try
			{
				enderecoDB = _bancoContext.Endereco.FirstOrDefaultAsync(x => x.Id == id);
			}
			catch (System.Exception e)
			{
				throw new System.Exception($"{e.Message}Houve um erro na busca do endereco");
			}

			return await enderecoDB;
		}

		public async Task<EnderecoModel> ListarPorCep(string cep)
		{
			Task<EnderecoModel> enderecoDB;

			try
			{
				enderecoDB = _bancoContext.Endereco.FirstOrDefaultAsync(x => x.cep == cep);
			}
			catch (System.Exception e)
			{
				throw new System.Exception($"{e.Message}Houve um erro na busca do endereco");
			}

			return await enderecoDB;
		}

		public async Task<EnderecoModel> Atualizar(EnderecoModel endereco)
		{
			EnderecoModel enderecoDB = await ListarPorId(endereco.Id);

			if (enderecoDB == null) throw new System.Exception("Houve um erro na atualização do endereco");

			enderecoDB.cep = endereco.cep;
			enderecoDB.logradouro = endereco.logradouro;
			enderecoDB.bairro = endereco.bairro;
			enderecoDB.cidade = endereco.cidade;
			enderecoDB.ClienteId = endereco.ClienteId;
			enderecoDB.Cliente = endereco.Cliente;
			
			_bancoContext.Endereco.Update(enderecoDB);
			await _bancoContext.SaveChangesAsync();

			return await Task.FromResult(enderecoDB);
		}

		public async Task<bool> Apagar(long id)
		{
			EnderecoModel enderecoDB = await ListarPorId(id);

			if (enderecoDB == null) throw new System.Exception("Houve um erro na exclusão do endereco");
			_bancoContext.Endereco.Remove(enderecoDB);
			await _bancoContext.SaveChangesAsync();

			return await Task.FromResult(true);
		}

		
	}
}