using System.Reflection;

namespace SetJournal;

public class Questionnair
{
    public List<IQuestion> Questions{get; set;} // just using a list to store all the questions. It has built in stuff that I really like for this project.
    string? Name{get; set;}
    int Current{get; set;} // to go throught the questions displaying each one also for pulling up certain questions by index
    public Questionnair()
    {
        Name = "Unamed";
        Questions = new List<IQuestion>();
    }

    public void AddQuestions()
    {
        Console.WriteLine("What type of question would you like to add 1(Free Response) 2(Multiple Choice)."); 
        string? Input = "";
        while (Input == "")
        {
            Input = Console.ReadLine();
            if(Input == "")
            {
                Console.WriteLine("not an option");
                Console.WriteLine("What type of question would you like to add 1(Free Response) 2(Multiple Choice)."); 
            }
        }
        
        try
        {
            int choice = Convert.ToInt32(Input);
            if(choice == 1)
            {
                AddQuestions(FreeResponseFactory.freeResponse());
            }else if(choice == 2)
            {
                AddQuestions(MultipleChoiceFactory.MultipleChoice());
            }else
            {
                Console.WriteLine("Not an option");
                AddQuestions();
            }
        }catch(FormatException)
        {
            Console.WriteLine("not an option");
            AddQuestions();
        }
        
    }

    public void AddQuestions(params IQuestion[] questions)
    {
        Questions.AddRange(questions.ToList());
    }

    public void RunQuestionnair()
    {
        for(int i = 0; i < Questions.Count; i++)
        {
            Console.WriteLine(Questions[i].Ask);
            string? awners = Console.ReadLine();
            while(awners == null)
            {
                awners = Console.ReadLine();
            }
            Questions[i].PutAnwser(awners);
        }
        AddAllToFile();
    }
    public void AddAwnsersToFile()
    {
        IEnumerable<IQuestion> questions = from o in Questions where o.Awnser != "" select o;
        string AwnserString = "";
        foreach (IQuestion question in questions)
        {
            AwnserString += $"{question.Ask} \n {question.Awnser} \n \n"; // one space between the awnsers 2 spaces between each question.
        }
        Console.WriteLine("Would you like to name this journal?");
        if(Console.ReadLine() == "yes")
        {
            
            while(Name == null || Name == "")
            {
                Console.WriteLine("What would you like its name to be(can not be nothing)?");
                Name = Console.ReadLine();
            }
            
        }
        string path = $"{Name} {getDate()}.txt";
        File.Create(path);
        File.WriteAllText(path, AwnserString); // create a new file with the name and the date month/day/year 
    }
    public void AddAllToFile() // for just adding all of the questions to a file
    {
        string AwnserString = "";
        foreach(IQuestion question in Questions)
        {
            AwnserString += $"{question.Ask} \n {question.Awnser} \n \n";
        }
        Console.WriteLine("Would you like to name this journal?");
        if(Console.ReadLine() == "yes")
        {
            Name = "";
            while(Name == null || Name == "")
            {
                Console.WriteLine("What would you like its name to be(can not be nothing)?");
                Name = Console.ReadLine();
            }
            
        }

        string date = getDate();
        string extention = ".txt";
        string fileName = $"{Name}_{date}{extention}";
        File.WriteAllText(fileName, AwnserString);
    }
    public string getDate()
    {
        return DateTime.Now.ToString("yyyyMMdd");
    }

    public void GetQuestionnair() //Making so you can just get a questionnair from a file and use it. So you can change your awnser from previouse trades
    {

    }
}

public class MultipleChoice : IQuestion
{
    public string Ask{get;set;} //The question that will be asked
    public string Awnser{get;set;} //The Awnser that you will give
    List<string> Awnsers{get;set;} // Awnsers.count will be the max amount of choices

    public MultipleChoice(string ask, params string[] awnsers)
    {
        Ask = ask;
        Awnsers = awnsers.ToList();
        for(int i = 0; i < Awnsers.Count; i++)
        {
            Ask += $" Option {i+1}{awnsers[i]}"; 
        }
        Awnser = ""; // sets awnsers to just a blank for now this is dealing with null
    }

    public void PutAnwser(string Input = "")//Multiple Choice will have a number for each option and hopefully I can just have it make so you just push the number key
    {
        try
        {
            if (Input == null || Input == "next") // dealing with null
            {
                Awnser = "";
            }else
            {
                int choice = Convert.ToInt32(Input);
                Awnser = Awnsers[choice -1]; // The choices are usually numberd 1 - max lists start go from 0-max
            }
        }catch(FormatException) // if the question is not in the right format it should ask it again.
        {
            PutAnwser(Input);
        }
    }
}

public class FreeResponse : IQuestion
{
    public string Ask{get;set;} //The Question that will be asked
    public string Awnser{get; set;} //The Awnser that you will give

    public FreeResponse(string ask)
    {
        Ask = ask;
        Awnser = ""; // set the awnser empty for now later in the questionair the question will be awnsered.
    }

    public void PutAnwser(string Input = "") //Free Response so setup should be something like just a Console.ReadLine();
    {
        if(Input == null || Input == "next")
        {
            Awnser = "";
        }else
        {
            Awnser = Input;
        }
    }
}

static public class FreeResponseFactory
{
    static string? _ask{get;set;}
    static public void buildQuestion()
    {
        _ask = "";
        Console.WriteLine("What would you like this question to ask?");
        _ask = Console.ReadLine();
    }

    static public IQuestion freeResponse()
    {
        _ask = "";
        while(_ask == "" || _ask == null)
        {
            buildQuestion();
        }
        return new FreeResponse(_ask);
    }
}

public static class MultipleChoiceFactory
{
    static string? _ask{get;set;}
    static List<string> options = new List<string>();
    static public void buildQuestion()
    {
        Console.WriteLine("What would you like this question to ask?");
        while(_ask == null || _ask == "")
        {
            _ask = Console.ReadLine();
        }


        while(true)
        {
            Console.WriteLine("What would you like to add as an option? (must add at least 2) enter 'no more' when you do not want to add any more options");
            string? option = Console.ReadLine();
            while(option == "")
            {
                option = Console.ReadLine();
                Console.WriteLine("You must enter something");
            }

            if(options.Count > 1 && option == "no more")
            {
                break;
            }else
            {
                try
                {
                    if(option == null)
                    {
                        throw new Exception ("No option provided somehow");
                    }
                    options.Add(option);
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
            }   
        }
    }
    static public IQuestion MultipleChoice()
    {
        if(_ask == null || _ask == "")
        {
            buildQuestion();
        }
        return new MultipleChoice(_ask, options.ToArray());
    }
}