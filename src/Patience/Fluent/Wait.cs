using System;

namespace Patience.Fluent {
	public class Wait {
		private TimeSpan _defaultPollingPeriod = TimeSpan.FromSeconds(5);
		private TimeSpan _defaultTimeout = TimeSpan.FromSeconds(30);
        private Action<WaitTimeoutException> _doThis;

		public static Wait Patiently {
			get {
				return new Wait();
			}
		}

		public static Wait TappingFoot {
			get {
				return new Wait();
			}
		}

		public static Wait SlightlyFrustrated {
			get {
				return new Wait();
			}
		}

		public Wait ForUpTo(TimeSpan howLong) {
			_defaultTimeout = howLong;
			return this;
		}

		public Wait PollingEvery(TimeSpan howLong) {
			_defaultPollingPeriod = howLong;
			return this;
		}

		public Wait IfTimesOutThen(Action<WaitTimeoutException> doThis) {
			_doThis = doThis;
			return this;
		}

		public void Until(Func<Boolean> what) {
			try {
				new Garcon(_defaultPollingPeriod, _defaultTimeout).WaitFor(what);
			} catch (WaitTimeoutException e) {
				if (null == _doThis)
					throw;

				_doThis(e);
			}
		}
	}
}