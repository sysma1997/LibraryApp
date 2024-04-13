using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Library
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            migrateDatabase();
        }

        public async void migrateDatabase()
        {
            Models.DatabaseContext db = new Models.DatabaseContext();
            await db.Database.MigrateAsync();
        }
    }
}
