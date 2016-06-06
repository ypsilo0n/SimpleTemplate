namespace SimpleTemplate.Test.Data
{
	public static class NestedLoops
	{
		public static object Model =>
			new
			{
				Version = "1.2.3.4567",
				PBIs = new object[]
				{
					new { Id = "1234", Title = "Title1", Tasks = new[] { "1235", "1236", "1237" } },
					new { Id = "2345", Title = "Title2", Tasks = new[] { "2346", "2347", "2348" } }
				}
			};

		public static string Template =>
@"$Version$

= PBIs =

foreach $PBI$ in $PBIs$
{
	$PBI.Id$ - $PBI.Title$
	foreach $Task$ in $PBI.Tasks$
	{
	  - $Task$
	}
}
";

		public static string Expected =>
@"1.2.3.4567

= PBIs =

	1234 - Title1
	  - 1235
	  - 1236
	  - 1237
	2345 - Title2
	  - 2346
	  - 2347
	  - 2348
";
	}
}
