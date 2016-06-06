namespace SimpleTemplate.Test.Data
{
	public static class SimpleLoop
	{
		public static object Model => new
		{
			Version = "1.2.3.4567",
			PBIs = new[]
				{
					new { Id = "1234", Title = "Title1" },
					new { Id = "2345", Title = "Title2" }
				}
		};

		public static string Template =>
@"$Version$

= PBIs =

foreach $PBI$ in $PBIs$
{
	$PBI.Id$ - $PBI.Title$
}
";

		public static string Expected =>
@"1.2.3.4567

= PBIs =

	1234 - Title1
	2345 - Title2
";
	}
}
