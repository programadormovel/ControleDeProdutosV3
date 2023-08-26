using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;

namespace ControleDeProdutosAula.Models
{
	public class ClienteModel
	{
		public Int32 Id { get; set; }

		//[RegularExpression(@"^A-Za-z\s]*$", ErrorMessage = "Nome não pode conter caracteres especiais e/ou números")]
		[Required]
		public string Nome { get; set; }
		
		//[RegularExpression(@"^[A-Z]+[a-zA-Z\D]*$", ErrorMessage = "Sobrenome não pode conter caracteres especiais e/ou números")]
		[Required]
		public string Sobrenome { get; set; }

		[StringLength(3, MinimumLength = 2, ErrorMessage = "Mínimo de 2 digitos!")]
		[Required]
		public string DDD { get; set; }

		[StringLength(10, MinimumLength = 9, ErrorMessage = "Entre 9 e 10 dígitos")]
		[Required]
		public string Telefone { get; set; }

		[Required]
		public string CEP { get; set; }

		[DataType(DataType.DateTime)]
		[Required]
		public DateTime DataDeRegistro { get; internal set; }

		[MaybeNull]
		public string NomeDaFoto { get; set; }

		[MaybeNull]
		public byte[] Foto { get; set; }

		[Required]
		public bool Ativo { get; set; }

	}
}
