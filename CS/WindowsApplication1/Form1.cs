using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace WindowsApplication1
{
	public partial class Form1 : Form
	{
		public List<int> DisabledRowHandles;

		public Form1() {
			InitializeComponent();
			DisabledRowHandles = new List<int>() { 0, 3, 4, 7, 9 };
			gridControl1.DataSource = CreateTable(20);
			disabledCellEvents1.ProcessingCell += DisabledCellEvents1_ProcessingCell;
		}

		private static DataTable CreateTable(int RowCount) {
			DataTable tbl = new DataTable();
			tbl.Columns.Add("Name", typeof(string));
			tbl.Columns.Add("ID", typeof(int));
			tbl.Columns.Add("Number", typeof(int));
			tbl.Columns.Add("Date", typeof(DateTime));
			for(int i = 0; i < RowCount; i++)
				tbl.Rows.Add(new object[] { String.Format("Name{0}", i), i, 3 - i, DateTime.Now.AddDays(i) });
			return tbl;
		}
		private void DisabledCellEvents1_ProcessingCell(object sender, DevExpress.Utils.Behaviors.Common.ProcessCellEventArgs e) {
			if(DisabledRowHandles.Contains(e.RecordId))
				e.Disabled = true;
		}
	}
}