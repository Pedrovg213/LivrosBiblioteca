using LivrosBiblioteca.Entidades;
using LivrosBiblioteca.Extensões;
using LivrosBiblioteca.Servicos;
using Mopups.Services;

namespace LivrosBiblioteca.PopUps;

public partial class AdicionarLivroPopUp
{
	// VARIÁVEIS: private
	private string livroCaminho;
	private List<ListView> autoresListViews = new List<ListView>();
	private List<SearchBar> barrasDeNomes = new List<SearchBar>();
	private List<VerticalStackLayout> stackNovosAutores = new List<VerticalStackLayout>();
	private List<AutorInfo> autoresInfo = new List<AutorInfo>();


	// CLASSES: private
	private class AutorInfo
	{
		// VARIÁVEIS: private
		private SearchBar nome;
		private Entry nascimentoData;
		private Entry morteData;


		// CONSTRUTORES: public
		public AutorInfo ( SearchBar nome, Entry nascimentoData, Entry morteData, Livro livro )
		{
			this.nome = nome;
			this.nascimentoData = nascimentoData;
			this.morteData = morteData;
		}


		/// <summary>
		/// Converte as informações de autor para a classe Autor.
		/// </summary>
		public Autor ConverterParaAutor ()
		{
			DateTime nascimento = nascimentoData.Text.ConverterParaDateTime();

			if (string.IsNullOrEmpty( morteData.Text ))
			{
				DateTime morte = morteData.Text.ConverterParaDateTime();

				return new Autor( nome.Text, nascimento, morte );
			}

			return new Autor( nome.Text, nascimento );
		}
	}


	// CONSTRUTORES

	/// <summary>
	/// Inicialização dos componentes.
	/// </summary>
	public AdicionarLivroPopUp ()
	{
		InitializeComponent( );

		autoresListViews.Add( searchResults );
		barrasDeNomes.Add( searchBar );
	}


	// FUNÇÕES: private Elementos
	private void SearchBar_TextChanged ( object sender, TextChangedEventArgs e )
	{
		SearchBar searchBar = (SearchBar)sender;

		int index = barrasDeNomes.IndexOf(searchBar);

		autoresListViews[index].ItemsSource = DataBase.ProcurarAutoresPorNome( searchBar.Text );
	}

	private void DateEntry_TextChanged ( object sender, TextChangedEventArgs e )
	{
		char[] caracteresPermitidos =
		{
			'1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
		};
		Entry entry = (Entry)sender;
		string velhoText = e.OldTextValue;
		string novoTexto = e.NewTextValue;

		// Se nem o novo nem o antigo texto for nulo ou estiver vazio
		if (!string.IsNullOrEmpty( velhoText ) && !string.IsNullOrEmpty( novoTexto ))
		{
			// Se o texto novo é menor que o texto velho e o último caracter do novo texto é '/'
			if (novoTexto.Length < velhoText.Length && novoTexto.Last( ) == '/')
				novoTexto = novoTexto.Remove( novoTexto.Length - 1 );

			// Se os últimos dígitos do texto velho e novo NÃO são '/'
			else if (velhoText.Last( ) != '/' && novoTexto.Last( ) != '/')
			{
				if (novoTexto.Length == 3 && velhoText.Length == 2)
					novoTexto = novoTexto.Insert( 2, "/" );

				else if (novoTexto.Length == 6 && velhoText.Length == 5)
					novoTexto = novoTexto.Insert( 5, "/" );
			}
		}

		for (int i = 0 ; i < novoTexto.Length ; i++)
		{
			char charCheck = novoTexto[i];

			if (i == 2 || i == 5 && charCheck == '/')
				continue;

			if (i < 10 && !caracteresPermitidos.Contains( charCheck ))
			{
				novoTexto = novoTexto.Remove( i );
				i--;
				continue;
			}

			if (i > 9)
			{
				switch (i)
				{
					case 10:
						if (novoTexto[i] != ' ')
							novoTexto = novoTexto.Remove( 10 );
						break;
					case 11:
						if (novoTexto.ToLower( )[i] != 'a' && novoTexto.ToLower( )[i] != 'd')
							novoTexto = novoTexto.Remove( 11 );
						break;
					case 12:
						if (novoTexto.ToLower( )[i] != 'c')
							novoTexto = novoTexto.Remove( 12 );
						break;
				}

				continue;
			}


			if (i > 12)
			{
				novoTexto = novoTexto.Remove( i );
				i--;
			}


		}

		entry.Text = novoTexto;
	}

	private void Entry_Unfocused ( object sender, FocusEventArgs e )
	{
		Entry entry = (Entry)sender;

		if (string.IsNullOrEmpty( entry.Text ))
			return;

		string texto = entry.Text.Trim().ToLower();
		bool textoValido = texto.CheckDataValida().Result;

		if (!textoValido)
			entry.Focus( );
	}

	private async void AdicionarAutor_BtnClick ( object sender, EventArgs e )
	{
		VerticalStackLayout autor = new VerticalStackLayout();

		autorSLTGeral.Add( autor );
		stackNovosAutores.Add( autor );

		// NOME DO AUTOR
		VerticalStackLayout nomeAutorVSL = new VerticalStackLayout();

		nomeAutorVSL.VerticalOptions = LayoutOptions.End;

		autor.Add( nomeAutorVSL );

		// NUMERAÇÃO DO AUTOR
		Label numLbl = new Label();

		numLbl.Text = $"Autor {(autoresListViews.Count + 1).ToString( "00" )}";
		numLbl.HorizontalOptions = LayoutOptions.Center;
		numLbl.HorizontalTextAlignment = TextAlignment.Center;

		nomeAutorVSL.Add( numLbl );

		// BARRA DE PESQUISA DO NOME DO AUTOR
		SearchBar nomeAutorSBR = new SearchBar();

		nomeAutorSBR.HorizontalTextAlignment = TextAlignment.Center;
		nomeAutorSBR.Placeholder = "Nome do Autor";
		nomeAutorSBR.TextChanged += SearchBar_TextChanged;

		barrasDeNomes.Add( nomeAutorSBR );

		nomeAutorVSL.Add( nomeAutorSBR );

		// LISTA DE VISUALIZAÇÃO DOS NOMES DOS AUTORES
		ListView nomeAutorLVW = new ListView();

		nomeAutorLVW.WidthRequest = 0;

		autoresListViews.Add( nomeAutorLVW );

		nomeAutorVSL.Add( nomeAutorLVW );


		// GRID NASCIMENTO + MORTE
		Grid gridDatas = new Grid();

		gridDatas.ColumnDefinitions = gridRef.ColumnDefinitions;
		gridDatas.ColumnSpacing = 15;
		gridDatas.HorizontalOptions = LayoutOptions.Fill;

		autor.Add( gridDatas );


		// NASCIMENTO
		VerticalStackLayout nascimentoVSL = new VerticalStackLayout();

		nascimentoVSL.HorizontalOptions = LayoutOptions.Fill;
		nascimentoVSL.VerticalOptions = LayoutOptions.Start;
		Grid.SetColumn( nascimentoVSL, 0 );

		gridDatas.Add( nascimentoVSL );

		Label nascimentoLbl = new Label();

		nascimentoLbl.Text = "Nascimento:";
		nascimentoLbl.HorizontalOptions = LayoutOptions.Center;

		nascimentoVSL.Add( nascimentoLbl );

		Entry nascimentoEty = new Entry();

		nascimentoEty.Placeholder = "dd/mm/aaaa ac";
		nascimentoEty.HorizontalOptions = LayoutOptions.Fill;
		nascimentoEty.HorizontalTextAlignment = TextAlignment.Center;
		nascimentoEty.TextChanged += DateEntry_TextChanged;
		nascimentoEty.Unfocused += Entry_Unfocused;

		nascimentoVSL.Add( nascimentoEty );


		// MORTE
		VerticalStackLayout morteVSL = new VerticalStackLayout();

		morteVSL.HorizontalOptions = LayoutOptions.Fill;
		morteVSL.VerticalOptions = LayoutOptions.Start;
		Grid.SetColumn( morteVSL, 1 );

		gridDatas.Add( morteVSL );

		Label morteLbl = new Label();

		morteLbl.Text = "Morte:";
		morteLbl.HorizontalOptions = LayoutOptions.Center;

		morteVSL.Add( morteLbl );

		Entry morteEty = new Entry();

		morteEty.Placeholder = "dd/mm/aaaa ac";
		morteEty.HorizontalOptions = LayoutOptions.Fill;
		morteEty.HorizontalTextAlignment = TextAlignment.Center;
		morteEty.TextChanged += DateEntry_TextChanged;
		morteEty.Unfocused += Entry_Unfocused;

		morteVSL.Add( morteEty );

		autor01Lbl.IsVisible = autoresListViews.Count > 1;

		await autorSVw.ScrollToAsync( autorSLTGeral, ScrollToPosition.Start, true );
	}

	private async void ProcurarArquivo_BtnClick ( object sender, EventArgs e )
	{
		FilePickerFileType fileType = new (new Dictionary<DevicePlatform, IEnumerable<string>>
		{
			{DevicePlatform.WinUI, new []{".pdf", ".txt", ".docx", ".doc"} },
		});
		PickOptions pickOptions = new PickOptions();

		PickOptions options = new PickOptions()
		{
			FileTypes = fileType
		};

		FileResult result = await FilePicker.Default.PickAsync(options);

		if (string.IsNullOrEmpty( result.FullPath ))
		{
			livroCaminho = string.Empty;
			return;
		}

		livroCaminho = result.FullPath;
	}

	private void Limpar_BtnClick ( object sender, EventArgs e )
	{
		tituloEty.Text = string.Empty;
		tituloEty.Focus( );

		lancamentoEty.Text = string.Empty;

		autoresListViews.Clear( );
		autoresListViews.Add( searchResults );

		barrasDeNomes.Clear( );
		barrasDeNomes.Add( searchBar );

		foreach (VerticalStackLayout verticalSLT in stackNovosAutores)
			autorSLTGeral.Remove( verticalSLT );

		stackNovosAutores.Clear( );

		autor01Lbl.IsVisible = autoresListViews.Count > 1;
	}

	private async void Cancelar_BtnClick ( object sender, EventArgs e )
	{
		if (MopupService.Instance.PopupStack.Count > 0)
			await MopupService.Instance.PopAsync( );
	}

	private void Ok_BtnClick ( object sender, EventArgs e )
	{

		Cancelar_BtnClick( sender, e );
	}
}