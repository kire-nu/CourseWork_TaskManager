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
using TaskManagerLib;

namespace TaskManager {
    /// <summary>
    /// Interaction logic for NewProject.xaml
    /// </summary>
    public partial class NewItemDialog : Window {

        ProjectMgr projectManager;

        public NewItemDialog(ProjectMgr projectManager) {
            InitializeComponent();
            this.projectManager = projectManager;
            Owner = Application.Current.MainWindow;
        }

        public string ProjectNumber { get { return newProjectNumber.Text; } }
        public string ProjectName { get { return newProjectName.Text; } }

        private void newProjectAdd_Click(object sender, RoutedEventArgs e) {
            if ((newProjectName.Text.Length > 0) && (newProjectNumber.Text.Length > 0)) {
                if (projectManager.ValidateId(newProjectNumber.Text)) {
                    this.DialogResult = true;
                } else {
                    MessageBox.Show("Error: project number already exist", "Error");
                }
            } else {
                MessageBox.Show("Error: Name and number cannot be blank", "Error");
            }
        }

    }
}
