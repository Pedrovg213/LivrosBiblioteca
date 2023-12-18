using LivrosBiblioteca.Entidades;

namespace LivrosBiblioteca.ModelosVisuais.Livros;

public partial class LivroMV : ModeloVisual
{
	// VARIÁVEIS: private

	/// <summary>
	/// Livro que tem as informações a serem tratadas.
	/// </summary>
	private Livro livro;

	/// <summary>
	/// Página que contém todos os livros.
	/// </summary>
	private LivrosListaMV livrosListaMV;


	// CONSTRUTURES: public
	public LivroMV ( Livro livro, LivrosListaMV livrosListaMV )
	{
		this.livro = livro;
		this.livrosListaMV = livrosListaMV;
	}
}
