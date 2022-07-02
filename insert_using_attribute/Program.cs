using System;
using System.Linq;
using System.Xml.Linq;

namespace insert_using_attribute
{
    class Program
    {
        static void Main(string[] args)
        {
            var xFruit = XElement.Parse(source);
			var detail = xFruit.FindPath(@"Apple\Store");
			detail?.Add(
				 new XElement("price", "$10"),
				 new XElement("qty", "10"),
				 new XElement("amount", "$100")
			);
			Console.WriteLine(xFruit.ToString());
		}
        const string source =
		@"<Fruits>
			<node text=""Apple"" tag=""a"" imageindex=""0"">
				<node text = ""Store"" tag=""b"" imageindex=""1"" />
				<node text = ""City"" tag=""c"" imageindex=""2"" />
			</node>
			<node text = ""Orange"" tag=""a"" imageindex=""0"">
				<node text = ""Store"" tag=""b"" imageindex=""1"" />
				<node text = ""City"" tag=""c"" imageindex=""2"" />
			</node>
		</Fruits>";
    }
	static class Extensions
	{
		public static bool TryGetAttributeValue(
			this XElement xel,
			string name,
			out string value)
		{
			var attr = xel.Attributes()
				.FirstOrDefault(@try => string.Equals(name, @try.Name.LocalName));
			if (attr == null)
			{
				value = string.Empty;
				return false;
			}
			else
			{
				value = attr.Value;
				return true;
			}
		}
		public static XElement FindPath(this XElement xRoot,string path)
		{
			XElement xTraverse = xRoot;
			string[] split = path.Split('\\');
			for (int i = 0; i < split.Length; i++)
			{
				xTraverse =
					xTraverse.Elements()
					.FirstOrDefault(match =>
						match.TryGetAttributeValue("text", out string value) &&
						(value == split[i]));
				if (xTraverse == null) break;
            }
			return xTraverse;
		}
	}
}
