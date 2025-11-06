using Labb3_NET22.DataModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using System.IO;

namespace Labb3_NET22
{
    /// <summary>
    /// Interaction logic for StartMenuView.xaml
    /// </summary>
    public partial class StartMenuView : UserControl
    {
        public StartMenuView()
        {
            InitializeComponent();
        }
        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            Quiz quiz = new Quiz();
            var playView = new PlayQuizView(quiz);


            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.Content = playView;
        }
        private void CreateQuiz_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {
                mainWindow.Content = new CreateQuizView();
            }
        }

        private void PlaySavedQuiz_Click(object sender, RoutedEventArgs e)
        {
            
            var dialog = new OpenFileDialog();
            dialog.Filter = "JSON Files|*.json";
            dialog.InitialDirectory = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Labb3_NET22");

            if (dialog.ShowDialog() == true)
            {
                string json = File.ReadAllText(dialog.FileName);
                Quiz loadedQuiz = JsonSerializer.Deserialize<Quiz>(json);

                var playView = new PlayQuizView(loadedQuiz);
                var mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.Content = playView;
            }
        }
        private void EditQuiz_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Content = new EditQuizView();
            }
        }

    }
}
