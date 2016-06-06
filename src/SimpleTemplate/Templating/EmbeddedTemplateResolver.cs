using System;
using System.IO;

namespace SimpleTemplate.Templating
{
	public class EmbeddedTemplateResolver
	{
		private readonly Type _rootType;

		public EmbeddedTemplateResolver(Type rootType)
		{
			_rootType = rootType;
		}

		public string Resolve(string templateName)
		{
			var assembly = _rootType.Assembly;
			var assemblyName = assembly.GetName().Name;
			var resource = $"{assemblyName}.{templateName}";

			using (var resourceStream = assembly.GetManifestResourceStream(resource))
			{
				try
				{
					using (var reader = new StreamReader(resourceStream))
					{
						return reader.ReadToEnd();
					}
				}
				catch (Exception)
				{
					throw new ArgumentException($"Can't find template [{resource}]");
				}
			}
		}
	}
}
