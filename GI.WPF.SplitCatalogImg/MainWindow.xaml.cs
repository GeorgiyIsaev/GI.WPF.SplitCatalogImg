using Microsoft.Win32;
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

        private string currentCatalog = "";
        public string GetCeurrntCatalog() { return currentCatalog; }

        public void SetCeurrntCatalog(string Catalog) 
        {
            if (Catalog == "") return;

            if (!Directory.Exists(Catalog))
            {
                MessageBox.Show("Доступ к каталогу \"" + Catalog  + "\" отсутствует или такой катлог не существует!");
                return;
            }  
            currentCatalog = Catalog;
            Title = "SplitCatalog:  " + Catalog;
            
            List<string> listCatalogs = new List<string>(currentCatalog.Split('\\'));
            if(listCatalogs.Last() == "")
            {
                listCatalogs.RemoveAt(listCatalogs.Count - 1);
            }

            if(listCatalogs.Count<1)
            {
                TextBox_NameParentFolder.Text = "";
                TextBox_NameCurrentFolder.Text = "";
            }
            if (listCatalogs.Count == 1)
            {
                TextBox_NameParentFolder.Text = "";
                TextBox_NameCurrentFolder.Text = listCatalogs.Last();
            }
            else
            {
                TextBox_NameParentFolder.Text = listCatalogs.ElementAt(listCatalogs.Count - 2);
                TextBox_NameCurrentFolder.Text = listCatalogs.Last();
            }
            TextBox_BeforeCounter.Text = "";
            TextBox_AfterCounter.Text = "";

            var a = Directory.GetFiles(currentCatalog);


            TextBox_BeginningCounter.Text = "1";          
            TextBox_CountFiles.Text = "";
            TextBox_CountСharacter.Text = "";
          

        }



        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //string nameFileConfig = "fileConfig.txt";
            //string nameCatalog = GetNameCatalog(nameFileConfig);

            //if (Directory.Exists(nameCatalog))
            //{
            //    Title = "SplitCatalog:  " + nameCatalog;
            //}
        }





        private string GetNameCatalog(string nameConfigFile)
        {
            if (System.IO.File.Exists(nameConfigFile))
            {
                string text = new StreamReader(nameConfigFile).ReadLine();
                return text;
            }

            return "";
        }

        private void MenuItem_OpeCatalog_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = "";
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
            }
            SetCeurrntCatalog(folderPath);
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
                System.Windows.MessageBox.Show("Каталог " + nameCatalog + " не обнаружен!");
                Title = "SplitCatalog:";
                return;
            }         


            string nameFileConfig = "fileConfig.txt";
            using (FileStream fileStream = File.Open(nameFileConfig, FileMode.Create))
            {
                using (StreamWriter output = new StreamWriter(fileStream))
                {
                    output.Write(nameCatalog);
                }
            }

            SetCeurrntCatalog(nameCatalog);
        }
       //System.Windows.Forms.OpenFileDialog;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            string folderPath = "";
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
            }        
        }
    }
}
