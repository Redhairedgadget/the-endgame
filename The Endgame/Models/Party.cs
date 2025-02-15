namespace The_Endgame.Models;

public class Party
{
    public List<Character> characters { get; private set; }
    public double partySize { get; private set; }
    public string partyName { get; private set; }

    public Party(string name)
    {
        characters = new List<Character>();
        partySize = 0;
        partyName = name;
    }

    public void Add(Character character)
    {
        characters.Add(character);
        partySize++;
    }

    public void Remove(Character character)
    {
        characters.Remove(character);
        partySize--;
    }
}