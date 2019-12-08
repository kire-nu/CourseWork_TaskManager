using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using TaskManagerLib;

namespace TaskManager {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private string fileName = string.Empty;
        private ProjectMgr projectMgr = new ProjectMgr();
        private TaskMgr taskMgr = new TaskMgr();
        private List<TaskItem> allTaskItems = new List<TaskItem>();
        private List<TaskItem> linkedTaskItems = new List<TaskItem>();
        private List<ProjectItem> projects = new List<ProjectItem>();
        private List<string> persons = new List<string>();


        public MainWindow() {
            InitializeComponent();
            //Debug();
            UpdateGUI();
            this.Title = "Task Manager - untitiled.dat";
        }

        private void Debug() {
            ProjectItem projectItem;
            string person;
            TaskItem taskItem;
            projectItem = new ProjectItem("1111", "1 example");
            person = "Ting";
            projectItem.Persons.Add(person);
            person = "Fing";
            projectItem.Persons.Add(person);
            person = "Sing";
            projectItem.Persons.Add(person);
            projectMgr.Add(projectItem);
            projectItem = new ProjectItem("1112", "2 example");
            person = "Ling";
            projectItem.Persons.Add(person);
            person = "Ring";
            projectItem.Persons.Add(person);
            projectMgr.Add(projectItem);

            taskItem = new TaskItem(1, "Scout", "Search nearby area");
            taskMgr.Add(taskItem);
            taskItem = new TaskItem(2, "Build", "Build a shelter");
            taskMgr.Add(taskItem);
            taskItem = new TaskItem(3, "Food", "Find food");
            taskMgr.Add(taskItem);
            taskItem = new TaskItem(4, "Fire", "Make fire");
            taskMgr.Add(taskItem);

            taskItem = new TaskItem(5, "Data Collection", "Research the data required");
            taskMgr.Add(taskItem);
            taskItem = new TaskItem(6, "Data Analysis", "Analyse the data from data collection step");
            taskMgr.Add(taskItem);
            taskItem = new TaskItem(7, "Presentation", "Present the anlysis");
            taskMgr.Add(taskItem);

        }

        private void UpdateGUI() {
            projects = projectMgr.ToList();
            allTaskItems = taskMgr.ToList();
            listBoxProjects.ItemsSource = projects;
            listBoxAllTasks.ItemsSource = allTaskItems;
            listBoxLinkedTasks.ItemsSource = linkedTaskItems;
            listBoxProjects.Items.Refresh();
            listBoxPersons.Items.Refresh();
            listBoxAllTasks.Items.Refresh();
            listBoxLinkedTasks.Items.Refresh();
        }

        #region Menubar actions

        /// <summary>
        /// Create new file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileNew_Click(object sender, RoutedEventArgs e) {
            // Reset everything
            fileName = string.Empty;
            projectMgr = new ProjectMgr();
            taskMgr = new TaskMgr();
            allTaskItems = new List<TaskItem>();
            linkedTaskItems = new List<TaskItem>();
            projects = new List<ProjectItem>();
            persons = new List<string>();
            this.Title = "Task Manager - untitiled.dat";
            UpdateGUI();
        }

        /// <summary>
        /// Open file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileOpen_Click(object sender, RoutedEventArgs e) {
            // Open dialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog.Filter = "Data file (.dat)|*.bin|All Files *.*|*.*";
            if (openFileDialog.ShowDialog() == true) {
                fileName = openFileDialog.FileName;
                try {
                    // Restore (deserialize)
                    Serialization serialization = new Serialization();
                    serialization.BinaryDeSerialize(fileName, out taskMgr, out projectMgr);
                    this.Title = string.Concat("Task Manager - ", System.IO.Path.GetFileName(fileName));
                    UpdateGUI();
                    MessageBox.Show("Data read from file", "Success");
                } catch (Exception ex) {
                    MessageBox.Show(string.Format("Error:\n{0}", ex.ToString()), "Error");
                }
            }
        }

        /// <summary>
        /// Save 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileSave_Click(object sender, RoutedEventArgs e) {
            if (fileName == string.Empty) {
                // If not saved before, go to save as
                SaveAs();
            } else {
                try {
                    // Save (serialize)
                    Serialization serialization = new Serialization();
                    serialization.BinarySerialize(fileName, taskMgr, projectMgr);
                    MessageBox.Show("File saved", "Success");
                } catch (Exception ex) {
                    MessageBox.Show(string.Format("Error:\n{0}", ex.ToString()), "Error");
                }
            }
        }

        /// <summary>
        /// Save as
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileSaveAs_Click(object sender, RoutedEventArgs e) {
            // Go to method
            SaveAs();
        }

        /// <summary>
        /// Save as
        /// </summary>
        private void SaveAs() {
            // Open dialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog.Filter = "Data file (.dat)|*.bin|All Files *.*|*.*";

            if (saveFileDialog.ShowDialog() == true) {
                fileName = saveFileDialog.FileName;
                try {
                    // Save (serialize)
                    Serialization serialization = new Serialization();
                    serialization.BinarySerialize(fileName, taskMgr, projectMgr);
                    MessageBox.Show("File saved", "Success");
                    this.Title = string.Concat("Task Manager - ", System.IO.Path.GetFileName(fileName));
                } catch (Exception ex) {
                    MessageBox.Show(string.Format("Error:\n{0}", ex.ToString()), "Error");
                }
            }
        }

        /// <summary>
        /// Exit program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileClose_Click(object sender, RoutedEventArgs e) {
            Environment.Exit(0);
        }

        #endregion

        #region Projects

        private void buttonAddProject_Click(object sender, RoutedEventArgs e) {
            NewItemDialog newItemDialog = new NewItemDialog(projectMgr);
            if (newItemDialog.ShowDialog() == true) {
                string projectNumber = newItemDialog.ProjectNumber;
                string projectName = newItemDialog.ProjectName;
                ProjectItem projectItem = new ProjectItem(projectNumber, projectName);
                projectMgr.Add(projectItem);
            }
            UpdateGUI();
        }

        private void buttonRemoveProject_Click(object sender, RoutedEventArgs e) {
            // Item must be selected
            if (listBoxProjects.SelectedItem != null) {
                int index = listBoxProjects.SelectedIndex;
                // Display warning
                MessageBoxResult messageBoxResult = MessageBox.Show(string.Format("Do you want to remove project {0}", projects[index].ProjectName), "Warning", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes) {
                    // Remove for list and update GUI
                    projectMgr.DeleteAt(index);
                    UpdateGUI();
                }
            }
        }

        private void buttonRenameProject_Click(object sender, RoutedEventArgs e) {
            // Item must be selected
            if (listBoxProjects.SelectedItem != null) {
                int index = listBoxProjects.SelectedIndex;
                // Open dialog to rename with current name
                NameItemDialog nameItemDialog = new NameItemDialog(projects[index].ProjectName, "Project");
                // If renamed
                if (nameItemDialog.ShowDialog() == true) {
                    //Change name and update GUI
                    projects[index].ProjectName = nameItemDialog.ItemName;
                    persons = null;
                    UpdateGUI();
                }
            }
        }

        private void listBoxProjects_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            // Check selection
            int index = listBoxProjects.SelectedIndex;
            if (index >= 0) {
                persons = projects[index].Persons;
            } else {
            }
            // Update list
            listBoxPersons.ItemsSource = persons;
            UpdateGUI();
        }

        #endregion

        #region Persons

        private void buttonRenamePerson_Click(object sender, RoutedEventArgs e) {
            // Item must be selected
            if (listBoxPersons.SelectedItem != null) {
                int index = listBoxPersons.SelectedIndex;
                // Open dialog to rename with current name
                NameItemDialog nameItemDialog = new NameItemDialog(persons[index], "Person");
                // If renamed
                if (nameItemDialog.ShowDialog() == true) {
                    //Change name and update GUI
                    persons[index] = nameItemDialog.ItemName;
                    UpdateGUI();
                }
            }
        }

        private void buttonRemovePerson_Click(object sender, RoutedEventArgs e) {
            // Item must be selected
            if (listBoxPersons.SelectedItem != null) {
                int index = listBoxPersons.SelectedIndex;
                // Open dialog to rename with current name
                NameItemDialog nameItemDialog = new NameItemDialog(persons[index], "Project");
                // Display warning
                MessageBoxResult messageBoxResult = MessageBox.Show(string.Format("Do you want to remove project {0}", projects[index].ProjectName), "Warning", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes) {
                    // Remove for list and update GUI
                    persons.RemoveAt(index);
                    UpdateGUI();
                }
            }
        }

        private void buttonAddPerson_Click(object sender, RoutedEventArgs e) {
            // Item must be selected
            if (listBoxProjects.SelectedItem != null) {
                int index = listBoxProjects.SelectedIndex;
                // Open dialog to rename with current name
                NameItemDialog nameItemDialog = new NameItemDialog(string.Empty, "Project");
                // If renamed
                if (nameItemDialog.ShowDialog() == true) {
                    //Change name and update GUI
                    persons.Add(nameItemDialog.ItemName);
                    listBoxPersons.ItemsSource = persons;
                    UpdateGUI();
                }
            }
        }



        private void listBoxPersons_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            // Check selection
            int personIndex = listBoxPersons.SelectedIndex;
            int projectIndex = listBoxProjects.SelectedIndex;
            if ((personIndex >= 0) && (projectIndex >= 0)) {
                linkedTaskItems = taskMgr.GetLinkedTasks(projects[projectIndex].Id, persons[personIndex]);
            }
            UpdateGUI();
        }

        #endregion

        #region All Task List

        /// <summary>
        /// Update task description
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdateTask_Click(object sender, RoutedEventArgs e) {
            // Item must be selected
            if (listBoxAllTasks.SelectedItem != null) {
                if (textBoxTaskName.Text.Length > 0) {
                    // Get data
                    int index = listBoxAllTasks.SelectedIndex;
                    TaskItem taskItem = taskMgr.GetAt(index);
                    taskItem.Name = textBoxTaskName.Text;
                    taskItem.Description = textBoxTaskDescription.Text;
                    // Update item
                    taskMgr.ChangeAt(taskItem, index);
                    // Update GUI
                    textBoxTaskName.Text = string.Empty;
                    textBoxTaskDescription.Text = string.Empty;
                    listBoxAllTasks.UnselectAll();
                    UpdateGUI();
                } else {
                    MessageBox.Show("Task name cannot be empty", "Warning");
                }
            }
        }

        private void buttonRemoveTask_Click(object sender, RoutedEventArgs e) {
            // Item must be selected
            if (listBoxAllTasks.SelectedItem != null) {
                int index = listBoxAllTasks.SelectedIndex;
                // Display warning
                MessageBoxResult messageBoxResult = MessageBox.Show(string.Format("Do you want to remove task {0}", allTaskItems[index].Name), "Warning", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes) {
                    // Remove for list and update GUI
                    taskMgr.DeleteAt(index);
                    textBoxTaskName.Text = string.Empty;
                    textBoxTaskDescription.Text = string.Empty;
                    UpdateGUI();
                }
            }
        }

        private void buttonAddTask_Click(object sender, RoutedEventArgs e) {
            // Name cannot be empty
            if (textBoxTaskName.Text.Length > 0) {
                // Get data
                string taskName = textBoxTaskName.Text;
                string taskDescription = textBoxTaskDescription.Text;
                int id = taskMgr.GetId();
                // Create task
                TaskItem taskItem = new TaskItem(id, taskName, taskDescription);
                taskMgr.Add(taskItem);
                // Update GUI
                textBoxTaskName.Text = string.Empty;
                textBoxTaskDescription.Text = string.Empty;
                UpdateGUI();
            } else {
                MessageBox.Show("Task name cannot be empty", "Warning");
            }
        }


        private void listBoxAllTasks_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            // Check selection
            int index = listBoxAllTasks.SelectedIndex;
            if (index >= 0) {
                TaskItem taskItem = allTaskItems[index];
                textBoxTaskName.Text = taskItem.Name;
                textBoxTaskDescription.Text = taskItem.Description;
            } 
            UpdateGUI();
        }

        #endregion

        #region Linked Tasks

        /// <summary>
        /// Asssign taks to person and project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLinkTask_Click(object sender, RoutedEventArgs e) {
            if ((listBoxPersons.SelectedItem != null) && (listBoxAllTasks.SelectedItem != null)) {
                // Get data
                int taskIndex = listBoxAllTasks.SelectedIndex;
                string projectNumber = projects[listBoxProjects.SelectedIndex].Id;
                int projectIndex = listBoxProjects.SelectedIndex;
                string person = listBoxPersons.SelectedItem.ToString();
                int personIndex = listBoxPersons.SelectedIndex;
                TaskItem taskItem = taskMgr.GetAt(taskIndex);
                // Assign and update
                taskItem.AddToLinkProjectAndPerson(projectNumber, person);
                taskMgr.ChangeAt(taskItem, taskIndex);
                // Update GUI
                textBoxTaskName.Text = string.Empty;
                textBoxTaskDescription.Text = string.Empty;
                listBoxAllTasks.UnselectAll();
                linkedTaskItems = taskMgr.GetLinkedTasks(projects[projectIndex].Id, persons[personIndex]);
            }
            UpdateGUI();
        }

        private void buttonUnlinkTask_Click(object sender, RoutedEventArgs e) {
            // Check selection
            int personIndex = listBoxPersons.SelectedIndex;
            int projectIndex = listBoxProjects.SelectedIndex;
            int taskIndex = listBoxAllTasks.SelectedIndex;
            if ((personIndex >= 0) && (taskIndex >= 0)) {
                // Get data
                string projectNumber = projects[listBoxProjects.SelectedIndex].Id;
                string person = listBoxPersons.SelectedItem.ToString();
                TaskItem taskItem = taskMgr.GetAt(taskIndex);
                // Remove link
                taskMgr.UnlinkTask(taskItem, projectNumber, person);
                // Update GUI
                linkedTaskItems = taskMgr.GetLinkedTasks(projects[projectIndex].Id, persons[personIndex]);
            }
            UpdateGUI();
        }

        private void listBoxLinkedTasks_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            int linkedIndex = listBoxLinkedTasks.SelectedIndex;
            if (linkedIndex >= 0) {
                TaskItem linkedTaskItem = linkedTaskItems[linkedIndex];
                int allIndex = allTaskItems.IndexOf(linkedTaskItem);
                listBoxAllTasks.SelectedIndex = allIndex;
            }
        }

        #endregion

    }
}
