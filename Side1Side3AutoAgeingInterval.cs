using System;
using System.Linq;
using System.Windows.Forms;

namespace Leitner_Three
{
    public partial class Side1Side3AutoAgeingInterval : Form
    {
        public Side1Side3AutoAgeingInterval()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
			if (Variables.Leitner.Setting[0].A_C_AutoAgeInterval == Convert.ToInt32(A_C_AutoAgeingInterval.Text))
				Close();

			else
			{
				Variables.Leitner.Setting[0].A_C_AutoAgeInterval = Convert.ToInt32(A_C_AutoAgeingInterval.Text);

				using (Variables.SettingDataContext = LeitnerLessonsDataContext.GetSettingContext(Variables.SettingTableName))
				{

					var settings = from s in Variables.SettingDataContext.Setting01s
								   select s;

					foreach (var n01set in settings)
					{
						n01set.A_C_AutoAgeInterval = Convert.ToInt32(A_C_AutoAgeingInterval.Text);
					}

					Variables.SettingDataContext.SubmitChanges();
				}

				Close();
			}
        }

        private void A_C_AutoAgeingInterval_TextChanged(object sender, EventArgs e)
        {
            if (validate_number(this.A_C_AutoAgeingInterval.Text) != true)
            {
                this.A_C_AutoAgeingInterval.Text = "";

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

        private void Side1Side3AutoAgeingInterval_Load(object sender, EventArgs e)
        {
            A_C_AutoAgeingInterval.Text = Variables.Leitner.Setting[0].A_C_AutoAgeInterval.ToString();
        }

        private void A_C_AutoAgeingInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                buttonOK_Click(sender, e);
            }
        }
    }
}