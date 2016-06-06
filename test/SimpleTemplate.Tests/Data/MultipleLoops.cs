namespace SimpleTemplate.Test.Data
{
	public static class MultipleLoops
	{
		public static object Model =>
			new
			{
				Version = "1.2.3.4567",
				PBIs = new[]
				{
					new { Id = "1234", Title = "Title1" },
					new { Id = "2345", Title = "Title2" }
				},
				Bugs = new[]
				{
					new { Id = "6543", Title = "Title1" },
					new { Id = "9876", Title = "Title2" }
				}
			};

		public static string Template =>
@"$Version$

= PBIs =

foreach $PBI$ in $PBIs$
{
	$PBI.Id$ - $PBI.Title$
}

= Bugs =

foreach $Bug$ in $Bugs$
{
	$Bug.Id$ - $Bug.Title$
}
";

		public static string Expected =>
@"1.2.3.4567

= PBIs =

	1234 - Title1
	2345 - Title2

= Bugs =

	6543 - Title1
	9876 - Title2
";
	}
}
