using Newtonsoft.Json;

namespace PatrickMcDougle_CTL_Star.Json
{
	public class JsonFile
	{
		public T DeserializeFromFile<T>(string file)
		{
			string text = System.IO.File.ReadAllText(file);

			return JsonConvert.DeserializeObject<T>(text);
		}

		public bool SerializeToFile(object o, string path)
		{
			string json = JsonConvert.SerializeObject(o, Formatting.Indented);

			System.IO.File.WriteAllText(path, json);

			return true;
		}
	}
}