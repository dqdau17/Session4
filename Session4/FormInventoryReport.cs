using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Session4
{
    public partial class FormInventoryReport : Form
    {
        private int required;

        public FormInventoryReport()
        {
            InitializeComponent();
        }

        private void FormInventoryReport_Load(object sender, EventArgs e)
        {
            cmbWarehouse.DataSource = (from w in Connect.db.Warehouses
                                       select w.Name).ToList();

            
        }

        private void rdbCurrent_CheckedChanged(object sender, EventArgs e)
        {
            dgvResult.Update();
            dgvResult.Refresh();
            CurrentStock();
        }

        private void CurrentStock()
        {
            
            for (int i = 0; i < Connect.db.Parts.Select(p => p.ID).Count(); i++)
            {

                dgvResult.DataSource = Connect.db.OrderItems
                   .Select(o => new
                   {
                       ID = o.Part.ID,
                       PartName = o.Part.Name,
                   }).Distinct().ToList();

                dgvResult.Columns[0].HeaderText = "ID";
                //dgvResult.Columns[0].Visible = false;
                dgvResult.Columns[1].HeaderText = "Part Name";
                //dgvResult.Columns[2].HeaderText = "Current Stock";
                //dgvResult.Columns[3].HeaderText = "Received Stock";

            }
            // Show batch number link
            DataGridViewLinkColumn link = new DataGridViewLinkColumn();
            link.UseColumnTextForLinkValue = true;
            link.HeaderText = "Action";
            link.DataPropertyName = "linkColumn";
            link.LinkBehavior = LinkBehavior.SystemDefault;
            link.Text = "View Batch Number";
            dgvResult.Columns.Add(link);
        }

        private void rdbReceived_CheckedChanged(object sender, EventArgs e)
        {
            dgvResult.Update();
            dgvResult.Refresh();
            //ReceivedStock();
        }

        private void ReceivedStock()
        {
            
        }

        private void rdbOut_CheckedChanged(object sender, EventArgs e)
        {
            dgvResult.Update();
            dgvResult.Refresh();
            //OutofStock();
        }

        private void OutofStock()
        {

        }

        private void FormInventoryReport_Closed(object sender, FormClosedEventArgs e)
        {
            FormInventoryManagement form = new FormInventoryManagement();
            form.Show();
        }
    }
}
