using System.Collections.ObjectModel;

namespace LivrosBiblioteca.Extensoes;

public static class IEnumerableExtensions
{
	public static List<R> SelectList<T, R> ( this IEnumerable<T> collection, Func<T, R> selector ) =>
		collection.Select( selector ).ToList( );

	public static List<T> OrderByList<T, TKey> ( this IEnumerable<T> collection, Func<T, TKey> keySelector ) =>
		collection.OrderBy( keySelector ).ToList( );

	public static ObservableCollection<T> OrderByObservableCollection<T, TKey> ( this IEnumerable<T> collection, Func<T, TKey> keySelector ) =>
		new( collection.OrderBy( keySelector ) );

	public static ObservableCollection<T> WhereObservableCollection<T> ( this IEnumerable<T> collection, Func<T, bool> predicado ) =>
		new ObservableCollection<T>( collection.Where( predicado ) );
}
