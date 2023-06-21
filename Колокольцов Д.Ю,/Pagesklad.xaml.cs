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
    /// Логика взаимодействия для Pagesklad.xaml
    /// </summary>
    public partial class Pagesklad : Page
    {
        public static tkmEntities7 DatatkmEntities { get; set; }

        ObservableCollection<Номенклатура> ListSklad;
        public Pagesklad()
        {
            DatatkmEntities = new tkmEntities7();
            InitializeComponent();
            ListSklad = new ObservableCollection<Номенклатура>();
            var querySklad = from d in DatatkmEntities.Номенклатура
                              orderby d.Код_номенклатуры
                              select d;
            foreach (Номенклатура d in querySklad)
            {
                ListSklad.Add(d);
            }
            DataGridSklad.ItemsSource = ListSklad;
        }
        private bool isDirty = true;
        private void btnx_Click1(object sender, RoutedEventArgs e)
        {
            int maxIDSklad = (from un in DatatkmEntities.Номенклатура
                               select un.Код_номенклатуры).Max();
            Номенклатура номенклатура = new Номенклатура();
            номенклатура.Код_номенклатуры = maxIDSklad + 1;
            DatatkmEntities.Номенклатура.Add(номенклатура);
            ListSklad.Add(номенклатура);
            DataGridSklad.ScrollIntoView(номенклатура);
            DataGridSklad.SelectedIndex = DataGridSklad.Items.Count - 1;
            DataGridSklad.Focus();
            DataGridSklad.IsReadOnly = false;
            DatatkmEntities.SaveChanges();
        }

        private void btnx_Save1(object sender, RoutedEventArgs e)
        {
            DatatkmEntities.SaveChanges();
            isDirty = true;
            DataGridSklad.IsReadOnly = true;
            MessageBox.Show("Сохранено");
        }

        private void btnx_Edit1(object sender, RoutedEventArgs e)
        {
            DataGridSklad.IsReadOnly = false;
            DataGridSklad.BeginEdit();
            isDirty = false;
        }

        private void btnx_Delete1(object sender, RoutedEventArgs e)
        {
           Номенклатура per = DataGridSklad.SelectedItem as Номенклатура;
            if (per != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить ячейку: " + per.Код_номенклатуры,
                 "Warning", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DatatkmEntities.Номенклатура.Remove(per);
                    DataGridSklad.SelectedIndex =
                   DataGridSklad.SelectedIndex == 0 ? 1 :
                    DataGridSklad.SelectedIndex - 1;
                    ListSklad.Remove(per);
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
    }
}
