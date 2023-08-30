using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ControleDeProdutosAula.Models
{
	public class LoginModel
	{
		[Key]
		public Int64 Id { get; set; }
		[Required]
		public EmailAddressAttribute EmailAddress { get; set; }

		[Required]
		public string Usuario { get; set; }
		
		[Required]
		public string Senha { get; set; }

		[MaybeNull]
		public int? NivelAcesso { get; set; }
		
		public int Ativo { get; set; }
		
		public bool? EmailConfirmado { get; set; }
		
		public bool? TelefoneConfirmado { get; set; }
	
		public DateTime DataDeRegistro { get; set; }
	}
}
