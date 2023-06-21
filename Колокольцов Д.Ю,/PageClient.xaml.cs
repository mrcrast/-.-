using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
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
    /// Логика взаимодействия для PageClient.xaml
    /// </summary>
    public partial class PageClient : Page
    {
        string find = "";
        
        public static tkmEntities7 DatatkmEntities { get; set; }

        ObservableCollection<Клиент> ListClient;
        public PageClient()
        {
            DatatkmEntities = new tkmEntities7();
            InitializeComponent();
            ListClient = new ObservableCollection<Клиент>();
            var queryClient = from d in DatatkmEntities.Клиент
                              orderby d.Код_клиента
                              select d;
            foreach (Клиент d in queryClient)
            {
                ListClient.Add(d);
            }
            DataGridClient.ItemsSource = ListClient;
        }
        private bool isDirty = true;
        private void btnx_Click1(object sender, RoutedEventArgs e)
        {
            int maxIDClient = (from un in DatatkmEntities.Клиент
                               select un.Код_клиента).Max();
            Клиент клиент = new Клиент();
            клиент.Код_клиента = maxIDClient + 1;
            DatatkmEntities.Клиент.Add(клиент);
            ListClient.Add(клиент);
            DataGridClient.ScrollIntoView(клиент);
            DataGridClient.SelectedIndex = DataGridClient.Items.Count - 1;
            DataGridClient.Focus();
            DataGridClient.IsReadOnly = false;
            DatatkmEntities.SaveChanges();
        }

        private void btnx_Save1(object sender, RoutedEventArgs e)
        {
            DatatkmEntities.SaveChanges();
            isDirty = true;
            DataGridClient.IsReadOnly = true;
            MessageBox.Show("Сохранено");
        }

        private void btnx_Edit1(object sender, RoutedEventArgs e)
        {
            DataGridClient.IsReadOnly = false;
            DataGridClient.BeginEdit();
            isDirty = false;
        }

        private void btnx_Delete1(object sender, RoutedEventArgs e)
        {
            Клиент per = DataGridClient.SelectedItem as Клиент;
            if (per != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить ячейку: " + per.Код_клиента,
                 "Warning", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DatatkmEntities.Клиент.Remove(per);
                    DataGridClient.SelectedIndex =
                    DataGridClient.SelectedIndex == 0 ? 1 :
                    DataGridClient.SelectedIndex - 1;
                    ListClient.Remove(per);
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
            NavigationService.Navigate(new Uri("PageManager.xaml", UriKind.Relative));
        }
        private void RewriteFind()
        {
            DatatkmEntities = new tkmEntities7();
            ListClient.Clear();
            Find();
        }
        private void Find()
        {
            var queryPerson = from d in PageClient.DatatkmEntities.Клиент
                              orderby d.Код_клиента
                              select d;

            foreach (Клиент d in queryPerson)
            {
                ListClient.Add(d);
            }
            DataGridClient.ItemsSource = ListClient;

        }

      

        private void ButtonFind_Click(object sender, RoutedEventArgs e)
        {
            int find1;
            find = Convert.ToString(tbxfind.Text);


            if (find == "")
            {
                RewriteFind();
            }
            else
            {
                find1 = Convert.ToInt32(find);
                var queryFind = from f in DatatkmEntities.Клиент
                                where find1 == f.Код_клиента
                                select f;
                ListClient.Clear();
                foreach (Клиент f in queryFind)
                {
                    ListClient.Add(f);
                }
                DataGridClient.ItemsSource = ListClient;
                DataGridClient.UpdateLayout();
            }
                

            


        }
    }
}

