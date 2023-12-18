using LivrosBiblioteca.Servicos;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SeletorLivros.Extensoes;

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
	/// Dia, mês e ano de morte do autor.
	/// </summary>
	[BsonElement( DataBase.AUTOR_MORTE )]
	private DateTime morte
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
	/// Pega o nome do autor.
	/// </summary>
	/// <returns>Retorna o nome (string) do autor.</returns>
	public string PegarNome () =>
		nome;

	/// <summary>
	/// Adicionar livros na lista de livros que o autor escreveu.
	/// </summary>
	/// <param name="livros">Livros a serem adicionados a lista.</param>
	public void AdicionarLivros ( params Livro[] livros ) =>
		this.livros = livros.SelectList( l => l.PegarId( ) );
}
