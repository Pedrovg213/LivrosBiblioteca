using LivrosBiblioteca.ModelosVisuais.Autores;

namespace LivrosBiblioteca.Paginas.Autores;

public partial class AutoresListaPagina : ContentPage
{
	// VARIÁVEIS: private

	/// <summary>
	/// Modelo Visual com os controles e variáveis da página.
	/// </summary>
	private AutoresListaMV autoresListaMV;


	public AutoresListaPagina ()
	{
		InitializeComponent( );

		autoresListaMV = new AutoresListaMV( );
		BindingContext = autoresListaMV;
	}
}