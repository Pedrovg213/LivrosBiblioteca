using LivrosBiblioteca.Entidades;
using MongoDB.Driver;

namespace LivrosBiblioteca.Servicos;

public class DataBase
{
	// CONSTANTES: private
	private const string DATABASE = "LivrosBiblioteca";
	private const string AUTORES_COLECAO = "Autores";
	private const string LIVROS_COLECAO = "Livros";

	// CONSTANTES: public
	public const string CONTIDOS_ID = "Contidos Id";


	public const string LIVRO_TITULO = "Titulo";
	public const string LIVRO_SITUACAO = "Situação";
	public const string LIVRO_LANCAMENTO = "Lançamento";
	public const string LIVRO_LANCAMENTOAC = "Lançamento AC";
	public const string LIVRO_ARQUIVO = "Arquivo";

	public const string AUTOR_NOME = "Nome";
	public const string AUTOR_NASCIMENTO = "Nascimento";
	public const string AUTOR_NASCIMENTOAC = "Nascimento AC";
	public const string AUTOR_MORTE = "Morte";
	public const string AUTOR_MORTEAC = "Morte AC";


	// VARIÁVEIS: private static

	private static MongoClient clienteMongo = new MongoClient();
	private static IMongoDatabase dataBase = PegarDataBase();
	private static IMongoCollection<Autor> autoresColecao = PegarAutoresColecao();
	private static IMongoCollection<Livro> livrosColecao = PegarLivrosColecao();


	// FUNÇÕES: private static

	/// <summary>
	/// Pega no banco de dados a base de dados necessária para o programa.
	/// </summary>
	/// <returns>Data base (IMongoDatabase) com as informações buscadas</returns>
	private static IMongoDatabase PegarDataBase () =>
		clienteMongo.GetDatabase( DATABASE );

	/// <summary>
	/// Pega, no banco de dados, a coleção que contém os autores.
	/// </summary>
	private static IMongoCollection<Autor> PegarAutoresColecao ()
	{
		if (!ColecaoExiste( AUTORES_COLECAO ))
			dataBase.CreateCollection( AUTORES_COLECAO );

		return dataBase.GetCollection<Autor>( AUTORES_COLECAO );
	}

	/// <summary>
	/// Pega, no banco de dados, a coleção de contém os autores.
	/// </summary>
	private static IMongoCollection<Livro> PegarLivrosColecao ()
	{
		if (!ColecaoExiste( LIVROS_COLECAO ))
			dataBase.CreateCollection( LIVROS_COLECAO );

		return dataBase.GetCollection<Livro>( LIVROS_COLECAO );
	}

	/// <summary>
	/// Checa se uma coleção com o nome dado como valor existe na base de dados.
	/// </summary>
	/// <param name="colecaoNome">Nome da coleção procurada no banco de dados.</param>
	/// <returns>
	/// TRUE: se uma coleção com o nome dado foi encontrada. <br></br>
	/// FALSE: se nenhuma coleção foi encontrada com o nome dado.
	/// </returns>
	private static bool ColecaoExiste ( string colecaoNome )
	{
		IEnumerable<string> nomes = dataBase.ListCollectionNames().ToEnumerable();

		return nomes.Contains( colecaoNome );
	}

	// FUNÇÕES: public static

	/// <summary>
	/// Adicionar um autor com suas informações à coleção de autores da base de dados.
	/// </summary>
	/// <param name="autor">Autor a ser adicionado à base de dados.</param>
	public static void AdicionarAutor ( Autor autor )
	{
		if (PegarAutores( ).Contains( autor ))
		{
			AtualizarAutor( autor );
			return;
		}

		autoresColecao.InsertOne( autor );
	}

	public static void AtualizarAutor ( Autor autor )
	{
		if (!PegarAutores( ).Contains( autor ))
		{
			AdicionarAutor( autor );
			return;
		}

		UpdateDefinition<Autor> updateDef = Builders<Autor>
			.Update
			.Set(CONTIDOS_ID, autor.PegarLivrosIds())
			.Set(AUTOR_NOME, autor.PegarNome())
			.Set(AUTOR_NASCIMENTO, autor.PegarNascimento())
			.Set(AUTOR_NASCIMENTOAC, autor.PegarNascimentoAC())
			.Set(AUTOR_MORTE, autor.PegarMorte())
			.Set(AUTOR_MORTEAC, autor.PegarMorteAC());

		autoresColecao.UpdateOne( a => a == autor, updateDef );
	}

	/// <summary>
	/// Adicionar um livro com suas informações à coleção dos livros da base de dados.
	/// </summary>
	/// <param name="livro">Livro a ser adicionado à base de dados.</param>
	public static void AdicionarLivro ( Livro livro )
	{
		if (PegarLivros( ).Contains( livro ))
		{
			AtualizarLivro( livro );
			return;
		}

		livrosColecao.InsertOne( livro );
	}
	public static void AtualizarLivro ( Livro livro )
	{
		if (!PegarLivros( ).Contains( livro ))
		{
			AdicionarLivro( livro );
			return;
		}

		UpdateDefinition<Livro> updateDef = Builders<Livro>
			.Update
			.Set(CONTIDOS_ID, livro.PegarAutoresIds())
			.Set(LIVRO_TITULO, livro.PegarTitulo())
			.Set(LIVRO_SITUACAO, livro.PegarSituação())
			.Set(LIVRO_LANCAMENTO, livro.PegarLancamento())
			.Set(LIVRO_LANCAMENTOAC, livro.PegarLancamentoAC())
			.Set(LIVRO_ARQUIVO, livro.PegarArquivo());

		livrosColecao.UpdateOne( l => l == livro, updateDef );
	}

	/// <summary>
	/// Adiciona livros com suas informações à colação dos livros da base de dados.
	/// </summary>
	/// <param name="livro">Livros a serem adicionados à base de dados.</param>
	public static void AdicionarLivro ( params Livro[] livro ) =>
		livrosColecao.InsertMany( livro );

	/// <summary>
	/// Pega a lista de todos os autores salvos na base de dados.
	/// </summary>
	public static IEnumerable<Autor> PegarAutores () =>
		autoresColecao.AsQueryable( ).AsEnumerable( );

	/// <summary>
	/// Pega a lista de todos os livros salvos na base de dados.
	/// </summary>
	public static IEnumerable<Livro> PegarLivros () =>
		livrosColecao.AsQueryable( ).AsEnumerable( );


	/// <summary>
	/// Procura entre os autores salvos os que têm nome correspondente com o nome escrito na procura.
	/// </summary>
	/// <param name="procura">Letras que compõe parte do nome que deve ser procurado</param>
	/// <returns>Lista de nomes (string) de autores que tem todas as letras da pesquisa.</returns>
	public static List<string> ProcurarAutoresPorNome ( string procura )
	{
		List<string> resultado = new List<string>();
		IEnumerable<string> autoresLista = PegarAutores().Select(a => a.PegarNome());

		foreach (string autorNome in autoresLista)
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
