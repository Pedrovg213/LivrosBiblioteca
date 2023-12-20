using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosBiblioteca.Servicos;
public static class Erros
{
	// FUNÇÕES: public async static

	/// <summary>
	/// Lança, em PopUp, uma mensagem de erro.
	/// </summary>
	/// <param name="titulo">Título apresentado no popup</param>
	/// <param name="messagem">Mensagem apresentada no popup</param>
	public async static void LancarErro ( string titulo, string messagem ) =>
		await Application.Current.MainPage.DisplayAlert( titulo, messagem, "Ok" );
}