public interface IQuestion
{
    string Ask{get; set;}
    string Awnser{get; set;}

    public void PutAnwser(string Input);
}