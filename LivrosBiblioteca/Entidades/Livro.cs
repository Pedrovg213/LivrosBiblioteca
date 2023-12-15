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
	/// Dia, mês e ano de lançamentodo livro.
	/// </summary>
	[BsonElement( DataBase.LIVRO_LANCAMENTO )]
	private DateTime lancamento
	{
		get; set;
	}

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
}
