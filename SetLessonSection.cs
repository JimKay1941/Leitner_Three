using System;
using System.Windows.Forms;

namespace Leitner_Three
{
    public partial class SetLessonSection : Form
    {
        public SetLessonSection()
        {
            InitializeComponent();
        }

        private void SetLessonSection_Load(object sender, EventArgs e)
        {
            textLessonSection.Text = Properties.Settings.Default.user_section;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.user_section = textLessonSection.Text;
            Properties.Settings.Default.Save();
			Close();
        }

        private void textLessonSection_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                buttonOK_Click(sender, e);
            }
        }
    }
}