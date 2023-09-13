using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ControleDeProdutosAula.Models
{
	public class LoginModel
	{
		public Int64 Id { get; set; }
		[Required]
		public string Email { get; set; }

		[Required]
		public string Usuario { get; set; }
		
		[Required]
		public string Senha { get; set; }

		public int NivelAcesso { get; set; }
		
		public int Ativo { get; set; }
		
		public bool? EmailConfirmado { get; set; }
		
		public bool? TelefoneConfirmado { get; set; }
	
		public DateTime DataDeRegistro { get; set; }
	}
}
