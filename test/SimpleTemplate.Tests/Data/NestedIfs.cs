namespace SimpleTemplate.Test.Data
{
	public static class NestedIfs
	{
		public static object ModelTrue =>
			new
			{
				Person = new
				{
					Name = "Hugo",
					Age = 20,
					IsTrue = true
				},
				Condition = true,
				AlsoTrue = true
			};

		public static object ModelFalse =>
			new
			{
				Person = new
				{
					Name = "Hugo",
					Age = 20,
					IsTrue = false
				},
				Condition = false,
				AlsoTrue = false
			};

		public static string Template =>
@"Überschrift

if $Person.IsTrue$
{
	$Person.Name$
	if $AlsoTrue$
	{
		Haha
		if $Condition$
		{
			Herst-True
		}
		else
		{
			Herst-False
		}
	}
}
else
{
	$Person.Age$
}
Nachher";

		public static string ExpectedTrue =>
@"Überschrift

	Hugo
		Haha
			Herst-True
Nachher";

		public static string ExpectedFalse =>
@"Überschrift

	20
Nachher";

	}
}
