namespace SetJournal;

public class Questionnair
{
    int Count{get;set;} //for the amount of questions
    IQuestion Current{get;set;} //for the current question
    List<IQuestion> questions{get;set;}
}

public class MultipleChoice : IQuestion
{
    public string Ask{get;set;} //The question that will be asked
    public string Awnser{get;set;} //The Awnser that you will give
    int Choices{get;set;} //The Amount of choices

    public void PutAnwser()//Multiple Choice will have a number for each option and hopefully I can just have it make so you just push the number key
    {

    }
}

public class FreeResponse : IQuestion
{
    public string Ask{get;set;} //The Question that will be asked
    public string Awnser{get; set;} //The Awnser that you will give

    public void PutAnwser() //Free Response so setup should be something like just a Console.ReadLine();
    {

    }
}