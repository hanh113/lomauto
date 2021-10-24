using JadeLib;
using KAutoHelper;
using LOMAuto.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOMAuto
{
    public partial class Form2 : Form
    {
        public List<string> listDevice;
        private List<GameObject> lstGameObj = new List<GameObject>();
        public Form2()
        {
            InitializeComponent();
            dgDeviceChar.AllowUserToAddRows = false;
            dgDeviceChar.CellContentClick += new DataGridViewCellEventHandler(dgDeviceChar_CellClick);
            dgDeviceChar.CellValueChanged += new DataGridViewCellEventHandler(dgDeviceChar_CurrentCellDirtyStateChanged);



            ClientUtils.ConnStr = ConfigurationManager.AppSettings["CONN_STR"];
            //ClientUtils.ConnStr = @"data source=VN-NB-013709YY1\SQLEXPRESS;initial catalog=LOM;persist security info=True;user id=sa;password=Admin123;";

            string deviceID = null;
            listDevice = KAutoHelper.ADBHelper.GetDevices();
            if (listDevice != null && listDevice.Count > 0)
            {
                deviceID = listDevice.First();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string deviceID = null;
            listDevice = KAutoHelper.ADBHelper.GetDevices();
            if (listDevice != null && listDevice.Count > 0)
            {
                deviceID = listDevice.First();
            }
            //ADBHelper.Tap(deviceID, 930.1, 1480.6);
        }

        private void btnGetCurrentLevel_Click(object sender, EventArgs e)
        {
            foreach (var item in listDevice)
            {
                GameObject gObject = new GameObject(item);
                gObject.SetCharInfo();
            }
        }

        private void btnCurrentStatus_Click(object sender, EventArgs e)
        {
            foreach (var item in listDevice)
            {
                GameObject gObject = new GameObject(item);
                gObject.SetCurrentStatus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in listDevice)
            {
                GameObject gObject = new GameObject(item);
                //gObject.GetNewGame();
                lstGameObj.Add(gObject);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var dt = ClientUtils.ExecuteSQL(" select * from dbo.t_lom order by update_time ").Tables[0];
            dgDeviceChar.DataSource = dt;

            foreach (DataGridViewRow row in dgDeviceChar.Rows)
            {
                var isAuto = row.Cells["IS_AUTO"].Value;
                //More code here
            }
        }

        private void dgDeviceChar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCheckBoxCell dgvCheck = (DataGridViewCheckBoxCell)(this.dgDeviceChar.Rows[this.dgDeviceChar.CurrentCell.RowIndex].Cells["IS_AUTO"]);
            if (this.dgDeviceChar.Columns[this.dgDeviceChar.CurrentCell.ColumnIndex].Name == "IS_AUTO")
            {
                var chk = Convert.ToBoolean(dgvCheck.EditedFormattedValue) ? "1" : "0";
                string DEVICE_ID = this.dgDeviceChar.Rows[this.dgDeviceChar.CurrentCell.RowIndex].Cells["DEVICE_ID"].EditedFormattedValue.ToString();
                ClientUtils.ExecuteSQLAsync(" update t_lom set is_auto = '" + chk + "' where DEVICE_ID = '" + DEVICE_ID + "'");

                var gobj = lstGameObj.Where(x => x.DeviceID == DEVICE_ID).FirstOrDefault();
                gobj.mainChar.IS_AUTO = false;
            }
        }
        private void dgDeviceChar_CurrentCellDirtyStateChanged(object sender, DataGridViewCellEventArgs e)
        {
            int i = this.dgDeviceChar.CurrentCell.ColumnIndex;
            int i1 = this.dgDeviceChar.CurrentCell.RowIndex;

            string colName = this.dgDeviceChar.Columns[this.dgDeviceChar.CurrentCell.ColumnIndex].Name;

            if (colName == "ID" || colName == "DEVICE_ID")
            {
                MessageBox.Show("Cannot change this column!");
                return;
            }
            else
            {
                string DEVICE_ID = this.dgDeviceChar.Rows[this.dgDeviceChar.CurrentCell.RowIndex].Cells["DEVICE_ID"].EditedFormattedValue.ToString();
                string value = this.dgDeviceChar.Rows[this.dgDeviceChar.CurrentCell.RowIndex].Cells[colName].EditedFormattedValue.ToString();

                ClientUtils.ExecuteSQLAsync(" update t_lom set " + colName + " = '" + value + "' where DEVICE_ID = '" + DEVICE_ID + "'");
                MessageBox.Show(i + " " + i1 + " " + colName + " " + value);

                var gobj = lstGameObj.Where(x => x.DeviceID == DEVICE_ID).FirstOrDefault();
                var objChar = gobj.mainChar;

                var propertyInfo = objChar.GetType().GetProperties().Where(x => x.Name == colName).FirstOrDefault();


                var valueType = Convert.ChangeType(value, propertyInfo.PropertyType);
                propertyInfo.SetValue(objChar, valueType, null);
            }
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            foreach (var gObject in lstGameObj)
            {
                gObject.Auto();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
