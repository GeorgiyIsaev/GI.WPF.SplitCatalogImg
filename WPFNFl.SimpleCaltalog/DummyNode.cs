using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

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

    public class ExtendedTreeView : TreeView
    {
        public ExtendedTreeView()
            : base()
        {
            this.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(___ICH);
        }

        void ___ICH(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (SelectedItem != null)
            {
                SetValue(SelectedItem_Property, SelectedItem);
            }
        }

        public object SelectedItem_
        {
            get { return (object)GetValue(SelectedItem_Property); }
            set { SetValue(SelectedItem_Property, value); }
        }
        public static readonly DependencyProperty SelectedItem_Property = DependencyProperty.Register("SelectedItem_", typeof(object), typeof(ExtendedTreeView), new UIPropertyMetadata(null));
    }
}
