using ControleDeProdutosAula.Data;
using ControleDeProdutosAula.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ControleDeProdutosAula.Repository
{
	public class LoginRepositorio : ILoginRepositorio
	{
		private readonly BancoContext _bancoContext;

		public LoginRepositorio(BancoContext bancoContext)
		{
			_bancoContext = bancoContext;
		}

		public async Task<List<LoginModel>> BuscarTodos()
		{
			return await _bancoContext.Login.ToListAsync();
		}

		public async Task<LoginModel> Adicionar(LoginModel login)
		{
			await _bancoContext.Login.AddAsync(login);
			await _bancoContext.SaveChangesAsync();
			return await Task.FromResult(login);
		}

		public async Task<LoginModel> ListarPorId(long id)
		{
			return await _bancoContext.Login.FirstOrDefaultAsync(l =>
			l.Id == id);
		}

		public async Task<LoginModel> ListarPorEmail(string email)
		{
			return await _bancoContext.Login.FirstOrDefaultAsync(l => l.Email == email);
		}

		public async Task<LoginModel> ListarPorEmailSenha(string email, string senha)
		{
			return await _bancoContext.Login.FirstOrDefaultAsync(l => l.Email == email && l.Senha == senha);
		}

		public async Task<LoginModel> Atualizar(LoginModel login)
		{
			LoginModel loginDB = await _bancoContext.Login.FirstOrDefaultAsync(x => x.Id == login.Id);

			if (loginDB == null) throw new System.Exception("Houve um erro na atualização do login");

			loginDB.Usuario = login.Usuario;
			loginDB.Email = login.Email;
			loginDB.Senha = login.Senha;
			loginDB.NivelAcesso = login.NivelAcesso;
			loginDB.Ativo = login.Ativo;

			_bancoContext.Login.Update(loginDB);
			await _bancoContext.SaveChangesAsync();

			return await Task.FromResult(loginDB);
		}

		public async Task<LoginModel> AtualizarUsuario(LoginModel login)
		{
			LoginModel loginDB = await ListarPorId(login.Id);

			if (loginDB == null) throw new System.Exception("Houve um erro na atualização do login");

			loginDB.Usuario = login.Email;
			loginDB.Email = login.Email;
			loginDB.Senha = login.Senha;
			loginDB.NivelAcesso = login.NivelAcesso;
			loginDB.Ativo = login.Ativo;

			_bancoContext.Login.Update(loginDB);
			await _bancoContext.SaveChangesAsync();

			return await Task.FromResult(loginDB);
		}

		public async Task<bool> Apagar(long id)
		{
			LoginModel loginDB = await _bancoContext.Login.FirstOrDefaultAsync(x => x.Id == id);

			if (loginDB == null) throw new System.Exception("Houve um erro na exclusão do login");

			_bancoContext.Login.Remove(loginDB);
			return await Task.FromResult<bool>(false);
		}
	}
}
