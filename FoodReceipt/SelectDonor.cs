using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FoodReceipt;
using System.Data.SqlClient;
using System.Collections;

namespace ClientcardFB3
{
    public partial class SelectDonor : Form
    {

        LoginForm frmLogIn;
        FoodDonations clsFoodDonations = new FoodDonations(CCFBGlobal.connectionString);
        Donors clsDonors = new Donors(CCFBGlobal.connectionString);

        public SelectDonor(LoginForm loginform)
        {
            frmLogIn = loginform;
            InitializeComponent();
            formLoad();

        }
        private void formLoad()
        {
            IEnumerator enumerator = this.tabPage2.Controls.GetEnumerator();
            clsFoodDonations.getFavorite();
            try
            {
                for (int i = 0; i < clsFoodDonations.RowCount; i++)
                {
                    enumerator.MoveNext();
                    System.Windows.Forms.Button button = (System.Windows.Forms.Button)enumerator.Current;
                    button.Text = clsFoodDonations.DSet.Tables["FoodDonations"].Rows[i][1].ToString();
                    button.Name = clsFoodDonations.DSet.Tables["FoodDonations"].Rows[i][0].ToString();

                }
                enumerator.Reset();
                for (int j = 0; j < 20; j++)
                {
                    enumerator.MoveNext();
                    System.Windows.Forms.Button button = (System.Windows.Forms.Button)enumerator.Current;
                    if (button.Text == "")
                    {
                        button.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }
        private void SelectDonor_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmLogIn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}
