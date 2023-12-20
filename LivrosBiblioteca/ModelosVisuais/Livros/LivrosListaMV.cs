using CommunityToolkit.Mvvm.ComponentModel;
using LivrosBiblioteca.Entidades;
using LivrosBiblioteca.Extensoes;
using LivrosBiblioteca.PopUps;
using System.Collections.ObjectModel;

namespace LivrosBiblioteca.ModelosVisuais.Livros;

public partial class LivrosListaMV : ListasModeloVisual
{
	// VARIÁVEIS: private

	/// <summary>
	/// Os livros estão sendo organizados em ordem alfabética?
	/// </summary>
	private bool ordemAlfabetica = true;

	// VARIÁVEIS: private ObservableProperty

	/// <summary>
	/// Coleção dos livros que estão esperando para serem lidos.
	/// </summary>
	[ObservableProperty]
	private ObservableCollection<LivroMV> livrosAguardando;

	/// <summary>
	/// Coleção dos lisvro que estão sendo lidos.
	/// </summary>
	[ObservableProperty]
	private ObservableCollection<LivroMV> livrosIniciados;

	/// <summary>
	/// Coleção dos livros que foram terminados de serem lidos.
	/// </summary>
	[ObservableProperty]
	private ObservableCollection<LivroMV> livrosFinalizados;

	/// <summary>
	/// Coleção dos livros que foram abandonados na leitura.
	/// </summary>
	[ObservableProperty]
	private ObservableCollection<LivroMV> livrosAbandonados;

	// CONSTRUTORES

	/// <summary>
	/// Inicialização padrão da classe.
	/// </summary>
	public LivrosListaMV ()
	{
		ResetarAdicionarLivroPopUp( );
	}


	// FUNÇÕES: private

	/// <summary>
	/// Adiciona livro da lista pertinente a sua situação de leitura.
	/// </summary>
	/// <param name="livro">Livro a ser dicionado.</param>
	private void AdicionarLivro ( Livro livro )
	{
		LivroMV livroMV = new LivroMV(livro, this);

		switch (livroMV.PegarSituacao( ))
		{
			case Enums.LeituraSituacao.Aguardando:
				LivrosAguardando.Add( livroMV );
				break;
			case Enums.LeituraSituacao.Iniciado:
				LivrosIniciados.Add( livroMV );
				break;
			case Enums.LeituraSituacao.Finalizado:
				LivrosFinalizados.Add( livroMV );
				break;
			case Enums.LeituraSituacao.Abandonado:
				LivrosAbandonados.Add( livroMV );
				break;
		}

		AtualizarColecoesLivros( livroMV );
	}

	private void AtualizarColecoesLivros ( LivroMV livroMV )
	{
		switch (livroMV.PegarSituacao( ))
		{
			case Enums.LeituraSituacao.Aguardando:

				LivrosIniciados.Remove( livroMV );
				LivrosFinalizados.Remove( livroMV );
				LivrosAbandonados.Remove( livroMV );

				ReordenarLivros( LivrosAguardando );

				break;

			case Enums.LeituraSituacao.Iniciado:

				LivrosAguardando.Remove( livroMV );
				LivrosFinalizados.Remove( livroMV );
				LivrosAbandonados.Remove( livroMV );

				ReordenarLivros( LivrosIniciados );

				break;

			case Enums.LeituraSituacao.Finalizado:

				LivrosAguardando.Remove( livroMV );
				LivrosIniciados.Remove( livroMV );
				LivrosAbandonados.Remove( livroMV );

				ReordenarLivros( LivrosFinalizados );

				break;

			case Enums.LeituraSituacao.Abandonado:

				LivrosAguardando.Remove( livroMV );
				LivrosIniciados.Remove( livroMV );
				LivrosFinalizados.Remove( livroMV );

				ReordenarLivros( LivrosAbandonados );

				break;
		}
	}

	/// <summary>
	/// Reinicia os status do popup de adicionar livro.
	/// </summary>
	private void ResetarAdicionarLivroPopUp ()
	{
		AdicionarPage = new AdicionarLivroPopUp( AdicionarLivro );

		AdicionarPage.Unloaded += AdicionarLivroPopUp_Unloaded;
	}

	/// <summary>
	/// Reordenar, em ordem alfabética ou por data, todas as coleções de livros.
	/// </summary>
	private void ReordenarLivros ()
	{
		ReordenarLivros( LivrosAguardando );
		ReordenarLivros( LivrosIniciados );
		ReordenarLivros( LivrosFinalizados );
		ReordenarLivros( LivrosAbandonados );
	}

	/// <summary>
	/// Reordenar, em ordem alfabética ou por data, a coleção de livros passada como parâmetro.
	/// </summary>
	/// <param name="colecaodeLivros">Coleção a ser reordenada.</param>
	private void ReordenarLivros ( ObservableCollection<LivroMV> colecaodeLivros )
	{
		int colecaoHashCode = colecaodeLivros.GetHashCode();

		// Livros Aguardando.
		if (colecaoHashCode == LivrosAguardando.GetHashCode( ))
		{
			if (ordemAlfabetica)
				LivrosAguardando = LivrosAguardando.OrderByObservableCollection( l => l.PegarTitulo( ) );
			else
				LivrosAguardando = LivrosAguardando.OrderByObservableCollection( l => l.PegarLancamento( ) );

		}
		// Livros Iniciados.
		else if (colecaoHashCode == LivrosIniciados.GetHashCode( ))
		{
			if (ordemAlfabetica)
				LivrosIniciados = LivrosIniciados.OrderByObservableCollection( l => l.PegarTitulo( ) );
			else
				LivrosIniciados = LivrosIniciados.OrderByObservableCollection( l => l.PegarLancamento( ) );

		}
		// Livros Finalizados.
		else if (colecaoHashCode == LivrosFinalizados.GetHashCode( ))
		{
			if (ordemAlfabetica)
				LivrosFinalizados = LivrosFinalizados.OrderByObservableCollection( l => l.PegarTitulo( ) );
			else
				LivrosFinalizados = LivrosFinalizados.OrderByObservableCollection( l => l.PegarLancamento( ) );

		}
		// Livros Abandonados.
		else if (colecaoHashCode == LivrosAbandonados.GetHashCode( ))
		{
			if (ordemAlfabetica)
				LivrosAbandonados = LivrosAbandonados.OrderByObservableCollection( l => l.PegarTitulo( ) );
			else
				LivrosAbandonados = LivrosAbandonados.OrderByObservableCollection( l => l.PegarLancamento( ) );

		}
	}

	// FUNÇÕES: private Elementos

	/// <summary>
	/// Função executada quando o popup de adicionar livro for descarregado.
	/// </summary>
	private void AdicionarLivroPopUp_Unloaded ( object _sender, EventArgs _e ) =>
		ResetarAdicionarLivroPopUp( );

	// FUNÇÕES: public

	/// <summary>
	/// Pega a identificação de se a página deve ou não ser organizada em ordem alfabética ou por data.
	/// </summary>
	public bool PegarOrdemAlfabetica () =>
		ordemAlfabetica;
}
