using ControleDeProdutosAula.Data;
using ControleDeProdutosAula.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeProdutosAula.Repository
{
	public class ClienteRepositorio : IClienteRepositorio
	{
		private readonly BancoContext _bancoContext;

		public ClienteRepositorio(BancoContext bancoContext)
		{
			_bancoContext = bancoContext;
		}

		public async Task<ClienteModel> Adicionar(ClienteModel cliente)
		{
			await _bancoContext.Cliente.AddAsync(cliente);
			await _bancoContext.SaveChangesAsync();

			return cliente;
		}

		public async Task<List<ClienteModel>> BuscarTodos()
		{
			return await _bancoContext.Cliente.ToListAsync();
		}

		public async Task<ClienteModel> ListarPorId(long id)
		{
			Task<ClienteModel> clienteDB;

			try
			{
				clienteDB = _bancoContext.Cliente.FirstOrDefaultAsync(x => x.Id == id);
			}
			catch (System.Exception e)
			{
				throw new System.Exception($"{e.Message}Houve um erro na busca do cliente");
			}

			return await clienteDB;
		}

		public async Task<ClienteModel> Atualizar(ClienteModel cliente)
		{
			ClienteModel clienteDB = await ListarPorId(cliente.Id);

			if (clienteDB == null) throw new System.Exception("Houve um erro na atualização do cliente");

			clienteDB.Nome = cliente.Nome;
			clienteDB.Sobrenome = cliente.Sobrenome;
			clienteDB.DDD = cliente.DDD;
			clienteDB.Telefone = cliente.Telefone;
			clienteDB.CEP = cliente.CEP;
			clienteDB.DataDeRegistro = cliente.DataDeRegistro;
			#pragma warning disable CS8601 // Possible null reference assignment.
			clienteDB.NomeDaFoto = cliente.NomeDaFoto;
			clienteDB.Foto = cliente.Foto;
			#pragma warning restore CS8601 // Possible null reference assignment.
			clienteDB.Ativo = cliente.Ativo;

			_bancoContext.Cliente.Update(clienteDB);
			await _bancoContext.SaveChangesAsync();

			return await Task.FromResult(clienteDB);
		}

		public async Task<bool> Apagar(long id)
		{
			ClienteModel clienteDB = await ListarPorId(id);

			if (clienteDB == null) throw new System.Exception("Houve um erro na exclusão do cliente");
			_bancoContext.Cliente.Remove(clienteDB);
			await _bancoContext.SaveChangesAsync();

			return await Task.FromResult(true);
		}

		public async Task<bool> AtivarDesativar(long id)
		{
			ClienteModel clienteDB = await ListarPorId(id);

			if (clienteDB == null) throw new System.Exception("Houve um erro na inatividade do cliente");
			
			if (clienteDB.Ativo == true)
			{
				clienteDB.Ativo = false;
			}
			else
			{
				clienteDB.Ativo = true;
			}

			_bancoContext.Cliente.Update(clienteDB);
			await _bancoContext.SaveChangesAsync();

			return await Task.FromResult(true);
		}
	}
}