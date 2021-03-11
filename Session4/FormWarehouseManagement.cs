using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Session4
{
    public partial class FormWarehouseManagement : Form
    {
        private long partID;

        public FormWarehouseManagement()
        {
            InitializeComponent();          
        }

        private void FormWM_Load(object sender, EventArgs e)
        { 
            cmbSource.DataSource = (from ws in Connect.db.Warehouses
                                    select ws.Name).ToList();

            cmbDestination.DataSource = (from wd in Connect.db.Warehouses
                                         select wd.Name).ToList();

            cmbPartname.DataSource = (from p in Connect.db.Parts
                                      select p.Name).ToList();

            DataGridViewLinkColumn link = new DataGridViewLinkColumn();
            link.UseColumnTextForLinkValue = true;
            link.HeaderText = "Action";
            link.Text = "Remove";
            link.ActiveLinkColor = default;
            dgvPartList.Columns.Add(link);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            partID = Connect.db.Parts.Where(p => p.Name == cmbPartname.Text).Select(p => p.ID).FirstOrDefault();
            dgvPartList.Rows.Add(partID, cmbPartname.Text, cmbBatch.Text, txtAmount.Text);
            txtAmount.Text = "";
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            long sourceID = Connect.db.Warehouses.Where(w => w.Name == cmbSource.Text).Select(w => w.ID).FirstOrDefault();
            long destinationID = Connect.db.Warehouses.Where(w => w.Name == cmbDestination.Text).Select(w => w.ID).FirstOrDefault();

            if (cmbSource.Text != cmbDestination.Text)
            {
                try
                {
                    Order order = new Order();
                    order.TransactionTypeID = 2;
                    order.SupplierID = null;
                    order.SourceWarehouseID = sourceID;
                    order.DestinationWarehouseID = destinationID;
                    order.Date = DateTime.Parse(dtpDate.Value.ToString());
                    Connect.db.Orders.Add(order);

                    long orderID = Connect.db.Orders.OrderByDescending(o => o.ID).FirstOrDefault().ID;
                    for (int i = 0; i < dgvPartList.Rows.Count; i++)
                    {
                        bool? checkBatch = Connect.db.Parts
                            .Where(c => c.Name == cmbPartname.Text)
                            .Select(c => c.BatchNumberHasRequired)
                            .FirstOrDefault();
                        partID = long.Parse(dgvPartList.Rows[i].Cells[0].Value.ToString());

                        OrderItem orderItem = new OrderItem();
                        orderItem.OrderID = orderID;
                        orderItem.PartID = partID;
                        if (checkBatch == true)
                        {
                            orderItem.BatchNumber = dgvPartList.Rows[i].Cells[2].Value.ToString();
                        }
                        else
                        {
                            orderItem.BatchNumber = "";
                        }
                        orderItem.Amount = decimal.Parse(dgvPartList.Rows[i].Cells[3].Value.ToString());
                        Connect.db.OrderItems.Add(orderItem);
                    }

                    Connect.db.SaveChanges();
                    MessageBox.Show("Submit Order is successfully!", "Notify");
                    Close();
                }
                catch (Exception x)
                {
                    MessageBox.Show("Undetected Error: " + x.Message.ToString(), "Error");
                }
            } 
            else
            {
                MessageBox.Show("Source Warehouse can't duplicate with Destination Warehouse!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbPartname_SelectedIndexChanged(object sender, EventArgs e)
        {
            partID = Connect.db.Parts.Where(p => p.Name == cmbPartname.SelectedItem.ToString()).Select(p => p.ID).FirstOrDefault();
            cmbBatch.DataSource = Connect.db.OrderItems.Where(c => c.PartID == partID).Select(c => c.BatchNumber).Distinct().ToList();
        }

        private void dgvPartList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 4)
            {
                dgvPartList.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void FormWN_Closed(object sender, FormClosedEventArgs e)
        {
            FormInventoryManagement formIM = new FormInventoryManagement();
            formIM.Show();
        }
    }
}
