using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace LivrosBiblioteca.ModelosVisuais.Autores;
public partial class AutoresListaMV : ListasModeloVisual
{
	// VARIÁVEIS: private ObservableProperty
	/// <summary>
	/// Lista com todos os autores salvos.
	/// </summary>
	[ObservableProperty]
	private ObservableCollection<AutorMV> autoresMV;



	// CONSTRUTORES

	/// <summary>
	/// Construtor padrão.
	/// </summary>
	public AutoresListaMV ()
	{
	}
}
