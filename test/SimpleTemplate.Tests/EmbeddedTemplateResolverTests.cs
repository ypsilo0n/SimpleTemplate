using FluentAssertions;
using SimpleTemplate.Templating;
using System;
using Xunit;

namespace SimpleTemplate.Test
{
	public class EmbeddedTemplateResolverTests
	{
		[Fact]
		public void ResolveTemplate_TemplateNotFound_ThrowsArgumentException()
		{
			var resolver = new EmbeddedTemplateResolver(GetType());

			Action action = () => resolver.Resolve("Templates.NonExistant.html");

			action.ShouldThrow<ArgumentException>()
				.WithMessage("Can't find template [SimpleTemplate.Tests.Templates.NonExistant.html]");
		}

		[Fact]
		public void ResolveTemplate_TemplateFound_ReturnsTemplate()
		{
			var resolver = new EmbeddedTemplateResolver(GetType());

			var result = resolver.Resolve("Templates.Test.html");

			result.Should().Be("<h1>$Name$</h1>");
		}
	}
}