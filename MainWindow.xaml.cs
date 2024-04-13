using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library
{
    enum MainWindowViews { BOOKS, CLIENTS, LOANS }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Models.DatabaseContext db = new Models.DatabaseContext();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void setView(MainWindowViews view)
        {
            //
        }

        private void btnBooksShow(object sender, RoutedEventArgs e)
        {
        }
        private void btnClientsShow(object sender, RoutedEventArgs e)
        {

        }
        private void btnLoansShow(object sender, RoutedEventArgs e)
        {

        }
    }
}
