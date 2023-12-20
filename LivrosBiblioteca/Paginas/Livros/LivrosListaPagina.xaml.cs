using LivrosBiblioteca.ModelosVisuais.Livros;

namespace LivrosBiblioteca.Paginas.Livros;

public partial class LivrosListaPagina : ContentPage
{
	/// <summary>
	/// Modelo Visual com os controles e vari�veis da p�gina
	/// </summary>
	protected LivrosListaBaseMV livrosListaMV;


	// CONTRUTORES

	/// <summary>
	/// Construtor padr�o.
	/// </summary>
	public LivrosListaPagina () =>
		InitializeComponent( );
}