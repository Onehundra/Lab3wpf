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
    /// Interaction logic for EditQuizView.xaml
    /// </summary>
    public partial class EditQuizView : UserControl
    {
        private Quiz loadedQuiz;
        private string currentFilePath;


        public EditQuizView()
        {
            InitializeComponent();
        }

        private async void LoadQuiz_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = System.IO.Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "Labb3_NET22");
            
            System.IO.Directory.CreateDirectory(folderPath);

            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "JSON Files|*.json";
            dialog.InitialDirectory = folderPath;

            if (dialog.ShowDialog() == true)
            {
                currentFilePath = dialog.FileName;

                string json = await System.IO.File.ReadAllTextAsync(dialog.FileName);
                loadedQuiz = System.Text.Json.JsonSerializer.Deserialize<Labb3_NET22.DataModels.Quiz>(json);

                QuestionListBox.Items.Clear();
                foreach (var q in loadedQuiz.Questions)
                {
                    QuestionListBox.Items.Add(q.Statement);
                }

                MessageBox.Show("Quiz loaded!");
            }
        }

        private void QuestionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (QuestionListBox.SelectedIndex >= 0)
            {
                Question q = loadedQuiz.Questions.ElementAt(QuestionListBox.SelectedIndex);
                EditQuestionBox.Text = q.Statement;
                EditAnswer0Box.Text = q.Answers[0];
                EditAnswer1Box.Text = q.Answers[1];
                EditAnswer2Box.Text = q.Answers[2];
                EditAnswer3Box.Text = q.Answers[3];
                EditCorrectBox.Text = q.CorrectAnswer.ToString();
            }
        }

        private async void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            
            int selectedIndex = QuestionListBox.SelectedIndex;
            List<Question> updatedQuestions = loadedQuiz.Questions.ToList();

            
            Question newQuestion = new Question(EditQuestionBox.Text,new string[]
            {
                EditAnswer0Box.Text,
                EditAnswer1Box.Text,
                EditAnswer2Box.Text,
                EditAnswer3Box.Text
            },

                int.Parse(EditCorrectBox.Text)
            );


            updatedQuestions[selectedIndex] = newQuestion;

            loadedQuiz = new Quiz();

            foreach (var q in updatedQuestions)
            {
                loadedQuiz.AddQuestion(q.Statement, q.CorrectAnswer, q.Answers);
            }

            



            string json = JsonSerializer.Serialize(loadedQuiz, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(currentFilePath, json);

            QuestionListBox.Items[selectedIndex] = newQuestion.Statement;

            MessageBox.Show("Question updated!");
        }

        private async void DeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionListBox.SelectedIndex >= 0)
            {
                List<Question> list = loadedQuiz.Questions.ToList();

                list.RemoveAt(QuestionListBox.SelectedIndex);

                loadedQuiz = new Quiz();
                foreach (Question q in list)
                {
                    loadedQuiz.AddQuestion(q.Statement, q.CorrectAnswer, q.Answers);
                }

             
                string json = JsonSerializer.Serialize(loadedQuiz, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(currentFilePath, json);

                QuestionListBox.Items.RemoveAt(QuestionListBox.SelectedIndex);

                MessageBox.Show("Question deleted!");
            }
        }

        private void ReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.Content = new StartMenuView();
        }
    }
}

