Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils

Namespace WindowsApplication1

    Public Class RowStateHelper
        Inherits Component

        Private _AppearanceDisabledRow As AppearanceObject

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
        Public ReadOnly Property AppearanceDisabledRow As AppearanceObject
            Get
                Return _AppearanceDisabledRow
            End Get
        End Property

        Private _GridView As GridView

        Public Property GridView As GridView
            Get
                Return _GridView
            End Get

            Set(ByVal value As GridView)
                UnSubscribeEvents(value)
                _GridView = value
                SubscribeEvents(value)
            End Set
        End Property

        Private _DisabledRows As List(Of Integer)

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
        Public ReadOnly Property DisabledRows As List(Of Integer)
            Get
                Return _DisabledRows
            End Get
        End Property

        Public Sub New()
            _DisabledRows = New List(Of Integer)()
            _AppearanceDisabledRow = New AppearanceObject()
            AddHandler AppearanceDisabledRow.Changed, AddressOf _AppearanceDisabledRow_Changed
        End Sub

        Private Sub _AppearanceDisabledRow_Changed(ByVal sender As Object, ByVal e As EventArgs)
            If GridView Is Nothing Then Return
            For i As Integer = 0 To DisabledRows.Count - 1
                GridView.RefreshRow(GridView.GetRowHandle(DisabledRows(i)))
            Next
        End Sub

        Private Sub UnSubscribeEvents(ByVal view As GridView)
            RemoveHandler view.RowCellStyle, AddressOf view_RowCellStyle
            RemoveHandler view.ShowingEditor, AddressOf view_ShowingEditor
        End Sub

        Private Sub SubscribeEvents(ByVal view As GridView)
            AddHandler view.RowCellStyle, AddressOf view_RowCellStyle
            AddHandler view.ShowingEditor, AddressOf view_ShowingEditor
        End Sub

        Public Sub DisableRow(ByVal dataSourceRowIndex As Integer)
            DisabledRows.Add(dataSourceRowIndex)
        End Sub

        Public Function IsRowDisabled(ByVal dataSourceRowIndex As Integer) As Boolean
            Return DisabledRows.Contains(dataSourceRowIndex)
        End Function

        Public Sub EnableRow(ByVal dataSourceRowIndex As Integer)
            While IsRowDisabled(dataSourceRowIndex)
                DisabledRows.Remove(dataSourceRowIndex)
            End While
        End Sub

        Private Sub view_ShowingEditor(ByVal sender As Object, ByVal e As CancelEventArgs)
            e.Cancel = IsRowDisabled(GridView.GetDataSourceRowIndex(GridView.FocusedRowHandle))
        End Sub

        Private Sub view_RowCellStyle(ByVal sender As Object, ByVal e As RowCellStyleEventArgs)
            Console.WriteLine(Date.Now.Millisecond)
            If IsRowDisabled(GridView.GetDataSourceRowIndex(e.RowHandle)) Then e.Appearance.Assign(AppearanceDisabledRow)
        End Sub
    End Class
End Namespace
