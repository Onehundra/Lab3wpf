using Labb3_NET22.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Labb3_NET22
{
    /// <summary>
    /// Interaction logic for CreateQuizView.xaml
    /// </summary>
    public partial class CreateQuizView : UserControl
    {
        private Quiz currentQuiz;

        public CreateQuizView()
        {
            InitializeComponent();
            currentQuiz = new Quiz();
        }

        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            string questionText = QuestionBox.Text;
            string[] answers = { Answer1Box.Text, Answer2Box.Text, Answer3Box.Text, Answer4Box.Text };
            int correctAnswer = int.Parse(CorrectBox.Text);

            currentQuiz.AddQuestion(questionText, correctAnswer, answers);

            MessageBox.Show("Question added!");
            QuestionBox.Text = "";
            Answer1Box.Text = "";
            Answer2Box.Text = "";
            Answer3Box.Text = "";
            Answer4Box.Text = "";
            CorrectBox.Text = "1";
        }

        private void SaveQuiz_Click(object sender, RoutedEventArgs e)
        {
            string folder = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Labb3_NET22");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string filePath = System.IO.Path.Combine(folder, QuizTitleBox.Text + ".json");

            string json = JsonSerializer.Serialize(currentQuiz, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);

            MessageBox.Show($"Quiz saved as {QuizTitleBox.Text}.json");
        }

        private void ReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.Content = new StartMenuView();
        }
    }
}
