using System;
using System.Linq;
using System.Windows.Forms;

namespace Leitner_Three
{
    public partial class Side2Side1AutoAgeingInterval : Form
    {
        public Side2Side1AutoAgeingInterval()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (Variables.Leitner.Setting[0].B_A_AutoAgeInterval == Convert.ToInt32(B_A_AutoAgeingInterval.Text))
                Close();

            else
            {
                Variables.Leitner.Setting[0].B_A_AutoAgeInterval = Convert.ToInt32(B_A_AutoAgeingInterval.Text);

                using (
                    Variables.SettingDataContext =
                    LeitnerLessonsDataContext.GetSettingContext(Variables.SettingTableName))
                {
                    var settings = from s in Variables.SettingDataContext.Setting01s
                                                     select s;

                    foreach (Setting01 n01Set in settings)
                    {
                        n01Set.B_A_AutoAgeInterval = Convert.ToInt32(B_A_AutoAgeingInterval.Text);
                    }

                    Variables.SettingDataContext.SubmitChanges();
                }

                Close();
            }
        }

        private void B_A_AutoAgeingInterval_TextChanged(object sender, EventArgs e)
        {
            if (validate_number(B_A_AutoAgeingInterval.Text) != true)
            {
                B_A_AutoAgeingInterval.Text = "";

                MessageBox.Show("You must enter a decimal Number.", "Ageing Interval Entry Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }
            else
            {
                return;
            }
        }

        private bool validate_number(string textbox)
        {
            if (textbox.Length == 0)
            {
                return true;
            }
            else
            {
                int number1 = 0;
                if (int.TryParse(textbox, out number1))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void Side2Side1AutoAgeingInterval_Load(object sender, EventArgs e)
        {
            B_A_AutoAgeingInterval.Text = Variables.Leitner.Setting[0].B_A_AutoAgeInterval.ToString();
        }

        private void B_A_AutoAgeingInterval_TextChanged_1(object sender, EventArgs e)
        {
            if (validate_number(B_A_AutoAgeingInterval.Text) != true)
            {
                B_A_AutoAgeingInterval.Text = "";

                MessageBox.Show("You must enter a decimal Number.", "Ageing Interval Entry Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }
            else
            {
                return;
            }
        }

        private void B_A_AutoAgeingInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                buttonOK_Click(sender, e);
            }
        }
    }
}