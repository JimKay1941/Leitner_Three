using System.Xml;
using System.Xml.Linq;
using System.Data.Linq.Mapping;
using System.IO;
using System.Reflection;
using System.Linq;

namespace Leitner_Three
{
	public partial class LeitnerLessonsDataContext
	{
		public static LeitnerLessonsDataContext GetLessonContext(string tableName)
		{
			// Get the .xml file into memory
			Stream ioSt = Assembly.GetExecutingAssembly().GetManifestResourceStream("Leitner_Three.Lesson01.xml");
			XElement xe = XElement.Load(XmlReader.Create(ioSt));

			// Replace the table name value in memory
			var tableElements = xe.Elements().AsQueryable().Where(e => e.Name.LocalName.Equals("Table"));

			foreach (var t in tableElements)
			{
				var nameAttribute = t.Attributes().Where(a => a.Name.LocalName.Equals("Name"));

				foreach (var a in nameAttribute)
				{
					if (a.Value.Equals("dbo.Lesson01"))
					{
						a.Value = a.Value.Replace("Lesson01", tableName);
					}
				}
			}

			// Obtain and retunr the dynamic DataContext
			XmlMappingSource source = XmlMappingSource.FromXml(xe.ToString());
			return new LeitnerLessonsDataContext(Properties.Settings.Default.LessonConnectionString, source);
		}

		public static LeitnerLessonsDataContext GetSettingContext(string tableName)
		{
			// Get the .xml file into memory
			Stream ioSt = Assembly.GetExecutingAssembly().GetManifestResourceStream("Leitner_Three.Setting01.xml");
			//var looker = Assembly.GetExecutingAssembly();
			//Stream ioSt = looker.GetManifestResourceStream("LeitnerDevelopment._01Setting.xml");
			XElement xe = XElement.Load(XmlReader.Create(ioSt));

			// Replace the table name value in memory
			var tableElements = xe.Elements().AsQueryable().Where(e => e.Name.LocalName.Equals("Table"));

			foreach (var t in tableElements)
			{
				var nameAttribute = t.Attributes().Where(a => a.Name.LocalName.Equals("Name"));

				foreach (var a in nameAttribute)
				{
					if (a.Value.Equals("dbo.Setting01"))
					{
						a.Value = a.Value.Replace("Setting01", tableName);
					}
				}
			}

			// Obtain and return the dynamic DataContext
			XmlMappingSource source = XmlMappingSource.FromXml(xe.ToString());
			return new LeitnerLessonsDataContext(Properties.Settings.Default.LessonConnectionString, source);
		}
	}
}
