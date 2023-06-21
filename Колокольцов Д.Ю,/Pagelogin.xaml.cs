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
    /// Логика взаимодействия для Pagelogin.xaml
    /// </summary>
    public partial class Pagelogin : Page
    {
        public static tkmEntities7 DatatkmEntities { get; set; }
        public Pagelogin()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool valid = false;
            string userType = "";
            tkmEntities7 dbtkm = new tkmEntities7();
            using (dbtkm) 
            {
                var user = dbtkm.tpUser;
                foreach (tpUser u in user)
                {
                    if (tbxlogin.Text == u.login & tbxpassword.Password == u.password)
                    {
                        valid = true;
                        userType = u.type;
                        
                    }
                }
            }
            if (valid == false)
            {
                MessageBox.Show("Неверный логин или пароль");
            }
            if (userType == "Менеджер")
            {
                MessageBox.Show("Добро пожаловать" + "," + userType);
                NavigationService.Navigate(new Uri("PageManager.xaml", UriKind.Relative));
            }

            if (userType == "Мастер")
            {
                MessageBox.Show("Добро пожаловать" + "," + userType);
                NavigationService.Navigate(new Uri("PageZayavka.xaml", UriKind.Relative));
            }
           
            if (userType == "Админ")
            {
                MessageBox.Show("Добро пожаловать" + "," + userType);
                NavigationService.Navigate(new Uri("Pageadmin.xaml", UriKind.Relative));
            }
      





        }
    }
}
