using CommunityToolkit.Mvvm.ComponentModel;
using LivrosBiblioteca.Entidades;

namespace LivrosBiblioteca.ModelosVisuais.Autores;

public partial class AutorMV : ModeloVisual
{
	// VARIÁVEIS: private
	private Autor autor;

	// VARIÁVEIS: private ObservableProperty

	/// <summary>
	/// Nome do autor que escreveu livros.
	/// </summary>
	[ObservableProperty]
	private string nome;

	/// <summary>
	/// Dia, mês e ano de nascimento do autor.
	/// </summary>
	[ObservableProperty]
	private DateTime nascimento;


	// CONSTRUTORES: public
	public AutorMV ( Autor autor )
	{
		this.autor = autor;
	}

	// FUNÇÕES: public

	/// <summary>
	/// Pegar a data de nascimento do autor.
	/// </summary>
	/// <returns>Data de nascimento (DateTime) do autor.</returns>
	public DateTime PegarNascimento () =>
		Nascimento;

	/// <summary>
	/// Pegar o nome do autor.
	/// </summary>
	/// <returns>Nome (string) do autor.</returns>
	public string PegarNome () =>
		Nome;
}
