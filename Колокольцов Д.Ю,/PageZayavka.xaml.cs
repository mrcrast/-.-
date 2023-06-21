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
    /// Логика взаимодействия для PageZayavka.xaml
    /// </summary>
    public partial class PageZayavka : Page
    {
        public static tkmEntities7 DatatkmEntities { get; set; }

        ObservableCollection<Заказ_номенклатура> ListZayavka;
        public PageZayavka()
        {
            DatatkmEntities = new tkmEntities7();
            InitializeComponent();
            ListZayavka = new ObservableCollection<Заказ_номенклатура>();
            var queryMaster = from d in DatatkmEntities.Заказ_номенклатура
                              orderby d.Код_заказ_номенклатура
                              join x in DatatkmEntities.Номенклатура on d.Код_номенклатуры equals x.Код_номенклатуры
                              select d;

            foreach (Заказ_номенклатура d in queryMaster)
            {
                if (d.Количество_для_изготовления == 0)
                {
                }
                else
                {
                    ListZayavka.Add(d);
                }
            }


            DataGridZayavka.ItemsSource = ListZayavka;
        }
        private bool isDirty = true;
        private void btn_Edit(object sender, RoutedEventArgs e)
        {

            DataGridZayavka.BeginEdit();
            isDirty = false;

        }

        private void btn_Save(object sender, RoutedEventArgs e)
        {
            DatatkmEntities.SaveChanges();
            isDirty = true;
            DataGridZayavka.IsReadOnly = true;
            MessageBox.Show("Сохранено");
        }

        private void btn_Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pagelogin.xaml", UriKind.Relative));
        }

        private void RewriteFind()
        {
            DatatkmEntities = new tkmEntities7();
            ListZayavka.Clear();
            Find();
        }
        private void Find()
        {
            var queryPerson = from d in PageZayavka.DatatkmEntities.Заказ_номенклатура
                              orderby d.Код_заказ_номенклатура
                              select d;

            foreach (Заказ_номенклатура d in queryPerson)
            {
                if (d.Количество_для_изготовления == 0)
                {
                }
                else
                {
                    ListZayavka.Add(d);
                }
            }
            DataGridZayavka.ItemsSource = ListZayavka;

        }



        private void ButtonFind_Click(object sender, RoutedEventArgs e)
        {
            string find = "";

            find = Convert.ToString(tbxfind.Text);


            if (find == "")
            {
                RewriteFind();
            }
            else
            {
                var queryFind = from d in DatatkmEntities.Заказ_номенклатура
                                  orderby d.Код_заказ_номенклатура
                                  where find == d.Статус_заявки
                                  select d;

                ListZayavka.Clear();
                foreach (Заказ_номенклатура d in queryFind)
                {
                    ListZayavka.Add(d);
                }
                DataGridZayavka.ItemsSource = ListZayavka;
                DataGridZayavka.UpdateLayout();
            }
        }
    }
}
