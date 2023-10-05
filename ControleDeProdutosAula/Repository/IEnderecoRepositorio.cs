using ControleDeProdutosAula.Models;

namespace ControleDeProdutosAula.Repository
{
	public interface IEnderecoRepositorio
	{
		Task<List<EnderecoModel>> BuscarTodos();
		Task<EnderecoModel> Adicionar(EnderecoModel endereco);
		Task<EnderecoModel> ListarPorId(long id);
		Task<EnderecoModel> ListarPorCep(string cep);
		Task<EnderecoModel> Atualizar(EnderecoModel endereco);
		Task<bool> Apagar(long id);
	}
}
