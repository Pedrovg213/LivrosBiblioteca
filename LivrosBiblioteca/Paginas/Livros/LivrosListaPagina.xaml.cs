using LivrosBiblioteca.ModelosVisuais.Livros;

namespace LivrosBiblioteca.Paginas.Livros;

public partial class LivrosListaPagina : ContentPage
{
	/// <summary>
	/// Modelo Visual com os controles e variáveis da página
	/// </summary>
	protected LivrosListaBaseMV livrosListaMV;


	// CONTRUTORES

	/// <summary>
	/// Construtor padrão.
	/// </summary>
	public LivrosListaPagina () =>
		InitializeComponent( );
}