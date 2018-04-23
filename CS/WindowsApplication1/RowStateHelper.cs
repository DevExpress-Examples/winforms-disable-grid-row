using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WindowsApplication1
{
    [DesignerCategory("")]
    [Designer("")]
    public class RowStateHelper : Component
    {
        private AppearanceObject _AppearanceDisabledRow;

        private List<int> _DisabledRows;

        private GridView _GridView;

        public RowStateHelper()
        {
            _DisabledRows = new List<int>();
            _AppearanceDisabledRow = new AppearanceObject();
            AppearanceDisabledRow.Changed += _AppearanceDisabledRow_Changed;
        }

        void _AppearanceDisabledRow_Changed(object sender, EventArgs e)
        {
            if (GridView == null) return;
            for (int i = 0; i < DisabledRows.Count; i++)
            {
                GridView.RefreshRow(GridView.GetRowHandle(DisabledRows[i]));
            }
        }

        private void SubscribeEvents(GridView view)
        {
            if (view != null)
            {
                view.RowCellStyle += view_RowCellStyle;
                view.ShowingEditor += view_ShowingEditor;
            }
        }

        private void UnSubscribeEvents(GridView view)
        {
            if (view != null)
            {
                view.RowCellStyle -= view_RowCellStyle;
                view.ShowingEditor -= view_ShowingEditor;
            }
        }

        void view_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            Console.WriteLine(DateTime.Now.Millisecond);
            if (IsRowDisabled(GridView.GetDataSourceRowIndex(e.RowHandle)))
                e.Appearance.Assign(AppearanceDisabledRow);
        }

        void view_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = IsRowDisabled(GridView.GetDataSourceRowIndex(GridView.FocusedRowHandle));
        }

        public void DisableRow(int dataSourceRowIndex)
        {
            DisabledRows.Add(dataSourceRowIndex);
        }

        public void EnableRow(int dataSourceRowIndex)
        {
            while (IsRowDisabled(dataSourceRowIndex))
            {
                DisabledRows.Remove(dataSourceRowIndex);
            }
        }

        public bool IsRowDisabled(int dataSourceRowIndex)
        {
            return DisabledRows.Contains(dataSourceRowIndex);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public AppearanceObject AppearanceDisabledRow {
            get { return _AppearanceDisabledRow; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<int> DisabledRows {
            get { return _DisabledRows; }
        }
        public GridView GridView {
            get { return _GridView; }
            set { UnSubscribeEvents(_GridView); _GridView = value; SubscribeEvents(value); }
        }
    }
}
