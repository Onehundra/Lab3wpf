using Labb3_NET22.DataModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Labb3_NET22
{
    /// <summary>
    /// Interaction logic for PlayQuizView.xaml
    /// </summary>
    public partial class PlayQuizView : UserControl
    {
        private Quiz currentQuiz;
        private List<Question> questions;
        private int currentIndex = 0;
        private int correctAnswers = 0;

        public PlayQuizView(Quiz quiz)
        {
            InitializeComponent();
            currentQuiz = quiz;
            questions = quiz.Questions.ToList();
            ShowQuestion();
        }

        private void ShowQuestion()
        {
            if (currentIndex < questions.Count)
            {
                var q = questions[currentIndex];
                QuestionText.Text = q.Statement;

                AnswerBtn1.Content = q.Answers[0];
                AnswerBtn2.Content = q.Answers[1];
                AnswerBtn3.Content = q.Answers[2];
                AnswerBtn4.Content = q.Answers[3];

                AnswerBtn1.IsEnabled = true;
                AnswerBtn2.IsEnabled = true;
                AnswerBtn3.IsEnabled = true;
                AnswerBtn4.IsEnabled = true;

                ResultText.Text = "";

                double percent;
                if (currentIndex == 0)
                {
                    percent = 0;
                }
                else
                {
                    percent = (double)correctAnswers / currentIndex * 100;
                }
                ScoreText.Text = $"Score: {correctAnswers}/{currentIndex} ({percent:F1}%)";
            }
            else
            {
                QuestionText.Text = "Quiz Complete!";
                AnswerBtn1.Visibility = Visibility.Collapsed;
                AnswerBtn2.Visibility = Visibility.Collapsed;
                AnswerBtn3.Visibility = Visibility.Collapsed;
                AnswerBtn4.Visibility = Visibility.Collapsed;
                NextButton.Visibility = Visibility.Collapsed;

                double finalPercent = (double)correctAnswers / questions.Count * 100;
                ScoreText.Text = $"Final Score: {correctAnswers}/{questions.Count} ({finalPercent:0}%)";
            }
        }

        private void Answer_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int selectedIndex = int.Parse(button.Tag.ToString());
            var q = questions[currentIndex];

            if (selectedIndex == q.CorrectAnswer)
            {
                correctAnswers++;
                ResultText.Text = "Correct!";
                ResultText.Foreground = Brushes.Green;
            }
            else
            {
                ResultText.Text = $"Wrong! Correct: {q.Answers[q.CorrectAnswer]}";
                ResultText.Foreground = Brushes.Red;
            }

            double percent = ((double)correctAnswers / (currentIndex + 1)) * 100;
            ScoreText.Text = $"Score: {correctAnswers}/{currentIndex + 1} ({percent:F1}%)";

            // Stäng av knappar tills man går vidare
            AnswerBtn1.IsEnabled = false;
            AnswerBtn2.IsEnabled = false;
            AnswerBtn3.IsEnabled = false;
            AnswerBtn4.IsEnabled = false;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            currentIndex++;
            ShowQuestion();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.Content = new StartMenuView();
        }
    }
}
