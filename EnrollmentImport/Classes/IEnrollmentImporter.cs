namespace EnrollmentImport.Classes
{
	using System.Collections.Generic;

	/// <summary>
	/// Interface for importing enrollment records from a file
	/// </summary>
	public interface IEnrollmentImporter
	{
		/// <summary>
		/// Method to import the Enrollment records from the given file path using the given file reader, enrollment reader and enrollment validator
		/// </summary>
		/// <param name="filePath">The path to the file containing the Enrollment data</param>
		/// <param name="fileReader">The file reader class used to read the contents of a file</param>
		/// <param name="enrollmentReader">The enrollment reader class used to read contents from file reader into Enrollment class objects</param>
		/// <param name="enrollmentValidator">The enrollment validator class used to validate that the Enrollment objects are valid</param>
		/// <returns>List of Enrollment records</returns>
		List<Enrollment> Import(string filePath, IFileReader fileReader, IEnrollmentReader enrollmentReader, IEnrollmentValidator enrollmentValidator);
	}
}
