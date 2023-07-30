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
using static System.Net.Mime.MediaTypeNames;

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
            string nameFileConfig = "fileConfig.txt";
            string nameCatalog = GetNameCatalog(nameFileConfig);

            if (Directory.Exists(nameCatalog))
            {
                Title = "SplitCatalog:  " + nameCatalog;
            }


            //if (isExistsCatalog(nameCatalog))
            //{
            //    Title = "SplitCatalog:  " + nameCatalog;
            //}
           


            //string nameFileConfig = "NameCatalog1.txt";
            //string nameCatalog = "";
            //if (!isExistsCatalog(nameFileConfig))
            //{            
            //    var dialog = new WindowGetNameCatalog();
            //    if (dialog.ShowDialog() == true)
            //    {
            //        nameCatalog = dialog.ResponseText;       
            //    }
            //}
            ////Запись нового пути к каталогу в файл
            //using (FileStream fileStream = File.Open(nameFileConfig, FileMode.Create))
            //{
            //    using (StreamWriter output = new StreamWriter(fileStream))
            //    {                   
            //        output.Write(nameCatalog);
            //    }
            //}



            //nameCatalog = GetNameCatalog(nameFileConfig);
            //if (!isExistsCatalog(nameCatalog))
            //{
            //    nameCatalog = "Каталог " + nameCatalog + " не найден!";
            //}
            //Title = "SplitCatalog:  "  + nameCatalog ;
            //// "C:\test\0"
        }

        //private bool isExistsCatalog(string nameCatalog) 
        //{    
        //    if (!Directory.Exists(nameCatalog))
        //    {        
        //        return false;
        //    }
        //    else
        //        return true;
        //}



        private string GetNameCatalog(string nameConfigFile)
        {
            if (System.IO.File.Exists(nameConfigFile))
            {
                string text = new StreamReader(nameConfigFile).ReadLine();
                return text;
            }

            return "";
        }

        private void MenuItem_OpenNewCatalog_Click(object sender, RoutedEventArgs e)
        {
            string nameCatalog = "";
            var dialog = new WindowGetNameCatalog();
            if (dialog.ShowDialog() == true)
            {
                nameCatalog = dialog.ResponseText;
            }
            if (!Directory.Exists(nameCatalog))
            {
                MessageBox.Show("Каталог " + nameCatalog + " не обнаружен!");
                Title = "SplitCatalog:";
                return;
            }

            //if (!isExistsCatalog(nameCatalog))
            //{
            //    MessageBox.Show("Каталог " + nameCatalog + " не обнаружен!");
            //    return;
            //}          


            string nameFileConfig = "fileConfig.txt";
            using (FileStream fileStream = File.Open(nameFileConfig, FileMode.Create))
            {
                using (StreamWriter output = new StreamWriter(fileStream))
                {
                    output.Write(nameCatalog);
                }
            }

            Title = "SplitCatalog:  " + nameCatalog;
        }
    }
}
