namespace SimpleTemplate.Test.Data
{
	public static class MultipleIfs
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
HAHA
}

if $Condition$
{
$Person.Name$
Haha
}
else
{
$Person.Age$
}
Nachher";

		public static string ExpectedTrue =>
@"Überschrift
HAHA

Hugo
Haha
Nachher";

		public static string ExpectedFalse =>
@"Überschrift

20
Nachher";

	}
}
