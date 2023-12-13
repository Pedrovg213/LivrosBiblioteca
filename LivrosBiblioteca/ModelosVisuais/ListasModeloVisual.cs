using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Pages;
using Mopups.Services;

namespace LivrosBiblioteca.ModelosVisuais;

public partial class ListasModeloVisual : ObservableObject
{
	protected PopupPage AdicionarPage;

	[RelayCommand]
	protected async Task AbrirAdicionarPopUp () =>
		await MopupService.Instance.PushAsync( AdicionarPage );
}
