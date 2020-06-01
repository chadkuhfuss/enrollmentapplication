namespace EnrollmentImport.Classes
{
	using EnrollmentImport.Enumerations;
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.IO;

	/// <summary>
	/// Class for reading Enrollment data from a CSV file
	/// </summary>
	public class EnrollmentReaderCSV : IEnrollmentReader
	{
		/// <summary>
		/// Read Enrollments from file 
		/// </summary>
		/// <param name="fileReader">The file reader that reads data from a file</param>
		/// <param name="filePath">The path to the enrollments file</param>
		/// <returns>Enumeration of Enrollment objects</returns>
		public IEnumerable<Enrollment> ReadFromFile(IFileReader fileReader, string filePath)
		{
			var records = fileReader.ReadLines(filePath);

			// Loop through each record/line in the file and split into field values of Enrollment class
			foreach (var record in records)
			{
				var enrollmentRecord = new Enrollment();

				// Split record/line by comma
				var fields = record.Split(',');

				for (var i = 0; i < fields.Length; i++)
				{
					var fieldValue = fields[i].ToString();

					switch (i)
					{
						case 0:
							enrollmentRecord.FirstName = fieldValue;
							break;
						case 1:
							enrollmentRecord.LastName = fieldValue;
							break;
						case 2:
							// If Date of Birth is not in correct format then return as minimum date value.  Validation will fail.
							if (!DateTime.TryParseExact(fieldValue, "MMddyyyy", CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTime dateOfBirth))
							{
								dateOfBirth = DateTime.MinValue;
							}
							enrollmentRecord.DateOfBirth = dateOfBirth;
							break;
						case 3:
							// If Plan Type is not in valid then return as None.  Validation will fail.
							if (!Enum.TryParse(fieldValue, out EnrollmentPlanType planType))
							{
								planType = EnrollmentPlanType.None;
							}
							enrollmentRecord.PlanType = planType;
							break;
						case 4:
							// If Effective Date is not in correct format then return as minimum date value.  Validation will fail.
							if (!DateTime.TryParseExact(fieldValue, "MMddyyyy", CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTime effectiveDate))
							{
								effectiveDate = DateTime.MinValue;
							}
							enrollmentRecord.EffectiveDate = effectiveDate;
							break;
					}
				}

				yield return enrollmentRecord;
			}
		}
	}
}
