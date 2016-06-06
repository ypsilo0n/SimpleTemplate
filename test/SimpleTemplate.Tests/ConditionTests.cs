using FluentAssertions;
using SimpleTemplate.Test.Data;
using System.Collections.Generic;
using Xunit;

namespace SimpleTemplate.Test
{
	public class ConditionTests
	{
		[Theory]
		[MemberData(nameof(ConditionTestData))]
		public void RenderIf(TestData testData)
		{
			var result = Template.Render(testData.Template, testData.Model);

			result.Should().Be(testData.Expected);
		}

		public static IEnumerable<object[]> ConditionTestData
		{
			get
			{
				yield return new object[] { new TestData(nameof(SimpleIf) + "-True", SimpleIf.ModelTrue, SimpleIf.Template, SimpleIf.ExpectedTrue) };
				yield return new object[] { new TestData(nameof(SimpleIf) + "-False", SimpleIf.ModelFalse, SimpleIf.Template, SimpleIf.ExpectedFalse) };
				yield return new object[] { new TestData(nameof(IfElse) + "-True", IfElse.ModelTrue, IfElse.Template, IfElse.ExpectedTrue) };
				yield return new object[] { new TestData(nameof(IfElse) + "-False", IfElse.ModelFalse, IfElse.Template, IfElse.ExpectedFalse) };
				yield return new object[] { new TestData(nameof(MultipleIfs) + "-True", MultipleIfs.ModelTrue, MultipleIfs.Template, MultipleIfs.ExpectedTrue) };
				yield return new object[] { new TestData(nameof(MultipleIfs) + "-False", MultipleIfs.ModelFalse, MultipleIfs.Template, MultipleIfs.ExpectedFalse) };
				yield return new object[] { new TestData(nameof(NestedIfs) + "-True", NestedIfs.ModelTrue, NestedIfs.Template, NestedIfs.ExpectedTrue) };
				yield return new object[] { new TestData(nameof(NestedIfs) + "-False", NestedIfs.ModelFalse, NestedIfs.Template, NestedIfs.ExpectedFalse) };
			}
		}
	}
}
