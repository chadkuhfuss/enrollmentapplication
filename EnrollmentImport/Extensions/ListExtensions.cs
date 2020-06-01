namespace EnrollmentImport.Extensions
{
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class for extension methods of List
	/// </summary>
	public static class ListExtensions
	{
		/// <summary>
		/// Determines if the List is null or empty
		/// </summary>
		/// <typeparam name="T">Type of list</typeparam>
		/// <param name="enumerable">The list</param>
		/// <returns>True if the list is either null or empty, otherwise false.</returns>
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
		{
			return (enumerable == null || enumerable.Count() == 0);
		}
	}
}
