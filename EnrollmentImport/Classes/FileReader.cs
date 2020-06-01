namespace EnrollmentImport.Classes
{
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.IO;

	/// <summary>
	/// Class for reading the lines of a file
	/// Excluding from code coverage because it is a file system method
	/// </summary>
	[ExcludeFromCodeCoverage]
	public class FileReader : IFileReader
	{
		/// <summary>
		/// Read the lines of the file
		/// </summary>
		/// <param name="filePath">The path to the file containing Enrollment data.</param>
		/// <returns>Enumerable of lines</returns>
		public IEnumerable<string> ReadLines(string filePath)
		{
			return File.ReadLines(filePath);
		}
	}
}
