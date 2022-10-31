using Logic;
using Ookii.Dialogs.Wpf;
using System;
using System.IO;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MessageBox = System.Windows.Forms.MessageBox;
using Window = System.Windows.Window;

namespace Generator
{
    /// <summary>пе
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Storage storage = Storage.GetStorage();
        PasswordGenerator generator = new PasswordGenerator();

        public MainWindow()
        {
            storage.Deserialize();
            InitializeComponent();

            foreach (Password pwd in storage.Passwords)
            {
                //DataGridPwds.Items.Add($"{pwd.Website}-{pwd.Login}:{pwd.Pwd}");
                DataGridPwds.Items.Add(pwd);
            }

            lableFilePath.Content = storage.Path;
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            if (storage.Path is null || storage.Path == "")
            {
                MessageBox.Show("No path to save");
            }
            else
            {
                textBoxNewPassword.Text =
                    generator.Generate(passwordBoxMasterPassword.Password, textBoxWebsiteName.Text, TextBoxLogin.Text);

                DataGridPwds.Items.Add(storage.Passwords[storage.Passwords.Count-1]);

                if (checkBoxIsClean.IsChecked == true)
                {
                    passwordBoxMasterPassword.Password = "";
                    textBoxWebsiteName.Text = "";
                    TextBoxLogin.Text = "";
                }
            }
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            storage.Serialize();
        }

        private void btnDeleteAllPwds_Click(object sender, RoutedEventArgs e)
        {
            storage.Passwords = new();
            File.Delete($"{storage.Path}\\{storage.FileName}");
            DataGridPwds.Items.Clear();
        }

        private void btnSelectFolder_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();
            dlg.ShowNewFolderButton = true;

            dlg.ShowDialog();
            
            string path = dlg.SelectedPath;
            lableFilePath.Content = path;
            Environment.SetEnvironmentVariable(storage._pathVariableName, path, EnvironmentVariableTarget.User);
            storage.Path = dlg.SelectedPath;
        }

        private void btnDeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            lableFilePath.Content = DataGridPwds.SelectedIndex;
        }
    }
}
