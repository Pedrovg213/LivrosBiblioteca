using LivrosBiblioteca.ModelosVisuais.Livros;

namespace LivrosBiblioteca.Paginas.Livros;

public class LivrosFinalizadosPagina : LivrosListaPagina
{
	public LivrosFinalizadosPagina () : base( )
	{
		livrosListaMV = new LivrosFinalizadosMV( );
		BindingContext = livrosListaMV;
	}
}