# Windows Forms BindingSource filter extension methods

[TechNet article](https://social.technet.microsoft.com/wiki/contents/articles/53413.bindingsource-filter-with-starts-contains-ends-with-and-case-sensitive-options.aspx)

This code sample focuses on filtering a BindingSource component Jump where its data source is a DataTable Jump rather than filtering from the Filter property of a BindingSource. The reason is that many developers writing window form applications use a BindingSource in tangent with a TableAdapter Jump or simply using a DataSet Jump or DataTable and need case or case insensitive capabilities for filter where the BindingSource component filter does not have the ability to filter with case insensitive casing.

In this article a language extension Jump library which has extensions for a BindingSource with a DataSource set to a DataTable Jump will show how to make filter using LIKE conditions easy. There are methods focus on LIKE conditions for starts-with, ends-with and contains with one for a general equal, all provide an option for casing.
