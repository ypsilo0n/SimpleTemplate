namespace SimpleTemplate.Test.Data
{
	public static class IfInsideLoop
	{
		public static object Model =>
			new
			{
				Version = "1.2.3.4567",
				PBIs = new[]
				{
					new { IsOdd = false, Id = "1234", Title = "Title1" },
					new { IsOdd = true, Id = "1235", Title = "Title1" },
					new { IsOdd = true, Id = "2345", Title = "Title2" }
				}
			};

		public static string Template =>
@"Only the odd ones

foreach $PBI$ in $PBIs$
{
	if $PBI.IsOdd$ 
	{
	$PBI.Id$ - $PBI.Title$
	}
}
";

		public static string Expected =>
@"Only the odd ones

	1235 - Title1
	2345 - Title2
";
	}
}
