namespace SimpleTemplate.Test.Data
{
	public static class SimpleIf
	{
		public static object ModelTrue =>
			new
			{
				Person = new
				{
					Name = "Hugo",
					Age = 20
				},
				Condition = true
			};

		public static object ModelFalse =>
			new
			{
				Person = new
				{
					Name = "Hugo",
					Age = 20
				},
				Condition = false
			};

		public static string Template =>
@"Überschrift

if $Condition$
{
$Person.Name$
Haha
}
Nachher";

		public static string ExpectedTrue =>
@"Überschrift

Hugo
Haha
Nachher";

		public static string ExpectedFalse =>
@"Überschrift

Nachher";
	}
}
