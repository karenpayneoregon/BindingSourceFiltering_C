using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Operations;
using ExtensionLibrary;
using System.Windows.Forms;

using static ExtensionLibrary.BindingSourceExtensions;

namespace FilteringTest
{
    /// <summary>
    /// This test class sole purpose is to test functionality
    /// prior to placing them in use within a application.
    /// 
    /// Let's say you want specific functionality such
    /// as a field starts with in a DataTable which is the
    /// data source of a BindingSource with the capability to
    /// do case sensitive and case insensitive filtering.
    /// We write the code, here the final code is in the Extensions
    /// library as language extensions. We create test such
    /// as below and while doing so run a SQL SELECT statement say
    /// in  SQL-Server Management Studio, get the record count
    /// and validate that count against the count returned by
    /// the extension method. If they don't match then your code 
    /// would need to alter to match to what you want.
    /// Once you have validated this they can be put to use in
    /// your application.
    /// </summary>
    [TestClass]
    public class FilterTest
    {

        private readonly BindingSource _bindingSource = new BindingSource();
        private DataOperations _dataOperations;
        private DataTable _customersDataTable;

        [TestInitialize]
        public void Init()
        {

            _dataOperations = new DataOperations();
            _customersDataTable = _dataOperations.GetCustomers();
            _bindingSource.DataSource = _customersDataTable;

        }
        /// <summary>
        /// Get all rows
        /// </summary>
        [TestMethod]
        public void ReturnAllRowsNoFiltering()
        {
            Assert.IsTrue(_customersDataTable.Rows.Count == 93,
                "Incorrect row count");            
        }
        /// <summary>
        /// Test case sensitive filter
        /// </summary>
        [TestMethod]
        public void RowFilter_EqualsCaseSensitive()
        {
            // arrange - done in Init method

            // act
            _bindingSource.RowFilter("ContactTitle", "Owner", true);

            // assert
            Assert.IsTrue(_bindingSource.Count == 19, 
                "Expected 19 records");

        }
        /// <summary>
        /// Test case sensitive filter, in this case there are no matches
        /// as all titles begin with the first character as upper case.
        /// </summary>
        [TestMethod]
        public void RowFilter_EqualsCaseSensitive_DataDoesNotMeetCondition()
        {
            // arrange
            var bindingSource = new BindingSource();
            var dataOperations = new DataOperations();

            bindingSource.DataSource = dataOperations.GetCustomers();

            // act
            bindingSource.RowFilter("ContactTitle", "owner", true);

            // assert
            Assert.IsTrue(bindingSource.Count == 0,
                "Expected 0 records");
        }
        /// <summary>
        /// This test will validate a new view can be created 
        /// along with a direct view on the data.
        /// </summary>
        [TestMethod]
        public void RowFilter_EqualsCaseSensitiveUsingNewView()
        {
            // arrange
            var bindingSource = new BindingSource();
            var dataOperations = new DataOperations();

            bindingSource.DataSource = dataOperations.GetCustomers();

            // act
            var newView = bindingSource.RowFilterNewView("ContactTitle", "Owner", true);

            // assert 1
            Assert.IsTrue(bindingSource.Count == 19,
                "Expected 19 records");

            // act 2
            bindingSource.RowFilter("ContactTitle", "Order Administrator", true);

            // assert 2
            Assert.IsTrue(bindingSource.Count == 2,
                "Expected 2 records");

        }

        /// <summary>
        /// Test to ensure the extension method HasData functions properly
        /// </summary>
        [TestMethod]
        public void BindingSource_HasData()
        {
            // arrange
            var bindingSource = new BindingSource();
            var dataOperations = new DataOperations();

            // act
            bindingSource.DataSource = dataOperations.GetCustomers();

            // assert
            Assert.IsTrue(bindingSource.HasData(), 
                "Expected data");
        }
        /// <summary>
        /// Expects the extension method HasData functions with no data
        /// </summary>
        [TestMethod]
        public void BindingSource_Has_No_Data()
        {
            var bindingSource = new BindingSource();

            Assert.IsFalse(bindingSource.HasData(), 
                "Expected  no data");

        }
        /// <summary>
        /// Get all contact names starting with An case sensitive
        /// </summary>
        [TestMethod]
        public void ContactNameStartsWith_CaseSensitive_Good()
        {
            // arrange
            var bindingSource = new BindingSource();
            var dataOperations = new DataOperations();

            bindingSource.DataSource = dataOperations.GetCustomers();

            // act
            bindingSource.RowFilterStartsWith("ContactName", "An", true);           

            // assert
            Assert.IsTrue(bindingSource.Count == 6, 
                "Expected six records");
        }
        /// <summary>
        /// Get all contact names starting with An case sensitive using an overload of RowFilter
        /// </summary>
        [TestMethod]
        public void ContactNameStartsWith_CaseSensitive_OverLoad_Good()
        {
            // arrange
            var bindingSource = new BindingSource();
            var dataOperations = new DataOperations();

            bindingSource.DataSource = dataOperations.GetCustomers();

            // act
            bindingSource.RowFilter("ContactName", "An", FilterCondition.StartsWith, true);

            // assert
            Assert.IsTrue(bindingSource.Count == 6, 
                "Expected six records");
        }
        /// <summary>
        /// Get all contact names starting with An case sensitive where
        /// in this case there are no records to match because we have
        /// the last parameter as true.
        /// </summary>
        [TestMethod]
        public void ContactNameStartsWith_CaseSensitive_Bad()
        {
            // arrange
            var bindingSource = new BindingSource();
            var dataOperations = new DataOperations();

            bindingSource.DataSource = dataOperations.GetCustomers();

            // act
            bindingSource.RowFilterStartsWith("ContactName", "an",true);
            
            // assert
            Assert.IsTrue(bindingSource.Count == 0, 
                "Expected zero records");
        }
        /// <summary>
        /// Get all contact names that contain exactly Ro
        /// </summary>
        [TestMethod]
        public void ContactNameContains_CaseSensitive_Good()
        {
            // arrange
            var bindingSource = new BindingSource();
            var dataOperations = new DataOperations();

            bindingSource.DataSource = dataOperations.GetCustomers();

            // act
            bindingSource.RowFilterContains("ContactName", "Ro", true);

            // assert
            Assert.IsTrue(bindingSource.Count == 5, 
                "Expected five records");
        }
        /// <summary>
        /// Get all contact names that contain exactly Ro using RowFilter overload
        /// </summary>
        [TestMethod]
        public void ContactNameContains_CaseSensitive_OverLoad_Good()
        {
            // arrange
            var bindingSource = new BindingSource();
            var dataOperations = new DataOperations();

            bindingSource.DataSource = dataOperations.GetCustomers();

            // act
            bindingSource.RowFilter("ContactName", "Ro", FilterCondition.Contains, true);

            // assert
            Assert.IsTrue(bindingSource.Count == 5, 
                "Expected five records");
        }
        /// <summary>
        /// Get all contact names that contain ro where case sensitive will
        /// return a different count then above.
        /// </summary>
        [TestMethod]
        public void ContactNameContains_CaseSensitive_Bad()
        {
            // arrange
            var bindingSource = new BindingSource();
            var dataOperations = new DataOperations();

            bindingSource.DataSource = dataOperations.GetCustomers();

            // act
            bindingSource.RowFilterContains("ContactName", "ro", true);

            // assert
            Assert.IsTrue(bindingSource.Count == 7, 
                "Expected 7 records");
        }
        [TestMethod]
        public void ContactTitleEndsWith_CaseSensitive_Good()
        {
            // arrange
            var bindingSource = new BindingSource();
            var dataOperations = new DataOperations();

            bindingSource.DataSource = dataOperations.GetCustomers();

            // act
            bindingSource.RowFilterEndsWith("ContactTitle", "Manager", true);

            // assert
            Assert.IsTrue(bindingSource.Count == 33, 
                "Expected 33 records");
        }
        [TestMethod]
        public void ContactTitleEndsWith_CaseSensitive_With_ApostropheEmbedded_Good()
        {
            // arrange
            var bindingSource = new BindingSource();
            var dataOperations = new DataOperations();

            bindingSource.DataSource = dataOperations.GetCustomers();

            // act
            bindingSource.RowFilterEndsWith("CompanyName", "Bon app'", true);

            // assert
            Assert.IsTrue(bindingSource.Count == 1,
                "Expected 33 records");
        }
        [TestMethod]
        public void ContactTitleEndsWith_CaseSensitive_OverLoad_Good()
        {
            // arrange
            var bindingSource = new BindingSource();
            var dataOperations = new DataOperations();

            bindingSource.DataSource = dataOperations.GetCustomers();

            // act
            bindingSource.RowFilter("ContactTitle", "Manager", FilterCondition.EndsWith, true);

            // assert
            Assert.IsTrue(bindingSource.Count == 33, 
                "Expected 33 records");
        }
        [TestMethod]
        public void ContactTitleEndsWith_CaseSensitive_Bad()
        {
            // arrange
            var bindingSource = new BindingSource();
            var dataOperations = new DataOperations();

            bindingSource.DataSource = dataOperations.GetCustomers();

            // act
            bindingSource.RowFilterEndsWith("ContactTitle", "manager", true);

            // assert
            Assert.IsTrue(bindingSource.Count == 0, 
                "Expected 0 records");
        }
        [TestMethod]
        public void FreeForm_ContactTitle_CaseSensitive_OnBoth_Conditions()
        {
            // arrange
            var bindingSource = new BindingSource();
            var dataOperations = new DataOperations();

            bindingSource.DataSource = dataOperations.GetCustomers();

            // act
            bindingSource.RowFilterFreeForm(
                "ContactTitle LIKE '%Manager' OR ContactTitle LIKE 'Sales%'", true);

            // assert
            Assert.IsTrue(bindingSource.Count == 62, 
                "Expected 62 records");
        }
        [TestMethod]
        public void FreeForm_County_CaseSensitive_OnAll()
        {
            // arrange
            var bindingSource = new BindingSource();
            var dataOperations = new DataOperations();

            bindingSource.DataSource = dataOperations.GetCustomers();

            // act
            bindingSource.RowFilterFreeForm("Country IN ('Argentina', 'Canada', 'UK')", true);

            // assert
            Assert.IsTrue(bindingSource.Count == 13, 
                "Expected 13 records");
        }
        [TestMethod]
        public void FreeForm_CaseSensitive_OnAll_Bad()
        {
            // arrange
            var bindingSource = new BindingSource();
            var dataOperations = new DataOperations();

            bindingSource.DataSource = dataOperations.GetCustomers();

            // act - Canada is missing the ending 'a'
            bindingSource.RowFilterFreeForm("Country IN ('Argentina', 'Canad', 'UK')", true);

            // assert
            Assert.IsTrue(bindingSource.Count == 10, 
                "Expected 10 records");
        }
        [TestMethod]
        public void FreeForm_CaseSensitive_OnBoth_Conditions_LastField_NotExact()
        {
            // arrange
            var bindingSource = new BindingSource();
            var dataOperations = new DataOperations();

            bindingSource.DataSource = dataOperations.GetCustomers();

            // act
            bindingSource.RowFilterFreeForm(
                "ContactTitle LIKE '%Manager' OR ContactTitle LIKE 'sales%'", true);

            // assert
            Assert.IsTrue(bindingSource.Count == 33, "Expected 33 records");
        }
        [TestMethod]
        public void FreeForm_CaseSensitive_OnBoth_Conditions_LastField_CaseSwitchFalse()
        {
            // arrange
            var bindingSource = new BindingSource();
            var dataOperations = new DataOperations();

            bindingSource.DataSource = dataOperations.GetCustomers();

            // act
            bindingSource.RowFilterFreeForm(
                "ContactTitle LIKE '%Manager' OR ContactTitle LIKE 'sales%'", false);

            Console.WriteLine(bindingSource.Count);
            // assert
            Assert.IsTrue(bindingSource.Count == 62, "Expected 62 records");
        }
    }
}
