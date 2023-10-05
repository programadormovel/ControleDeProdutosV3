namespace ControleDeProdutosAula.Models
{
	public class EnderecoModel
	{
		public Int32 Id { get; set; }
		public String cep { get; set; }
		public String logradouro { get; set; }
		public String bairro { get; set; }
		public String cidade { get; set; }
		public Int32? ClienteId { get; set; }
		public ClienteModel? Cliente { get; set; }
	}
}
