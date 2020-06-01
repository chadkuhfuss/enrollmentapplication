namespace EnrollmentImport.Classes
{
	using EnrollmentImport.Enumerations;
	using System;

	/// <summary>
	/// Class for storing enrollment data
	/// </summary>
	public class Enrollment
	{
		private string firstName;
		private string lastName;
		private DateTime dateOfBirth;
		private EnrollmentPlanType planType;
		private DateTime effectiveDate;
		private EnrollmentStatus status;

		/// <summary>
		/// Constructor for the Enrollment
		/// </summary>
		public Enrollment()
		{
		}

		/// <summary>
		/// The First Name for the Enrollment
		/// </summary>
		public string FirstName
		{
			get
			{
				return this.firstName;
			}
			set
			{
				this.firstName = value;
			}
		}

		/// <summary>
		/// The Last Name for the Enrollment
		/// </summary>
		public string LastName
		{
			get
			{
				return this.lastName;
			}
			set
			{
				this.lastName = value;
			}
		}

		/// <summary>
		/// The Date of Birth for the Enrollment
		/// </summary>
		public DateTime DateOfBirth
		{
			get
			{
				return this.dateOfBirth;
			}
			set
			{
				this.dateOfBirth = value;
			}
		}

		/// <summary>
		/// The Plan Type for the Enrollment
		/// </summary>
		public EnrollmentPlanType PlanType
		{
			get
			{
				return this.planType;
			}
			set
			{
				this.planType = value;
			}
		}

		/// <summary>
		/// The Effective Date of the Enrollment
		/// </summary>
		public DateTime EffectiveDate
		{
			get
			{
				return this.effectiveDate;
			}
			set
			{
				this.effectiveDate = value;
			}
		}

		/// <summary>
		/// The Status of the Enrollment
		/// </summary>
		public EnrollmentStatus Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}
	}
}
