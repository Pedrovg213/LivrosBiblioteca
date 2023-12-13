using CommunityToolkit.Mvvm.ComponentModel;

namespace LivrosBiblioteca.ModelosVisuais.Autores;

public partial class AutorMV : ModeloVisual
{
	// VARIÁVEIS: private ObservableProperty
	
	/// <summary>
	/// Nome do autor que escreveu livros.
	/// </summary>
	[ObservableProperty]
	private string nome;


	// FUNÇÕES: public

	/// <summary>
	/// Pegar o nome do autor.
	/// </summary>
	/// <returns>Nome (string) do autor.</returns>
	public string PegarNome () =>
		Nome;
}
