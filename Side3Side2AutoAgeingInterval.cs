using System;
using System.Linq;
using System.Windows.Forms;

namespace Leitner_Three
{
    public partial class Side3Side2AutoAgeingInterval : Form
    {
        public Side3Side2AutoAgeingInterval()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
			if (Variables.Leitner.Setting[0].C_B_AutoAgeInterval == Convert.ToInt32(C_B_AutoAgeingInterval.Text))
				Close();

			else
			{
				Variables.Leitner.Setting[0].C_B_AutoAgeInterval = Convert.ToInt32(C_B_AutoAgeingInterval.Text);

				using (Variables.SettingDataContext = LeitnerLessonsDataContext.GetSettingContext(Variables.SettingTableName))
				{

					var settings = from s in Variables.SettingDataContext.Setting01s
								   select s;

					foreach (var n01Set in settings)
					{
						n01Set.C_B_AutoAgeInterval = Convert.ToInt32(C_B_AutoAgeingInterval.Text);
					}

					Variables.SettingDataContext.SubmitChanges();
				}

				Close();
			}
        }

        private void C_B_AutoAgeingInterval_TextChanged(object sender, EventArgs e)
        {
            if (validate_number(this.C_B_AutoAgeingInterval.Text) != true)
            {
                this.C_B_AutoAgeingInterval.Text = "";

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

        private void Side3Side2AutoAgeingInterval_Load(object sender, EventArgs e)
        {
            C_B_AutoAgeingInterval.Text = Variables.Leitner.Setting[0].C_B_AutoAgeInterval.ToString();
        }

        private void C_B_AutoAgeingInterval_TextChanged_1(object sender, EventArgs e)
        {
            if (validate_number(this.C_B_AutoAgeingInterval.Text) != true)
            {
                this.C_B_AutoAgeingInterval.Text = "";

                MessageBox.Show("You must enter a decimal Number.", "Ageing Interval Entry Error",
         MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }
            else
            {
                return;
            }
        }

        private void C_B_AutoAgeingInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                buttonOK_Click(sender, e);
            }
        }
    }
}