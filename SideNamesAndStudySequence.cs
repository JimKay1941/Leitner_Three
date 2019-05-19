using System;
using System.Linq;
using System.Windows.Forms;

namespace Leitner_Three
{
    public partial class SideNamesAndStudySequence : Form
    {
        public SideNamesAndStudySequence()
        {
            InitializeComponent();
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            if (textBoxSide1Name.Text != "") return;
            textBoxSide1Name.Text = Variables.Leitner.Setting[0].Side1Name;
            textBoxSide2Name.Text = Variables.Leitner.Setting[0].Side2Name;
            textBoxSide3Name.Text = Variables.Leitner.Setting[0].Side3Name;
            textBoxStudySequence.Text = Variables.Leitner.Setting[0].StudySequence;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxSide1Name.Text.Length < 1)
            {
                MessageBox.Show(@"The name for Side1 must contain at least one character", "Side1 Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxSide1Name.Text = Variables.Leitner.Setting[0].Side1Name;
            }
            else if (textBoxSide2Name.Text.Length < 1)
            {
                MessageBox.Show(@"The name for Side2 must contain at least one character", "Side2 Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxSide2Name.Text = Variables.Leitner.Setting[0].Side2Name;
            }
            else if (textBoxSide3Name.Text.Length < 1)
            {
                MessageBox.Show(@"The name for Side3 must contain at least one character", "Side3 Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxSide3Name.Text = Variables.Leitner.Setting[0].Side3Name;
            }

            else if (textBoxStudySequence.Text.Length != 6)
            {
                MessageBox.Show(@"The StudySequence must contain six numeric digits, each in the range of 1-6", "StudySequence", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxStudySequence.Text = Variables.Leitner.Setting[0].StudySequence;
                int start = 0;
                ValidateOneDigit(start);
                start = 1;
                ValidateOneDigit(start);
                start = 2;
                ValidateOneDigit(start);
                start = 3;
                ValidateOneDigit(start);
                start = 4;
                ValidateOneDigit(start);
                start = 5;
                ValidateOneDigit(start);
            }
            else
            {
                Variables.Leitner.Setting[0].Side1Name = textBoxSide1Name.Text;
                Variables.Leitner.Setting[0].Side2Name = textBoxSide2Name.Text;
                Variables.Leitner.Setting[0].Side3Name = textBoxSide3Name.Text;
                Variables.Leitner.Setting[0].StudySequence = textBoxStudySequence.Text;

				using (Variables.SettingDataContext = LeitnerLessonsDataContext.GetSettingContext(Variables.SettingTableName))
				{

					var settings = from s in Variables.SettingDataContext.Setting01s
								   select s;

					foreach (var n01set in settings)
					{
						n01set.QuestionTextBox = Variables.Leitner.Setting[0].QuestionTextBox;
						n01set.AnswerTextBox = Variables.Leitner.Setting[0].AnswerTextBox;
						n01set.HintTextBox = Variables.Leitner.Setting[0].HintTextBox;
						n01set.Date = Variables.Leitner.Setting[0].Date;
						n01set.StartTime = Variables.Leitner.Setting[0].StartTime;
						n01set.A_B_PreviousAutoAge = Variables.Leitner.Setting[0].A_B_PreviousAutoAge;
						n01set.A_C_PreviousAutoAge = Variables.Leitner.Setting[0].A_C_PreviousAutoAge;
						n01set.B_A_PreviousAutoAge = Variables.Leitner.Setting[0].B_A_PreviousAutoAge;
						n01set.B_C_PreviousAutoAge = Variables.Leitner.Setting[0].B_C_PreviousAutoAge;
						n01set.C_A_PreviousAutoAge = Variables.Leitner.Setting[0].C_A_PreviousAutoAge;
						n01set.C_B_PreviousAutoAge = Variables.Leitner.Setting[0].C_B_PreviousAutoAge;
						n01set.A_B_AutoAgeInterval = Variables.Leitner.Setting[0].A_B_AutoAgeInterval;
						n01set.A_C_AutoAgeInterval = Variables.Leitner.Setting[0].A_C_AutoAgeInterval;
						n01set.B_A_AutoAgeInterval = Variables.Leitner.Setting[0].B_A_AutoAgeInterval;
						n01set.B_C_AutoAgeInterval = Variables.Leitner.Setting[0].B_C_AutoAgeInterval;
						n01set.C_A_AutoAgeInterval = Variables.Leitner.Setting[0].C_A_AutoAgeInterval;
						n01set.C_B_AutoAgeInterval = Variables.Leitner.Setting[0].C_A_AutoAgeInterval;
						n01set.Side1Name = Variables.Leitner.Setting[0].Side1Name;
						n01set.Side2Name = Variables.Leitner.Setting[0].Side2Name;
						n01set.Side3Name = Variables.Leitner.Setting[0].Side3Name;
						n01set.StudySequence = Variables.Leitner.Setting[0].StudySequence;
						n01set.StudyMode = Variables.Leitner.Setting[0].StudyMode;
					}

					Variables.SettingDataContext.SubmitChanges();
				}
                Close();
            }
        }

        private void ValidateOneDigit(int start)
        {
            if ((textBoxStudySequence.Text.Substring(start, 1) != "1") &&
                            (textBoxStudySequence.Text.Substring(start, 1) != "2") &&
                            (textBoxStudySequence.Text.Substring(start, 1) != "3") &&
                            (textBoxStudySequence.Text.Substring(start, 1) != "4") &&
                            (textBoxStudySequence.Text.Substring(start, 1) != "5") &&
                            (textBoxStudySequence.Text.Substring(start, 1) != "6"))
            {
                MessageBox.Show("The StudySequence must contain six numeric digits, each in the range of 1-6", "StudySequence", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxStudySequence.Text = Variables.Leitner.Setting[0].StudySequence;
            }
        }
    }
}