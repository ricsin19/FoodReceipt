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
                for (int j = 0; j < this.tabPage2.Controls.Count; j++)
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
                for (int j = 0; j < this.tabPage1.Controls.Count; j++)
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

        private void fillDataGrid(String buttonID)
        {
            int val = Convert.ToInt32(buttonID);
            string whereClause = " WHERE RcdType=" + val;
            clsDonors.openWhere(whereClause);
            dgvDonorList.Rows.Clear();
            string ID = "";
            string Name = "";
            DataGridViewRow dvr;
            int rowCount = 0;
            for (int i = 0; i < clsDonors.RowCount; i++)
            {
                try
                {
                    clsDonors.setDataRow(i);
                    Name = clsDonors.DSet.Tables["Donors"].Rows[i][2].ToString();
                    ID = clsDonors.DSet.Tables["Donors"].Rows[i][0].ToString();

                    //DataGrid View
                    dgvDonorList.Rows.Add(ID, Name);
                    dvr = dgvDonorList.Rows[rowCount];
                    rowCount++;           
                }
                catch (Exception ex)
                {
                    CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
                }
            }
        }
        private void button21_Click(object sender, EventArgs e)
        {
            fillDataGrid(button21.Name);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            fillDataGrid(button22.Name);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            fillDataGrid(button23.Name);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            fillDataGrid(button24.Name);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            fillDataGrid(button25.Name);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            fillDataGrid(button26.Name);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            fillDataGrid(button27.Name);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            fillDataGrid(button28.Name);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            fillDataGrid(button29.Name);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            fillDataGrid(button30.Name);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            fillDataGrid(button31.Name);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            fillDataGrid(button32.Name);
        }

        private void button33_Click(object sender, EventArgs e)
        {
            fillDataGrid(button33.Name);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            fillDataGrid(button34.Name);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            fillDataGrid(button35.Name);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            fillDataGrid(button36.Name);
        }

    }
}
