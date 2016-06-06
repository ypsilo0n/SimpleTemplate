namespace SimpleTemplate
{
	public static class Template
	{
		public static string Render(string template, object model)
		{
			return new TemplateRenderer().Render(template, model);
		}
	}
}