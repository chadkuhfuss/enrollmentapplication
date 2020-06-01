namespace EnrollmentImport.Classes
{
	using System.Collections.Generic;

	/// <summary>
	/// Interface for reading enrollment records from a file into Enrollment objects
	/// </summary>
	public interface IEnrollmentReader
	{
		/// <summary>
		/// Read Enrollments from file 
		/// </summary>
		/// <param name="fileReader">The file reader that reads data from a file</param>
		/// <param name="filePath">The path to the enrollments file</param>
		/// <returns>Enumeration of Enrollment objects</returns>
		IEnumerable<Enrollment> ReadFromFile(IFileReader fileReader, string filePath);
	}
}
