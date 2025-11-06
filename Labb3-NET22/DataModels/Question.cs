namespace Labb3_NET22.DataModels;

public class Question
{
    public string Statement { get; set; }
    public string[] Answers { get; set; }
    public int CorrectAnswer { get; set; }

    
    public Question() { }

    
    public Question(string statement, string[] answers, int correctAnswer)
    {
        Statement = statement;
        Answers = answers;
        CorrectAnswer = correctAnswer;
    }
}
