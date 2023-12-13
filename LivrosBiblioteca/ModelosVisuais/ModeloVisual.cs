using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LivrosBiblioteca.ModelosVisuais.Livros;
using Mopups.Pages;

namespace LivrosBiblioteca.ModelosVisuais;

public partial class ModeloVisual : ObservableObject
{
	// VARIÁVEIS: protected
	protected PopupPage EditarPage;


	// FUNÇÕES: private RelayCommand

	/// <summary>
	/// Excluir o objeto da base de dadose da lista de pertencimento.
	/// </summary>
	[RelayCommand]
	private void Excluir ()
	{
	}

	/// <summary>
	/// Edita as informações do objeto.
	/// </summary>
	[RelayCommand]
	private async Task Editar ()
	{
		if (this is LivroMV)
			await Application.Current.MainPage.DisplayAlert( "Alooo", "Aleeee", "Ok" );
	}
}
