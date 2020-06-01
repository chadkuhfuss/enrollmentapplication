namespace EnrollmentImportTests
{
	using System;
	using System.Collections.Generic;
	using Castle.Core.Internal;
	using EnrollmentImport;
	using EnrollmentImport.Classes;
	using EnrollmentImport.Enumerations;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Moq;

	/// <summary>
	/// Class for testing EnrollmentImporter class
	/// </summary>
	[TestClass]
	public class EnrollmentImporterTests
	{
		/// <summary>
		/// Class for testing Import method
		/// </summary>
		[TestClass]
		public class EnrollmentImporter_Import_Tests
		{
			[TestMethod]
			[ExpectedException(typeof(InvalidOperationException))]
			public void WhenFilePathIsEmpty_ThenExceptionIsThrown()
			{
				var filePath = String.Empty;
				var fileReader = new FileReader();
				var enrollmentReader = new EnrollmentReaderCSV();
				var enrollmentValidator = new EnrollmentValidator();

				var enrollmentImporter = new EnrollmentImporter();
				enrollmentImporter.Import(filePath, fileReader, enrollmentReader, enrollmentValidator);
			}

			[TestMethod]
			[ExpectedException(typeof(InvalidOperationException))]
			public void WhenFileReaderIsNull_ThenExceptionIsThrown()
			{
				var filePath = "enrollments.csv";
				var enrollmentReader = new EnrollmentReaderCSV();
				var enrollmentValidator = new EnrollmentValidator();

				var enrollmentImporter = new EnrollmentImporter();
				enrollmentImporter.Import(filePath, null, enrollmentReader, enrollmentValidator);
			}

			[TestMethod]
			[ExpectedException(typeof(InvalidOperationException))]
			public void WhenFilePathIsNull_ThenExceptionIsThrown()
			{
				string filePath = null;
				var fileReader = new FileReader();
				var enrollmentReader = new EnrollmentReaderCSV();
				var enrollmentValidator = new EnrollmentValidator();

				var enrollmentImporter = new EnrollmentImporter();
				enrollmentImporter.Import(filePath, fileReader, enrollmentReader, enrollmentValidator);
			}

			[TestMethod]
			[ExpectedException(typeof(InvalidOperationException))]
			public void WhenReaderIsNull_ThenExceptionIsThrown()
			{
				string filePath = "enrollments.csv";
				var fileReader = new FileReader();
				var enrollmentValidator = new EnrollmentValidator();

				var enrollmentImporter = new EnrollmentImporter();
				enrollmentImporter.Import(filePath, fileReader, null, enrollmentValidator);
			}

			[TestMethod]
			[ExpectedException(typeof(InvalidOperationException))]
			public void WhenValidatorIsNull_ThenExceptionIsThrown()
			{
				string filePath = "enrollments.csv";
				var fileReader = new FileReader();
				var enrollmentReader = new EnrollmentReaderCSV();

				var enrollmentImporter = new EnrollmentImporter();
				enrollmentImporter.Import(filePath, fileReader, enrollmentReader, null);
			}

			[TestMethod]
			public void WhenValidParametersAndOneRecordHasMissingFirstName_ThenReturnNull()
			{
				string filePath = "enrollments.csv";
				var fileReader = new FileReader();
				var enrollmentReader = new Mock<IEnrollmentReader>(MockBehavior.Strict);
				var enrollmentValidator = new EnrollmentValidator();

				var enrollmentWithMissingFirstName = new List<Enrollment>
				{
					new Enrollment
					{
						FirstName = String.Empty,
						LastName = "Doe",
						DateOfBirth = new DateTime(1970, 8, 16),
						PlanType = EnrollmentPlanType.FSA,
						EffectiveDate = DateTime.Today.AddDays(3)
					}
				};

				enrollmentReader.Setup(x => x.ReadFromFile(It.IsAny<IFileReader>(), It.IsAny<string>())).Returns(enrollmentWithMissingFirstName);

				var enrollmentImporter = new EnrollmentImporter();
				var savedEnrollmentRecords = enrollmentImporter.Import(filePath, fileReader, enrollmentReader.Object, enrollmentValidator);

				Assert.IsTrue(savedEnrollmentRecords.IsNullOrEmpty());
			}

			[TestMethod]
			public void WhenValidParametersAndOneRecordHasMissingLastName_ThenReturnNull()
			{
				string filePath = "enrollments.csv";
				var fileReader = new FileReader();
				var enrollmentReader = new Mock<IEnrollmentReader>(MockBehavior.Strict);
				var enrollmentValidator = new EnrollmentValidator();

				var enrollmentWithMissingLastName = new List<Enrollment>
				{
					new Enrollment
					{
						FirstName = "Jane",
						LastName = String.Empty,
						DateOfBirth = new DateTime(1970, 8, 16),
						PlanType = EnrollmentPlanType.FSA,
						EffectiveDate = DateTime.Today.AddDays(3)
					}
				};

				enrollmentReader.Setup(x => x.ReadFromFile(It.IsAny<IFileReader>(), It.IsAny<string>())).Returns(enrollmentWithMissingLastName);

				var enrollmentImporter = new EnrollmentImporter();
				var savedEnrollmentRecords = enrollmentImporter.Import(filePath, fileReader, enrollmentReader.Object, enrollmentValidator);

				Assert.IsTrue(savedEnrollmentRecords.IsNullOrEmpty());
			}

			[TestMethod]
			public void WhenValidParametersAndOneRecordHasMissingDateOfBirth_ThenReturnNull()
			{
				string filePath = "enrollments.csv";
				var fileReader = new FileReader();
				var enrollmentReader = new Mock<IEnrollmentReader>(MockBehavior.Strict);
				var enrollmentValidator = new EnrollmentValidator();

				var enrollmentWithMissingDateOfBirth = new List<Enrollment>
				{
					new Enrollment
					{
						FirstName = "Jane",
						LastName = "Doe",
						DateOfBirth = DateTime.MinValue,
						PlanType = EnrollmentPlanType.FSA,
						EffectiveDate = DateTime.Today.AddDays(3)
					}
				};

				enrollmentReader.Setup(x => x.ReadFromFile(It.IsAny<IFileReader>(), It.IsAny<string>())).Returns(enrollmentWithMissingDateOfBirth);

				var enrollmentImporter = new EnrollmentImporter();
				var savedEnrollmentRecords = enrollmentImporter.Import(filePath, fileReader, enrollmentReader.Object, enrollmentValidator);

				Assert.IsTrue(savedEnrollmentRecords.IsNullOrEmpty());
			}

			[TestMethod]
			public void WhenValidParametersAndOneRecordHasMissingPlanType_ThenReturnNull()
			{
				string filePath = "enrollments.csv";
				var fileReader = new FileReader();
				var enrollmentReader = new Mock<IEnrollmentReader>(MockBehavior.Strict);
				var enrollmentValidator = new EnrollmentValidator();

				var enrollmentWithMissingPlanType = new List<Enrollment>
				{
					new Enrollment
					{
						FirstName = "Jane",
						LastName = "Doe",
						DateOfBirth = new DateTime(1970, 8, 16),
						PlanType = EnrollmentPlanType.None,
						EffectiveDate = DateTime.Today.AddDays(3)
					}
				};

				enrollmentReader.Setup(x => x.ReadFromFile(It.IsAny<IFileReader>(), It.IsAny<string>())).Returns(enrollmentWithMissingPlanType);

				var enrollmentImporter = new EnrollmentImporter();
				var savedEnrollmentRecords = enrollmentImporter.Import(filePath, fileReader, enrollmentReader.Object, enrollmentValidator);

				Assert.IsTrue(savedEnrollmentRecords.IsNullOrEmpty());
			}

			[TestMethod]
			public void WhenValidParametersAndOneRecordHasMissingEffectiveDate_ThenReturnNull()
			{
				string filePath = "enrollments.csv";
				var fileReader = new FileReader();
				var enrollmentReader = new Mock<IEnrollmentReader>(MockBehavior.Strict);
				var enrollmentValidator = new EnrollmentValidator();

				var enrollmentWithMissingEffectiveDate = new List<Enrollment>
				{
					new Enrollment
					{
						FirstName = "Jane",
						LastName = "Doe",
						DateOfBirth = new DateTime(1970, 8, 16),
						PlanType = EnrollmentPlanType.FSA,
						EffectiveDate = DateTime.MinValue
					}
				};

				enrollmentReader.Setup(x => x.ReadFromFile(It.IsAny<IFileReader>(), It.IsAny<string>())).Returns(enrollmentWithMissingEffectiveDate);

				var enrollmentImporter = new EnrollmentImporter();
				var savedEnrollmentRecords = enrollmentImporter.Import(filePath, fileReader, enrollmentReader.Object, enrollmentValidator);

				Assert.IsTrue(savedEnrollmentRecords.IsNullOrEmpty());
			}

			[TestMethod]
			public void WhenValidParametersAndOneRecordHasDateOfBirthUnder18_ThenReturnOneRecordWithStatusOfRejected()
			{
				string filePath = "enrollments.csv";
				var fileReader = new FileReader();
				var enrollmentReader = new Mock<IEnrollmentReader>(MockBehavior.Strict);
				var enrollmentValidator = new EnrollmentValidator();

				var enrollmentWithOneValidRecord = new List<Enrollment>
				{
					new Enrollment
					{
						FirstName = "Jane",
						LastName = "Doe",
						DateOfBirth = new DateTime(2015, 8, 16),
						PlanType = EnrollmentPlanType.FSA,
						EffectiveDate = DateTime.Today.AddDays(3)
					}
				};

				enrollmentReader.Setup(x => x.ReadFromFile(It.IsAny<IFileReader>(), It.IsAny<string>())).Returns(enrollmentWithOneValidRecord);

				var enrollmentImporter = new EnrollmentImporter();
				var savedEnrollmentRecords = enrollmentImporter.Import(filePath, fileReader, enrollmentReader.Object, enrollmentValidator);

				Assert.IsTrue(savedEnrollmentRecords.Count == 1);
				Assert.IsTrue(savedEnrollmentRecords[0].Status == EnrollmentStatus.Rejected);
			}

			[TestMethod]
			public void WhenValidParametersAndOneRecordHasEffectiveDateMoreThan30DaysInFuture_ThenReturnOneRecordWithStatusOfRejected()
			{
				string filePath = "enrollments.csv";
				var fileReader = new FileReader();
				var enrollmentReader = new Mock<IEnrollmentReader>(MockBehavior.Strict);
				var enrollmentValidator = new EnrollmentValidator();

				var enrollmentWithOneValidRecord = new List<Enrollment>
				{
					new Enrollment
					{
						FirstName = "Jane",
						LastName = "Doe",
						DateOfBirth = new DateTime(1955, 8, 16),
						PlanType = EnrollmentPlanType.FSA,
						EffectiveDate = DateTime.Today.AddDays(60)
					}
				};

				enrollmentReader.Setup(x => x.ReadFromFile(It.IsAny<IFileReader>(), It.IsAny<string>())).Returns(enrollmentWithOneValidRecord);

				var enrollmentImporter = new EnrollmentImporter();
				var savedEnrollmentRecords = enrollmentImporter.Import(filePath, fileReader, enrollmentReader.Object, enrollmentValidator);

				Assert.IsTrue(savedEnrollmentRecords.Count == 1);
				Assert.IsTrue(savedEnrollmentRecords[0].Status == EnrollmentStatus.Rejected);
			}

			[TestMethod]
			public void WhenValidParametersAndOneRecordHasOneValidRecord_ThenReturnOneRecord()
			{
				string filePath = "enrollments.csv";
				var fileReader = new FileReader();
				var enrollmentReader = new Mock<IEnrollmentReader>(MockBehavior.Strict);
				var enrollmentValidator = new EnrollmentValidator();

				var enrollmentWithOneValidRecord = new List<Enrollment>
				{
					new Enrollment
					{
						FirstName = "Jane",
						LastName = "Doe",
						DateOfBirth = new DateTime(1970, 8, 16),
						PlanType = EnrollmentPlanType.FSA,
						EffectiveDate = DateTime.Today.AddDays(3)
					}
				};

				enrollmentReader.Setup(x => x.ReadFromFile(It.IsAny<IFileReader>(), It.IsAny<string>())).Returns(enrollmentWithOneValidRecord);

				var enrollmentImporter = new EnrollmentImporter();
				var savedEnrollmentRecords = enrollmentImporter.Import(filePath, fileReader, enrollmentReader.Object, enrollmentValidator);

				Assert.IsTrue(savedEnrollmentRecords.Count == 1);
				Assert.IsTrue(savedEnrollmentRecords[0].Status == EnrollmentStatus.Accepted);
			}

			[TestMethod]
			public void WhenValidParametersAndOneRecordHasTwoValidRecords_ThenReturnTwoRecords()
			{
				string filePath = "enrollments.csv";
				var fileReader = new FileReader();
				var enrollmentReader = new Mock<IEnrollmentReader>(MockBehavior.Strict);
				var enrollmentValidator = new EnrollmentValidator();

				var enrollmentWithOneValidRecord = new List<Enrollment>
				{
					new Enrollment
					{
						FirstName = "Jane",
						LastName = "Doe",
						DateOfBirth = new DateTime(1970, 8, 16),
						PlanType = EnrollmentPlanType.FSA,
						EffectiveDate = DateTime.Today.AddDays(3)
					},
					new Enrollment
					{
						FirstName = "John",
						LastName = "Henry",
						DateOfBirth = new DateTime(1973, 8, 16),
						PlanType = EnrollmentPlanType.HRA,
						EffectiveDate = DateTime.Today.AddDays(13)
					},
				};

				enrollmentReader.Setup(x => x.ReadFromFile(It.IsAny<IFileReader>(), It.IsAny<string>())).Returns(enrollmentWithOneValidRecord);

				var enrollmentImporter = new EnrollmentImporter();
				var savedEnrollmentRecords = enrollmentImporter.Import(filePath, fileReader, enrollmentReader.Object, enrollmentValidator);

				Assert.IsTrue(savedEnrollmentRecords.Count == 2);
				Assert.IsTrue(savedEnrollmentRecords[0].Status == EnrollmentStatus.Accepted);
				Assert.IsTrue(savedEnrollmentRecords[1].Status == EnrollmentStatus.Accepted);
			}
		}
	}
}
