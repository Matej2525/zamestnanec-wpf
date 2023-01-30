using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

namespace formular_zamestnanec_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class Person
    {
        protected string name, surname;
        protected string dateOfBirth;
        protected bool elementarySchool, middleSchool, highSchool;
        protected string school;
    }

    public class Employee : Person, INotifyPropertyChanged
    {
        string work, money;
        public string School
        {
            get => school;
            set
            {
                school = Topschool();
            }
        }

        public static List<Employee> AllEmployes { get; set; } = new List<Employee>();

        public event PropertyChangedEventHandler PropertyChanged;

        private Visibility warningName;
        private Visibility warningSurname;
        private Visibility warningDateOfBirth;
        private Visibility warningschool;
        private Visibility warningWork;
        private Visibility warningMoney;

        public Visibility WarningName
        {
            get => warningName;
            set
            {
                warningName = value;
                OnPropertyChanged("WarningName");
            }
        }

        public Visibility WarningSurname
        {
            get => warningSurname;
            set
            {
                warningSurname = value;
                OnPropertyChanged("WarningSurname");
            }
        }

        public Visibility WarningDateOfBirth
        {
            get => warningDateOfBirth;
            set
            {
                warningDateOfBirth = value;
                OnPropertyChanged("WarningDateOfBirth");
            }
        }

        public Visibility WarningSchool
        {
            get => warningschool;
            set
            {
                warningschool = value;
                OnPropertyChanged("WarningSchool");
            }
        }

        public Visibility WarningWork
        {
            get => warningWork;
            set
            {
                warningWork = value;
                OnPropertyChanged("WarningWork");
            }
        }

        public Visibility WarningMoney
        {
            get => warningMoney;
            set
            {
                warningMoney = value;
                OnPropertyChanged("WarningMoney");
            }
        }


        public string Name
        {
            get => name;
            set
            {
                if (value.Length <= 0)
                {
                    name = value;
                    WarningName = Visibility.Visible;
                }
                else
                {
                    name = value;
                    OnPropertyChanged("Name");
                    OnPropertyChanged("Status");
                    WarningName = Visibility.Hidden;
                }
            }
        }

        public string Surname
        {
            get => surname;
            set
            {
                if (value.Length <= 0)
                {
                    surname = value;
                    WarningSurname = Visibility.Visible;
                }
                else
                {
                    surname = value;
                    OnPropertyChanged("Surname");
                    OnPropertyChanged("Status");
                    WarningSurname = Visibility.Hidden;
                }
            }
        }

        public string DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                DateTime time;
                if (DateTime.TryParse(value, out time))
                {
                    dateOfBirth = value;
                    WarningDateOfBirth = Visibility.Visible;
                }
                if (time == DateTime.Now)
                {
                    dateOfBirth = value;
                    WarningDateOfBirth = Visibility.Visible;
                }
                else
                {
                    dateOfBirth = value;
                    OnPropertyChanged("DateOfBirth");
                    OnPropertyChanged("Status");
                    WarningDateOfBirth = Visibility.Hidden;
                }

            }
        }

        public string Work
        {
            get => work;
            set
            {
                if (value.Length <= 0)
                {
                    work = string.Empty;
                    WarningWork = Visibility.Visible;
                }
                else
                {
                    work = value;
                    OnPropertyChanged("Work");
                    OnPropertyChanged("Status");
                    WarningWork = Visibility.Hidden;
                }

            }
        }
        public string Money
        {
            get => money;
            set
            {
                int moneyNum;
                if (value.Length <= 0)
                {
                    money = string.Empty;
                    WarningMoney = Visibility.Visible;
                }
                else if (int.TryParse(value, out moneyNum))
                {
                    money = value;
                    OnPropertyChanged("Money");
                    OnPropertyChanged("Status");
                    WarningMoney = Visibility.Hidden;
                }
                else
                {
                    money = value;
                    WarningMoney = Visibility.Visible;
                }
            }
        }

        public bool ElementarySchool
        {
            get => elementarySchool;
            set
            {
                elementarySchool = value;
                OnPropertyChanged("ElementarySchool");
                OnPropertyChanged("Status");
            }
        }

        public bool MiddleSchool
        {
            get => middleSchool;
            set
            {
                middleSchool = value;
                OnPropertyChanged("MiddleSchool");
                OnPropertyChanged("Status");
            }
        }

        public bool HighSchool
        {
            get => highSchool;
            set
            {
                highSchool = value;
                OnPropertyChanged("HighSchool");
                OnPropertyChanged("Status");
            }
        }

        public string Topschool()
        {
            if (HighSchool)
            {
                return "Vysoká Škola";
            }
            else if (MiddleSchool)
            {
                return "Střední Škola";
            }
            else if (ElementarySchool)
            {
                return "Základní Škola";
            }
            return "Lopata";
        }

        public string Status
        {
            get => "Jméno: " + Name + " Příjmení: " + Surname + " Narození: " + DateOfBirth + " Vzdělání: " + Topschool() + " Práce: " + Work + " Příjem: " + Money;
        }

        public override string ToString()
        {
            return $"{Name}|{Surname}|{DateOfBirth}|{Topschool()}|{Work}|{Money}@";
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public static Employee EmployeeCopy(Employee employee)
        {
            Employee newEmployee = new Employee() { Name = employee.Name, surname = employee.Surname, DateOfBirth = employee.DateOfBirth, School = employee.School, Work = employee.Work, Money = employee.Money };
            return newEmployee;
        }

    }
    public partial class MainWindow : Window
    {
        Employee employee;
        public MainWindow()
        {
            InitializeComponent();
            DataContext =
           employee = new Employee()
           {
               DateOfBirth = DateTime.Now.ToString(),
               WarningName = Visibility.Hidden,
               WarningSurname = Visibility.Hidden,
               WarningDateOfBirth = Visibility.Hidden,
               WarningSchool = Visibility.Hidden,
               WarningWork = Visibility.Hidden,
               WarningMoney = Visibility.Hidden,
               ElementarySchool = true

           };
        }

        private void btSend_Click(object sender, RoutedEventArgs e)
        {

            if (employee.Name != null && employee.Surname != null && employee.Money != null && employee.Work != null)
            {

                Employee.AllEmployes.Add(Employee.EmployeeCopy(employee));
                lv.ItemsSource = null;
                lv.ItemsSource = Employee.AllEmployes;

                const string path = @"data.txt";
                if (!File.Exists(path))
                {
                    File.Create(path);
                }

                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(employee.ToString());
                }

                Cleardata();
            }
            else if (employee.Name == null)
            {
                employee.WarningName = Visibility.Visible;
            }
            else if (employee.Surname == null)
            {
                employee.WarningSurname = Visibility.Visible;
            }
            else if (employee.Money == null)
            {
                employee.WarningMoney = Visibility.Visible;
            }
            else if (employee.Work == null)
            {
                employee.WarningWork = Visibility.Visible;
            }
        }

        public void Cleardata()
        {
            employee.Name = employee.Surname = employee.Money = employee.Work = " ";
            employee.DateOfBirth = DateTime.Now.ToString();
            employee.ElementarySchool = employee.MiddleSchool = employee.HighSchool = false;
            employee.WarningName = Visibility.Hidden;
            employee.WarningSurname = Visibility.Hidden;
            employee.WarningDateOfBirth = Visibility.Hidden;
            employee.WarningSchool = Visibility.Hidden;
            employee.WarningWork = Visibility.Hidden;
            employee.WarningMoney = Visibility.Hidden;
        }

        private void btClear_Click(object sender, RoutedEventArgs e)
        {
            Cleardata();
        }
    }
}
        
    

