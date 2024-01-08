using LivrosBiblioteca.Enums;
using LivrosBiblioteca.Servicos;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LivrosBiblioteca.Entidades;

public class Livro : InfosGenericas
{
	// VARIÁVEIS: private BsonElement

	/// <summary>
	/// Título da obra.
	/// </summary>
	[BsonElement( DataBase.LIVRO_TITULO )]
	private string titulo
	{
		get; set;
	}

	/// <summary>
	/// Situação atual de leitura do livro <br></br>
	///	1. Aguardando: livro ainda não teve sua leitura iniciado; <br></br>
	///	2. Iniciado: livro começou a ser lido; <br></br>
	///	3. Finalizado: a leitura do livro foi encerrada; <br></br>
	///	4. Desistência: Não há mais intenção de continuidade na leitura do livro.
	/// </summary>
	[BsonElement(DataBase.LIVRO_SITUACAO)]
	private LeituraSituacao situacao;

	/// <summary>
	/// Dia, mês e ano de lançamentodo livro.
	/// </summary>
	[BsonElement( DataBase.LIVRO_LANCAMENTO )]
	private DateTime lancamento
	{
		get; set;
	}

	/// <summary>
	/// Identifica se o lançamento da obra foi feito antes de Cristo. <br></br>
	///	TRUE: Antes de Cristo. <br></br>
	///	FALSE: Depois de Cristo.
	/// </summary>
	[BsonElement( DataBase.LIVRO_LANCAMENTOAC )]
	private bool lancamentoAC
	{
		get; set;
	}

	/// <summary>
	/// Caminho no sistema para o arquivo correspondênte ao livro.
	/// </summary>
	[BsonElement( DataBase.LIVRO_ARQUIVO )]
	private string arquivo
	{
		get; set;
	}

	// PROPRIEDADES: private

	/// <summary>
	/// Lista de identidades dos autores que escreveram o livro.
	/// </summary>
	private List<ObjectId> autores
	{
		get => Contidos;
		set => Contidos = value;
	}


	// CONTRUTORES: private

	/// <summary>
	/// Construtor base da classe.
	/// </summary>
	private Livro ()
	{
		if (BsonId == ObjectId.Empty)
			BsonId = ObjectId.GenerateNewId( );
	}

	// CONSTRUTORES: public

	/// <summary>
	/// Construtor para quando houver apenas título para a obra.
	/// </summary>
	/// <param name="titulo">Título da obra.</param>
	/// <param name="situacao">Situação atual da leitura.</param>
	public Livro ( string titulo, LeituraSituacao situacao ) : this( )
	{
		this.titulo = titulo;
		this.situacao = situacao;
	}

	/// <summary>
	/// Construtor para quando ouvir apenas título, situção de leitura e autores.
	/// </summary>
	/// <param name="titulo">Titulo da obra.</param>
	/// <param name="situacao">Situação atual da leitura.</param>
	/// <param name="autores">Autores que escreveram a obra.</param>
	public Livro ( string titulo, LeituraSituacao situacao, List<ObjectId> autores ) : this( titulo, situacao ) =>
		this.autores = autores;

	/// <summary>
	/// Construtor para quando houver apenas título, situação de leitura e data de lançamento da obra.
	/// </summary>
	/// <param name="titulo">Título da obra.</param>
	/// <param name="situacao">Situação atual da leitura.</param>
	/// <param name="lancamento">Data de lançamento da obra.</param>
	public Livro ( string titulo, LeituraSituacao situacao, DateTime lancamento ) : this( titulo, situacao ) =>
		this.lancamento = lancamento;

	/// <summary>
	/// Construtor para quando houver apenas titulo, situação de leitura e caminho de arquivo.
	/// </summary>
	/// <param name="titulo">Título da obra.</param>
	/// <param name="situacao">Situação atual da leitura.</param>
	/// <param name="arquivo">Caminho para o arquivo da obra no sistema.</param>
	public Livro ( string titulo, LeituraSituacao situacao, string arquivo ) : this( titulo, situacao ) =>
		this.arquivo = arquivo;

	/// <summary>
	/// Construtor para quando houver titulo, situação de leitura, caminho de arquivo e autores da obra.
	/// </summary>
	/// <param name="titulo">Título da obra.</param>
	/// <param name="situacao">Situação atual da leitura.</param>
	/// <param name="arquivo">Caminho para o arquivo da obra no sistema.</param>
	/// <param name="autores">Autores que escreveram a obra.</param>

	public Livro ( string titulo, LeituraSituacao situacao, string arquivo, List<ObjectId> autores ) : this( titulo, situacao, autores ) =>
		this.arquivo = arquivo;

	/// <summary>
	/// Construtor para quando houver título, situação de leitura, caminho de arquivo e data de lançamento.
	/// </summary>
	/// <param name="titulo">Título da obra.</param>
	/// <param name="situacao">Situação atual da leitura.</param>
	/// <param name="arquivo">Caminho para o arquivo da obra no sistema.</param>
	/// <param name="lancamento">Data de lançamento da obra.</param>

	public Livro ( string titulo, LeituraSituacao situacao, string arquivo, DateTime lancamento ) : this( titulo, situacao, lancamento ) =>
		this.arquivo = arquivo;

	/// <summary>
	/// Construtor para quando houver titulo, lista de autores e data de lançamento.
	/// </summary>
	/// <param name="titulo">Titulo da obra.</param>
	/// <param name="situacao">Situação atual da leitura.</param>
	/// <param name="autores">Autores da obra.</param>
	/// <param name="lancamento">Data de lançamento da obra.</param>
	public Livro ( string titulo, LeituraSituacao situacao, List<ObjectId> autores, DateTime lancamento ) : this( titulo, situacao, autores ) =>
		this.lancamento = lancamento;

	/// <summary>
	/// Construtor para quando houver todas as variáveis do livro.
	/// </summary>
	/// <param name="titulo">Título da obra.</param>
	/// <param name="situacao">Situação atual da leitura.</param>
	/// <param name="arquivo">Caminho para o arquivo da obra no sistema.</param>
	/// <param name="autores">Autores que escreveram a obra.</param>
	/// <param name="lancamento">Data de lançamento da obra.</param>
	public Livro ( string titulo, LeituraSituacao situacao, string arquivo, List<ObjectId> autores, DateTime lancamento ) : this( titulo, situacao, arquivo, autores ) =>
		this.lancamento = lancamento;


	// FUNÇÕES: public

	/// <summary>
	/// Pegar o caminho no sistema para o arquivo do livro.
	/// </summary>
	/// <returns>Caminho no sistema do arquivo (string)</returns>
	public string PegarArquivo () =>
		arquivo;

	/// <summary>
	/// Pegar lista de identidade dos autores que escreveram o livro.
	/// </summary>
	/// <returns>Identidades (ObjectId) dos autores que escreveram o livro.</returns>
	public List<ObjectId> PegarAutoresIds () =>
		autores;

	/// <summary>
	/// Pegar data de lançamento do livro.
	/// </summary>
	/// <returns>Lançamento (DateTime) do livro.</returns>
	public DateTime PegarLancamento () =>
		lancamento;

	/// <summary>
	/// Pega o identificador de a obra ter sido lançada antes de Cristo.
	/// </summary>
	public bool PegarLancamentoAC () =>
		lancamentoAC;

	/// <summary>
	/// Pega a situação atual do livro.
	/// </summary>
	/// <returns>Situação atual (enum) do livro.</returns>
	public LeituraSituacao PegarSituação () =>
		situacao;

	/// <summary>
	/// Pega o título do livro.
	/// </summary>
	/// <returns>Título (string) da obra.</returns>
	public string PegarTitulo () =>
		titulo;

	// FUNÇÕES: public override

	public override bool Equals ( object obj )
	{
		Livro outro = (Livro)obj;

		return PegarId( ).Equals( outro.PegarId( ) );
	}

	public override int GetHashCode () =>
		PegarId( ).GetHashCode( );
}
