using LivrosBiblioteca.Enums;

namespace LivrosBiblioteca.ModelosVisuais.Livros;

public partial class LivrosAguardandoMV : LivrosListaBaseMV
{
	// CONSTRUTORES

	/// <summary>
	/// Inicialização padrão da classe.
	/// </summary>
	public LivrosAguardandoMV () : base( LeituraSituacao.Aguardando )
	{
	}
}