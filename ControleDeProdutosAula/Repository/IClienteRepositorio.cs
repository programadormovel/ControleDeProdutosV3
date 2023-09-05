using ControleDeProdutosAula.Models;

namespace ControleDeProdutosAula.Repository
{
	public interface IClienteRepositorio
	{
		Task<List<ClienteModel>> BuscarTodos();
		Task<ClienteModel> Adicionar(ClienteModel cliente);
		Task<ClienteModel> ListarPorId(long id);
		Task<ClienteModel> Atualizar(ClienteModel cliente);
		Task<bool> AtivarDesativar(long id);
		Task<bool> Apagar(long id);
	}
}
