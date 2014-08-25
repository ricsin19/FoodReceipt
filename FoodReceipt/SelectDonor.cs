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
        parmTypeCodes parmDonorTypeCode = new parmTypeCodes(CCFBGlobal.parmTbl_Donor, CCFBGlobal.connectionString, "");
  


        public SelectDonor(LoginForm loginform)
        {
            frmLogIn = loginform;
            InitializeComponent();
            formLoad();
        }
        private void formLoad()
        {

            IEnumerator enumerator = this.tabPage2.Controls.GetEnumerator();
            IEnumerator enumerator1 = this.tabPage1.Controls.GetEnumerator();
            clsFoodDonations.getFavorite();
            System.Windows.Forms.Button button;
            try
            {
                
                for (int i = 0; i < clsFoodDonations.RowCount; i++)
                {
                    enumerator.MoveNext();
                    button = (System.Windows.Forms.Button)enumerator.Current;
                    button.Text = clsFoodDonations.DSet.Tables["FoodDonations"].Rows[i][1].ToString();
                    button.Name = clsFoodDonations.DSet.Tables["FoodDonations"].Rows[i][0].ToString();

                }
                for (int j = 0; j < 20; j++)
                {
                    enumerator.MoveNext();
                    button = (System.Windows.Forms.Button)enumerator.Current;
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
            try
            {
                for (int i = 0; i < parmDonorTypeCode.TypeCodesArray.Count; i++)
                {
                    enumerator1.MoveNext();
                    button = (System.Windows.Forms.Button)enumerator1.Current;
                    button.Text = parmDonorTypeCode.GetLongName(i);
                    string tmp = parmDonorTypeCode.GetLongName(i);
                    button.Name = parmDonorTypeCode.GetId(tmp).ToString();
                }
                for (int j = 0; j < 16; j++)
                {
                    enumerator1.MoveNext();
                    button = (System.Windows.Forms.Button)enumerator1.Current;
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

        private void button21_Click(object sender, EventArgs e)
        {

        }
    }

}
