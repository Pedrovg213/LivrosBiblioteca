using CommunityToolkit.Mvvm.ComponentModel;
using LivrosBiblioteca.Entidades;
using LivrosBiblioteca.Enums;
using LivrosBiblioteca.Extensoes;
using LivrosBiblioteca.ModelosVisuais.Autores;
using LivrosBiblioteca.Servicos;
using System.Collections.ObjectModel;

namespace LivrosBiblioteca.ModelosVisuais.Livros;

public partial class LivroMV : ModeloVisual
{
	// VARIÁVEIS: private

	/// <summary>
	/// Livro que tem as informações a serem tratadas.
	/// </summary>
	private Livro livro;

	/// <summary>
	/// Página que contém todos os livros.
	/// </summary>
	private LivrosListaBaseMV livrosListaMV;

	// VARIÁVEIS: private ObservableProperty

	/// <summary>
	/// Título do livro.
	/// </summary>
	[ObservableProperty]
	private string titulo;

	/// <summary>
	/// Situação atual de leitura do livro <br></br>
	///	1. Aguardando: livro ainda não teve sua leitura iniciado; <br></br>
	///	2. Iniciado: livro começou a ser lido; <br></br>
	///	3. Finalizado: a leitura do livro foi encerrada; <br></br>
	///	4. Desistência: Não há mais intenção de continuidade na leitura do livro.
	/// </summary>
	[ObservableProperty]
	private LeituraSituacao situacao;

	/// <summary>
	/// Caminho no sistema para o arquivo correspondênte ao livro.
	/// </summary>
	[ObservableProperty]
	private string arquivo;

	/// <summary>
	/// Lista de autores que escreveram o livro.
	/// </summary>
	[ObservableProperty]
	private ObservableCollection<AutorMV> autores;


	// CONSTRUTURES: public
	public LivroMV ( Livro livro, LivrosListaBaseMV livrosListaMV )
	{
		this.livro = livro;
		this.livrosListaMV = livrosListaMV;

		SetarPropriedades( );
	}

	// FUNÇÕES: private
	private void SetarPropriedades ()
	{
		Titulo = livro.PegarTitulo( );
		Situacao = livro.PegarSituação( );
		Arquivo = livro.PegarArquivo( );
		IEnumerable<Autor> autores = DataBase.PegarAutores( )
			.Where( a => livro.PegarAutoresIds( ).Contains( a.PegarId( ) ) );

		ObservableCollection<AutorMV> autorMVs = new ObservableCollection<AutorMV>();

		foreach (Autor autor in autores)
			autorMVs.Add( new AutorMV( autor ) );

		if (livrosListaMV.PegarOrdemAlfabetica( ))
			Autores = autorMVs.OrderByObservableCollection( a => a.PegarNome( ) );
		else
			Autores = autorMVs.OrderByObservableCollection( a => a.PegarNascimento( ) );
	}

	// FUNÇÕES: public

	/// <summary>
	/// Pegar data de lançamento do livro.
	/// </summary>
	/// <returns>Lançamento (DateTime) do livro.</returns>
	public int PegarLancamento () =>
		livro.PegarLancamento( );

	/// <summary>
	/// Pegar título do livro.
	/// </summary>
	/// <returns>Titulo (string) do livro</returns>
	public string PegarTitulo () =>
		Titulo;

	/// <summary>
	/// Pegar a situação atual de leitura do livro.
	/// </summary>
	/// <returns>Situação atual (enum) de leitura do livro.</returns>
	public LeituraSituacao PegarSituacao () =>
		Situacao;

	// FUNÇÕES: public override

	public override bool Equals ( object obj )
	{
		LivroMV outro = (LivroMV)obj;

		return livro.Equals( outro.livro );
	}

	public override int GetHashCode () =>
		livro.GetHashCode( );
}
