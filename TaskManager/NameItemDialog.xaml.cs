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

namespace TaskManager {
    /// <summary>
    /// Interaction logic for NameItemDialog.xaml
    /// </summary>
    public partial class NameItemDialog : Window {
        public NameItemDialog(string name, string item) {
            InitializeComponent();
            textboxName.Text = name;
            Owner = Application.Current.MainWindow;
            this.Title = string.Concat("Name ",item);
            this.labelEnterName.Content = string.Concat("Enter Name of ", item,":");
        }
        public string ItemName { get { return textboxName.Text; } }

        private void bottonDialogOk_Click(object sender, RoutedEventArgs e) {
            if (textboxName.Text.Length > 0) {
                this.DialogResult = true;
            } else {
                MessageBox.Show("Error: Name cannot be blank", "Error");
            }
        }
    }
}
