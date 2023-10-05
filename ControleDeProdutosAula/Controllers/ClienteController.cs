using ControleDeProdutosAula.Models;
using ControleDeProdutosAula.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ControleDeProdutosAula.Controllers
{
	public class ClienteController : Controller
	{
		private IHostingEnvironment Environment;
		public const string SessionKeyUser = "_Usuario";
		public const string SessionKeyEmail = "_Email";
		public const string SessionKeyNivel = "_Nivel";
		public const string SessionKeyIdCliente = "_IdCliente";

		private readonly IClienteRepositorio _clienteRepositorio;

		public ClienteController(IClienteRepositorio clienteRepositorio,
			IHostingEnvironment _environment)
		{
			_clienteRepositorio = clienteRepositorio;
			Environment = _environment;
		}

		public async Task<IActionResult> Index()
		{
			List<ClienteModel> clientes = await _clienteRepositorio.BuscarTodos();

			var usuario = HttpContext.Session.GetString(SessionKeyUser);
			if (!usuario.IsNullOrEmpty())
			{
				return await Task.FromResult(View(clientes));
			}
			return await Task.FromResult(RedirectToAction("Index", "Home"));
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

		public async Task<IActionResult> AtivarDesativar(long id)
		{
			await _clienteRepositorio.AtivarDesativar(id);

			return await Task.FromResult(RedirectToAction("Index"));
		}

		public async Task<IActionResult> Apagar(long id)
		{
			await _clienteRepositorio.Apagar(id);

			return await Task.FromResult(RedirectToAction("Index"));
		}

		[HttpPost]
		public async Task<IActionResult> Criar(ClienteModel cliente, IFormFile? imagemCarregada, string logradouro, string bairro, string cidade)
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

			// Carregamento de imagem
			string wwwPath = this.Environment.WebRootPath;
			string path = Path.Combine(wwwPath, "Uploads");
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			string fileName = Path.GetFileName(imagemCarregada!.FileName);

			var caminhoCompleto = Path.Combine(path, fileName);

			using (FileStream stream = new FileStream(caminhoCompleto, FileMode.Create))
			{
				imagemCarregada.CopyTo(stream);
				model.NomeDaFoto = caminhoCompleto;
			}

			model.Foto = Util.ReadFully2(caminhoCompleto);

			model.Enderecos.Add(new EnderecoModel
			{
				cep = model.CEP,
				logradouro = logradouro,
				bairro = bairro, 
				cidade = cidade, 
				ClienteId = model.Id
			});

            model = await _clienteRepositorio.Adicionar(model);

            HttpContext.Session.SetInt32("_IdCliente", model.Id);

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
