using LivrosBiblioteca.ModelosVisuais.Livros;

namespace LivrosBiblioteca.Paginas.Livros;

public class LivrosAguardandoPagina : LivrosListaPagina
{
	public LivrosAguardandoPagina() : base()
	{
		livrosListaMV = new LivrosAguardandoMV( );
		BindingContext = livrosListaMV;
	}
}