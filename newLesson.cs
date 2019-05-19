using System;
using System.Linq;
using System.Windows.Forms;

namespace Leitner_Three
{
    public partial class NewLesson : Form
    {
        public NewLesson()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
				using (Variables.TabOfContDataContext = new LeitnerLessonsDataContext(Properties.Settings.Default.LessonConnectionString))
				{
				    var ntocs = from n1 in Variables.TabOfContDataContext.TabOfConts
								select n1;

				    if (Enumerable.Any(ntocs, ntoc => ntoc.Lesson_Name == maskedTextBox1.Text.Trim()))
				    {
				        errorProvider1.SetError(maskedTextBox1, " This name is already exist ");
				        return;
				    }
				}
                using (Variables.TabOfContDataContext = new LeitnerLessonsDataContext(Properties.Settings.Default.LessonConnectionString))
				{
					var ntocs = from n1 in Variables.TabOfContDataContext.TabOfConts
								where n1.Lesson_Name == null || n1.Lesson_Name == ""
								select n1;

					foreach (var ntoc in ntocs)
					{
						ntoc.Lesson_Name = maskedTextBox1.Text.Trim();
						Variables.LessonName = maskedTextBox1.Text.Trim();
						Variables.LessonTableName = ntoc.Table_Name;
						Variables.LessonTableNumber = Variables.LessonTableName.Substring(6, 2);
						Variables.SettingTableName = "Setting" + Variables.LessonTableNumber;
						break;
					}
					Variables.TabOfContDataContext.SubmitChanges();
				}

				Variables.XmlFileName = Properties.Settings.Default.user_path + Variables.LessonName + ".xml";

                //Variables.Leitner.Setting[0].QuestionTextBox = @"LeftToRight";
                //Variables.Leitner.Setting[0].AnswerTextBox = @"LeftToRight";
                //Variables.Leitner.Setting[0].HintTextBox = @"LeftToRight";
                //Variables.Leitner.Setting[0].Date = @"Christian";
                //Variables.Leitner.Setting[0].StartTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                //Variables.Leitner.Setting[0].A_B_PreviousAutoAge = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                //Variables.Leitner.Setting[0].A_C_PreviousAutoAge = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                //Variables.Leitner.Setting[0].B_A_PreviousAutoAge = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                //Variables.Leitner.Setting[0].B_C_PreviousAutoAge = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                //Variables.Leitner.Setting[0].C_A_PreviousAutoAge = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                //Variables.Leitner.Setting[0].C_B_PreviousAutoAge = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                //Variables.Leitner.Setting[0].A_B_AutoAgeInterval = 0;
                //Variables.Leitner.Setting[0].A_C_AutoAgeInterval = 0;
                //Variables.Leitner.Setting[0].B_A_AutoAgeInterval = 0;
                //Variables.Leitner.Setting[0].B_C_AutoAgeInterval = 0;
                //Variables.Leitner.Setting[0].C_A_AutoAgeInterval = 0;
                //Variables.Leitner.Setting[0].C_B_AutoAgeInterval = 0;
                //Variables.Leitner.Setting[0].Side1Name = @"Zhuyin";
                //Variables.Leitner.Setting[0].Side2Name = @"Chinese";
                //Variables.Leitner.Setting[0].Side3Name = @"English";
                //Variables.Leitner.Setting[0].StudyMode = 4;
                //Variables.Leitner.Setting[0].StudySequence = @"451236";


                //Variables.Reading = new FileStream(Application.StartupPath + "\\" + "Default.XML", FileMode.Open, FileAccess.Read, FileShare.Read);
                //Variables.Leitner = new LeitnerBox();
                //Variables.Leitner.ReadXml(Variables.Reading);
                //Variables.xDocument = XDocument.Load(Application.StartupPath + "\\" + "Default.XML");

                //var TWOstartTime = Variables.Leitner.Setting[0].StartTime.ToString();
                //Variables.Leitner.Setting[0].StartTime = DateTime.Now.ToString();

                //var startTime = (from q in Variables.xDocument.Descendants("StartTime") select q).First();
                //startTime.Value = DateTime.Now.ToString();

                //if (!Directory.Exists(Variables.usersFolder)) Directory.CreateDirectory(Variables.usersFolder);
                //if (File.Exists(Variables.usersFolder + "\\" + maskedTextBox1.Text.Trim() + ".xml"))
                //{
                //    errorProvider1.SetError(this.maskedTextBox1, " This name is already exist ");
                //    return;
                //}
                //Variables.Writing = new FileStream(Variables.usersFolder + maskedTextBox1.Text.Trim() + ".xml", FileMode.Create, FileAccess.Write, FileShare.Write);
                //Variables.Leitner.WriteXml(Variables.Writing);
                //Variables.Reading.Close();
                //Variables.Writing.Close();
                //Variables.xDocument.Save(Variables.usersFolder + maskedTextBox1.Text.Trim() + ".xml");
                //Variables.LessonTableName = Variables.usersFolder + maskedTextBox1.Text.Trim() + ".xml";
                //Variables.Leitner = new LeitnerBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                button1_Click(sender, e);
            }
        }
    }
}