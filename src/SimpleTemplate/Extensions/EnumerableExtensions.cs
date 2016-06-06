using System.Collections;

namespace SimpleTemplate.Extensions
{
	public static class EnumerableExtensions
	{
		public static bool Any(this IEnumerable enumerable)
		{
			var enumerator = enumerable.GetEnumerator();

			while (enumerator.MoveNext())
			{
				return true;
			}

			return false;
		}
	}
}
