using LivrosBiblioteca.ModelosVisuais.Livros;

namespace LivrosBiblioteca.Paginas.Livros;

public class LivrosIniciadosPagina : LivrosListaPagina
{
	public LivrosIniciadosPagina () : base( )
	{
		livrosListaMV = new LivrosIniciadosMV( );
		BindingContext = livrosListaMV;
	}
}
