namespace LivrosBiblioteca.Extensoes;

public static class StringExtensions
{
	/// <summary>
	/// Converte o texto em data.
	/// </summary>
	/// <param name="texto">Texto para ser convertido em data.</param>
	public static DateTime ConverterParaDateTime ( this string texto )
	{
		string[] dataString = texto.Split(' ');

		return DateTime.Parse( dataString[1] );
	}

	/// <summary>
	/// Verifica se a data está com formatação correta.
	/// </summary>
	/// <param name="texto">Texto para ser verificado.</param>
	/// <returns>
	/// TRUE: Se o texto está com a formatação correta. <br></br>
	/// FALSE: Se o texto está com a formatação errada.
	/// </returns>
	public static async Task<bool> CheckDataValida ( this string texto )
	{
		bool tamanhoMenor = texto.Length < 9;
		bool tamanhoMaior= texto.Length > 13;
		bool espacoErrado = texto.Length > 10 && texto[10] != ' ';
		bool ADerrados = texto.Length > 11 && (texto[11] != 'a' && texto[11] != 'd');
		bool cErrado = texto.Length > 12 && texto[12] != 'c';

		bool textoInvalido = tamanhoMenor || tamanhoMaior || espacoErrado || ADerrados || cErrado;

		if (textoInvalido)
		{
			await Application
				.Current
				.MainPage
				.DisplayAlert( "Data com Formatação Inválida!!!", "Digite uma Data com formatação válida:\ndd/mm/aaaa\ndd/mm/aaaa ac (Antes de Cristo)\ndd/mm/aaaa dc (depois de Cristo)", "Ok" );

			return false;
		}

		return true;
	}
}
