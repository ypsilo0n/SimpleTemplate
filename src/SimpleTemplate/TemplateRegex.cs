using System.Text.RegularExpressions;

namespace SimpleTemplate
{
	internal static class TemplateRegex
	{
		public static Regex VarRegex => 
			new Regex(
				@"\$([\w+|\.]+(\[\d+\][\w+|\.]*)?)+\$",
				RegexOptions.Compiled | RegexOptions.Singleline
			);

		public static Regex IfElse => 
			new Regex(
				@"[\ \t]*if\s(?<cond>\$[\w\.\[\]]+\$)\s*" +
				@"\{" +
					@"(?<" + TrueGroup + @">([^{}]+ | (?<Level>\{) | (?<-Level>\}))+)" +
				@"\}" +
				@"(\s*else\s*" +
				@"\{" +
					@"(?<" + FalseGroup + @">([^{}]+ | (?<Level>\{) | (?<-Level>\}))+)" +
				@"\})?" + @"(\r\n)?",
				RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace
			);

		public static string TrueGroup => "true";
		public static string FalseGroup => "false";

		public static Regex ForEach => 
			new Regex(
				@"[\ \t]*foreach\s+(?<ctx>\$[\w\.\[\]]+\$)\s+in\s+(?<var>\$[\w\.\[\]]+\$)\s*" +
				@"\{" +
					@"(?<body>([^{}]+ | (?<Level>\{) | (?<-Level>\}))+)" +
				@"\}",
				RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace
			);
	}
}
