using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Bridge
{
    class Program
    {
        static void Main(string[] args)
        {
            var customers = new Customers();

            customers.DataObject = new CustomersData { City = "İstanbul" };

            customers.Show();
            customers.Next();
            customers.Show();
            customers.Next();

            customers.Add("Customer 5");
            customers.ShowAll();

            Console.Read();
        }
    }

    interface IDataObject<T>
    {
        void NextRecord();
        void PriorRecord();
        void AddRecord(T t);
        void DeleteRecord(T t);
        T GetCurrentRecord();
        void ShowRecord();
        void ShowAllRecord();
    }

    class CustomerBase
    {
        public IDataObject<string> DataObject { get; set; }

        public virtual void Next()
        {
            DataObject.NextRecord();
        }

        public virtual void Prior()
        {
            DataObject.PriorRecord();
        }

        public virtual void Add(string name)
        {
            DataObject.AddRecord(name);
        }

        public virtual void Delete(string name)
        {
            DataObject.DeleteRecord(name);
        }

        public virtual void Show()
        {
            DataObject.ShowRecord();
        }

        public virtual void ShowAll()
        {
            DataObject.ShowAllRecord();
        }
    }

    class Customers : CustomerBase
    {
        public override void ShowAll()
        {
            Console.WriteLine("-------------------------");
            base.ShowAll();
            Console.WriteLine("-------------------------");
        }
    }

    class CustomersData : IDataObject<string>
    {
        public string City { get; set; }
        private List<string> _customers;
        private int _curent = 0;
        public CustomersData()
        {
            _customers = new List<string>
            {
                "Customer 1",
                "Customer 2",
                "Customer 3",
                "Customer 4"
            };
        }
        public void AddRecord(string t)
        {
            _customers.Add(t);
        }

        public void DeleteRecord(string t)
        {
            _customers.Remove(t);
        }

        public string GetCurrentRecord()
        {
            return _customers[_curent];
        }

        public void NextRecord()
        {
            if (_curent <= _customers.Count - 1)
            {
                _curent++;
            }
        }

        public void PriorRecord()
        {
            if (_curent > 0)
            {
                _curent--;
            }
        }

        public void ShowAllRecord()
        {
            Console.WriteLine("Customers: " + City);
            _customers.ForEach(c => Console.WriteLine(" " + c));
        }

        public void ShowRecord()
        {
            Console.WriteLine("Customer {0}", _customers[_curent]);
        }
    }
}
