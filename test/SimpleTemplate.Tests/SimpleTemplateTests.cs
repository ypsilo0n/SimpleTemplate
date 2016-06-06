using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace SimpleTemplate.Test
{
	public class SimpleTemplateTests
	{
		private static dynamic _model = new
		{
			Person = new
			{
				Name = "Hugo",
				Age = 25
			},
			UserName = "hugomaster2000",
			IsTrue = true
		};

		[Theory]
		[MemberData(nameof(InvalidArguments))]
		public void Render_InvalidArgumentsSupplied_Throws(string template, object model)
		{
			Action action = () => Template.Render(template, model);

			action.ShouldThrow<ArgumentException>();
		}

		public static IEnumerable<object[]> InvalidArguments
		{
			get
			{
				yield return new object[] { "", new { Name = "Hugo" } };
				yield return new object[] { string.Empty, new { Name = "Hugo" } };
				yield return new object[] { "  ", new { Name = "Hugo" } };
				yield return new object[] { "This is a template", null };
			}
		}

		[Theory]
		[InlineData("$Missing$", "Property [Missing] not found")]
		[InlineData("My Name is $Person.FirstName$", "Property [Person.FirstName] not found")]
		public void Render_UnkownModelPropertyInTemplate_Throws(string template, string expectedMessage)
		{
			Action action = () => Template.Render(template, new { Name = "Hugo" });

			action.ShouldThrow<ArgumentException>()
				.WithMessage(expectedMessage);
		}

		[Theory]
		[InlineData("Test", "Test")]
		[InlineData("My Name is $Name$.", "My Name is Hugo.")]
		[InlineData("<h1>My Name is $Name$.</h1>", "<h1>My Name is Hugo.</h1>")]
		public void Render_SimpleModel_RenderedTemplate(string template, string expected)
		{
			var result = Template.Render(template, new { Name = "Hugo" });

			result.Should().Be(expected);
		}

		[Theory]
		[InlineData("My Name is $Person.Name$", "My Name is Hugo")]
		[InlineData("My Name is $Person.Name$, I'm $Person.Age$.", "My Name is Hugo, I'm 25.")]
		[InlineData("My Name is $Person.Name$, I'm $Person.Age$. You know me as $UserName$.", "My Name is Hugo, I'm 25. You know me as hugomaster2000.")]
		public void Render_ComplexModel_RenderedTemplate(string template, string expected)
		{
			var model = new
			{
				Person = new
				{
					Name = "Hugo",
					Age = 25
				},
				UserName = "hugomaster2000"
			};

			var result = Template.Render(template, model);

			result.Should().Be(expected);
		}

		[Fact]
		public void Throws_on_missing_property()
		{
			var exception = Record.Exception(() => Template.Render("My Name is $Person.FirstName$", _model));

			Assert.Equal("Property [Person.FirstName] not found", exception.Message);
		}
	}
}
