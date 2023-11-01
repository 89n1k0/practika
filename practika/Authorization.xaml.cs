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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Пользователь user;
        public Authorization()
        {
            InitializeComponent();
        }

        private void Autho_Click(object sender, RoutedEventArgs e)
        {
            using (praktikaEntities db = new praktikaEntities())
            {
                if (db.Пользователь.Where(x => x.login == login.Text && x.password == password.Password).Any())
                {
                    user = db.Пользователь.Where(x => x.login == login.Text && x.password == password.Password).First();
                    if (user.id_role == 1) { MainWindowCustomer customer = new MainWindowCustomer(user); customer.Show(); this.Hide(); }
                    else if (user.id_role == 2) { MainWindowManager manager = new MainWindowManager(user); manager.Show(); this.Hide(); }
                    else if (user.id_role == 3) { MainWindowStorekeeper storekeeper = new MainWindowStorekeeper(user); storekeeper.Show(); this.Hide(); }
                    else if (user.id_role == 4) { MainWindowDirectorate directorate = new MainWindowDirectorate(user); directorate.Show(); this.Hide(); }

                }
                else
                {
                    MessageBox.Show("Данный аккаунт отсутствует либо допущены ошибки в логине и пароле");
                }
            }
        }

        private void Regist_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Hide();
        }
    }
}
