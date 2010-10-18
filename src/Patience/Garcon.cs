using System;
using System.Diagnostics;
using System.Threading;

namespace Patience {
	public class Garcon {
		private readonly TimeSpan _pollingPeriod;
		private readonly TimeSpan _timeout;
		private Stopwatch _timer;

		private Stopwatch Timer {
			get { return _timer ?? (_timer = new Stopwatch()); }
		}

	    public Garcon(TimeSpan pollingPeriod, TimeSpan timeout) {
			_pollingPeriod = pollingPeriod;
			_timeout = timeout;
		}

		public void WaitFor(Func<Boolean> what) {
			StartTiming();

			try {
				WaitCore(what);
			} finally {
				StopTiming();
			}
		}

		private void WaitCore(Func<bool> what) {
			while (false == what()) {
				if (Timer.Elapsed > _timeout)
					TimeOut();

				Sleep();
			}
		}

		private void StartTiming() {
			Timer.Start();
		}

		private void StopTiming() {
			Timer.Stop();
		}

		private void Sleep() {
			Thread.Sleep(_pollingPeriod);
		}

		private void TimeOut() {
            if (Debugger.IsAttached) {
                Timer.Reset();
                Timer.Start();                
            } else {
                throw new WaitTimeoutException(_timeout, "Timed out after waiting for about {0}", _timeout);
            }
		}
	}
}