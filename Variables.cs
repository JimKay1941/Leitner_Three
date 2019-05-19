using System.IO;
using System.Windows.Forms;

namespace Leitner_Three
{
    public static class Variables
    {
        public const int MaxStudy = 25;
        public const int Maxwords = 50000;
		public static string XmlFileName;
        public static FileStream Reading;
        public static LeitnerBox Leitner;
		public static LeitnerLessonsDataContext LessonDataContext;
		public static LeitnerLessonsDataContext SettingDataContext;
		public static LeitnerLessonsDataContext TabOfContDataContext;
		public static string LessonName;
		public static string LessonTableName = "";
		public static string LessonTableNumber;
		public static string SettingTableName;
        public static bool EnableAutoComplete = true;
        public static readonly char[] Delim1 = new char[1];
        public static readonly char[] Delim2 = new char[1];
        public static int InputCounter = 0;
        public static readonly OpenFileDialog ChooseInputFileDialog = new OpenFileDialog();
        public static readonly string Title = "Leitner 3 -> ";
        public static string FirstRead;
        public static string LastStartTime = null;
        public static string TranslatorLine;
        public static string UsersFolder = $"{Application.StartupPath}\\users\\";
        public static readonly string[] RadioButtonText = new string[7];
    }
}