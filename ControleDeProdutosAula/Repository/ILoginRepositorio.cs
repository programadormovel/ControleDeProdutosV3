using ControleDeProdutosAula.Models;
using System.ComponentModel.DataAnnotations;

namespace ControleDeProdutosAula.Repository
{
	public interface ILoginRepositorio
	{
		Task<List<LoginModel>> BuscarTodos();
		Task<LoginModel> Adicionar(LoginModel login);
		Task<LoginModel> ListarPorId(long id);
		Task<LoginModel> ListarPorEmail(EmailAddressAttribute email);
		Task<LoginModel> ListarPorEmailSenha(EmailAddressAttribute email, string senha);
		Task<LoginModel> Atualizar(LoginModel login);
		Task<bool> Apagar(long id);
	}
}
