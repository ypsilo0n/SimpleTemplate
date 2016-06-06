using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleTemplate.Extensions;
using SimpleTemplate.Helper;
using System.Collections;
using System.Text;

namespace SimpleTemplate
{
	internal class TemplateRenderer
	{
		public string Render(string template, object model)
		{
			Throw.IfNullOrWhiteSpace(template, "Not a valid template");
			Throw.IfNull(model, "Not a valid model");

			var jmodel = JObject.Parse(JsonConvert.SerializeObject(model));

			var processedTemplate = template;

			processedTemplate = ProcessLoops(processedTemplate, jmodel);
			processedTemplate = ProcessConditions(processedTemplate, jmodel);
			processedTemplate = ProcessVars(processedTemplate, jmodel);

			return processedTemplate;
		}

		private string ProcessConditions(string template, JObject model)
		{
			var match = TemplateRegex.IfElse.Match(template);
			if (!match.Success) return template;

			var processedTemplate = template;

			while (match.Success)
			{
				var propertyName = match.Groups["cond"].Value.Trim('$');
				var propertyValue = model.GetPropertyValue(propertyName);

				var boolString = propertyValue.ResolveObjectToRegexGroupName();
				var replacement = match.Groups[boolString].Value.TrimEnd('\t').TrimStart('\r', '\n');

				processedTemplate = TemplateRegex.IfElse.Replace(processedTemplate, replacement, 1);

				match = match.NextMatch();
			}

			return ProcessConditions(processedTemplate, model);
		}

		private string ProcessLoops(string template, JObject model)
		{
			var match = TemplateRegex.ForEach.Match(template);
			if (!match.Success) return template;

			var processedTemplate = template;

			while (match.Success)
			{
				var propertyName = match.Groups["var"].Value.Trim('$');
				var context = match.Groups["ctx"].Value.TrimEnd('$');

				var propertyValue = model.GetPropertyValue(propertyName) as IEnumerable;

				Throw.IfNull(propertyValue, $"{propertyName} is not an IEnumerable [Index: {match.Groups["var"].Index}].");

				var oldBody = match.Groups["body"].Value.TrimEnd('\t', '\n', '\r');

				var i = 0;
				var sb = new StringBuilder();

				foreach (var item in propertyValue)
				{
					sb.Append(oldBody.Replace(context, $"${propertyName}[{i++}]"));
				}

				var newBody = sb.ToString().TrimStart('\n', '\r');
				processedTemplate = processedTemplate.Replace(match.Value, newBody);

				match = match.NextMatch();
			}

			return ProcessLoops(processedTemplate, model);
		}

		private string ProcessVars(string template, JObject model)
		{
			var match = TemplateRegex.VarRegex.Match(template);

			var processedTemplate = template;

			while (match.Success)
			{
				var value = model.GetPropertyValue(match.Value.Trim('$')).ToString();
				processedTemplate = processedTemplate.Replace(match.Value, value);

				match = match.NextMatch();
			}

			return processedTemplate;
		}
	}
}
