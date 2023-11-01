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

namespace practika
{
    /// <summary>
    /// Логика взаимодействия для MainWindowManager.xaml
    /// </summary>
    public partial class MainWindowManager : Window
    {
        private praktikaEntities db = new praktikaEntities();
        public Пользователь user;
        public MainWindowManager(Пользователь user)
        {
            InitializeComponent();
            this.user = user;
            foreach (var d in db.Заказ)
            {
                orders.Items.Add(d.Номер);
            }
            ListViewLoad();
        }
        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            Authorization mainWindow = new Authorization();
            mainWindow.Show();
            this.Hide();
        }
        public void ListViewLoad()
        {
            using (praktikaEntities db = new praktikaEntities())
            {
                var orders = db.Заказ.ToList();
                var goods = db.Изделия.ToList();
                Orders.ItemsSource = orders;
                Goods.ItemsSource = goods;
            }
        }
        private void Find_Click(object sender, RoutedEventArgs e)
        {
            string searchText = Find.Text;
            using (praktikaEntities db = new praktikaEntities())
            {
                var query = from data in db.Изделия
                            where data.Наименование.Contains(searchText)
                            select data;
                Goods.ItemsSource = query.ToList();
            }
        }
        private void Save_changes_Click(object sender, RoutedEventArgs e)
        {
            using (praktikaEntities db = new praktikaEntities())
            {
                var id = Convert.ToInt32(orders.SelectedItem);
                Заказ заказ = db.Заказ.Where(x => x.Номер == id).FirstOrDefault();
                var status_order = (status.SelectedValue as ComboBoxItem).Content.ToString(); ;
                заказ.Этап_выполнения = status_order;
                db.SaveChanges();
                MessageBox.Show("Статус заказа изменен");
                ListViewLoad();
            }
        }
    }
}
