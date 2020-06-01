namespace EnrollmentImportTests
{
	using EnrollmentImport.Classes;
	using EnrollmentImport.Enumerations;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Moq;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class for testing EnrollmetnReaderCSV class
	/// </summary>
	[TestClass]
	public class EnrollmentReaderCSVTests
	{
		/// <summary>
		/// Class for testing ReadFromFile method
		/// </summary>
		[TestClass]
		public class EnrollmentReaderCSV_ReadFromFile_Tests
		{
			[TestMethod]
			public void WhenEnrollmentFileHasInvalidDateOfBirthFormat_ThenReturnMinimumDateTimeValue()
			{
				var filePath = "enrollments.csv";
				var fileReader = new Mock<IFileReader>(MockBehavior.Strict);
				var enrollmentReader = new EnrollmentReaderCSV();

				var enrollmentCSVLines = new List<string>
				{
					"Jane,Doe,612002,HSA,06012020",
				};
				
				fileReader.Setup(x => x.ReadLines(It.IsAny<string>())).Returns(enrollmentCSVLines);

				var enrollmentRecords = enrollmentReader.ReadFromFile(fileReader.Object, filePath).ToList();

				Assert.IsTrue(enrollmentRecords.Count == 1);
				Assert.IsTrue(enrollmentRecords[0].DateOfBirth == DateTime.MinValue);
			}

			[TestMethod]
			public void WhenEnrollmentFileHasInvalidEffectiveDateFormat_ThenReturnMinimumDateTimeValue()
			{
				var filePath = "enrollments.csv";
				var fileReader = new Mock<IFileReader>(MockBehavior.Strict);
				var enrollmentReader = new EnrollmentReaderCSV();

				var enrollmentCSVLines = new List<string>
				{
					"Jane,Doe,06012002,HSA,612020",
				};

				fileReader.Setup(x => x.ReadLines(It.IsAny<string>())).Returns(enrollmentCSVLines);

				var enrollmentRecords = enrollmentReader.ReadFromFile(fileReader.Object, filePath).ToList();

				Assert.IsTrue(enrollmentRecords.Count == 1);
				Assert.IsTrue(enrollmentRecords[0].EffectiveDate == DateTime.MinValue);
			}

			[TestMethod]
			public void WhenEnrollmentFileHasInvalidPlanType_ThenReturnMinimumDateTimeValue()
			{
				var filePath = "enrollments.csv";
				var fileReader = new Mock<IFileReader>(MockBehavior.Strict);
				var enrollmentReader = new EnrollmentReaderCSV();

				var enrollmentCSVLines = new List<string>
				{
					"Jane,Doe,06012002,FMLA,612020",
				};

				fileReader.Setup(x => x.ReadLines(It.IsAny<string>())).Returns(enrollmentCSVLines);

				var enrollmentRecords = enrollmentReader.ReadFromFile(fileReader.Object, filePath).ToList();

				Assert.IsTrue(enrollmentRecords.Count == 1);
				Assert.IsTrue(enrollmentRecords[0].PlanType == EnrollmentPlanType.None);
			}

			[TestMethod]
			public void WhenEnrollmentFileIsValid_ThenReturnEnrollmentRecord()
			{
				var filePath = "enrollments.csv";
				var fileReader = new Mock<IFileReader>(MockBehavior.Strict);
				var enrollmentReader = new EnrollmentReaderCSV();

				//build all field values dynamically for easier asserts
				//build effective date string dynamically from today so test does not start failing in future
				var firstName = "Jane";
				var lastName = "Doe";
				var dateOfBirth = new DateTime(1975, 4, 1);
				var planType = EnrollmentPlanType.HRA;
				var effectiveDate = DateTime.Today.AddDays(10);

				var enrollmentCSVLines = new List<string>
				{
					String.Format("{0},{1},{2},{3},{4}", firstName, lastName, dateOfBirth.ToString("MMddyyyy"), planType, effectiveDate.ToString("MMddyyyy"))
				};

				fileReader.Setup(x => x.ReadLines(It.IsAny<string>())).Returns(enrollmentCSVLines);

				var enrollmentRecords = enrollmentReader.ReadFromFile(fileReader.Object, filePath).ToList();

				Assert.IsTrue(enrollmentRecords.Count == 1);
				Assert.IsTrue(enrollmentRecords[0].FirstName == firstName);
				Assert.IsTrue(enrollmentRecords[0].LastName == lastName);
				Assert.IsTrue(enrollmentRecords[0].DateOfBirth == dateOfBirth);
				Assert.IsTrue(enrollmentRecords[0].PlanType == planType);
				Assert.IsTrue(enrollmentRecords[0].EffectiveDate == effectiveDate);
			}
		}
	}
}
