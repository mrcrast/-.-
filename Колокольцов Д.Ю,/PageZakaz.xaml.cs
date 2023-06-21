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
    /// Логика взаимодействия для PageZakaz.xaml
    /// </summary>
    public partial class PageZakaz : Page
    {
        public static tkmEntities7 DatatkmEntities { get; set; }


        int KolNom;
        int NameNom;
        int Nalichie;
        int StoimostZakaza;
        int StoimostEdinica;
        int kolnasklade;
        int kolvoizgotovlenie;
        int kolvosklad;
        int kolvobshii;
        string result;
        public class b_Zakaz
        {
            public string Naimen { get; set; }
            public int KodZakaz { get; set; }
            public DateTime DateZakaz { get; set; }
            public int KodClienta { get; set; }
            public int KodMastera { get; set; }

            public int Stoimost { get; set; }

            public int KolvoSoSklada { get; set; }
            public int KolvoNaIzgotovlenie { get; set; }
            public string KolvoZakaz { get; set; }
            public string StatusZakaza { get; set; }


            public b_Zakaz()
            {
            }
        }
        ObservableCollection<b_Zakaz> ListZakaz;
       


        private bool isDirty = true;

        public PageZakaz()
        {
            DatatkmEntities = new tkmEntities7();
            InitializeComponent();
            ListZakaz = new ObservableCollection<b_Zakaz>();
            var queryZakaz = from d in DatatkmEntities.Заказ
                             orderby d.Код_Заказа
                             join x in DatatkmEntities.Заказ_номенклатура on d.Код_Заказа equals x.Код_Заказа
                             join c in DatatkmEntities.Номенклатура on x.Код_номенклатуры equals c.Код_номенклатуры
                             select x;


            foreach (var zakaz_ in queryZakaz)
            {
                    b_Zakaz zakaz = new b_Zakaz();
                    
                    zakaz.KodZakaz = (int)zakaz_.Заказ.Код_Заказа;
                    
                    int countZakaz = (from p in ListZakaz
                                      where p.KodZakaz == zakaz_.Заказ.Код_Заказа
                                      select p).Count();
                    if (countZakaz == 0) 
                    {
                    zakaz.DateZakaz = (DateTime)zakaz_.Заказ.Дата_заказа;
                    zakaz.KodClienta = (int)zakaz_.Заказ.Код_клиента;
                    zakaz.KodMastera = (int)zakaz_.Заказ.Код_мастера;
                    
                    zakaz.Stoimost = (int)zakaz_.Заказ.Итоговая_стоимость;
                    zakaz.StatusZakaza = zakaz_.Заказ.Статус_заказа;
                    string ZakazN = "";
                    string KolvoZakazi = "";
                    string comma = "";
                        foreach (var nomenklatura_ in queryZakaz) 
                        {
                            if (zakaz_.Заказ.Код_Заказа == nomenklatura_.Заказ.Код_Заказа)
                            {
                                
                                ZakazN = ZakazN + comma + nomenklatura_.Номенклатура.Наименование; 
                                KolvoZakazi = KolvoZakazi + comma + (nomenklatura_.Количество_со_склада + nomenklatura_.Количество_для_изготовления);
                                comma = ",";
                            }
                        }
                        zakaz.Naimen = ZakazN;
                        zakaz.KolvoZakaz = KolvoZakazi;
                        ListZakaz.Add(zakaz); 


                    
                }
                DataGridZakaz.ItemsSource = ListZakaz;
            }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            Zakaz.Visibility = Visibility.Visible;
        }

        private void Button_Otmena(object sender, RoutedEventArgs e)
        {
            Zakaz.Visibility = Visibility.Hidden;
        }

        private void Button_Add1(object sender, RoutedEventArgs e)
        {
            int maxКодЗаказ = (from un in DatatkmEntities.Заказ
                               select un.Код_Заказа).Max();
                Заказ заказ = new Заказ();
                заказ.Код_Заказа = maxКодЗаказ + 1;
                заказ.Код_клиента = (int)(ComboCodClienta.SelectedValue);
                заказ.Дата_заказа = DateTime.Now;
                NameNom = (int)(ComboCodNomenklatura.SelectedValue);
                заказ.Итоговая_стоимость = Convert.ToInt32(StoimostZakaz.Text);
                заказ.Код_мастера = (int)(ComboCodMastera.SelectedValue);
                заказ.Количество = Convert.ToInt32(ColvoZakaz.Text);
                заказ.Статус_заказа = "В работе";
                KolNom = Convert.ToInt32(ColvoZakaz.Text);
                DatatkmEntities.Заказ.Add(заказ);
                DatatkmEntities.SaveChanges();
            
            tkmEntities7 tkmEntities7 = new tkmEntities7();

                using (tkmEntities7)
                {
                    var nomenkla = tkmEntities7.Номенклатура;
                    foreach (Номенклатура q in nomenkla)
                    {
                        if (NameNom == q.Код_номенклатуры)
                        {
                            int kolnasklade = (int)q.Количество_на_складе;
                            {
                                if (kolnasklade > KolNom || kolnasklade == KolNom)
                                {
                                    int maxКодЗаказНом = (from un in DatatkmEntities.Заказ_номенклатура
                                                          select un.Код_заказ_номенклатура).Max();
                                    Заказ_номенклатура заказ_Номенклатура = new Заказ_номенклатура();
                                    заказ_Номенклатура.Код_заказ_номенклатура = maxКодЗаказНом + 1;
                                    заказ_Номенклатура.Количество_со_склада = Convert.ToInt32(ColvoZakaz.Text);
                                    заказ_Номенклатура.Код_номенклатуры = (int)(ComboCodNomenklatura.SelectedValue);
                                    заказ_Номенклатура.Код_Заказа = maxКодЗаказ + 1;
                                    заказ_Номенклатура.Количество_для_изготовления = 0;
                                    заказ_Номенклатура.Статус_заявки = "В работе";
                                    DatatkmEntities.Заказ_номенклатура.Add(заказ_Номенклатура);
                                    result = result + "Заказ успешно добавлен. Количество взятое со склада: " + заказ_Номенклатура.Количество_со_склада + " " + "Количество отправленное на изготовление: " + заказ_Номенклатура.Количество_для_изготовления;
                                MessageBox.Show(result);
                                DatatkmEntities.SaveChanges();
                                    Zakaz.Visibility = Visibility.Hidden;
                                    ColvoZakaz.Text = "";
                                    ComboCodNomenklatura.SelectedValue = null;
                                    ComboCodMastera.SelectedValue = null;
                                    ComboCodClienta.SelectedValue = null;
                                }
                                if (kolnasklade < KolNom)
                                {
                                    int kolnasklade1 = (int)q.Количество_на_складе;
                                    Nalichie = KolNom - kolnasklade;
                                    int maxКодЗаказНом = (from un in DatatkmEntities.Заказ_номенклатура
                                                          select un.Код_заказ_номенклатура).Max();
                                    Заказ_номенклатура заказ_Номенклатура = new Заказ_номенклатура();
                                    заказ_Номенклатура.Код_заказ_номенклатура = maxКодЗаказНом + 1;
                                    заказ_Номенклатура.Количество_со_склада = kolnasklade1;
                                    заказ_Номенклатура.Код_номенклатуры = (int)(ComboCodNomenklatura.SelectedValue);
                                    заказ_Номенклатура.Код_Заказа = maxКодЗаказ + 1;
                                    заказ_Номенклатура.Количество_для_изготовления = Nalichie;
                                    заказ_Номенклатура.Статус_заявки = "В работе";
                                    DatatkmEntities.Заказ_номенклатура.Add(заказ_Номенклатура);
                                result = result + "Заказ успешно добавлен. Количество взятое со склада: " + заказ_Номенклатура.Количество_со_склада + " " + "Количество отправленное на изготовление: " + заказ_Номенклатура.Количество_для_изготовления;
                                MessageBox.Show(result);
                                DatatkmEntities.SaveChanges();
                                    Zakaz.Visibility = Visibility.Hidden;
                                    ColvoZakaz.Text = "";
                                    ComboCodNomenklatura.SelectedValue = null;
                                    ComboCodMastera.SelectedValue = null;
                                    ComboCodClienta.SelectedValue = null;
                                }

                            }
                        }
                    }
                }

            
            
        }



        private void Button_Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("PageManager.xaml", UriKind.Relative));
        }




        private void Button_AddNomenkla(object sender, RoutedEventArgs e)
        {
            ZakazNomenklatura.Visibility = Visibility.Visible;
        }

        private void Button_Otmena1(object sender, RoutedEventArgs e)
        {
            ZakazNomenklatura.Visibility = Visibility.Hidden;
        }

        private void Button_Add2(object sender, RoutedEventArgs e)
        {
            NameNom = (int)(ComboNomenklatura.SelectedValue);
            KolNom = Convert.ToInt32(ColvoZakazNomenklatura_Colvo.Text);
            tkmEntities7 tkmEntities7 = new tkmEntities7();
            using (tkmEntities7)
            {
                var nomenkla = tkmEntities7.Номенклатура;
                foreach (Номенклатура q in nomenkla)
                {
                    if (NameNom == q.Код_номенклатуры)
                    {
                        int kolnasklade = (int)q.Количество_на_складе;
                        {
                            if (kolnasklade > KolNom || kolnasklade == KolNom)
                            {
                                int maxКодЗаказНом = (from un in DatatkmEntities.Заказ_номенклатура
                                                      select un.Код_заказ_номенклатура).Max();
                                Заказ_номенклатура заказ_Номенклатура = new Заказ_номенклатура();
                                заказ_Номенклатура.Код_заказ_номенклатура = maxКодЗаказНом + 1;
                                заказ_Номенклатура.Количество_со_склада = Convert.ToInt32(ColvoZakazNomenklatura_Colvo.Text);
                                заказ_Номенклатура.Код_номенклатуры = (int)(ComboNomenklatura.SelectedValue);
                                заказ_Номенклатура.Код_Заказа = Convert.ToInt32(CodZakazNomenklatura.Text);
                                заказ_Номенклатура.Количество_для_изготовления = 0;
                                заказ_Номенклатура.Статус_заявки = "В работе";
                                DatatkmEntities.Заказ_номенклатура.Add(заказ_Номенклатура);
                                result = result + "Номенклатура успешно добавлена. Количество взятое со склада: " + заказ_Номенклатура.Количество_со_склада + " " + "Количество отправленное на изготовление: " + заказ_Номенклатура.Количество_для_изготовления;
                                MessageBox.Show(result);
                                DatatkmEntities.SaveChanges();
                                ZakazNomenklatura.Visibility = Visibility.Hidden;
                                CodZakazNomenklatura.Text = "";
                                ColvoZakazNomenklatura_Colvo.Text = "";
                                ComboNomenklatura.SelectedValue = null;
                            }
                            if (kolnasklade < KolNom)
                            {
                                int kolnasklade1 = (int)q.Количество_на_складе;
                                Nalichie = KolNom - kolnasklade;
                                int maxКодЗаказНом = (from un in DatatkmEntities.Заказ_номенклатура
                                                      select un.Код_заказ_номенклатура).Max();
                                Заказ_номенклатура заказ_Номенклатура = new Заказ_номенклатура();
                                заказ_Номенклатура.Код_заказ_номенклатура = maxКодЗаказНом + 1;
                                заказ_Номенклатура.Количество_со_склада = kolnasklade1;
                                заказ_Номенклатура.Код_номенклатуры = (int)(ComboNomenklatura.SelectedValue);
                                заказ_Номенклатура.Код_Заказа = Convert.ToInt32(CodZakazNomenklatura.Text);
                                заказ_Номенклатура.Количество_для_изготовления = Nalichie;
                                заказ_Номенклатура.Статус_заявки = "В работе";
                                DatatkmEntities.Заказ_номенклатура.Add(заказ_Номенклатура);
                                result = result + "Номенклатура успешно добавлена. Количество взятое со склада: " + заказ_Номенклатура.Количество_со_склада + " " + "Количество отправленное на изготовление: " + заказ_Номенклатура.Количество_для_изготовления;
                                MessageBox.Show(result);
                                DatatkmEntities.SaveChanges();
                                ZakazNomenklatura.Visibility = Visibility.Hidden;
                                CodZakazNomenklatura.Text = "";
                                ColvoZakazNomenklatura_Colvo.Text = "";
                                ComboNomenklatura.SelectedValue = null;
                            }

                        }
                    }
                }
            }
        }

        private void btn_Save(object sender, RoutedEventArgs e)
        {
            DatatkmEntities.SaveChanges();
            isDirty = false;
            MessageBox.Show("Сохранено");
        }

    

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            b_Zakaz per = DataGridZakaz.SelectedItem as b_Zakaz;
            Заказ opd = DataGridZakaz.SelectedItem as Заказ;
            Заказ_номенклатура ops = DataGridZakaz.SelectedItem as Заказ_номенклатура;
            if (per != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить ячейку: " + per.KodZakaz,
                 "Warning", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    //DatatkmEntities.Заказ_номенклатура.Remove(per);
                    //DatatkmEntities.Заказ.Remove(per);
                    DataGridZakaz.SelectedIndex =
                    DataGridZakaz.SelectedIndex == 0 ? 1 :
                    DataGridZakaz.SelectedIndex - 1;
                    ListZakaz.Remove(per);
                    DatatkmEntities.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Выберите ячейку для удаления");
            }
            isDirty = false;
        }
    }
}
    


