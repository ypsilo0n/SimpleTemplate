using SimpleTemplate.Test.Data;
using System.Collections.Generic;
using Xunit;

namespace SimpleTemplate.Test
{
	public class LoopTests
	{
		[Theory]
		[MemberData(nameof(LoopTestData))]
		public void RenderLoop(TestData testData)
		{
			var result = Template.Render(testData.Template, testData.Model);

			Assert.Equal(testData.Expected, result);
		}

		public static IEnumerable<object[]> LoopTestData
		{
			get
			{
				yield return new[] { new TestData(nameof(SimpleLoop), SimpleLoop.Model, SimpleLoop.Template, SimpleLoop.Expected) };
				yield return new[] { new TestData(nameof(MultipleLoops), MultipleLoops.Model, MultipleLoops.Template, MultipleLoops.Expected) };
				yield return new[] { new TestData(nameof(IfInsideLoop), IfInsideLoop.Model, IfInsideLoop.Template, IfInsideLoop.Expected) };
				yield return new[] { new TestData(nameof(NestedLoops), NestedLoops.Model, NestedLoops.Template, NestedLoops.Expected) };
			}
		}
	}
}
