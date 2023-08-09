using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GI.WPF.SplitCatalogImg
{
    public class NameFile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NewName { get; set; }

        private string fullName;
        public string GetFullName() { return fullName; }

        public NameFile(string fullName, int id)
        {
            Id = id.ToString();
            this.fullName = fullName;
            Name = fullName.Substring(fullName.LastIndexOf('\\') + 1);
            NewName = "";

        }
    }

    static public class ListFiles
    {
        static public ObservableCollection<NameFile> files = new ObservableCollection<NameFile>();

        static public void CreateNewListFile(string currentCatalog)
        {
            files = new ObservableCollection<NameFile>();

            List<string> sortFilesList = new List<string>(Directory.GetFiles(currentCatalog));
            if (sortFilesList.Count == 0) return;
            sortFilesList = SortFilesList(sortFilesList);

            int counter = 1;
            foreach (var namefile in sortFilesList)
            {
                files.Add(new NameFile(namefile, counter++));
            }
        }

        static public void NewNameFile(string befor, string after, int startCount, int characters)
        {
            foreach (var namefile in files)
            {
                string fileExtension = namefile.Name.Substring(namefile.Name.LastIndexOf('.') + 1);

                string countrStr = startCount.ToString();


                while (characters > countrStr.Length)
                {
                    countrStr = "0" + countrStr;
                }             

                namefile.NewName = befor + countrStr + after + "." + fileExtension;
               
                startCount++;
            }
        }


        static private List<string> SortFilesList(List<string> noSortFilesList)
        {
            List<string> sortFilesList = noSortFilesList;

            /*Сортируем*/
            sortFilesList.Sort(delegate (string x, string y)
            {
                string temp1 = x.Substring(x.LastIndexOf('\\') + 1);
                string temp2 = y.Substring(y.LastIndexOf('\\') + 1);

                string[] substr = { "\\", ".jpg", ".png", ".gif", ".webn" };
                foreach (string sub in substr)
                {
                    temp1 = temp1.Replace(sub, "");
                    temp2 = temp2.Replace(sub, "");
                }

                int number1; int number2;
                if (int.TryParse(temp1, out number1) && int.TryParse(temp2, out number2))
                {
                    return number1 - number2;
                }
                else if (int.TryParse(temp1, out number1) || int.TryParse(temp2, out number2))
                {
                    return x.CompareTo(y);
                }
                else
                {
                    return x.CompareTo(y);
                }
            });

            return sortFilesList;
        }


    }


    
}
