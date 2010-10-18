using System;
using System.Runtime.Serialization;

namespace Patience {
	public class WaitTimeoutException : Exception {
		private readonly TimeSpan _elapsed;

		public WaitTimeoutException() {}
		
		public WaitTimeoutException(string message) : base(message) {}
		
		public WaitTimeoutException(string message, Exception innerException) :
			base(message, innerException) {}
		
		protected WaitTimeoutException(SerializationInfo info, StreamingContext ctx) : 
			base(info, ctx) {}

		public WaitTimeoutException(string message, params Object[] args) : 
			base(String.Format(message, args)) { }

		public WaitTimeoutException(TimeSpan elapsed, string message, params Object[] args) : 
			base(String.Format(message, args)) {
			_elapsed = elapsed;
		}

		public TimeSpan Elapsed {
			get { return _elapsed; }
		}
	}
}