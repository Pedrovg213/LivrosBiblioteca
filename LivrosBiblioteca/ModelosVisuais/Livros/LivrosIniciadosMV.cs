using LivrosBiblioteca.Enums;

namespace LivrosBiblioteca.ModelosVisuais.Livros;
public partial class LivrosIniciadosMV : LivrosListaBaseMV
{
	// CONSTRUTORES

	/// <summary>
	/// Inicialização padrão da classe.
	/// </summary>
	public LivrosIniciadosMV () : base( LeituraSituacao.Iniciado )
	{
	}
}
