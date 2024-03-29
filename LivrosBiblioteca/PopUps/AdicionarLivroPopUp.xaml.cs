using LivrosBiblioteca.Entidades;
using LivrosBiblioteca.Enums;
using LivrosBiblioteca.Eventos;
using LivrosBiblioteca.Extensoes;
using LivrosBiblioteca.Servicos;
using Mopups.Services;

namespace LivrosBiblioteca.PopUps;

public partial class AdicionarLivroPopUp
{
	// VARI�VEIS: private

	/// <summary>
	/// Caminho, no sistema, do arquivo do livro.
	/// </summary>
	private string livroCaminho;

	/// <summary>
	/// Listas de barras de pesquisas de autores
	/// </summary>
	private List<ListView> autoresListViews = new List<ListView>();

	/// <summary>
	/// Lista de elementos que guardam os elementos que comp�em as entradas de novos autores.
	/// </summary>
	private List<VerticalStackLayout> stackNovosAutores = new List<VerticalStackLayout>();

	/// <summary>
	/// Lista de informa��es que devem ser usadas para compor os autores da obra.
	/// </summary>
	private List<AutorInfo> autoresInfo = new List<AutorInfo>();

	/// <summary>
	/// Situa��o de leitura de livro a ser adicionado.
	/// </summary>
	private LeituraSituacao situacao;


	// M�TODOS: private

	/// <summary>
	/// Evento de adi��o de livro na p�gina de livros.
	/// </summary>
	private EventAdicionarLivro AdicionarLivroEvento;


	// CLASSES: private
	private class AutorInfo
	{
		// VARI�VEIS: private
		public SearchBar nome;
		public Entry nascimentoData;
		public Entry morteData;


		// CONSTRUTORES: public
		public AutorInfo ( SearchBar nome, Entry nascimentoData, Entry morteData )
		{
			this.nome = nome;
			this.nascimentoData = nascimentoData;
			this.morteData = morteData;
		}


		/// <summary>
		/// Converte as informa��es de autor para a classe Autor.
		/// </summary>
		public Autor ConverterParaAutor (  )
		{
			DateTime nascimento = nascimentoData.Text.ConverterParaDateTime();

			if (!string.IsNullOrEmpty( morteData.Text ))
			{
				DateTime morte = morteData.Text.ConverterParaDateTime();
				Autor autorTemp = new Autor( nome.Text, nascimento, morte );

				DataBase.AdicionarAutor( autorTemp );

				return autorTemp;
			}

			Autor autor = new Autor( nome.Text, nascimento );

			DataBase.AdicionarAutor( autor );

			return autor;
		}
	}


	// CONSTRUTORES

	/// <summary>
	/// Inicializa��o dos componentes.
	/// </summary>
	public AdicionarLivroPopUp ( EventAdicionarLivro adicionarLivroEvento, LeituraSituacao leituraSituacao )
	{
		InitializeComponent( );

		autoresListViews.Add( searchResults );
		autoresInfo.Add( new( searchBar, nascimentoEty, morteEty ) );

		AdicionarLivroEvento = adicionarLivroEvento;
		situacao = leituraSituacao;
	}


	// FUN��ES: private Elementos

	/// <summary>
	/// Fun��o que � executada quando uma barra de pesquisa tem seu texto alterado.
	/// </summary>
	private void SearchBar_TextChanged ( object sender, TextChangedEventArgs e )
	{
		SearchBar searchBar = (SearchBar)sender;

		int index = autoresInfo.Select(a => a.nome).ToList().IndexOf(searchBar);

		autoresListViews[index].ItemsSource = DataBase.ProcurarAutoresPorNome( searchBar.Text );
	}

	/// <summary>
	/// Fun��o que for�a os espa�os de entradas de datas a manterem um padr�o (dd/mm/yyyy)
	/// </summary>
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
			// Se o texto novo � menor que o texto velho e o �ltimo caracter do novo texto � '/'
			if (novoTexto.Length < velhoText.Length && novoTexto.Last( ) == '/')
				novoTexto = novoTexto.Remove( novoTexto.Length - 1 );

			// Se os �ltimos d�gitos do texto velho e novo N�O s�o '/'
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

	/// <summary>
	/// Quando uma entrada de data � desfocada.
	/// </summary>
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

	/// <summary>
	/// Fun��o acionada quando o bot�o de adicionar novo autor � clicado.
	/// </summary>
	private void AdicionarAutor_BtnClick ( object sender, EventArgs e )
	{
		VerticalStackLayout autorSlt = new VerticalStackLayout();

		autorSLTGeral.Add( autorSlt );
		stackNovosAutores.Add( autorSlt );

		// NOME DO AUTOR
		VerticalStackLayout nomeAutorVSL = new VerticalStackLayout();

		nomeAutorVSL.VerticalOptions = LayoutOptions.End;

		autorSlt.Add( nomeAutorVSL );

		// NUMERA��O DO AUTOR
		Label numLbl = new Label();

		numLbl.Text = $"Autor {(autoresInfo.Count + 1).ToString( "00" )}";
		numLbl.HorizontalOptions = LayoutOptions.Center;
		numLbl.HorizontalTextAlignment = TextAlignment.Center;

		nomeAutorVSL.Add( numLbl );

		// BARRA DE PESQUISA DO NOME DO AUTOR
		SearchBar nomeAutorSBR = new SearchBar();

		nomeAutorSBR.HorizontalTextAlignment = TextAlignment.Center;
		nomeAutorSBR.Placeholder = "Nome do Autor";
		nomeAutorSBR.TextChanged += SearchBar_TextChanged;

		nomeAutorVSL.Add( nomeAutorSBR );

		// LISTA DE VISUALIZA��O DOS NOMES DOS AUTORES
		ListView nomeAutorLVW = new ListView();

		nomeAutorLVW.WidthRequest = 0;

		autoresListViews.Add( nomeAutorLVW );

		nomeAutorVSL.Add( nomeAutorLVW );


		// GRID NASCIMENTO + MORTE
		Grid gridDatas = new Grid();

		gridDatas.ColumnDefinitions = gridRef.ColumnDefinitions;
		gridDatas.ColumnSpacing = 15;
		gridDatas.HorizontalOptions = LayoutOptions.Fill;

		autorSlt.Add( gridDatas );


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

		autoresInfo.Add( new AutorInfo( nomeAutorSBR, nascimentoEty, morteEty ) );

		nomeAutorSBR.Focus( );
	}

	/// <summary>
	/// Fun��o acionada quando o bot�o de procurar pelo arquivo do livro no sistema � clicado.
	/// </summary>
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

		if (result != null && string.IsNullOrEmpty( result.FullPath ))
		{
			livroCaminho = string.Empty;
			return;
		}

		livroCaminho = result.FullPath;
	}

	/// <summary>
	/// Fun��o acionada quando o bot�o de limpar as informa��es for clicado.
	/// </summary>
	private void Limpar_BtnClick ( object sender, EventArgs e )
	{
		tituloEty.Text = string.Empty;
		tituloEty.Focus( );

		lancamentoEty.Text = string.Empty;

		foreach (VerticalStackLayout verticalSLT in stackNovosAutores)
			autorSLTGeral.Remove( verticalSLT );

		stackNovosAutores.Clear( );

		for (int i = 1 ; i < autoresInfo.Count ; i++)
		{
			autoresInfo.RemoveAt( i );
			autoresListViews.RemoveAt( i );
		}

		autor01Lbl.IsVisible = autoresListViews.Count > 1;
		autor01Lbl.Text = "Autor 01";
	}

	/// <summary>
	/// Fun��o acionada quando o bot�o de cancelar adi��o de livro for clicado.
	/// </summary>
	private async void Cancelar_BtnClick ( object sender, EventArgs e )
	{
		if (MopupService.Instance.PopupStack.Count > 0)
			await MopupService.Instance.PopAsync( );
	}

	/// <summary>
	/// Fun��o acionada quando o bot�o de finaliza��o de adi��o de livro (OK) for clicado.
	/// </summary>
	private async void Ok_BtnClick ( object sender, EventArgs e )
	{
		string tituloLivro = tituloEty.Text;

		if (string.IsNullOrEmpty( tituloLivro ))
		{
			await DisplayAlert( "Sem T�tulo de livro", "Adicione um T�tulo para o livro para poder adicion�-lo.", "Ok" );
			tituloEty.Focus( );
			return;
		}

		Livro livro;

		List<Autor> autores = autoresInfo
			.Where(a => !string.IsNullOrEmpty(a.nome.Text))
			.SelectList(a => a.ConverterParaAutor());


		// Se n�o houver nenhum autor.
		if (autoresInfo.Count <= 0)
		{
			// Se n�o houver data de lan�amento
			if (string.IsNullOrEmpty( lancamentoEty.Text ))
			{
				if (string.IsNullOrEmpty( livroCaminho ))
					livro = new Livro(
						  tituloLivro,
						  situacao );
				else
					livro = new Livro(
						  tituloLivro,
						  situacao,
						  livroCaminho );
			}
			// Se houver data de lan�amento
			else
			{
				int lancamento = int.Parse(lancamentoEty.Text);

				if (string.IsNullOrEmpty( livroCaminho ))
					livro = new Livro(
						tituloLivro,
						situacao,
						lancamento );
				else
					livro = new Livro(
						tituloLivro,
						situacao,
						livroCaminho,
						lancamento );
			}
		}
		// Se houver ao menos um autor
		else
		{
			// Se n�o houver data de lan�amento
			if (string.IsNullOrEmpty( lancamentoEty.Text ))
			{
				if (string.IsNullOrEmpty( livroCaminho ))
					livro = new Livro(
						 tituloLivro,
						 situacao,
						 autores.SelectList( a => a.PegarId( ) ) );
				else
					livro = new Livro(
						 tituloLivro,
						 situacao,
						 livroCaminho,
						 autores.SelectList( a => a.PegarId( ) ) );
			}
			// Se houver data de lan�amento
			else
			{
				int lancamento = int.Parse( lancamentoEty.Text);

				if (string.IsNullOrEmpty( livroCaminho ))
					livro = new Livro(
						 tituloLivro,
						 situacao,
						 autores.SelectList( a => a.PegarId( ) ),
						 lancamento );
				else
					livro = new Livro(
						 tituloLivro,
						 situacao,
						 livroCaminho,
						 autores.SelectList( a => a.PegarId( ) ),
						 lancamento );
			}
		}

		AdicionarLivroEvento.Invoke( livro );

		Cancelar_BtnClick( sender, e );
	}
}