using System;
using System.Windows.Forms;

namespace Leitner_Three
{
    public partial class SelectStudyMode : Form
    {
        public SelectStudyMode()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Variables.Leitner.Setting[0].StudyMode = Convert.ToInt32(Variables.Leitner.Setting[0].StudySequence.Substring(0, 1)) - 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Variables.Leitner.Setting[0].StudyMode = Convert.ToInt32(Variables.Leitner.Setting[0].StudySequence.Substring(1, 1)) - 1;
        }

		private void radioButton3_CheckedChanged(object sender, EventArgs e)
		{
			Variables.Leitner.Setting[0].StudyMode = Convert.ToInt32(Variables.Leitner.Setting[0].StudySequence.Substring(2, 1)) - 1;
		}

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Variables.Leitner.Setting[0].StudyMode = Convert.ToInt32(Variables.Leitner.Setting[0].StudySequence.Substring(3, 1)) - 1;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            Variables.Leitner.Setting[0].StudyMode = Convert.ToInt32(Variables.Leitner.Setting[0].StudySequence.Substring(4, 1)) - 1;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            Variables.Leitner.Setting[0].StudyMode = Convert.ToInt32(Variables.Leitner.Setting[0].StudySequence.Substring(5, 1)) - 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            int temp;

            temp = Convert.ToInt32(Variables.Leitner.Setting[0].StudySequence.Substring(0, 1));
            radioButton1.Text = Variables.RadioButtonText[temp];
            if (Variables.Leitner.Setting[0].StudyMode == temp - 1)
                radioButton1.Checked = true;

            temp = Convert.ToInt32(Variables.Leitner.Setting[0].StudySequence.Substring(1, 1));
            radioButton2.Text = Variables.RadioButtonText[temp];
            if (Variables.Leitner.Setting[0].StudyMode == temp - 1)
                radioButton2.Checked = true;

            temp = Convert.ToInt32(Variables.Leitner.Setting[0].StudySequence.Substring(2, 1));
            radioButton3.Text = Variables.RadioButtonText[temp];
            if (Variables.Leitner.Setting[0].StudyMode == temp - 1)
                radioButton3.Checked = true;

            temp = Convert.ToInt32(Variables.Leitner.Setting[0].StudySequence.Substring(3, 1));
            radioButton4.Text = Variables.RadioButtonText[temp];
            if (Variables.Leitner.Setting[0].StudyMode == temp - 1)
                radioButton4.Checked = true;

            temp = Convert.ToInt32(Variables.Leitner.Setting[0].StudySequence.Substring(4, 1));
            radioButton5.Text = Variables.RadioButtonText[temp];
            if (Variables.Leitner.Setting[0].StudyMode == temp - 1)
                radioButton5.Checked = true;

            temp = Convert.ToInt32(Variables.Leitner.Setting[0].StudySequence.Substring(5, 1));
            radioButton6.Text = Variables.RadioButtonText[temp];
            if (Variables.Leitner.Setting[0].StudyMode == temp - 1)
                radioButton6.Checked = true;
        }
    }
}