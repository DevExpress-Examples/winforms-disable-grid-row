Imports Microsoft.VisualBasic
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Grid
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel

Namespace WindowsApplication1
	<DesignerCategory(""), Designer("")> _
	Public Class RowStateHelper
		Inherits Component
		Private _AppearanceDisabledRow As AppearanceObject

		Private _DisabledRows As List(Of Integer)

		Private _GridView As GridView

		Public Sub New()
			_DisabledRows = New List(Of Integer)()
			_AppearanceDisabledRow = New AppearanceObject()
			AddHandler AppearanceDisabledRow.Changed, AddressOf _AppearanceDisabledRow_Changed
		End Sub

		Private Sub _AppearanceDisabledRow_Changed(ByVal sender As Object, ByVal e As EventArgs)
			If GridView Is Nothing Then
				Return
			End If
			For i As Integer = 0 To DisabledRows.Count - 1
				GridView.RefreshRow(GridView.GetRowHandle(DisabledRows(i)))
			Next i
		End Sub

		Private Sub SubscribeEvents(ByVal view As GridView)
			If view IsNot Nothing Then
				AddHandler view.RowCellStyle, AddressOf view_RowCellStyle
				AddHandler view.ShowingEditor, AddressOf view_ShowingEditor
			End If
		End Sub

		Private Sub UnSubscribeEvents(ByVal view As GridView)
			If view IsNot Nothing Then
				RemoveHandler view.RowCellStyle, AddressOf view_RowCellStyle
				RemoveHandler view.ShowingEditor, AddressOf view_ShowingEditor
			End If
		End Sub

		Private Sub view_RowCellStyle(ByVal sender As Object, ByVal e As RowCellStyleEventArgs)
			Console.WriteLine(DateTime.Now.Millisecond)
			If IsRowDisabled(GridView.GetDataSourceRowIndex(e.RowHandle)) Then
				e.Appearance.Assign(AppearanceDisabledRow)
			End If
		End Sub

		Private Sub view_ShowingEditor(ByVal sender As Object, ByVal e As CancelEventArgs)
			e.Cancel = IsRowDisabled(GridView.GetDataSourceRowIndex(GridView.FocusedRowHandle))
		End Sub

		Public Sub DisableRow(ByVal dataSourceRowIndex As Integer)
			DisabledRows.Add(dataSourceRowIndex)
		End Sub

		Public Sub EnableRow(ByVal dataSourceRowIndex As Integer)
			Do While IsRowDisabled(dataSourceRowIndex)
				DisabledRows.Remove(dataSourceRowIndex)
			Loop
		End Sub

		Public Function IsRowDisabled(ByVal dataSourceRowIndex As Integer) As Boolean
			Return DisabledRows.Contains(dataSourceRowIndex)
		End Function

		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property AppearanceDisabledRow() As AppearanceObject
			Get
				Return _AppearanceDisabledRow
			End Get
		End Property

		<DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
		Public ReadOnly Property DisabledRows() As List(Of Integer)
			Get
				Return _DisabledRows
			End Get
		End Property
		Public Property GridView() As GridView
			Get
				Return _GridView
			End Get
			Set(ByVal value As GridView)
				UnSubscribeEvents(_GridView)
				_GridView = value
				SubscribeEvents(value)
			End Set
		End Property
	End Class
End Namespace
