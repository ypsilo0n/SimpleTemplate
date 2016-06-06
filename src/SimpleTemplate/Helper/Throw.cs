using System;

namespace SimpleTemplate.Helper
{
	public static class Throw
	{
		public static void IfNullOrWhiteSpace(string value, string message)
		{
			Throw<ArgumentException>.If(string.IsNullOrWhiteSpace(value), message);
		}

		public static void IfNull<T>(T value, string message)
		{
			Throw<ArgumentException>.If(value == null, message);
		}
	}

	public static class Throw<TException> where TException : Exception
	{
		public static void If(bool condition, string message)
		{
			if (condition)
			{
				throw (TException)Activator.CreateInstance(typeof(TException), message);
			}
		}
	}
}
