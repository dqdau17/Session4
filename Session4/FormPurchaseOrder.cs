using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Session4
{
    public partial class FormPurchaseOrder : Form
    {
        private int required;
        private long partID;

        public FormPurchaseOrder()
        {
            InitializeComponent();
        }

        private void FormPO_Load(object sender, EventArgs e)
        {
            cmbSuppliers.DataSource = (from s in Connect.db.Suppliers
                                       select s.Name).ToList();
            cmbWarehouse.DataSource = (from w in Connect.db.Warehouses
                                       select w.Name).ToList();
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
            var partName = Connect.db.Parts.SingleOrDefault(p => p.Name == cmbPartname.Text);
            required = partName.BatchNumberHasRequired == true ? 1 : 0;
            decimal amount;
            switch (required)
            {
                case 0: 
                    if (!decimal.TryParse(txtAmount.Text, out amount) && amount <= 0)
                    {
                        MessageBox.Show("All field need to be fill!", "Error");
                    }
                    else
                    {
                        dgvPartList.Rows.Add(partID, cmbPartname.Text, "", txtAmount.Text);
                        txtAmount.Clear();
                    }
                    break;
                case 1:
                    if (!decimal.TryParse(txtAmount.Text, out amount) && amount <= 0 && string.IsNullOrEmpty(txtBatch.Text))
                    {
                        MessageBox.Show("All field need to be fill!", "Error");
                    }
                    else
                    {
                        // Batch Number is a unique number that differentiates between different productions of the part
                        // The list can contain multiple parts with same part name only if they have different batch numbers
                        var countBatch = Connect.db.OrderItems.Select(o => o.BatchNumber).Count();
                        if (countBatch == 1) 
                        {
                            MessageBox.Show("Batch Number is existed!", "Error");
                        }
                        else
                        {
                            dgvPartList.Rows.Add(partID, cmbPartname.Text, txtBatch.Text.ToUpper(), txtAmount.Text);
                            txtAmount.Clear();
                        }
                    }
                    break;
                default:
                    MessageBox.Show("Undetected error!", "Error");
                    break;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            long supplierID = Connect.db.Suppliers.FirstOrDefault(s => s.Name == cmbSuppliers.Text).ID;
            long warehouseID = Connect.db.Warehouses.FirstOrDefault(w => w.Name == cmbWarehouse.Text).ID;

            try
            {
                Order order = new Order();
                order.TransactionTypeID = 1;
                order.SupplierID = supplierID;
                order.SourceWarehouseID = null;
                order.DestinationWarehouseID = warehouseID;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvPartList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 4)
            {
                dgvPartList.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void FormPO_Closed(object sender, FormClosedEventArgs e)
        {
            FormInventoryManagement formIM = new FormInventoryManagement();
            formIM.Show();
        }
    }
}
