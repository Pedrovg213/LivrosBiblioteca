using LivrosBiblioteca.ModelosVisuais.Livros;

namespace LivrosBiblioteca.Paginas.Livros;

public class LivrosAbandonadosPagina : LivrosListaPagina
{
	public LivrosAbandonadosPagina () : base( )
	{
		livrosListaMV = new LivrosAguardandoMV( );
		BindingContext = livrosListaMV;
	}
}