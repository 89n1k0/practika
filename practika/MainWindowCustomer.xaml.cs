using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace practika
{
    /// <summary>
    /// Логика взаимодействия для MainWindowCustomer.xaml
    /// </summary>
    public partial class MainWindowCustomer : Window
    {
        public Пользователь user;
        public Изделия изделия;
        private praktikaEntities db = new praktikaEntities();
        public int all_count = 1;
        public MainWindowCustomer(Пользователь user)
        {
            InitializeComponent();
            ListViewLoad();
            this.user = user;
            count.IsReadOnly = true;
            count.Text = all_count.ToString();
            foreach (var d in db.Изделия)
            {
                goods.Items.Add(d.Наименование + "-" + d.Артикул);
            }
        }
        public void ListViewLoad()
        {
            using (praktikaEntities db = new praktikaEntities())
            {
                var orders = db.Заказ.ToList();
                Orders.ItemsSource = orders;
            }
        }
        private void goods_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (praktikaEntities db = new praktikaEntities())
            {
                string selectedItem = goods.SelectedItem.ToString();
                string[] good = selectedItem.Split(new char[] { '-' });
                string Name = good[1];
                изделия = db.Изделия.Where(x => x.Артикул == Name).FirstOrDefault();
                total_sum.Content = "0";
            }
        }

        private void minus_Click(object sender, RoutedEventArgs e)
        {
            if (all_count == 1)
            {
                MessageBox.Show("Данное количество минимально");
            }
            else
            {
                all_count--;
                count.Text = all_count.ToString();
                total_sum.Content = Convert.ToInt32(изделия.Цена) * all_count;
            }
        }

        private void plus_Click(object sender, RoutedEventArgs e)
        {
            if(all_count == изделия.Количество)
            {
                MessageBox.Show("Данное количество максимально");
            }
            else
            {
                all_count++;
                count.Text = all_count.ToString();
                total_sum.Content = Convert.ToInt32(изделия.Цена) * all_count;
            }
        }

        private void New_order_Click(object sender, RoutedEventArgs e)
        {
            using (praktikaEntities db = new praktikaEntities())
            {
                string selectedItem = goods.SelectedItem.ToString();
                string[] good = selectedItem.Split(new char[] { '-' });
                string Name = good[1];
                изделия = db.Изделия.Where(x => x.Артикул == Name).FirstOrDefault();
                var price = Convert.ToInt32(изделия.Цена) * all_count;
                int count_good = Convert.ToInt32(count.Text);
                int sum = Convert.ToInt32(total_sum.Content);
                Заказ заказ = new Заказ(System.DateTime.Now, "Новый");
                db.Заказ.Add(заказ);
                Заказанные_изделия заказанные_Изделия = new Заказанные_изделия(заказ.Номер, изделия.Артикул, count_good);
                db.Заказанные_изделия.Add(заказанные_Изделия);
                db.SaveChanges();
                MessageBox.Show("Заказ создан");
                all_count = 0;
                ListViewLoad();
            }
        }

        private void Create_Order_Click(object sender, RoutedEventArgs e)
        {
            using (praktikaEntities db = new praktikaEntities())
            {
                string selectedItem = goods.SelectedItem.ToString();
                string[] good = selectedItem.Split(new char[] { '-' });
                string Name = good[1];
                изделия = db.Изделия.Where(x => x.Артикул == Name).FirstOrDefault();
                var price = Convert.ToInt32(изделия.Цена) * all_count;
                int count_good = Convert.ToInt32(count.Text);
                int sum = Convert.ToInt32(total_sum.Content);
                Заказ заказ = new Заказ(System.DateTime.Now, "Новый");
                db.Заказ.Add(заказ);
                Заказанные_изделия заказанные_Изделия = new Заказанные_изделия(заказ.Номер, изделия.Артикул, count_good);
                db.Заказанные_изделия.Add(заказанные_Изделия);
                db.SaveChanges();
                MessageBox.Show("Заказ создан");
                all_count = 0;
                ListViewLoad();

            }
        }

        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }
    }
}
