using ControleDeProdutosAula.Models;
using ControleDeProdutosAula.Repository;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace ControleDeProdutosAula.Controllers
{
	public class LoginController : Controller
	{
		private readonly ILoginRepositorio _loginRepositorio;
		public const string SessionKeyUser = "_Usuario";
		public const string SessionKeyEmail = "_Email";
		public const string SessionKeyNivel = "_Nivel";
		public const string SessionKeyId = "_Id";

		public LoginController(ILoginRepositorio loginRepositorio)
		{
			_loginRepositorio = loginRepositorio;
		}

		async public Task<IActionResult> Index()
		{

			return await Task.FromResult(View());
		}

		async public Task<IActionResult> Editar(long id)
		{
			LoginModel login = await _loginRepositorio.ListarPorId(id);

			return await Task.FromResult(View(login));
		}

		public async Task<IActionResult> ApagarConfirmacao(long id)
		{
			LoginModel login = await _loginRepositorio.ListarPorId(id);
			
			return await Task.FromResult(View(login));
		}

		async public Task<IActionResult> Registro()
		{

			return await Task.FromResult(View());
		}


		[HttpPost]
		async public Task<IActionResult> Index(string email, string senha)
		{
			LoginModel loginDB = await _loginRepositorio.ListarPorEmail(email);
			var sucesso = false;

			if (senha != null)
				sucesso = Util.Decriptografia(loginDB, senha);

			if (sucesso)
			{
				// Gravando usuário logado na sessão
				HttpContext.Session.SetString(SessionKeyUser, loginDB.Usuario);
				HttpContext.Session.SetString(SessionKeyEmail, loginDB.Email);
				HttpContext.Session.SetInt32(SessionKeyNivel, (int)loginDB.NivelAcesso);
				HttpContext.Session.SetInt32(SessionKeyId, (int)loginDB.Id);

				return await Task.FromResult(RedirectToAction("Index", "Home"));
			}
			return await Task.FromResult(View());

		}

		[HttpPost]
		async public Task<IActionResult> Registro(LoginModel login, string? senha)
		{
			LoginModel loginDB = login;
			
			loginDB.Senha = Util.Criptografia(senha!);

			loginDB.NivelAcesso = 1;
			loginDB.DataDeRegistro = DateTime.Now;
			loginDB.Ativo = 1;
			loginDB.EmailConfirmado = false;
			loginDB.TelefoneConfirmado = false;

			await _loginRepositorio.Adicionar(loginDB);

			return await Task.FromResult(RedirectToAction("Index", "Login"));
		}

		[HttpPost]
		public async Task<IActionResult> Alterar(LoginModel login, string senhaAtual, string novaSenha)
		{
			LoginModel loginDB = await _loginRepositorio.ListarPorId(login.Id);

			var sucesso = Util.Decriptografia(loginDB, senhaAtual);
			if (sucesso)
			{
				loginDB.Usuario = login.Usuario;
				loginDB.Email = login.Email;
				loginDB.Senha = Util.Criptografia(novaSenha);
				await _loginRepositorio.Atualizar(loginDB);
				return await Task.FromResult(RedirectToAction("Sair", "Home"));
			}

			return await Task.FromResult(View());
		}

		

	}
}
