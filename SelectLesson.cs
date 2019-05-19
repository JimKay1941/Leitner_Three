using System;
using System.Windows.Forms;
using System.Linq;

namespace Leitner_Three
{
    public partial class SelectLesson : Form
    {
        public SelectLesson()
        {
            InitializeComponent();
            ListOfLessons();
        }


        private void ListOfLessons()
        {
            if (Properties.Settings.Default.LessonConnectionString == "") return;
            Variables.LessonName = null;
            Variables.LessonTableName = null;
            Variables.LessonTableNumber = null;
            Variables.LessonDataContext = null;
            Variables.SettingDataContext = null;

            var looker20 = Properties.Settings.Default.LessonConnectionString;

            using (Variables.TabOfContDataContext = new LeitnerLessonsDataContext(Properties.Settings.Default.LessonConnectionString))
            {
                var ntocs = from n1 in Variables.TabOfContDataContext.TabOfConts
                    orderby n1.Lesson_Name
                    select n1;

                foreach (var ntoc in ntocs)
                {
                    //Variables.LessonName = ntoc.Lesson_Name;
                    //Variables.LessonTableName = ntoc.Table_Name;
                    //Variables.SettingTableName = "Setting" + ntoc.Table_Name.Substring(6, 2);
                    //Variables.LessonTableNumber = ntoc.Id.ToString();
                    if ((ntoc.Lesson_Name != null) && (ntoc.Lesson_Name != ""))
                    {
                        if (Properties.Settings.Default.user_section.Length == 0)
                        {
                            listBox1.Items.Add(ntoc.Lesson_Name);
                            continue;
                        }
                        else
                        {
                            if (ntoc.Lesson_Name.Length <= Properties.Settings.Default.user_section.Length)
                            {
                                if (ntoc.Lesson_Name == Properties.Settings.Default.user_section.Substring(0, ntoc.Lesson_Name.Length))
                                {
                                    listBox1.Items.Add(ntoc.Lesson_Name);
                                    continue;
                                }
                            }
                            else
                            {
                                if (Properties.Settings.Default.user_section == ntoc.Lesson_Name.Substring(0, Properties.Settings.Default.user_section.Length))
                                {
                                    listBox1.Items.Add(ntoc.Lesson_Name);
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            Variables.LessonTableName = "";
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

			using (Variables.TabOfContDataContext = new LeitnerLessonsDataContext(Properties.Settings.Default.LessonConnectionString))
			{
				var ntocs = from n1 in Variables.TabOfContDataContext.TabOfConts
							where n1.Lesson_Name == listBox1.SelectedItem.ToString()
							select n1;

				foreach (var ntoc in ntocs)
				{
					ntoc.Lesson_Name = "";
					Variables.LessonTableName = ntoc.Table_Name;
				}

				Variables.TabOfContDataContext.SubmitChanges();
			}

			string SettingTableName = "Setting" + Variables.LessonTableName.Substring(6, 2);

			using (Variables.SettingDataContext = LeitnerLessonsDataContext.GetSettingContext(SettingTableName))
			{
				var n01setting = from S01 in Variables.SettingDataContext.Setting01s
								 select S01;

				foreach (var n01set in n01setting)
				{
					n01set.QuestionTextBox = "LeftToRight";
					n01set.AnswerTextBox = "LeftToRight";
					n01set.HintTextBox = "LeftToRight";
					n01set.Date = "Christian";
					n01set.StartTime = "7/13/2009 1:11:49 PM";
                    n01set.A_B_PreviousAutoAge = "7/13/2009 1:11:49 PM";
                    n01set.A_C_PreviousAutoAge = "7/13/2009 1:11:49 PM";
                    n01set.B_A_PreviousAutoAge = "7/13/2009 1:11:49 PM";
                    n01set.B_C_PreviousAutoAge = "7/13/2009 1:11:49 PM";
                    n01set.C_A_PreviousAutoAge = "7/13/2009 1:11:49 PM";
                    n01set.C_B_PreviousAutoAge = "7/13/2009 1:11:49 PM";
					n01set.A_B_AutoAgeInterval = 0;
					n01set.A_C_AutoAgeInterval = 0;
					n01set.B_A_AutoAgeInterval = 0;
					n01set.B_C_AutoAgeInterval = 0;
					n01set.C_A_AutoAgeInterval = 0;
					n01set.C_B_AutoAgeInterval = 0;
					n01set.Side1Name = "Pronunciation";
					n01set.Side2Name = "Chinese";
					n01set.Side3Name = "English";
					n01set.StudySequence = "451236";
					n01set.StudyMode = 3;
				}

				Variables.SettingDataContext.SubmitChanges();
			}

			using (Variables.LessonDataContext = LeitnerLessonsDataContext.GetLessonContext(Variables.LessonTableName))
			{
				var n01lesson = from S01 in Variables.LessonDataContext.Lesson01s
								select S01;
				Variables.LessonDataContext.Lesson01s.DeleteAllOnSubmit(n01lesson);

				Variables.LessonDataContext.SubmitChanges();
			}

            //File.Delete(Variables.usersFolder + "\\" + listBox1.SelectedItem + ".xml");
			Variables.LessonTableName = "";
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void buttonSelectLesson_Click(object sender, EventArgs e)
        {
			Variables.XmlFileName = Properties.Settings.Default.user_path + listBox1.SelectedItem + ".xml";

            Variables.LessonName = listBox1.SelectedItem.ToString();
            Variables.Leitner = new LeitnerBox();

			using (Variables.TabOfContDataContext = new LeitnerLessonsDataContext(Properties.Settings.Default.LessonConnectionString))
			{
				var ntocs = from n1 in Variables.TabOfContDataContext.TabOfConts
							where n1.Lesson_Name == Variables.LessonName
							select n1;

				foreach (var ntoc in ntocs)
				{
					Variables.LessonTableName = ntoc.Table_Name;
					Variables.SettingTableName = "Setting" + ntoc.Table_Name.Substring(6, 2);
					Variables.LessonTableNumber = ntoc.Id.ToString();
				}
			}

            this.Close();
        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                buttonSelectLesson_Click(sender, e);
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonSelectLesson_Click(sender, (EventArgs)e);
        }
    }
}