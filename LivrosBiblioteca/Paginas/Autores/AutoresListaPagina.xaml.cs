using LivrosBiblioteca.ModelosVisuais.Autores;

namespace LivrosBiblioteca.Paginas.Autores;

public partial class AutoresListaPagina : ContentPage
{
	// VARI�VEIS: private

	/// <summary>
	/// Modelo Visual com os controles e vari�veis da p�gina.
	/// </summary>
	private AutoresListaMV autoresListaMV;


	public AutoresListaPagina ()
	{
		InitializeComponent( );

		autoresListaMV = new AutoresListaMV( );
		BindingContext = autoresListaMV;
	}
}