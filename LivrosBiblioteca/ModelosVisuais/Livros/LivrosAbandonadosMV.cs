using LivrosBiblioteca.Enums;

namespace LivrosBiblioteca.ModelosVisuais.Livros;
internal class LivrosAbandonadosMV : LivrosListaBaseMV
{
	// CONSTRUTORES

	/// <summary>
	/// Inicialização padrão da classe.
	/// </summary>
	public LivrosAbandonadosMV () : base( LeituraSituacao.Abandonado )
	{
	}
}
