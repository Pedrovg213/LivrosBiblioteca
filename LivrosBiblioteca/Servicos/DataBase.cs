using LivrosBiblioteca.Entidades;

namespace LivrosBiblioteca.Servicos;

public static class DataBase
{
	// CONSTANTES: public
	public const string CONTIDOS_ID = "Contidos Id";

	public const string DATABASE = "LivrosBiblioteca";

	public const string LIVROS_COLECAO = "Livros";
	public const string LIVRO_TITULO = "Titulo";
	public const string LIVRO_SITUACAO = "Situação";
	public const string LIVRO_LANCAMENTO = "Lançamento";
	public const string LIVRO_LANCAMENTOAC = "Lançamento AC";
	public const string LIVRO_ARQUIVO = "Arquivo";

	public const string AUTORES_COLECAO = "Autores";
	public const string AUTOR_NOME = "Nome";
	public const string AUTOR_NASCIMENTO = "Nascimento";
	public const string AUTOR_NASCIMENTOAC = "Nascimento AC";
	public const string AUTOR_MORTEAC = "Morte AC";
	public const string AUTOR_MORTE = "Morte";


	// VARIÁVEIS: private static

	/// <summary>
	/// Lista de autores que deve corresponder com o salvo no sistema.
	/// </summary>
	private static List<Autor> autores = new List<Autor>();


	// FUNÇÕES: public static

	/// <summary>
	/// Pega a lista de todos os autores salvos na base de dados.
	/// </summary>
	public static List<Autor> PegaAutores () =>
		autores;


	/// <summary>
	/// Procura entre os autores salvos os que têm nome correspondente com o nome escrito na procura.
	/// </summary>
	/// <param name="procura">Letras que compõe parte do nome que deve ser procurado</param>
	/// <returns>Lista de nomes (string) de autores que tem todas as letras da pesquisa.</returns>
	public static List<string> ProcurarAutoresPorNome ( string procura )
	{
		List<string> resultado = new List<string>();

		foreach (string autorNome in autores.Select( a => a.PegarNome( ) ))
		{
			string checarNome = autorNome;
			foreach (char letra in procura)
			{
				if (checarNome.Count( ) <= 0)
					resultado.Add( autorNome );
				else if (checarNome.Contains( letra ))
				{
					checarNome.Remove( checarNome.IndexOf( letra ) );
					continue;
				} else
					break;
			}
		}

		return resultado;
	}
}
