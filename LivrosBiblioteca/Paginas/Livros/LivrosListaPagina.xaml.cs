using LivrosBiblioteca.ModelosVisuais.Livros;

namespace LivrosBiblioteca.Paginas.Livros;

public partial class LivrosListaPagina : ContentPage
{
	// VARI�VEIS: private

	/// <summary>
	/// Modelo Visual com os controles e vari�veis da p�gina
	/// </summary>
	private LivrosListaMV livrosListaMV;


	// CONTRUTORES

	/// <summary>
	/// Construtor padr�o.
	/// </summary>
	public LivrosListaPagina ()
	{
		InitializeComponent( );

		livrosListaMV = new LivrosListaMV( );
		BindingContext = livrosListaMV;
	}
}