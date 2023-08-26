using ControleDeProdutosAula.Models;
using ControleDeProdutosAula.Repository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ControleDeProdutosAula.Controllers
{
	public class ClienteController : Controller
	{
		private readonly IClienteRepositorio _clienteRepositorio;

		public ClienteController(IClienteRepositorio clienteRepositorio)
		{
			_clienteRepositorio = clienteRepositorio;
		}

		public async Task<IActionResult> Index()
		{
			List<ClienteModel> clientes = await _clienteRepositorio.BuscarTodos();

			return await Task.FromResult(View(clientes));
		}

		public async Task<IActionResult> Criar()
		{
			return await Task.FromResult(View());
		}

		public async Task<IActionResult> Editar(long id)
		{
			ClienteModel cliente = await _clienteRepositorio.ListarPorId(id);

			return await Task.FromResult(View(cliente));
		}

		public async Task<IActionResult> ApagarConfirmacao(long id)
		{
			ClienteModel cliente = await _clienteRepositorio.ListarPorId(id);

			return await Task.FromResult(View(cliente));
		}

		public async Task<IActionResult> Apagar(long id)
		{
			await _clienteRepositorio.Apagar(id);

			return await Task.FromResult(RedirectToAction("Index"));
		}

		[HttpPost]
		public async Task<IActionResult> Criar(ClienteModel cliente)
		{
			ClienteModel model = cliente;

			List<ValidationResult> results = new List<ValidationResult>();
			ValidationContext context = new ValidationContext(model, null, null);

			bool isValid = Validator.TryValidateObject(model, context, results, true);

			if (!isValid)
			{
				foreach (var validationResult in results)
				{
					return View(model);
				}
			}

			model.DataDeRegistro = DateTime.Now;
			model.Ativo = true;
			model.Telefone = cliente.Telefone.Replace("-", "");
			model.CEP = cliente.CEP.Replace("-", "");
			await _clienteRepositorio.Adicionar(model);

			return await Task.FromResult(RedirectToAction("Index"));
		}

		[HttpPost]
		public async Task<IActionResult> Alterar(ClienteModel cliente)
		{
			await _clienteRepositorio.Atualizar(cliente);
			return await Task.FromResult(RedirectToAction("Index"));
		}
	}
}
