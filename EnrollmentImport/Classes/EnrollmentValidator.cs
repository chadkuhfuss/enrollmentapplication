namespace EnrollmentImport.Classes
{
	using EnrollmentImport.Enumerations;
	using System;

	/// <summary>
	/// Class that validates an Enrollment record
	/// </summary>
	public class EnrollmentValidator : IEnrollmentValidator
	{
		/// <summary>
		/// Validates an Enrollment record
		/// </summary>
		/// <param name="enrollmentRecord">The Enrollment record to validate.</param>
		/// <returns>True if record is valid, otherwise false.</returns>
		public bool Validate(Enrollment enrollmentRecord)
		{
			if (String.IsNullOrWhiteSpace(enrollmentRecord.FirstName))
				throw new Exception("FirstName is required.");

			if (String.IsNullOrWhiteSpace(enrollmentRecord.LastName))
				throw new Exception("LastName is required.");

			if (enrollmentRecord.PlanType == EnrollmentPlanType.None)
				throw new Exception("PlanType is required.");

			if (enrollmentRecord.DateOfBirth == DateTime.MinValue)
				throw new Exception("DateOfBirth is required.");

			if (enrollmentRecord.EffectiveDate == DateTime.MinValue)
				throw new Exception("EffectiveDate is required.");

			if (enrollmentRecord.DateOfBirth.AddYears(18) > DateTime.Today)
				return false;

			if (enrollmentRecord.EffectiveDate > DateTime.Today.AddDays(30))
				return false;

			return true;
		}
	}
}
