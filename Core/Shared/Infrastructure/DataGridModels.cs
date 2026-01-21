using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Library.Core.Shared.Infrastructure
{
    public static class DataGridModels
    {
        public static void setItemBooks(DataGrid dataGrid)
        {
            dataGrid.Columns.Clear();
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Id",
                Binding = new Binding("id"),
                Visibility = Visibility.Collapsed
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Name",
                Binding = new Binding("name")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Author",
                Binding = new Binding("author")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Number pages",
                Binding = new Binding("numPages")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Quantity",
                Binding = new Binding("quantity"),
                Width = new DataGridLength(1, DataGridLengthUnitType.Star)
            });
        }
        public static void setItemClients(DataGrid dataGrid)
        {
            dataGrid.Columns.Clear();
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Id",
                Binding = new Binding("id"),
                Visibility = Visibility.Collapsed
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Card ID",
                Binding = new Binding("cardId")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Name",
                Binding = new Binding("name")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Phone",
                Binding = new Binding("phone"),
                Width = new DataGridLength(1, DataGridLengthUnitType.Star)
            });
        }
        public static void setItemLoans(DataGrid dataGrid)
        {
            dataGrid.Columns.Clear();
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Id",
                Binding = new Binding("id"),
                Visibility = Visibility.Collapsed
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Book",
                Binding = new Binding("book.name")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Client",
                Binding = new Binding("client.name")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Date",
                Binding = new Binding("date")
                {
                    StringFormat = "dd/MM/yyyy"
                }
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Deadline", 
                Binding = new Binding("deadline")
                {
                    StringFormat = "dd/MM/yyyy"
                },
                Width = new DataGridLength(1, DataGridLengthUnitType.Star)
            });
        }
    }
}
