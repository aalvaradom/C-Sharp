using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservation
{
    public partial class EditDelete : Form  
    {
        //Database objects
        private DataTable table;
        private OleDbCommand command;
        OleDbDataReader reader;

        public EditDelete(DataTable table)
        {
            InitializeComponent();
            command = new OleDbCommand();
            this.table = table;
        }

        //Bind the form objects to the data from the DataTable
        private void EditDelete_Load(object sender, EventArgs e)
        {
            
            //bind text boxes
            txtPassengerID.DataBindings.Add("Text", table, "ID"); //using the  aliases from the sql command
            txtName.DataBindings.Add("Text", table, "Name");
            txtSeatID.DataBindings.Add("Text", table, "SeatID");

            //bind drop downs
            var r = table.Rows[0]["SeatRow"].ToString();
            var row = r.Equals("") ? 0 : Convert.ToInt32(r);
            var c = table.Rows[0]["SeatColumn"].ToString();
            var column = c.Equals("") ? "None" : c;
            //txtTest.DataBindings.Add("Text", table, "SeatColumn");
            //txtTest2.DataBindings.Add("Text", table, "SeatRow");

            cmbRow.Text = row.ToString();
            cmbColumn.Text = column;
            //cmbRow.SelectedIndex = row;
            //cmbColumn.SelectedItem = column;

            chbWaiting.Checked = Convert.ToBoolean(table.Rows[0]["OnWaitingList"]);
            
        }

        //edit record
        private void btnEdit_Click(object sender, EventArgs e)
        {
            //validate input
            //make sure passenger name has been entered

            //passenger can't be on waiting list and have a seat assigned.

            //passenger must be either on waiting list or have a seat assigned

            //update record.
            //get the id of the new seat
            //check if the user is only updating the passenger name, if so, then ignore that the seat is taken.
            //update all tables.
            //  update passenger name in passengers' table
            //  update seats table. The old seat needs to be updated to not taken and new seat to taken
            //  assing new seatID to the passengerID in PassengerSeats table.

        }
    }
}
