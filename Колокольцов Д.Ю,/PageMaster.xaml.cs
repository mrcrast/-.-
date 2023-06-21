using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Objects;
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
    /// Логика взаимодействия для PageMaster.xaml
    /// </summary>
    public partial class PageMaster : Page
    {

        public class MasterOrderInfo
        {
            public string name { get; set; }
            public int kolvo { get; set; }
        }
        public static tkmEntities7 DatatkmEntities { get; set; }

        ObservableCollection<Заказ_номенклатура> ListMaster;
        public PageMaster()
        {
            DatatkmEntities = new tkmEntities7();
            InitializeComponent();
            ListMaster = new ObservableCollection<Заказ_номенклатура>();
            var queryMaster = from d in DatatkmEntities.Заказ_номенклатура
                              orderby d.Код_заказ_номенклатура
                              join x in DatatkmEntities.Номенклатура on d.Код_номенклатуры equals x.Код_номенклатуры
                              select d;

            foreach (Заказ_номенклатура d in queryMaster)
            {
                if (d.Количество_для_изготовления == 0) { }
                else
                {
                    ListMaster.Add(d);
                }
            }



            DataGridMaster.ItemsSource = ListMaster;
        }

        private void RewriteFind()
        {
            DatatkmEntities = new tkmEntities7();
            ListMaster.Clear();
            Find();
        }
        private void Find()
        {
            var queryPerson = from d in PageMaster.DatatkmEntities.Заказ_номенклатура
                              orderby d.Код_заказ_номенклатура
                              select d;

            foreach (Заказ_номенклатура d in queryPerson)
            {
                if (d.Количество_для_изготовления == 0) { }
                else
                {
                    ListMaster.Add(d);
                }
            }
            DataGridMaster.ItemsSource = ListMaster;

        }



        private void ButtonFind_Click(object sender, RoutedEventArgs e)
        {
            int find1;
            string find = "";
            find = Convert.ToString(tbxfind.Text);


            if (find == "")
            {
                RewriteFind();
            }
            else
            {
                find1 = Convert.ToInt32(find);
                var queryFind = from f in DatatkmEntities.Заказ_номенклатура
                                where find1 == f.Код_Заказа
                                select f;
                ListMaster.Clear();
                foreach (Заказ_номенклатура f in queryFind)
                {

                    if (f.Количество_для_изготовления == 0) { }
                    else
                    {
                        ListMaster.Add(f);
                    }
                }
                DataGridMaster.ItemsSource = ListMaster;
                DataGridMaster.UpdateLayout();
            }
        }

        private bool isDirty = true;

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            Заказ_номенклатура del = DataGridMaster.SelectedItem as Заказ_номенклатура;
            if (del != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить ячейку: " + del.Код_Заказа,
                 "Warning", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DatatkmEntities.Заказ_номенклатура.Remove(del);
                    DataGridMaster.SelectedIndex =
                    DataGridMaster.SelectedIndex == 0 ? 1 :
                    DataGridMaster.SelectedIndex - 1;
                    ListMaster.Remove(del);
                    DatatkmEntities.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Выберите ячейку для удаления");
            }
            isDirty = false;
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            DatatkmEntities.SaveChanges();
            isDirty = true;
            DataGridMaster.IsReadOnly = true;
            MessageBox.Show("Сохранено");
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("PageManager.xaml", UriKind.Relative));

        }
    }
}