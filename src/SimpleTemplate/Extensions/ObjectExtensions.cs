using Newtonsoft.Json.Linq;
using SimpleTemplate.Helper;
using System.Collections;

namespace SimpleTemplate.Extensions
{
	public static class ObjectExtensions
	{
		public static string ResolveObjectToRegexGroupName(this object condition)
		{
			var result = false;

			if (condition != null && !bool.TryParse(condition.ToString(), out result))
			{
				if (!(condition is string) && condition is IEnumerable)
				{
					result = ((IEnumerable)condition).Any();
				}
				else if (!string.IsNullOrWhiteSpace(condition.ToString()))
				{
					result = true;
				}
			}

			return result ? TemplateRegex.TrueGroup : TemplateRegex.FalseGroup;
		}

		public static object GetPropertyValue(this JObject model, string propertyName)
		{
			var result = model.SelectToken(propertyName);

			Throw.IfNull(result, $"Property [{propertyName}] not found");

			// Will not work as "return result is JArray ? ret : ret.ToString()"
			if (result is JArray)
			{
				return result;
			}
			else
			{
				return result.ToString();
			}
		}
	}
}
