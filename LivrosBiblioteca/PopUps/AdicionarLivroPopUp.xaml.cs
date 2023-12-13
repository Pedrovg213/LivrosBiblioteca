using LivrosBiblioteca.Servicos;

namespace LivrosBiblioteca.PopUps;

public partial class AdicionarLivroPopUp
{
	// CONSTRUTORES

	/// <summary>
	/// Inicializa��o dos componentes.
	/// </summary>
	public AdicionarLivroPopUp ()
	{
		InitializeComponent( );
	}


	// FUN��ES: private


	// FUN��ES: private Elementos
	private void SearchBar_TextChanged ( object sender, TextChangedEventArgs e )
	{
		SearchBar searchBar = (SearchBar)sender;
		searchResults.ItemsSource = DataBase.ProcurarAutoresPorNome( searchBar.Text );
	}

	private void Entry_TextChanged ( object sender, TextChangedEventArgs e )
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

	private async void Entry_Unfocused ( object sender, FocusEventArgs e )
	{
		Entry entry = (Entry)sender;

		string texto = entry.Text;

		if (string.IsNullOrEmpty( texto ))
			return;

		if (texto.Length > 9 && texto.Length < 13)
		{
			await DisplayAlert( "Data com Formata��o Errada!!!", "Digite uma Data com formata��o v�lida:\ndd\\mm\\aaaa", "Ok" );
		}
	}
}