using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleTemplate.Extensions;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace SimpleTemplate.Test
{
	public class ExtensionTests
	{
		[Theory]
		[MemberData(nameof(TruthyConditions))]
		public void ResolveObjectToRegexGroupName_TruthyCondition_RegexTrueGroup(object condition)
		{
			var result = condition.ResolveObjectToRegexGroupName();

			result.Should().Be(TemplateRegex.TrueGroup);
		}

		public static IEnumerable<object[]> TruthyConditions
		{
			get
			{
				yield return new object[] { new[] { 1, 2, 3 } };
				yield return new object[] { "This is a string" };
				yield return new object[] { 2.0 };
			}
		}

		[Theory]
		[MemberData(nameof(FalsyConditions))]
		public void ResolveObjectToRegexGroupName_FalsyCondition_RegexFalseGroup(object condition)
		{
			var result = condition.ResolveObjectToRegexGroupName();

			result.Should().Be(TemplateRegex.FalseGroup);
		}

		public static IEnumerable<object[]> FalsyConditions
		{
			get
			{
				yield return new object[] { "" };
				yield return new object[] { "  " };
				yield return new object[] { new int[] { } };
				yield return new object[] { null };
			}
		}

		[Theory]
		[MemberData(nameof(ModelTypes))]
		public void ResolvePropertyValue(object model, object expected)
		{
			JObject jmodel = JObject.Parse(JsonConvert.SerializeObject(model));

			var result = jmodel.GetPropertyValue("Property");

			Assert.Equal(expected, result);
			
			// TODO: Will fail for IEnumerable
			//result.Should().Be(expected);
		}

		public static IEnumerable<object[]> ModelTypes
		{
			get
			{
				yield return new object[] { new { Property = 2 }, "2" };
				yield return new object[] { new { Property = (IEnumerable)null }, "" };
				yield return new object[] { new { Property = new[] { 1, 2, 3 } }, JArray.FromObject(new[] { 1, 2, 3 }) };
				yield return new object[] { new { Property = new List<int>() { 1, 2, 3 } }, JArray.FromObject(new List<int>() { 1, 2, 3 }) };
			}
		}
	}
}
