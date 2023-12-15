using CommunityToolkit.Mvvm.ComponentModel;
using LivrosBiblioteca.PopUps;
using System.Collections.ObjectModel;

namespace LivrosBiblioteca.ModelosVisuais.Livros;

public partial class LivrosListaMV : ListasModeloVisual
{
	[ObservableProperty]
	private ObservableCollection<LivroMV> livrosEsperando;

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
	/// Reinicia os status do popup de adicionar livro.
	/// </summary>
	private void ResetarAdicionarLivroPopUp ()
	{
		AdicionarPage = new AdicionarLivroPopUp( );

		AdicionarPage.Unloaded += AdicionarLivroPopUp_Unloaded;
	}

	// FUNÇÕES: private Elementos

	/// <summary>
	/// Função executada quando o popup de adicionar livro for descarregado.
	/// </summary>
	private void AdicionarLivroPopUp_Unloaded ( object _sender, EventArgs _e ) =>
		ResetarAdicionarLivroPopUp( );
}
