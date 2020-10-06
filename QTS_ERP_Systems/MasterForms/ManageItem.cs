﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QTS_ERP_Systems.Model;
namespace QTS_ERP_Systems.MasterForms
{
    public partial class ManageItem : Form
    {
        readonly DbCon db = new DbCon();
        
        private string iTEMNo;

        private string itemid;
        private int itemcode;
        private string itemname;
        private string printablename;
        private string unittype;
        private int unitcost;
        private int sellingprice;
        private bool isservice;
        private readonly string Collection = "Items";
        public ManageItem()
        {
            InitializeComponent();
        }
        public void FormLoad()
        {
            dgvItem.DataSource = db.FilterItem("");
            dgvItem.Columns[0].Visible = false;
        }
        private void ManageItem_Load(object sender, EventArgs e)
        {
            FormLoad();
        }
        private void ClearText()
        {
            txtItemNo.Clear();
            txtItemCode.Clear();
            txtItemName.Clear();
            txtPrintName.Clear();
            txtUnitType.Clear();
            txtUnitCost.Clear();
            txtService.Clear();
            txtSellingPrice.Clear();
        }
        private void EnableTrue()
        {
            txtItemNo.Enabled = true;
            txtItemCode.Enabled = true;
            txtItemName.Enabled = true;
            txtPrintName.Enabled = true;
            txtUnitType.Enabled = true;
            txtUnitCost.Enabled = true;
            txtService.Enabled = true;
            txtSellingPrice.Enabled = true;
        }
        private void EnableFalse()
        {
            txtItemNo.Enabled = false;
            txtItemCode.Enabled = false;
            txtItemName.Enabled = false;
            txtPrintName.Enabled = false;
            txtUnitType.Enabled = false;
            txtUnitCost.Enabled = false;
            txtService.Enabled = false;
            txtSellingPrice.Enabled = false;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EnableTrue();
            ClearText();
            txtItemNo.Focus();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            int IMNo = Convert.ToInt32(txtItemNo.Text);
            bool IMChqNo = db.CheckChequeNo(IMNo);
            if (IMChqNo == true)
            {
                MessageBox.Show("This Item Already Added");
            }
            else {
                Item itm = new Item
                {
                    item_id = txtItemNo.Text,
                    item_code = Convert.ToInt32(txtItemCode.Text.Trim()),
                    item_name = txtItemName.Text.Trim(),
                    printable_name = txtPrintName.Text.Trim(),
                    unit_type = txtUnitType.Text.Trim(),
                    unit_cost = Convert.ToInt32(txtUnitCost.Text.Trim()),
                    selling_price = Convert.ToInt32(txtSellingPrice.Text),
                  is_service =Convert.ToBoolean(txtService.Text)

                };
                db.InsertItem(Collection, itm);
                ClearText();
                FormLoad();

            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
          itemid = txtItemNo.Text;
            itemcode = Convert.ToInt32(txtItemCode.Text.Trim());
            itemname = txtItemName.Text.Trim();
            printablename = txtPrintName.Text.Trim();
            unittype = txtUnitType.Text.Trim();
            unitcost = Convert.ToInt32(txtUnitCost.Text.Trim());
            sellingprice = Convert.ToInt32(txtSellingPrice.Text);
            isservice = Convert.ToBoolean(txtService.Text);

            db.UpdateItem(Collection, itemid, itemcode, itemname, printablename, unittype, unitcost, sellingprice, isservice);
            ClearText();
            
            //EnableTrue();
            FormLoad();
        }

        private void txtSearchItem_keypress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtSearchItem_keyup(object sender, KeyEventArgs e)
        {
            string IEM = txtSearchItem.Text.Trim();
            dgvItem.DataSource = db.FilterItem(IEM);
            //  e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            iTEMNo = txtItemNo.Text;
            db.DeleteItem(iTEMNo);
            ClearText();
            FormLoad();
            EnableTrue();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearText();
            EnableFalse();
        }

      

        private void dgvItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtItemNo.Text = dgvItem.CurrentRow.Cells[0].Value.ToString();
            txtItemCode.Text = dgvItem.CurrentRow.Cells[1].Value.ToString();
            txtItemName.Text = dgvItem.CurrentRow.Cells[2].Value.ToString();
            txtPrintName.Text = dgvItem.CurrentRow.Cells[3].Value.ToString();
            txtUnitType.Text = dgvItem.CurrentRow.Cells[4].Value.ToString();
            txtUnitCost.Text = dgvItem.CurrentRow.Cells[5].Value.ToString();
            txtSellingPrice.Text = dgvItem.CurrentRow.Cells[6].Value.ToString();
            txtService.Text = dgvItem.CurrentRow.Cells[7].Value.ToString();
            EnableTrue();
            //txtItemNo.Enabled = false;
        }
    }
}
