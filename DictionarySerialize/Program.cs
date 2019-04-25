using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DictionarySerialize
{
	class Program
	{
		static void Main(string[] args)
		{
			Dictionary<int, string> dict = new Dictionary<int, string>
			{
				{1,"one" }, {2, "tow"}
			};
			XmlSerializer serializer = new XmlSerializer(typeof(Item[]),
																							new XmlRootAttribute() { ElementName = "items" });

			serializer.Serialize(Console.Out, dict.Select(kv => new Item() { id = kv.Key, value = kv.Value }).ToArray());
		}
	}


	[Serializable]
	public class Item
	{
		[XmlAttribute("IDTest")]
		public int id { get; set; }

		[XmlAttribute]
		public string value { get; set; }
	}
}
