namespace App.Tests
{
    using App.Common;
    using System;

	public class MockCurrentDateTimeProvider : ICurrentDateTimeProvider
	{
		private DateTime fakeCurrentTime;

		public MockCurrentDateTimeProvider(DateTime fakeCurrentTime)
		{
			this.fakeCurrentTime = fakeCurrentTime;
		}

		public DateTime Now()
		{
			return this.fakeCurrentTime;
		}

		public DateTime Today()
		{
			return this.fakeCurrentTime.Date;
		}

		public DateTime UtcNow()
		{
			return this.fakeCurrentTime.ToUniversalTime();
		}
	}
}