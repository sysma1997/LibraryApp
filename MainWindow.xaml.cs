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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Models.DatabaseContext db = new Models.DatabaseContext();
        private CollectionViewSource libraryViewSource;

        public MainWindow()
        {
            InitializeComponent();

            libraryViewSource = (CollectionViewSource)FindResource(nameof(libraryViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db.Database.EnsureCreated();
            db.Libraries.Load();
            libraryViewSource.Source = db.Libraries.Local.ToObservableCollection();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }

        private void btnSave(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
            dgLibrary.Items.Refresh();
        }
    }
}
