using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFNFl.SimpleCaltalog
{
    public class DummyNode
    {
        private string name;
        private string fullname;
        public string GetName() { return name; }
        public string GetFullName() { return fullname; }
        public DummyNode(string fullname)
        {            
            this.fullname = fullname;
            var pathParts = fullname.Split('\\');
            List<string> test = new List<string>();
            foreach (var pathPart in pathParts)
            {
                test.Add(pathPart);
            }
            fullname = test.LastOrDefault();




        }
    }
}
