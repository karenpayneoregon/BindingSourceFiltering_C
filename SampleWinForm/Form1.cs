using System;
using System.Windows.Forms;
using Operations;
using FormHelpers;
using static ExtensionLibrary.BindingSourceExtensions;

namespace SampleWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Shown += Form1_Shown;
            ProductNameTextBox.KeyDown += ProductNameTextBox_KeyDown;
            ProductNameTextBox.TextChanged += ProductNameTextBox_TextChanged;
        }

        private readonly BindingSource _bindingSource = new BindingSource();

        private void Form1_Shown(object sender, EventArgs e)
        {
            ProductNameFilterComboBox.DataSource = Enum.GetValues(typeof(FilterCondition));

            var dataOperations = new DataOperations();

            _bindingSource.DataSource = dataOperations.GetProducts();
            dataGridView1.DataSource = _bindingSource;

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            ProductNameTextBox.SetCueText("Filter or empty to clear");

            ActiveControl = dataGridView1;
        }

        private void FilterDataButton_Click(object sender, EventArgs e)
        {
            FilterOperation();
        }
        private void ProductNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FilterOperation();
            }
        }
        /// <summary>
        /// Search while typing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductNameTextBox_TextChanged(object sender, EventArgs e)
        {
            var filterType = (FilterCondition)ProductNameFilterComboBox.SelectedItem;
            if (filterType == FilterCondition.Select) return;

            if (ProductNameTextBox.Text.Length > 0)
            {
                _bindingSource.RowFilter("ProductName", ProductNameTextBox.Text, filterType);
            }
            else
            {
                _bindingSource.RowFilterClear();
            }
        }
        private void FilterOperation()
        {
            var filterType = (FilterCondition)ProductNameFilterComboBox.SelectedItem;
            if (filterType == FilterCondition.Select) return;

            if (string.IsNullOrWhiteSpace(ProductNameTextBox.Text))
            {
                _bindingSource.RowFilterClear();
            }
            else
            {
                _bindingSource.RowFilter("ProductName", ProductNameTextBox.Text, filterType);
            }
        }
    }
}
