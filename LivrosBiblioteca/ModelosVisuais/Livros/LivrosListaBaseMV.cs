using CommunityToolkit.Mvvm.ComponentModel;
using LivrosBiblioteca.Entidades;
using LivrosBiblioteca.Enums;
using LivrosBiblioteca.Extensoes;
using LivrosBiblioteca.PopUps;
using LivrosBiblioteca.Servicos;
using System.Collections.ObjectModel;

namespace LivrosBiblioteca.ModelosVisuais.Livros;
public partial class LivrosListaBaseMV : ListasModeloVisual
{
	private LeituraSituacao leituraSituacao;

	// VARIÁVEIS: protected ObservableProperty

	/// <summary>
	/// Coleção dos livros que comporão a página.
	/// </summary>
	[ObservableProperty]
	protected ObservableCollection<LivroMV> livrosColecao;


	// CONSTRUTORES: protected

	/// <summary>
	/// Construtor padrão da classe.
	/// </summary>
	protected LivrosListaBaseMV ( LeituraSituacao leituraSituacao )
	{
		this.leituraSituacao = leituraSituacao;
		ResetarAdicionarLivroPopUp( );
	}



	// FUNÇÕES: private

	/// <summary>
	/// Adiciona novo livro na coleção de livros da página.
	/// </summary>
	/// <param name="livro">Livro a ser adicionado à página.</param>
	private void AdicionarLivro ( Livro livro )
	{
		LivroMV livroMV = new LivroMV(livro, this);

		LivrosColecao.Add( livroMV );

		ReordenarLivros( );

		DataBase.AdicionarLivro( livro );
	}

	/// <summary>
	/// Função executada quando o popup de adicionar livro for descarregado.
	/// </summary>
	private void AdicionarLivroPopUp_Unloaded ( object _sender, EventArgs _e ) =>
		ResetarAdicionarLivroPopUp( );

	/// <summary>
	/// Reinicia o estado do PopUp de adição de novo livro
	/// </summary>
	private void ResetarAdicionarLivroPopUp ()
	{
		AdicionarPage = new AdicionarLivroPopUp( AdicionarLivro, leituraSituacao );

		AdicionarPage.Unloaded += AdicionarLivroPopUp_Unloaded;
	}

	/// <summary>
	/// Reorganiza a sequência dos livros com base no fato de a organização dever acontecer em ordem alfabética ou por data.
	/// </summary>
	private void ReordenarLivros ()
	{
		if (ordemAlfabetica)
			LivrosColecao = LivrosColecao.OrderByObservableCollection( l => l.PegarTitulo( ) );
		else
			LivrosColecao = LivrosColecao.OrderByObservableCollection( l => l.PegarLancamento( ) );
	}
}