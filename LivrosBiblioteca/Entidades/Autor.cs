using LivrosBiblioteca.Extensoes;
using LivrosBiblioteca.Servicos;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LivrosBiblioteca.Entidades;

public class Autor : InfosGenericas
{
	// VARIÁVEIS: private BsonElement

	/// <summary>
	/// Nome do autor.
	/// </summary>
	[BsonElement( DataBase.AUTOR_NOME )]
	private string nome
	{
		get; set;
	}

	/// <summary>
	/// Dia, mês e ano de nascimento do autor.
	/// </summary>
	[BsonElement( DataBase.AUTOR_NASCIMENTO )]
	private DateTime nascimento
	{
		get; set;
	}

	/// <summary>
	/// Identifica se o nascimento do autor foi antes ou depois do nascimento de Cristo
	/// </summary>
	[BsonElement( DataBase.AUTOR_NASCIMENTOAC )]
	private bool nascimentoAC
	{
		get; set;
	}

	/// <summary>
	/// Dia, mês e ano de morte do autor.
	/// </summary>
	[BsonElement( DataBase.AUTOR_MORTE )]
	private DateTime morte
	{
		get; set;
	}

	/// <summary>
	/// Identifica se a morte do autor foi antes ou depois do nascimento de Cristo.
	/// </summary>
	[BsonElement( DataBase.AUTOR_MORTEAC )]
	private bool morteAC
	{
		get; set;
	}


	// PROPRIEDADES: private

	/// <summary>
	/// Lista de identidades dos livros que o autor escreveu para busca em banco de dados.
	/// </summary>
	private List<ObjectId> livros
	{
		get => Contidos;
		set => Contidos = value;
	}


	// CONSTRUTORES: private

	/// <summary>
	/// Construtor padrão da classe.
	/// </summary>
	private Autor ()
	{
		if (BsonId == ObjectId.Empty)
			BsonId = ObjectId.GenerateNewId( );
	}

	// CONSTRUTORES: public

	/// <summary>
	/// Construtor usado para quando não há data de morte definida.
	/// </summary>
	/// <param name="nome">Nome do autor.</param>
	/// <param name="nascimento">Data de nascimento do autor.</param>
	public Autor ( string nome, DateTime nascimento ) : this( )
	{
		this.nome = nome;
		this.nascimento = nascimento;
	}

	/// <summary>
	/// Construtor usado para quando há data de morte definida.
	/// </summary>
	/// <param name="nome">Nome do autor.</param>
	/// <param name="nascimento">Data de nascimento do autor.</param>
	/// <param name="morte">Data de morte do autor.</param>
	public Autor ( string nome, DateTime nascimento, DateTime morte ) : this( nome, nascimento ) =>
		this.morte = morte;


	// FUNÇÕES: public

	/// <summary>
	/// Adicionar livros na lista de livros que o autor escreveu.
	/// </summary>
	/// <param name="livros">Livros a serem adicionados a lista.</param>
	public void AdicionarLivros ( params Livro[] livros ) =>
		this.livros = livros.SelectList( l => l.PegarId( ) );

	/// <summary>
	/// Pega lista de identidade dos livros que foram escritos por este autor.
	/// </summary>
	/// <returns>Identidades (ObjectId) dos livros que foram escritos por este autor.</returns>
	public List<ObjectId> PegarLivrosIds () =>
		livros;

	/// <summary>
	/// Pega a data de morte do autor.
	/// </summary>
	public DateTime PegarMorte () =>
		morte;

	/// <summary>
	/// Pega a variável que identifica se o autor morreu antes ou depois de do nascimento de Cristo.
	/// </summary>
	/// <returns></returns>
	public bool PegarMorteAC () =>
		morteAC;

	/// <summary>
	/// Pega a data de nascimento do autor.
	/// </summary>
	public DateTime PegarNascimento () =>
		nascimento;

	/// <summary>
	/// Pega a variável que identifica se o autor nasceu antes ou depois de Cristo.
	/// </summary>
	public bool PegarNascimentoAC () =>
		nascimentoAC;

	/// <summary>
	/// Pega o nome do autor.
	/// </summary>
	/// <returns>Retorna o nome (string) do autor.</returns>
	public string PegarNome () =>
		nome;
}
