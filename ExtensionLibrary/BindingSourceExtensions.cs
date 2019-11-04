using System.Data;
using System.Windows.Forms;

namespace ExtensionLibrary
{
    public static partial class BindingSourceExtensions
    {
        /// <summary>
        /// Cast DataSource to a DataTable
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static DataTable DataTable(this BindingSource sender)
        {
            return (DataTable)sender.DataSource;
        }
        /// <summary>
        /// Access the DataView of the DataTable
        /// </summary>
        /// <param name="pSender"></param>
        /// <returns></returns>
        public static DataView DataView(this BindingSource pSender)
        {
            return ((DataTable)pSender.DataSource).DefaultView;
        }
        /// <summary>
        /// Provides filter for starts-with, contains or ends-with
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="field">Field to apply filter on</param>
        /// <param name="value">Value for filter</param>
        /// <param name="condition">Type of filter</param>
        /// <param name="pCaseSensitive">Filter should be case or case in-sensitive</param>
        public static void RowFilter(this BindingSource sender, string field, string value, FilterCondition condition, bool pCaseSensitive = false)
        {
            switch (condition)
            {
                case FilterCondition.StartsWith:
                    sender.RowFilterStartsWith(field, value.EscapeApostrophe(), pCaseSensitive);
                    break;
                case FilterCondition.Contains:
                    sender.RowFilterContains(field, value.EscapeApostrophe(), pCaseSensitive);
                    break;
                case FilterCondition.EndsWith:
                    sender.RowFilterEndsWith(field, value.EscapeApostrophe(), pCaseSensitive);
                    break;
            }
        }
        public static void RowFilter(this BindingSource sender, string field, string value, bool caseSensitive = false)
        {
            sender.DataTable().CaseSensitive = caseSensitive;
            sender.DataView().RowFilter = $"{field} = '{value.EscapeApostrophe()}'";
        }
        public static DataView RowFilterNewView(this BindingSource pSender, string pField, string pValue, bool pCaseSensitive = false)
        {
            pSender.DataTable().CaseSensitive = pCaseSensitive;
            pSender.DataView().RowFilter = $"{pField} = '{pValue.EscapeApostrophe()}'";

            var view = new DataView(pSender.DataTable())
            {
                RowFilter = $"{pField} = '{pValue.EscapeApostrophe()}'"
            };

            return view;

        }
        public static void RowFilterTwoConditions(this BindingSource sender, string field1, string value1, string pField2, string value2, bool caseSensitive = false)
        {
            sender.DataTable().CaseSensitive = caseSensitive;
            sender.DataView().RowFilter = $"{field1} = '{value1.EscapeApostrophe()}' AND {pField2} = '{value2.EscapeApostrophe()}'";
        }
        /// <summary>
        /// Apply a filter for Like 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="field">Field to apply filter on</param>
        /// <param name="pValue">Value for filter</param>
        /// <param name="caseSensitive">Filter should be case or case in-sensitive</param>
        public static void RowFilterContains(this BindingSource sender, string field, string pValue, bool caseSensitive = false)
        {
            sender.DataTable().CaseSensitive = caseSensitive;
            sender.DataView().RowFilter = $"{field} LIKE '%{pValue.EscapeApostrophe()}%'";
        }
        /// <summary>
        /// Apply a filter for Like starts with
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="field">Field to apply filter on</param>
        /// <param name="value">Value for filter</param>
        /// <param name="caseSensitive">Filter should be case or case in-sensitive</param>
        public static void RowFilterStartsWith(this BindingSource sender, string field, string value, bool caseSensitive = false)
        {
            sender.DataTable().CaseSensitive = caseSensitive;
            sender.DataView().RowFilter = $"{field} LIKE '{value.EscapeApostrophe()}%'";
        }
        public static void RowFilterEndsWith(this BindingSource sender, string field, string value, bool caseSensitive = false)
        {
            sender.DataTable().CaseSensitive = caseSensitive;
            sender.DataView().RowFilter = $"{field} LIKE '%{value.EscapeApostrophe()}'";
        }
        /// <summary>
        /// This extension is a free form method for filtering. The usage would be
        /// to provide a user interface to put together the condition.  See unit
        /// test FreeForm_CaseSensitive_OnBoth_Conditions_LastField_NotExact
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="filter"></param>
        /// <param name="caseSensitive"></param>
        public static void RowFilterFreeForm(this BindingSource sender, string filter, bool caseSensitive = false)
        {
            sender.DataTable().CaseSensitive = caseSensitive;
            sender.DataView().RowFilter = filter;
        }
        /// <summary>
        /// Clear DataView RowFilter
        /// </summary>
        /// <param name="sender"></param>
        public static void RowFilterClear(this BindingSource sender)
        {
            sender.DataView().RowFilter = "";
        }
        /// <summary>
        /// Determine if DataSource is set
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static bool HasData(this BindingSource sender)
        {
            return sender.DataSource != null;
        }
    }
}
