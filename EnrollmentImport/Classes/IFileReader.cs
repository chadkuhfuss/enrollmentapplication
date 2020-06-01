namespace EnrollmentImport.Classes
{
	using System.Collections.Generic;

	/// <summary>
	/// Interface for reading lines of a file
	/// </summary>
	public interface IFileReader
	{
		/// <summary>
		/// Reads lines of a file
		/// </summary>
		/// <param name="filePath">The path to the file.</param>
		/// <returns>Enumerable string of lines from the file.</returns>
		IEnumerable<string> ReadLines(string filePath);
	}
}
