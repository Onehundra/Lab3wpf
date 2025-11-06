using System;
using System.Collections.Generic;

namespace Labb3_NET22.DataModels;

public class Quiz
{
        public List<Question> Questions { get; set; } = new List<Question>();
        public string Title { get; set; } = "Quiz";
        public Quiz() { }

        
        public Quiz(bool includeDefaultQuestions)
        {
            if (includeDefaultQuestions)
            {
                Questions.Add(new Question(
                    "1. In 1768, Captain James Cook set out to explore which ocean?",
                    new string[] { "Pacific Ocean", "Atlantic Ocean", "Indian Ocean", "Arctic Ocean" },
                    0));

                Questions.Add(new Question(
                    "2. What is actually electricity?",
                    new string[] { "A flow of water", "A flow of air", "A flow of electrons", "A flow of atoms" },
                    2));

                Questions.Add(new Question(
                    "3. Which of the following is not an international organisation?",
                    new string[] { "FIFA", "NATO", "ASEAN", "FBI" },
                    3));

                Questions.Add(new Question(
                    "4. Which of the following disorders is the fear of being alone?",
                    new string[] { "Agoraphobia", "Aerophobia", "Acrophobia", "Arachnophobia" },
                    0));

                Questions.Add(new Question(
                    "5. Which of the following is a song by the German heavy metal band 'Scorpions'?",
                    new string[] { "Stairway to Heaven", "Wind of Change", "Don’t Stop Me Now", "Hey Jude" },
                    1));

                Questions.Add(new Question(
                    "6. What is the speed of sound?",
                    new string[] { "120 km/h", "1,200 km/h", "400 km/h", "700 km/h" },
                    1));

                Questions.Add(new Question(
                    "7. Which is the easiest way to tell the age of many trees?",
                    new string[] { "Measure the width", "Count the rings on the trunk", "Count the leaves", "Measure the height" },
                    1));

                Questions.Add(new Question(
                    "8. What do we call a newly hatched butterfly?",
                    new string[] { "A moth", "A butter", "A caterpillar", "A chrysalis" },
                    2));

                Questions.Add(new Question(
                    "9. In total, how many novels were written by the Bronte sisters?",
                    new string[] { "4", "5", "6", "7" },
                    3));

                Questions.Add(new Question(
                    "10. Which did Viking people use as money?",
                    new string[] { "Rune stones", "Jewellery", "Seal skins", "Wool" },
                    1));
            }
        }

        public void SetTitle(string title)
    {
        Title = title;
    }

    public Question GetRandomQuestion()
    {
        if (Questions.Count == 0)
            throw new InvalidOperationException("No questions available.");

        Random rnd = new Random();
        int index = rnd.Next(Questions.Count);
        return Questions[index];
    }

    public void AddQuestion(string statement, int correctAnswer, params string[] answers)
    {
        var question = new Question
        {
            Statement = statement,
            Answers = answers,
            CorrectAnswer = correctAnswer
        };

        Questions.Add(question);
    }

    public void RemoveQuestion(int index)
    {
        if (index >= 0 && index < Questions.Count)
            Questions.RemoveAt(index);
    }
}
