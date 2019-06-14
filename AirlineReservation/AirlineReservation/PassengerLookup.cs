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
    public partial class PassengerLookup : Form
    {
        private OleDbCommand command;
        private DataTable table;

        public PassengerLookup(DataTable table)
        {
            InitializeComponent();
            this.table = table; //the keyword this points to the variable; the one without the keyword is the argument for this method.

        }

        private void PassengerLookup_Load(object sender, EventArgs e)
        {
            dgvOutput.DataSource = this.table;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvOutput_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //get row that the user clicked
            var index = e.RowIndex; //gets the index of the row

            //get the passenger id from the clicked row and pass it to the command.
            int selectedID = Convert.ToInt32(dgvOutput.Rows[index].Cells[0].Value);

            //select sll the passenger info from all tables for the clicked passenger
            using (var conn = new OleDbConnection(DatabaseObjects.ConnectionString))
            {
                //execute command and place the resuls in the DataTable and pass it to the EditDelete Form.
                conn.Open();
                command = new OleDbCommand
                (
              "Select p.PassengerID as ID, s.SeatID, p.PassengerName as Name, s.SeatRow, s.SeatColumn, " +
                "p.PassengerOnWaitingList as OnWaitingList " +
                "FROM (Passengers p " +
                "inner join PassengerSeats ps on p.PassengerID = ps.PassengerID) " +
                "inner join Seats s on s.SeatID = ps.SeatID " +
                "WHERE p.PassengerID = @PassengerID " +
                "UNION " +
                "Select p.PassengerID, null, p.PassengerName, null,null,p.PassengerOnWaitingList " +
                "FROM Passengers p " +
                "WHERE p.PassengerOnWaitingList = true AND p.PassengerID = @PassengerID " +
                "ORDER BY s.SeatRow, s.SeatColumn"
               , conn);
                command.Parameters.Add(new OleDbParameter("PassengerID", selectedID));
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                EditDelete form = new EditDelete(dt);//passing the query results to the Passenger lookup form
                form.ShowDialog();
                Close();
                
            }

        }
    }
}
