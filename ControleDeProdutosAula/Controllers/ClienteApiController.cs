using ControleDeProdutosAula.Models;
using ControleDeProdutosAula.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeProdutosAula.Controllers
{
	[Route("api/Cliente")]
	[ApiController]
	public class ClienteApiController : ControllerBase
	{
		private readonly IClienteRepositorio _clienteRepositorio;

		public ClienteApiController(IClienteRepositorio clienteRepositorio)
		{
			_clienteRepositorio = clienteRepositorio;
		}

		// Get: api/Cliente
		[HttpGet]
		public async Task<ActionResult<List<ClienteModel>>> ListarClientes()
		{
			List<ClienteModel> clientes = await _clienteRepositorio.BuscarTodos();

			return await Task.FromResult(clientes);
		}

		// Get: api/Cliente/2
		[HttpGet("{id}")]
		public async Task<ActionResult<ClienteModel>> ObterProdutoPorId(long id)
		{
			ClienteModel cliente = await _clienteRepositorio.ListarPorId(id);

			return await Task.FromResult(cliente);
		}

		// Post: api/Cliente
		[HttpPost]
		public async Task<ActionResult<ClienteModel>> CadastrarCliente(ClienteModel cliente)
		{
			cliente.DataDeRegistro = DateTime.Now;
			cliente.Ativo = true;

			await _clienteRepositorio.Adicionar(cliente);

			return await Task.FromResult(cliente);
		}

		// Put: api/Cliente/{id}
		[HttpPut("{id}")]
		public async Task<ActionResult<ClienteModel>> AlterarCliente(long id, ClienteModel cliente)
		{
			if (id > 0)
			{
				if (cliente != null)
				{
					ClienteModel clienteBase = await _clienteRepositorio.ListarPorId(id);

					if (clienteBase == null)
					{
						return await Task.FromResult(NotFound());
					}

					clienteBase.Nome = cliente.Nome;
					clienteBase.Sobrenome = cliente.Sobrenome;
					clienteBase.CEP = cliente.CEP;
					clienteBase.DDD = cliente.DDD;
					clienteBase.Telefone = cliente.Telefone;
					clienteBase.NomeDaFoto = cliente.NomeDaFoto;
					clienteBase.Foto = cliente.Foto;

					clienteBase.Ativo = cliente.Ativo;

					await _clienteRepositorio.Atualizar(clienteBase);

					return await Task.FromResult(NoContent());
				}
				return await Task.FromResult(BadRequest());
			}
			return await Task.FromResult(BadRequest());
		}

		// Delete: api/Cliente/2
		[HttpDelete("{id}")]
		public async Task<ActionResult<String>> ApagarClientePorId(long id)
		{
			ClienteModel clienteBase = await _clienteRepositorio.ListarPorId(id);

			bool sucesso = await _clienteRepositorio.Apagar(id);

			if (sucesso)
			{
				return await Task.FromResult($"Cliente {clienteBase.Nome} {clienteBase.Sobrenome} apagado com Sucesso!");
			}
			else
			{
				return await Task.FromResult($"Cliente {clienteBase.Nome} não apagado!");
			}
		}
	}
}
