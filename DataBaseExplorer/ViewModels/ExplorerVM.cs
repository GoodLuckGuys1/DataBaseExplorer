using DataBaseExplorer.Context;
using DataBaseExplorer.Models;
using DataBaseExplorer.Repositories;
using DataBaseExplorer.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace DataBaseExplorer.ViewModels
{
    public class ExplorerVM : INotifyPropertyChanged
    {
        private ContextDb contextDb;
        private List<Order> allOrders;
        private string lastName;
        private string firstName;
        private string middleName;
        private string sex;
        private DateTime birthDate;
        private DateTime registrationDate;
        private Customer selectedCustomer;
        private DateTime orderDate;
        private decimal price;
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                OnPropertyChanged();
            }
        }
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                OnPropertyChanged();
            }
        }
        public string MiddleName
        {
            get
            {
                return middleName;
            }
            set
            {
                middleName = value;
                OnPropertyChanged();
            }
        }
        public string Sex
        {
            get
            {
                return sex;
            }
            set
            {
                sex = value;
                OnPropertyChanged();
            }
        }
        private bool? _dialogResult;

        public bool? DialogResult
        {
            get { return _dialogResult; }
            protected set
            {
                _dialogResult = value;
                OnPropertyChanged();
            }
        }
        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }
        public DateTime OrderDate
        {
            get
            {
                return orderDate;
            }
            set
            {
                orderDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime BirthDate
        {
            get
            {
                return birthDate;
            }
            set
            {
                birthDate = value;
                OnPropertyChanged();
            }
        }
        public Customer SelectedCustomer
        {
            get
            {
                return selectedCustomer;
            }
            set
            {
                selectedCustomer = value;
                Orders = new ObservableCollection<Order>(allOrders.Where(x => x.Customer == SelectedCustomer));
                OnPropertyChanged();
            }
        }
        public DateTime RegistrationDate
        {
            get
            {
                return registrationDate;
            }
            set
            {
                registrationDate = value;
                OnPropertyChanged();
            }
        }
        public ExplorerVM()
        {
            ConnectDb connectDb = new ConnectDb();
            ConnectDbVM connectDbVM = (ConnectDbVM)connectDb.DataContext;
            connectDb.ShowDialog();
            if (connectDbVM.IsSuccessConnect == false)
            {
                DialogResult = false;
                return;
            }
            contextDb = new ContextDb($"Server={connectDbVM.ServerDatabase};Database={connectDbVM.NameDatabase};Trusted_Connection=False;MultipleActiveResultSets=true;");
            CustomerRepository customerRepository = new CustomerRepository(contextDb);
            OrderRepository orderRepository = new OrderRepository(contextDb);
            Customers = customerRepository.GetCustomers();
            allOrders = orderRepository.GetOrders();
            Orders = new ObservableCollection<Order>(allOrders.Where(x => x.Customer == Customers.ToList().FirstOrDefault()));
        }

        private ObservableCollection<Customer> customers;
        public ObservableCollection<Customer> Customers
        {
            get
            {
                return customers;
            }
            set
            {
                customers = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Order> order;
        public ObservableCollection<Order> Orders
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
