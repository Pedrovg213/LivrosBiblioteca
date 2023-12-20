namespace LivrosBiblioteca.Enums;

public enum LeituraSituacao
{
	/// <summary>
	/// Livro ainda não teve sua leitura iniciada.
	/// </summary>
	Aguardando,
	/// <summary>
	/// Livro começou a ser lido.
	/// </summary>
	Iniciado,
	/// <summary>
	/// Leitura do livro já foi encerrada.
	/// </summary>
	Finalizado,
	/// <summary>
	/// Não há mais intenção de continuidade na leitura do livro.
	/// </summary>
	Abandonado
}