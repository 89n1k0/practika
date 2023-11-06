using System;
using System.Linq;
using System.Windows;

namespace practika
{
    /// <summary>
    /// Логика взаимодействия для MainWindowStorekeeper.xaml
    /// </summary>
    public partial class MainWindowStorekeeper : Window
    {
        private praktikaEntities db = new praktikaEntities();
        public Пользователь user;
        public MainWindowStorekeeper(Пользователь user)
        {
            InitializeComponent();
            ListViewLoad();
            foreach (var d in db.Ткани)
            {
                fabric1.Items.Add(d.Артикул);
                fabric2.Items.Add(d.Артикул);
            }

            this.user = user;
        }
        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            Authorization main = new Authorization();
            main.Show();
            this.Hide();
        }
        public void ListViewLoad()
        {
            using (praktikaEntities db = new praktikaEntities())
            {
                var fabric = db.Ткани.ToList();
                var furniture = db.Фурнитура.ToList();
                Fabric.ItemsSource = fabric;
                Furniture.ItemsSource = furniture;
            }
        }

        private void Find_fabric_Click(object sender, RoutedEventArgs e)
        {
            string searchText = Find_fabric.Text;
            using (praktikaEntities db = new praktikaEntities())
            {
                var query = from data in db.Ткани
                            where data.Наименование.Contains(searchText)
                            select data;
                Fabric.ItemsSource = query.ToList();
            }
        }
        private void Find_furniture_Click(object sender, RoutedEventArgs e)
        {
            string searchText = Find_furniture.Text;
            using (praktikaEntities db = new praktikaEntities())
            {
                var query = from data in db.Фурнитура
                            where data.Наименование.Contains(searchText)
                            select data;
                Furniture.ItemsSource = query.ToList();
            }
        }

        private void Deduct_Click(object sender, RoutedEventArgs e)
        {
            using (praktikaEntities db = new praktikaEntities())
            {
                if (fabric1.SelectedItem != null)
                {
                    var number = fabric1.SelectedItem.ToString();
                    Ткани ткани = db.Ткани.Where(x => x.Артикул == number).FirstOrDefault();
                    ткани.Длина = 0;
                    ткани.Ширина = 0;
                    ткани.Количество = 0;
                    db.SaveChanges();
                    MessageBox.Show("Данные успешно списаны");
                    fabric1.SelectedItem = null;
                }
                else
                {
                    MessageBox.Show("Заполните все поля");
                }
            }
        }

        private void Entrance_Click(object sender, RoutedEventArgs e)
        {
            if (fabric2.SelectedItem != null && length.Text != null && width.Text != null && count.Text != null && price.Text !=null)
            {
                var number = fabric2.SelectedItem.ToString();
                Ткани ткани = db.Ткани.Where(x => x.Артикул == number).FirstOrDefault();
                ткани.Длина = Convert.ToDouble(length.Text);
                ткани.Ширина = Convert.ToDouble(width.Text);
                ткани.Цена = Convert.ToDecimal(price.Text);
                ткани.Количество = Convert.ToInt32(count.Text);
                db.SaveChanges();
                MessageBox.Show("Данные были успешно поступлены");
                length.Text = "";
                fabric2.SelectedItem = null;
                width.Text = "";
                count.Text = "";
                price.Text = "";
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
    }
}
