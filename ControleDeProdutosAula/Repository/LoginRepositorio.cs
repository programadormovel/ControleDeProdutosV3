using ControleDeProdutosAula.Models;
using System.ComponentModel.DataAnnotations;

namespace ControleDeProdutosAula.Repository
{
	public class LoginRepositorio : ILoginRepositorio
	{
		private readonly ILoginRepositorio loginRepositorio;

		public LoginRepositorio(
			ILoginRepositorio _loginRepositorio)
		{
			loginRepositorio = _loginRepositorio;
		}

		public async Task<List<LoginModel>> BuscarTodos()
		{
			return await loginRepositorio.BuscarTodos();
		}

		public async Task<LoginModel> Adicionar(LoginModel login)
		{
			return await Task.FromResult(login);
		}

		public async Task<LoginModel> ListarPorId(long id)
		{
			return await loginRepositorio.ListarPorId(id);
		}

		public async Task<LoginModel> ListarPorEmail(EmailAddressAttribute email)
		{
			return await loginRepositorio.ListarPorEmail(email);
		}

		public async Task<LoginModel> ListarPorEmailSenha(EmailAddressAttribute email, string senha)
		{
			return await loginRepositorio.ListarPorEmailSenha(email, senha);
		}

		public async Task<LoginModel> Atualizar(LoginModel login)
		{
			return await loginRepositorio.Atualizar(login);
		}

		public async Task<bool> Apagar(long id)
		{
			return await Task.FromResult<bool>(false);
		}
	}
}
