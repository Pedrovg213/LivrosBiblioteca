using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Pages;
using Mopups.Services;

namespace LivrosBiblioteca.ModelosVisuais;

public partial class ListasModeloVisual : ObservableObject
{
	// VARIÁVEIS: protected

	/// <summary>
	/// Os livros estão sendo organizados em ordem alfabética?
	/// </summary>
	protected bool ordemAlfabetica = true;

	/// <summary>
	/// PopUp de adição de um novo elemento.
	/// </summary>
	protected PopupPage AdicionarPage;


	// FUNÇÕES: protected RelayCommand
	/// <summary>
	/// Função de abertura de PopUp onde será adicionado um novo elemento.
	/// </summary>
	/// <returns></returns>
	[RelayCommand]
	protected async Task AbrirAdicionarPopUp () =>
		await MopupService.Instance.PushAsync( AdicionarPage );


	// FUNÇÕES: public

	/// <summary>
	/// Pega a identificação de se a página deve ou não ser organizada em ordem alfabética ou por data.
	/// </summary>
	public bool PegarOrdemAlfabetica () =>
		ordemAlfabetica;
}
