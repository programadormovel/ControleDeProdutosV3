using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ControleDeProdutosAula.Models
{
	[Index(nameof(CodigoDeBarras), IsUnique = true)]
	public class ProdutoModel

	{
		public Int64 Id { get; set; }

		[Required(ErrorMessage = "Campo Obrigatório")]
		public string Descricao { get; set; } = string.Empty;

		[StringLength(12, MinimumLength = 12, ErrorMessage = "Mínimo de 12 caracteres!")]
		[Required(ErrorMessage = "Campo Obrigatório")]
		public string CodigoDeBarras { get; set; } = string.Empty;

		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		[DataType(DataType.Date)]
		public DateTime DataDeValidade { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime DataDeRegistro { get; set; }

		[Range(1, 1000)]
		public int Quantidade { get; set; }

		[DataType(DataType.Currency)]
		[Column(TypeName = "decimal(18, 2)")]
		public Decimal Valor { get; set; }

		[MaybeNull]
		public string? NomeDaFoto { get; set; }

		[MaybeNull]
		public byte[]? Foto { get; set; }

		[Required(ErrorMessage = "Campo Obrigatório")]
		public bool Ativo { get; set; }
	}
}
