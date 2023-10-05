using ControleDeProdutosAula.Models;
using ControleDeProdutosAula.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ControleDeProdutosAula.Controllers
{
	public class EnderecoController : Controller
	{
		public const string SessionKeyUser = "_Usuario";
		public const string SessionKeyEmail = "_Email";
		public const string SessionKeyNivel = "_Nivel";

		private readonly IEnderecoRepositorio _EnderecoRepositorio;

		public EnderecoController(IEnderecoRepositorio EnderecoRepositorio)
		{
			_EnderecoRepositorio = EnderecoRepositorio;
		}

		public async Task<IActionResult> Index()
		{
			List<EnderecoModel> enderecos = await _EnderecoRepositorio.BuscarTodos();

			var usuario = HttpContext.Session.GetString(SessionKeyUser);
			if (!usuario.IsNullOrEmpty())
			{
				return await Task.FromResult(View(enderecos));
			}
			return await Task.FromResult(RedirectToAction("Index", "Home"));
		}

		public async Task<IActionResult> Criar()
		{
			return await Task.FromResult(View());
		}

		public async Task<IActionResult> Editar(long id)
		{
			EnderecoModel endereco = await _EnderecoRepositorio.ListarPorId(id);

			return await Task.FromResult(View(endereco));
		}

		public async Task<IActionResult> ApagarConfirmacao(long id)
		{
			EnderecoModel endereco = await _EnderecoRepositorio.ListarPorId(id);

			return await Task.FromResult(View(endereco));
		}

		public async Task<IActionResult> Apagar(long id)
		{
			await _EnderecoRepositorio.Apagar(id);

			return await Task.FromResult(RedirectToAction("Index"));
		}

		[HttpPost]
		public async Task<IActionResult> Criar(EnderecoModel endereco, IFormFile? imagemCarregada)
		{
			EnderecoModel model = endereco;

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

			await _EnderecoRepositorio.Adicionar(model);

			return await Task.FromResult(RedirectToAction("Index"));
		}

		[HttpPost]
		public async Task<IActionResult> Alterar(EnderecoModel endereco)
		{
			await _EnderecoRepositorio.Atualizar(endereco);
			return await Task.FromResult(RedirectToAction("Index"));
		}
	}
}
