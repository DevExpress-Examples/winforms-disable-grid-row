Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Windows.Forms

Namespace WindowsApplication1
	Partial Public Class Form1
		Inherits Form
		Public DisabledRowHandles As List(Of Integer)

		Public Sub New()
			InitializeComponent()
			DisabledRowHandles = New List(Of Integer) (New Integer() {0, 3, 4, 7, 9})
			gridControl1.DataSource = CreateTable(20)
			AddHandler disabledCellEvents1.ProcessingCell, AddressOf DisabledCellEvents1_ProcessingCell
		End Sub

		Private Shared Function CreateTable(ByVal RowCount As Integer) As DataTable
			Dim tbl As New DataTable()
			tbl.Columns.Add("Name", GetType(String))
			tbl.Columns.Add("ID", GetType(Integer))
			tbl.Columns.Add("Number", GetType(Integer))
			tbl.Columns.Add("Date", GetType(DateTime))
			For i As Integer = 0 To RowCount - 1
				tbl.Rows.Add(New Object() { String.Format("Name{0}", i), i, 3 - i, DateTime.Now.AddDays(i) })
			Next i
			Return tbl
		End Function
		Private Sub DisabledCellEvents1_ProcessingCell(ByVal sender As Object, ByVal e As DevExpress.Utils.Behaviors.Common.ProcessCellEventArgs)
			If DisabledRowHandles.Contains(e.RecordId) Then
				e.Disabled = True
			End If
		End Sub
	End Class
End Namespace