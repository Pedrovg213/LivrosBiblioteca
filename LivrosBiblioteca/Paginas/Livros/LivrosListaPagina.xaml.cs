using LivrosBiblioteca.ModelosVisuais.Livros;

namespace LivrosBiblioteca.Paginas.Livros;

public partial class LivrosListaPagina : ContentPage
{
	// VARIÁVEIS: private

	/// <summary>
	/// Modelo Visual com os controles e variáveis da página
	/// </summary>
	private LivrosListaMV livrosListaMV;


	// CONTRUTORES

	/// <summary>
	/// Construtor padrão.
	/// </summary>
	public LivrosListaPagina ()
	{
		InitializeComponent( );

		livrosListaMV = new LivrosListaMV( );
		BindingContext = livrosListaMV;
	}
}