using System;
using NUnit.Framework;
using Patience.Fluent;

namespace Patience.Examples {
	[TestFixture]
	public class How_to {
		[Test]
		public void Wait_for_a_function_to_return_true() {
			Wait.Patiently.Until(() => true);
		}

		[Test]
		public void Wait_for_a_limited_time() {
			Wait.Patiently.
				ForUpTo(TimeSpan.FromSeconds(1)).
				Until(() => true);
		}

		[Test]
		public void Wait_with_a_specific_sample_rate() {
			Wait.SlightlyFrustrated.
				ForUpTo(TimeSpan.FromSeconds(1)).
                PollingEvery(TimeSpan.FromMilliseconds(100)).
				Until(() => true);
		}

		[Test]
		public void Know_that_your_wait_timed_out() {
			Assert.That(() => A_wait_that_times_out(), Throws.TypeOf(typeof(WaitTimeoutException)));
		}

		[Test]
		public void Do_something_if_your_wait_times_out() {
			Wait.TappingFoot.
                IfTimesOutThen(timedOut => Console.WriteLine("Timed out after waiting for about <{0}>", timedOut.Elapsed)).
				PollingEvery(TimeSpan.FromMilliseconds(10)).
				ForUpTo(TimeSpan.FromMilliseconds(50)).
				Until(Hell_freezes_over);		
		}

		private static void A_wait_that_times_out() {
			Wait.Patiently.
				PollingEvery(TimeSpan.FromMilliseconds(10)).
				ForUpTo(TimeSpan.FromMilliseconds(50)).
				Until(Hell_freezes_over);
		}

		private static bool Hell_freezes_over() {
			return false;
		}
	}
}
