using LivrosBiblioteca.Enums;

namespace LivrosBiblioteca.ModelosVisuais.Livros;
public partial class LivrosFinalizadosMV : LivrosListaBaseMV
{
	// CONSTRUTORES

	/// <summary>
	/// Inicialização padrão da classe.
	/// </summary>
	public LivrosFinalizadosMV () : base( LeituraSituacao.Finalizado )
	{
	}
}
