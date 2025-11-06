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

        private void LoadQuiz_Click(object sender, RoutedEventArgs e)
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

                string json = System.IO.File.ReadAllText(dialog.FileName);
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
                var q = loadedQuiz.Questions.ElementAt(QuestionListBox.SelectedIndex);
                EditQuestionBox.Text = q.Statement;
                EditAnswer1Box.Text = q.Answers[0];
                EditAnswer2Box.Text = q.Answers[1];
                EditAnswer3Box.Text = q.Answers[2];
                EditAnswer4Box.Text = q.Answers[3];
                EditCorrectBox.Text = q.CorrectAnswer.ToString();
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionListBox.SelectedIndex < 0) return;

            int index = QuestionListBox.SelectedIndex;
            var list = loadedQuiz.Questions.ToList();

            var newQ = new Question(
                EditQuestionBox.Text,
                new string[]
                {
                    EditAnswer1Box.Text,
                    EditAnswer2Box.Text,
                    EditAnswer3Box.Text,
                    EditAnswer4Box.Text
                },
                int.Parse(EditCorrectBox.Text)
            );

            list[index] = newQ;

            loadedQuiz = new Quiz();
            
            
            
            foreach (var q in list)
                loadedQuiz.AddQuestion(q.Statement, q.CorrectAnswer, q.Answers);

            File.WriteAllText(currentFilePath, JsonSerializer.Serialize(loadedQuiz, new JsonSerializerOptions { WriteIndented = true }));

            QuestionListBox.Items[index] = newQ.Statement;

            MessageBox.Show("Question updated!");
        }

        private void DeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionListBox.SelectedIndex >= 0)
            {
                var list = loadedQuiz.Questions.ToList();

                list.RemoveAt(QuestionListBox.SelectedIndex);
                
                
                loadedQuiz = new Quiz();
                foreach (var q in list)
                    loadedQuiz.AddQuestion(q.Statement, q.CorrectAnswer, q.Answers);

                File.WriteAllText(currentFilePath, JsonSerializer.Serialize(loadedQuiz, new JsonSerializerOptions { WriteIndented = true }));
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

