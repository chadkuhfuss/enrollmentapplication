namespace EnrollmentImportTests
{
	using EnrollmentImport.Classes;
	using EnrollmentImport.Enumerations;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using System;

	/// <summary>
	/// Class for testing EnrollmentValidator class
	/// </summary>
	[TestClass]
	public class EnrollmentValidatorTests
	{
		/// <summary>
		/// Class for testing Validate method
		/// </summary>
		[TestClass]
		public class EnrollmentValidator_Validate_Tests
		{
			[TestMethod]
			[ExpectedException(typeof(Exception))]
			public void WhenFirstNameIsNull_ThenExceptionIsThrown()
			{
				var enrollmentRecord = new Enrollment
				{
					FirstName = null,
					LastName = "Doe",
					DateOfBirth = new DateTime(1983, 9, 13),
					PlanType = EnrollmentPlanType.HSA,
					EffectiveDate = DateTime.Today.AddDays(5)
				};

				var enrollmentValidator = new EnrollmentValidator();

				var isValid = enrollmentValidator.Validate(enrollmentRecord);
			}

			[TestMethod]
			[ExpectedException(typeof(Exception))]
			public void WhenFirstNameIsEmpty_ThenExceptionIsThrown()
			{
				var enrollmentRecord = new Enrollment
				{
					FirstName = String.Empty,
					LastName = "Doe",
					DateOfBirth = new DateTime(1983, 9, 13),
					PlanType = EnrollmentPlanType.HSA,
					EffectiveDate = DateTime.Today.AddDays(5)
				};

				var enrollmentValidator = new EnrollmentValidator();

				var isValid = enrollmentValidator.Validate(enrollmentRecord);
			}

			[TestMethod]
			[ExpectedException(typeof(Exception))]
			public void WhenLastNameIsNull_ThenExceptionIsThrown()
			{
				var enrollmentRecord = new Enrollment
				{
					FirstName = "Jane",
					LastName = null,
					DateOfBirth = new DateTime(1983, 9, 13),
					PlanType = EnrollmentPlanType.HSA,
					EffectiveDate = DateTime.Today.AddDays(5)
				};

				var enrollmentValidator = new EnrollmentValidator();

				var isValid = enrollmentValidator.Validate(enrollmentRecord);
			}

			[TestMethod]
			[ExpectedException(typeof(Exception))]
			public void WhenLastNameIsEmpty_ThenExceptionIsThrown()
			{
				var enrollmentRecord = new Enrollment
				{
					FirstName = "Jane",
					LastName = String.Empty,
					DateOfBirth = new DateTime(1983, 9, 13),
					PlanType = EnrollmentPlanType.HSA,
					EffectiveDate = DateTime.Today.AddDays(5)
				};

				var enrollmentValidator = new EnrollmentValidator();

				var isValid = enrollmentValidator.Validate(enrollmentRecord);
			}

			[TestMethod]
			[ExpectedException(typeof(Exception))]
			public void WhenDateOfBirthIsMinDate_ThenExceptionIsThrown()
			{
				var enrollmentRecord = new Enrollment
				{
					FirstName = "Jane",
					LastName = "Doe",
					DateOfBirth = DateTime.MinValue,
					PlanType = EnrollmentPlanType.HSA,
					EffectiveDate = DateTime.Today.AddDays(5)
				};

				var enrollmentValidator = new EnrollmentValidator();

				var isValid = enrollmentValidator.Validate(enrollmentRecord);
			}

			[TestMethod]
			[ExpectedException(typeof(Exception))]
			public void WhenPlanTypeIsNone_ThenExceptionIsThrown()
			{
				var enrollmentRecord = new Enrollment
				{
					FirstName = "Jane",
					LastName = "Doe",
					DateOfBirth = new DateTime(1975, 7, 28),
					PlanType = EnrollmentPlanType.None,
					EffectiveDate = DateTime.Today.AddDays(5)
				};

				var enrollmentValidator = new EnrollmentValidator();

				var isValid = enrollmentValidator.Validate(enrollmentRecord);
			}

			[TestMethod]
			[ExpectedException(typeof(Exception))]
			public void WhenEffectiveDateIsMinDate_ThenExceptionIsThrown()
			{
				var enrollmentRecord = new Enrollment
				{
					FirstName = "Jane",
					LastName = "Doe",
					DateOfBirth = new DateTime(1975, 7, 28),
					PlanType = EnrollmentPlanType.HSA,
					EffectiveDate = DateTime.MinValue
				};

				var enrollmentValidator = new EnrollmentValidator();

				var isValid = enrollmentValidator.Validate(enrollmentRecord);
			}

			[TestMethod]
			public void WhenDateOfBirthIsLessThan18_ThenReturnsFalse()
			{
				var dateOfBirth = DateTime.Today.AddYears(-1);

				var enrollmentRecord = new Enrollment
				{
					FirstName = "Jane",
					LastName = "Doe",
					DateOfBirth = dateOfBirth,
					PlanType = EnrollmentPlanType.HSA,
					EffectiveDate = DateTime.Today.AddDays(3)
				};

				var enrollmentValidator = new EnrollmentValidator();

				var isValid = enrollmentValidator.Validate(enrollmentRecord);

				Assert.IsFalse(isValid);
			}

			[TestMethod]
			public void WhenEffectiveDateMoreThan30DaysInFuture_ThenReturnsFalse()
			{
				var effectiveDate = DateTime.Today.AddDays(53);

				var enrollmentRecord = new Enrollment
				{
					FirstName = "Jane",
					LastName = "Doe",
					DateOfBirth = new DateTime(1973, 3, 14),
					PlanType = EnrollmentPlanType.HSA,
					EffectiveDate = effectiveDate
				};

				var enrollmentValidator = new EnrollmentValidator();

				var isValid = enrollmentValidator.Validate(enrollmentRecord);

				Assert.IsFalse(isValid);
			}
		}
	}
}
