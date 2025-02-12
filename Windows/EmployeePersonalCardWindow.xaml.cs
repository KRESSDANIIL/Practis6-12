using Practis6_12.Models;
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
using System.Windows.Shapes;

namespace Practis6_12.Windows
{
    /// <summary>
    /// Логика взаимодействия для EmployeePersonalCardWindow.xaml
    /// </summary>
    public partial class EmployeePersonalCardWindow : Window
    {

        public DispatcherDisplayModel Dispatcher { get; set; }

        public EmployeePersonalCardWindow(DispatcherDisplayModel dispatcher)
        {
            InitializeComponent();

            Dispatcher = dispatcher;

            Sus.Content = Dispatcher.FullName;

        }
    }
}
