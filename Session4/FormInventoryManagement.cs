using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Session4
{
    public partial class FormInventoryManagement : Form
    {
        private long itemID;

        public FormInventoryManagement()
        {
            InitializeComponent();
        }

        private void FormIM_Load(object sender, EventArgs e)
        {
            LoadInvetory();

            //Edit link
            DataGridViewLinkColumn Editlink = new DataGridViewLinkColumn();
            Editlink.UseColumnTextForLinkValue = true;
            Editlink.HeaderText = "Action";
            Editlink.DataPropertyName = "linkColumn";
            Editlink.LinkBehavior = LinkBehavior.SystemDefault;
            Editlink.Text = "Edit";
            dgvInventory.Columns.Add(Editlink);

            //Delete link
            DataGridViewLinkColumn Deletelink = new DataGridViewLinkColumn();
            Deletelink.UseColumnTextForLinkValue = true;
            Deletelink.DataPropertyName = "linkColumn";
            Deletelink.LinkBehavior = LinkBehavior.SystemDefault;
            Deletelink.Text = "Delete";
            dgvInventory.Columns.Add(Deletelink);
        }

        private void LoadInvetory()
        {
            dgvInventory.DataSource = Connect.db.OrderItems
                .Select(o => new
                {
                    o.ID,
                    PartName = o.Part.Name,
                    TransactionType = o.Order.TransactionType.Name,
                    o.Order.Date,
                    o.Amount,
                    Supplier = o.Order.Supplier.Name,
                    Source = Connect.db.Warehouses.Where(w => w.ID == o.Order.SourceWarehouseID).FirstOrDefault().Name,
                    Destination = Connect.db.Warehouses.Where(w => w.ID == o.Order.DestinationWarehouseID).FirstOrDefault().Name,
                    Type = o.Order.TransactionType.ID
                }).OrderBy(x => x.Date).ThenBy(x => x.Type).ToList();

            dgvInventory.Columns[0].HeaderText = "ID";
            dgvInventory.Columns[0].Visible = false;
            dgvInventory.Columns[1].HeaderText = "Part Name";
            dgvInventory.Columns[2].HeaderText = "Transaction Type";
            dgvInventory.Columns[3].HeaderText = "Date";
            dgvInventory.Columns[4].HeaderText = "Amount";
            dgvInventory.Columns[5].HeaderText = "Supplier";
            dgvInventory.Columns[6].HeaderText = "Source";
            dgvInventory.Columns[7].HeaderText = "Destination";
            dgvInventory.Columns[8].HeaderText = "Type";
            dgvInventory.Columns[8].Visible = false;

            for (int i = 0; i < Connect.db.OrderItems.Select(o => o.ID).Count(); i++)
            {
                if (dgvInventory.Rows[i].Cells[2].Value.ToString() == "Purchase Order")
                {
                    dgvInventory.Rows[i].Cells[4].Style.BackColor = Color.Green;
                }
            }
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPurchaseOrder form = new FormPurchaseOrder();
            form.Show();
            Hide();
        }

        private void warehouseManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormWarehouseManagement form = new FormWarehouseManagement();
            form.Show();
            Hide();
        }

        private void inventoryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormInventoryReport form = new FormInventoryReport();
            form.Show();
            Hide();
        }

        private void FormIM_Closed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void dgvInventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dgvInventory.Rows.Count; i++)
            {
                itemID = long.Parse(dgvInventory.Rows[i].Cells[0].Value.ToString());
            }

            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 9)
                {
                    MessageBox.Show("Edit");
                } 
                else if (e.ColumnIndex == 10)
                {
                    var result = MessageBox.Show("Are you sure want to delete this item?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(result == DialogResult.Yes)
                    {
                        var deleteOrderItem = from item in Connect.db.OrderItems
                                              where item.ID == itemID
                                              select item;

                        foreach (var item in deleteOrderItem)
                        {
                            Connect.db.OrderItems.Remove(item);
                        }

                        try
                        {
                            Connect.db.SaveChanges();
                            MessageBox.Show("Item has been deleted!", "Notify");
                            dgvInventory.Update();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            // Provide for exceptions.
                        }
                    }                               
                }
            }
        }
    }
}
