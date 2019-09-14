namespace App.Common
{
	using System;

	public interface ICurrentDateTimeProvider
	{
		DateTime Now();

		DateTime UtcNow();

		DateTime Today();
	}

	public class CurrentDateTimeProvider : ICurrentDateTimeProvider
	{
		public DateTime Now()
		{
			return DateTime.Now;
		}

		public DateTime UtcNow()
		{
			return DateTime.UtcNow;
		}

		public DateTime Today()
		{
			return DateTime.Today;
		}
	}
}
