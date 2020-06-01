namespace EnrollmentImport.Classes
{
	using EnrollmentImport.Enumerations;
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Class for Importing Enrollment records
	/// </summary>
	public class EnrollmentImporter : IEnrollmentImporter
	{
		/// <summary>
		/// Method to import the Enrollment records from the given file path using the given file reader, enrollment reader and enrollment validator
		/// </summary>
		/// <param name="filePath">The path to the file containing the Enrollment data</param>
		/// <param name="fileReader">The file reader class used to read the contents of a file</param>
		/// <param name="enrollmentReader">The enrollment reader class used to read contents from file reader into Enrollment class objects</param>
		/// <param name="enrollmentValidator">The enrollment validator class used to validate that the Enrollment objects are valid</param>
		/// <returns>List of Enrollment records</returns>
		public List<Enrollment> Import(string filePath, IFileReader fileReader, IEnrollmentReader enrollmentReader, IEnrollmentValidator enrollmentValidator)
		{
			if (String.IsNullOrWhiteSpace(filePath))
				throw new InvalidOperationException("filePath is required.");
			if (fileReader == null)
				throw new InvalidOperationException("fileReader is required.");
			if (enrollmentReader == null)
				throw new InvalidOperationException("enrollmentReader is required.");
			if (enrollmentValidator == null)
				throw new InvalidOperationException("enrollmentValidator is required.");

			// Read all records from file
			var enrollmentRecords = enrollmentReader.ReadFromFile(fileReader, filePath);

			var savedEnrollmentRecords = new List<Enrollment>();

			try
			{
				// Loop through the enrollment records read from file and validate them and save in memory
				// If any values in file were not formatted or missing then exception is thrown and processing of the file stops
				foreach (var enrollmentRecord in enrollmentRecords)
				{
					enrollmentRecord.Status = enrollmentValidator.Validate(enrollmentRecord) ? EnrollmentStatus.Accepted : EnrollmentStatus.Rejected;
					savedEnrollmentRecords.Add(enrollmentRecord);
				}

				return savedEnrollmentRecords;
			}
			catch{}

			return savedEnrollmentRecords;
		}
	}
}
