using DataBaseExplorer.Context;
using DataBaseExplorer.HelperClasses;
using DataBaseExplorer.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataBaseExplorer.ViewModels
{
    public class ConnectDbVM : INotifyPropertyChanged
    {
        private string serverDatabase;
        private string nameDatabase;
        private string status;



        public string ServerDatabase
        {
            get { return serverDatabase; }
            set
            {
                serverDatabase = value;
                OnPropertyChanged();
            }
        }
        public string NameDatabase
        {
            get { return nameDatabase; }
            set
            {
                nameDatabase = value;
                OnPropertyChanged();
            }
        }

        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }
        public ICommand ConnectWithDb { get; set; }
        public ConnectDbVM()
        {
            ConnectWithDb = new RelayAsyncCommand(async () => await connectionWitDataBase());
        }

        private async Task connectionWitDataBase()
        {
            var status = new Progress<string>(value => Status = value);
            if (string.IsNullOrEmpty(NameDatabase))
            {
                Status = "Database name is not valid";
                return;
            }
            if (string.IsNullOrEmpty(ServerDatabase))
            {
                Status = "Server name is not valid";
                return;
            }
            ((IProgress<string>)status).Report("Connection...");
            await Task.Run(() =>
            {
                ContextDb context = new ContextDb($"Server={ServerDatabase};Database={NameDatabase};Trusted_Connection=True;MultipleActiveResultSets=true;");
                if (context.Customers.Count() == 0)
                {
                    ((IProgress<string>)status).Report("Database not found");
                    return;
                }
                ((IProgress<string>)status).Report("Connection Success");
                context.Dispose();
            });
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
