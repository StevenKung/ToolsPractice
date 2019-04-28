using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RecipeSystem
{
	public class XmlSaveLoad
	{
		public static void SaveFile( object obj, string fullPath)
		{
			using (StreamWriter writer = new StreamWriter(fullPath))
			{
				XmlSerializer serializer = new XmlSerializer(obj.GetType());
				serializer.Serialize(writer, obj);
			}
		}


		public static void LoadFile(ref object obj, string fullPath)
		{
			using (StreamReader reader = new StreamReader(fullPath))
			{
				XmlSerializer serializer = new XmlSerializer(obj.GetType());
				obj = serializer.Deserialize(reader);
			}
		}

	}
}
