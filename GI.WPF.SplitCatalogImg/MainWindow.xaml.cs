using System;
using System.Collections.Generic;
using System.IO;
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

namespace GI.WPF.SplitCatalogImg
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string nameFileConfig = "NameCatalog1.txt";
            if (!isExistsCatalog(nameFileConfig))
            {
                MessageBox.Show("Файл конфигурации "+ nameFileConfig + " не обнружен! ");
                var dialog = new WindowGetNameCatalog();
                if (dialog.ShowDialog() == true)
                {
                    MessageBox.Show("You said: " + dialog.ResponseText);
                }

            }

            string nameCatalog = GetNameCatalog(nameFileConfig);
            if (!isExistsCatalog(nameCatalog)) MessageBox.Show("Каталог \"" + nameCatalog + "\" не обнаружен!");
            Title = "SplitCatalog:  " + nameCatalog + " " + isExistsCatalog(nameCatalog);
            // "C:\test\0"
        }

        private bool isExistsCatalog(string nameCatalog) 
        {    
            if (!Directory.Exists(nameCatalog))
            {        
                return false;
            }
            else
                return true;
        }



        private string GetNameCatalog(string nameCatalog)
        {
            if (!isExistsCatalog(nameCatalog))
            {
                MessageBox.Show("Каталог " + nameCatalog + " не обнаружен!");
                return "";
            }
                    

            string text = new StreamReader(nameCatalog).ReadLine();
            return text;
        }


    }
}
