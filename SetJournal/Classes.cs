namespace SetJournal;

public class Questionnair
{
    int Count{get;set;}
    IQuestion Current{get;set;}
}

public class MultpleChoice : IQuestion
{
    public string Ask{get;set;}
    public string Awnser{get;set;}
    int Choices{get;set;}

    public void PutAnwser()
    {

    }
}

public class FreeResponse : IQuestion
{
    public string Ask{get;set;}
    public string Awnser{get; set;}

    public void PutAnwser()
    {
        
    }
}