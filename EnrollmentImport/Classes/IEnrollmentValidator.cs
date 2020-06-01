namespace EnrollmentImport.Classes
{
	/// <summary>
	/// Interface for validating an Enrollment class object
	/// </summary>
	public interface IEnrollmentValidator
	{
		/// <summary>
		/// Validates an Enrollment record
		/// </summary>
		/// <param name="enrollmentRecord">The Enrollment record to validate.</param>
		/// <returns>True if record is valid, otherwise false.</returns>
		bool Validate(Enrollment enrollmentRecord);
	}
}
