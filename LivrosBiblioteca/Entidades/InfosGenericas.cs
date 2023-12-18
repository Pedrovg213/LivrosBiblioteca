using LivrosBiblioteca.Servicos;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LivrosBiblioteca.Entidades;

public class InfosGenericas
{
	// VARIÁVEIS: protected

	/// <summary>
	/// Código de identificação do elemento.
	/// </summary>
	[BsonId]
	protected ObjectId BsonId
	{
		get; set;
	}

	/// <summary>
	/// Lista de códigos de elementos contidos neste elemento.
	/// </summary>
	[BsonElement( DataBase.CONTIDOS_ID )]
	protected List<ObjectId> Contidos
	{
		get; set;
	} = new List<ObjectId>( );


	// FUNÇÕES: public

	/// <summary>
	/// Pega o código único de identificação do elemento.
	/// </summary>
	/// <returns>Código de identificação (ObjectId) do elemento.</returns>
	public ObjectId PegarId () =>
		BsonId;
}
