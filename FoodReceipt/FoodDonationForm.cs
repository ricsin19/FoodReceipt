using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;
using HidLibrary;


namespace FoodReceipt
{
    public partial class FoodDonationForm : Form
    {
        FoodDonations clsFoodDonations = new FoodDonations(CCFBGlobal.connectionString);
        Donors clsDonors = new Donors(CCFBGlobal.connectionString);
        parmTypeCodes parmDonationType = new parmTypeCodes(CCFBGlobal.parmTbl_Donation, CCFBGlobal.connectionString, "");
        parmTypeCodes parmFoodClass = new parmTypeCodes(CCFBGlobal.parmTbl_FoodClass, CCFBGlobal.connectionString, "");
        string donorID, donorName;
        int FoodClassSelected = 0;
        string FoodCodeSelected = "";

        int refreshTimeLeft = 15;
        int refreshTimeStart = 15;

        public FoodDonationForm(string val1, String val2)
        {
            InitializeComponent();
            donorID = val1;
            donorName = val2;
            formLoad();
            fillCombos();
            initScalePort();
            StartTimer();

        }
        private void FoodDonation_Load(object sender, EventArgs e)
        {
            labelDonorID.Text = donorID;
            labelDonorName.Text = donorName;
        }
        private void fillCombos()
        {
            CCFBGlobal.InitCombo(cboDonationType, CCFBGlobal.parmTbl_Donation);
            clsDonors.openWhere(" Where ID=" + donorID.ToString());
            cboDonationType.SelectedValue = clsDonors.DefaultDonationType.ToString();
        }
        private void formLoad()
        {
            IEnumerator enumerator1 = this.groupBox2.Controls.GetEnumerator();
            System.Windows.Forms.Button button;
            try
            {
                for (int i = 0; i < parmFoodClass.TypeCodesArray.Count; i++)
                {
                    enumerator1.MoveNext();
                    button = (System.Windows.Forms.Button)enumerator1.Current;
                    button.Text = parmFoodClass.GetLongName(i);
                    string tmp = parmFoodClass.GetLongName(i);
                    button.Name = parmFoodClass.GetId(tmp).ToString();
                    FoodCodeSelected = button.Text;
                }
                clsFoodDonations.openAll();
            }
            catch (Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }
        private void foodClassbutton_Click(object sender, MouseEventArgs e)
        {
            updateOtherBtnColor();
            Button b = (Button)sender;
            b.BackColor = Color.Aqua;
            FoodClassSelected = Convert.ToInt32(b.Name);
            FoodCodeSelected = b.Text;
           
        }
        private void updateOtherBtnColor()
        {
            IEnumerator enumerator1 = this.groupBox2.Controls.GetEnumerator();
            System.Windows.Forms.Button button;
            for (int i = 0; i < parmFoodClass.TypeCodesArray.Count; i++)
            {
                enumerator1.MoveNext();
                button = (System.Windows.Forms.Button)enumerator1.Current;
                button.BackColor = Color.Empty;
            }
        }

        private void Cancelbtn_Click(object sender, MouseEventArgs e)
        {
            this.Close();
        }
        private void initScalePort()
        {
            decimal? weight;
            bool? isStable;

            USBScale s = new USBScale();
            s.Connect();

            if (s.IsConnected)
            {
                s.GetWeight(out weight, out isStable);
                s.Disconnect();
                decimal roundedWeight = weight.HasValue ? Math.Round(weight.Value) : 0;
                scaleWt.Text = Convert.ToString(roundedWeight);
                if (string.IsNullOrEmpty(tbLbs.Text) && (tbLbs.Text == "0"))
                {
                    tbLbs.Text = scaleWt.Text;
                }
            }
            else
            {
                scaleWt.Text = "0";
            }
        }
        private void addWeightButton_Click_1(object sender, MouseEventArgs e)
        {
            Int32 totalValue;
            if (!string.IsNullOrEmpty(scaleWt.Text))
            {
                Int32 currValue = Int32.Parse(scaleWt.Text);
                if (!string.IsNullOrEmpty(tbLbs.Text))
                {
                    totalValue = Int32.Parse(tbLbs.Text);
                }
                else
                {
                    totalValue = 0;
                }
                totalValue += currValue;
                tbLbs.Text = totalValue.ToString();
            }
        }
        private void LogEntrySave_Click(object sender, MouseEventArgs e)
        {
            if (tbLbs.Text.Trim() != "")
            {
                DataRow drow = clsFoodDonations.DSet.Tables[0].NewRow();
                drow["DonorID"] = labelDonorID.Text;
                if (string.IsNullOrEmpty(tbLbs.Text))
                {
                    drow["Pounds"] = "0";
                }
                else
                {
                    drow["Pounds"] = tbLbs.Text;
                }
                drow["Notes"] = noteTxt.Text.TrimEnd();
                drow["FoodClass"] = FoodClassSelected;
                drow["TrxDate"] = dateTimePicker1.Value.ToShortDateString(); ;
                drow["DonationType"] = cboDonationType.SelectedValue;
                drow["FoodCode"] = FoodCodeSelected;
                drow["DollarValue"] = 0;
                drow["CreatedBy"] = CCFBGlobal.dbUserName;
                drow["Created"] = DateTime.Now;
                drow["ModifiedBy"] = "CREATED";
                drow["Modified"] = DateTime.Now;
                drow["Flag0"] = false;
                drow["Flag1"] = false;
                drow["Flag2"] = false;
                clsFoodDonations.DSet.Tables[0].Rows.Add(drow);
                clsFoodDonations.insert();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Fill In Number Of Pounds, And Try Again");
            }
        }

        private void tbScaleWt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }
        private void refreshBtn_Click(object sender, MouseEventArgs e)
        {
            initScalePort();
        }
        private void clrBtn_Click(object sender, MouseEventArgs e)
        {
            scaleWt.Text = "0";
            tbLbs.Text = "0";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            refreshTimeLeft--;
            tssStatus.Text = refreshTimeLeft.ToString();
            Application.DoEvents();
            if (refreshTimeLeft <= 0)
            {
                initScalePort();
                timer1.Stop();
                StartTimer();
            }

        }
        private void StartTimer()
        {
            refreshTimeLeft = refreshTimeStart;
            tssStatus.Text = refreshTimeLeft.ToString();
            tssStatus.BackColor = Color.Aquamarine;
            timer1.Start();

        }
    }
}
