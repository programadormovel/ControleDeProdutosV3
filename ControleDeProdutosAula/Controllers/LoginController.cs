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

		public LoginController(ILoginRepositorio loginRepositorio)
		{
			_loginRepositorio = loginRepositorio;
		}

		async public Task<IActionResult> Index()
		{

			return await Task.FromResult(View());
		}

		async public Task<IActionResult> Registro()
		{

			return await Task.FromResult(View());
		}


		[HttpPost]
		async public Task<IActionResult> Index(string email, string senha)
		{
			LoginModel loginDB = await _loginRepositorio.ListarPorEmail(email);

			byte[] salt = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

			EncryptDecrypt enc = new EncryptDecrypt(salt);

			string senhaBanco = loginDB.Senha;

			var senhaDecriptada = enc.Decrypt(senhaBanco);

			if (senhaDecriptada.Equals(senha))
			{
				return await Task.FromResult(RedirectToAction("Index", "Home"));
			}

			return await Task.FromResult(View());

		}

		[HttpPost]
		async public Task<IActionResult> Registro(LoginModel login, string? senha)
		{
			LoginModel loginDB = login;
			var senhaEncriptada = "";

			byte[] salt = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

			EncryptDecrypt enc = new EncryptDecrypt(salt);

			senhaEncriptada = enc.Encrypt(senha);

			loginDB.Senha = senhaEncriptada;

			loginDB.NivelAcesso = 1;
			loginDB.DataDeRegistro =
DateTime.Now
;
			loginDB.Ativo = 1;
			loginDB.EmailConfirmado = false;
			loginDB.TelefoneConfirmado = false;

			await _loginRepositorio.Adicionar(loginDB);

			return await Task.FromResult(RedirectToAction("Index", "Login"));
		}

	}
}
