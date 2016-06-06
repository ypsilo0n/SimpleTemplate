using Newtonsoft.Json;
using Xunit.Abstractions;

namespace SimpleTemplate.Test.Data
{
	public class TestData : IXunitSerializable
	{
		public string Description { get; private set; }
		public string Expected { get; private set; }
		public object Model { get; private set; }
		public string Template { get; private set; }

		public TestData() { }  // Needed for deserializer

		public TestData(string description, object model, string template, string expected)
		{ 
			Description = description;
			Model = model;
			Template = template;
			Expected = expected;
		}

		public override string ToString()
		{
			return Description;
		}

		public void Deserialize(IXunitSerializationInfo info)
		{
			Description = info.GetValue<string>(nameof(Description));
			Expected = info.GetValue<string>(nameof(Expected));
			Template = info.GetValue<string>(nameof(Template));

			var modelJson = info.GetValue<string>(nameof(Model));
			Model = JsonConvert.DeserializeObject<dynamic>(modelJson);
		}

		public void Serialize(IXunitSerializationInfo info)
		{
			info.AddValue(nameof(Description), Description);
			info.AddValue(nameof(Expected), Expected);
			info.AddValue(nameof(Template), Template);

			var modelJson = JsonConvert.SerializeObject(Model);
			info.AddValue(nameof(Model), modelJson);
		}
	}
}
