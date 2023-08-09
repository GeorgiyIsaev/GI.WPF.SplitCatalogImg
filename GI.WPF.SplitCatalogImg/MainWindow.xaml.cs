using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
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
            /*Проверки неверных значений*/
            if (Catalog == "") return;
            if (!Directory.Exists(Catalog))
            {
                MessageBox.Show("Доступ к каталогу \"" + Catalog  + "\" отсутствует или такой катлог не существует!");
                return;
            }  

            /*Определение имени катлогой и род катлога*/
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


            /*Получение списка файлов*/

            ListFiles.CreateNewListFile(currentCatalog);
            DataGrid_NameFiles.ItemsSource = ListFiles.files;
            ResetComboBox_CountСharacter(ListFiles.files.Count);
            TextBox_CountFiles.Text = "" + ListFiles.files.Count;   
        }

        private void ResetComboBox_CountСharacter(int countFiles)
        {
            ComboBox_CountСharacter.Items.Clear();
            if(countFiles == 0)
            {
                ComboBox_CountСharacter.Items.Add(0);
                ComboBox_CountСharacter.SelectedIndex = 0;
                return;
            }
            int digitCount = (int)Math.Log10(countFiles) + 1;
            do
            {
                ComboBox_CountСharacter.Items.Add(digitCount.ToString());
                digitCount++;
            } while (digitCount <= 10);       
            ComboBox_CountСharacter.SelectedIndex = 0;  
        }


        //private List<string> SortFilesList()
        //{
        //    List<string> sortFilesList = new List<string>(Directory.GetFiles(currentCatalog));

        //    /*Сортируем*/
        //    sortFilesList.Sort(delegate (string x, string y)
        //    {
        //        string temp1 = x.Substring(x.LastIndexOf('\\') + 1);
        //        string temp2 = y.Substring(y.LastIndexOf('\\') + 1);

        //        string[] substr = { "\\", ".jpg", ".png", ".gif", ".webn"};
        //        foreach (string sub in substr)
        //        {
        //            temp1 = temp1.Replace(sub, "");
        //            temp2 = temp2.Replace(sub, "");
        //        }

        //        int number1; int number2;
        //        if (int.TryParse(temp1, out number1) && int.TryParse(temp2, out number2))
        //        {
        //            return number1 - number2;
        //        }
        //        else if (int.TryParse(temp1, out number1) || int.TryParse(temp2, out number2))
        //        {
        //            return x.CompareTo(y);
        //        }
        //        else
        //        {
        //            return x.CompareTo(y);
        //        }
        //    });

        //    return sortFilesList;
        //}





        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox_BeforeCounter.Text = "";
            TextBox_AfterCounter.Text = "";
            TextBox_BeginningCounter.Text = "1";
            TextBox_NameParentFolder.Text = "";
            TextBox_NameCurrentFolder.Text = "";
            TextBox_CountFiles.Text = "0";
            ComboBox_CountСharacter.Items.Add(0);
            ComboBox_CountСharacter.SelectedIndex = 0;        
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

        private void Button_Preview_Click(object sender, RoutedEventArgs e)
        {
            if (currentCatalog == "") return;
            if (TextBox_BeginningCounter.Text == "") TextBox_BeginningCounter.Text = "1";
            int countСharacter = int.Parse(ComboBox_CountСharacter.Text.ToString());
            int beginningCounter = int.Parse(TextBox_BeginningCounter.Text);

            ListFiles.NewNameFile(TextBox_BeforeCounter.Text,
                 TextBox_AfterCounter.Text,
                 beginningCounter,
                 countСharacter
                 );
            DataGrid_NameFiles.Items.Refresh();
        }

        private void Button_ReName_Click(object sender, RoutedEventArgs e)
        {
            if (currentCatalog == "") return;
            foreach(var item in ListFiles.files)
            {
                if (item.NewName == "") return;
                try
                {
                    if (File.Exists(item.GetFullName()))
                    {
                        File.Move(item.GetFullName(), currentCatalog + "\\" + item.NewName);
                    }                 
                }catch
                (Exception ex)
                { 
                    MessageBox.Show(ex.Message);
                }
            }

            SetCeurrntCatalog(currentCatalog);
        }

        private void TextBox_BeginningCounter_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {           
                if (!Char.IsDigit(e.Text, 0))
                {
                    e.Handled = true;
                }            
        }
    }
}
