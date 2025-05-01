using SetJournal;

Questionnair newTrade = new Questionnair();

Console.Clear();
Console.WriteLine("This program builds a questionnair then you awnser all the questions. After you awnser the questions it adds it to saves it to file. (press any key to continue)");
Console.ReadKey(true);
string? addMore = "";
int questionsAdded = 0;
while(true)
{
    Console.Clear();
    Console.WriteLine("Would you like to add a question to the questionnair (you must add more than 4)");
    addMore = Console.ReadLine();
    if(addMore == "yes")
    {
        newTrade.AddQuestions();
        questionsAdded ++;
    }else if(addMore == "no")
    {
        if(newTrade.Questions.Count > 3)
        {
            break;
        }
        break;
    }else
    {

    }
}

newTrade.RunQuestionnair();