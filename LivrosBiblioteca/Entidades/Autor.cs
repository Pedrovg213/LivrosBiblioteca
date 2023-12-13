using LivrosBiblioteca.Servicos;
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


	// FUNÇÕES: public
	public string PegarNome () =>
		nome;
}
