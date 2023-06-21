using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для Pageadmin.xaml
    /// </summary>
    public partial class Pageadmin : Page
    {
        public static tkmEntities7 DatatkmEntities { get; set; }

        ObservableCollection<tpUser> ListUser;
        public Pageadmin()
        {
            DatatkmEntities = new tkmEntities7();
            InitializeComponent();
            ListUser = new ObservableCollection<tpUser>();
            var queryClient = from d in DatatkmEntities.tpUser
                              orderby d.ID_User
                              select d;
            foreach (tpUser d in queryClient)
            {
                ListUser.Add(d);
            }
            DataGridAdmin.ItemsSource = ListUser;
        }
        private bool isDirty = true;
        private void btnx_Click1(object sender, RoutedEventArgs e)
        {
            int maxIDClient = (from un in DatatkmEntities.tpUser
                               select un.ID_User).Max();
            tpUser user = new tpUser();
            user.ID_User = maxIDClient + 1;
            DatatkmEntities.tpUser.Add(user);
            ListUser.Add(user);
            DataGridAdmin.ScrollIntoView(user);
            DataGridAdmin.SelectedIndex = DataGridAdmin.Items.Count - 1;
            DataGridAdmin.Focus();
            DataGridAdmin.IsReadOnly = false;
            DatatkmEntities.SaveChanges();
        }

        private void btnx_Save1(object sender, RoutedEventArgs e)
        {
            DatatkmEntities.SaveChanges();
            isDirty = true;
            DataGridAdmin.IsReadOnly = true;
            MessageBox.Show("Сохранено");
        }

        private void btnx_Edit1(object sender, RoutedEventArgs e)
        {
            DataGridAdmin.IsReadOnly = false;
            DataGridAdmin.BeginEdit();
            isDirty = false;
        }

        private void btnx_Delete1(object sender, RoutedEventArgs e)
        {
            tpUser per = DataGridAdmin.SelectedItem as tpUser;
            if (per != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить ячейку: " + per.ID_User,
                 "Warning", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DatatkmEntities.tpUser.Remove(per);
                    DataGridAdmin.SelectedIndex =
                   DataGridAdmin.SelectedIndex == 0 ? 1 :
                    DataGridAdmin.SelectedIndex - 1;
                    ListUser.Remove(per);
                    DatatkmEntities.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Выберите ячейку для удаления");
            }
            isDirty = false;
        }

        private void btnx_Back1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pagelogin.xaml", UriKind.Relative));
        }
    }
}
