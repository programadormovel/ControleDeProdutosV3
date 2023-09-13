using ControleDeProdutosAula.Models;
using ControleDeProdutosAula.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace ControleDeProdutosAula.Controllers
{
	[Route("api/Login")]
	[ApiController]
	public class LoginApiController : Controller
	{
		private readonly ILoginRepositorio _loginRepositorio;
		
		public LoginApiController(ILoginRepositorio loginRepositorio)
		{
			_loginRepositorio = loginRepositorio;
		}

		// Get: api/Login
		[HttpGet]
		async public Task<ActionResult<LoginModel>> Logar(string? email, string? senha)
		{
			LoginModel loginDB = await _loginRepositorio.ListarPorEmail(email!);
			var sucesso = false;

			if (senha != null)
				sucesso = Util.Decriptografia(loginDB, senha);

			if (sucesso)
			{
				return await Task.FromResult(loginDB);
			}
			return await Task.FromResult(BadRequest());
		}

		public class LoginUsuarioModel
		{
			public string? usuario { get; set; }
			public string email { get; set; }
			public string senha { get; set; }	
			public int? nivelAcesso { get; set; }
			
		}

		// Post: api/Login
		[EnableCors("MyPolicy")]
		[HttpPost]
		async public Task<ActionResult<LoginModel>> LogarPost(LoginUsuarioModel loginUsuarioModel)
		{
			LoginModel loginDB = await _loginRepositorio.ListarPorEmail(loginUsuarioModel.email);
			var sucesso = false;

			if (loginUsuarioModel.senha != null)
				sucesso = Util.Decriptografia(loginDB, loginUsuarioModel.senha);

			if (sucesso)
			{
				return await Task.FromResult(loginDB);
			}
			return await Task.FromResult(BadRequest());
		}

		//async public Task<IActionResult> Editar(long id)
		//{
		//	LoginModel login = await _loginRepositorio.ListarPorId(id);

		//	return await Task.FromResult(View(login));
		//}

		//public async Task<IActionResult> ApagarConfirmacao(long id)
		//{
		//	LoginModel login = await _loginRepositorio.ListarPorId(id);

		//	return await Task.FromResult(View(login));
		//}

		//async public Task<IActionResult> Registro()
		//{

		//	return await Task.FromResult(View());
		//}


		//[HttpPost]
		//async public Task<IActionResult> Index(string email, string senha)
		//{
		//	LoginModel loginDB = await _loginRepositorio.ListarPorEmail(email);
		//	var sucesso = false;

		//	if (senha != null)
		//		sucesso = Util.Decriptografia(loginDB, senha);

		//	if (sucesso)
		//	{
		//		// Gravando usuário logado na sessão


		//		return await Task.FromResult(RedirectToAction("Index", "Home"));
		//	}
		//	return await Task.FromResult(View());

		//}

		//[HttpPost]
		//async public Task<IActionResult> Registro(LoginModel login, string? senha)
		//{
		//	LoginModel loginDB = login;

		//	loginDB.Senha = Util.Criptografia(senha!);

		//	loginDB.NivelAcesso = 1;
		//	loginDB.DataDeRegistro = DateTime.Now;
		//	loginDB.Ativo = 1;
		//	loginDB.EmailConfirmado = false;
		//	loginDB.TelefoneConfirmado = false;

		//	await _loginRepositorio.Adicionar(loginDB);

		//	return await Task.FromResult(RedirectToAction("Index", "Login"));
		//}

		//[HttpPost]
		//public async Task<IActionResult> Alterar(LoginModel login, string senhaAtual, string novaSenha)
		//{
		//	LoginModel loginDB = await _loginRepositorio.ListarPorId(login.Id);

		//	var sucesso = Util.Decriptografia(loginDB, senhaAtual);
		//	if (sucesso)
		//	{
		//		loginDB.Usuario = login.Usuario;
		//		loginDB.Email = login.Email;
		//		loginDB.Senha = Util.Criptografia(novaSenha);
		//		await _loginRepositorio.Atualizar(loginDB);
		//		return await Task.FromResult(RedirectToAction("Sair", "Home"));
		//	}

		//	return await Task.FromResult(View());
		//}


	}
}
