namespace EnrollmentImport
{
	using EnrollmentImport.Classes;
	using EnrollmentImport.Extensions;
	using System;
	using System.IO;

	public class Program
	{
		/// <summary>
		/// Main entry point of console application
		/// </summary>
		/// <param name="args">Arguments list</param>
		static void Main(string[] args)
		{
			var filePath = Path.Combine(Environment.CurrentDirectory, "Enrollments.csv");

			// Create the classes needed to read, validate and import the Enrollment data
			var fileReader = new FileReader();
			var enrollmentCSVReader = new EnrollmentReaderCSV();
			var enrollmentValidator = new EnrollmentValidator();
			var enrollmentImporter = new EnrollmentImporter();

			// Read in and validate the Enrollment data from the file
			var savedEnrollmentRecords = enrollmentImporter.Import(filePath, fileReader, enrollmentCSVReader, enrollmentValidator);

			// If there are no records then processing was stopped due to some invalid formatting of data in the file.
			if (savedEnrollmentRecords.IsNullOrEmpty())
			{
				Console.WriteLine("A record in the file failed validation.  Processing has stopped.");
			}
			else
			{
				// The records were read in so display them.  Some records still may be invalid.
				foreach (var enrollment in savedEnrollmentRecords)
				{
					Console.WriteLine("\"{0},{1},{2},{3},{4},{5}\"", enrollment.Status, enrollment.FirstName, enrollment.LastName, enrollment.DateOfBirth.ToShortDateString(), enrollment.PlanType, enrollment.EffectiveDate.ToShortDateString());
				}
			}

			Console.WriteLine("Press Enter to quit");
			Console.ReadLine();
		}
	}
}
