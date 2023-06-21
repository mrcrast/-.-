using Diplom;
using Diplom.ApplicationData;
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
using Колокольцов_Д.Ю_.ApplicationData;

namespace Колокольцов_Д.Ю_
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppFrame.FrameMain = FrmMain;
            FrmMain.Navigate(new Pagelogin());
            AppConnect.model0db = new tkmEntities7();
        }
    }
}
