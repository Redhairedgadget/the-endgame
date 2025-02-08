namespace The_Endgame.Models;

public class Battle
{
    private List<Character> heroes;
    private List<Character> monsters;
    private int rounds;

    public Battle(List<Character> heroes, List<Character> monsters, int rounds)
    {
        this.heroes = heroes;
        this.monsters = monsters;
        this.rounds = rounds;
    }

    private void _runPartyTurn(List<Character> party, List<Character> enemies)
    {
        foreach (var character in party)
        { 
            Console.WriteLine($"It's {character.name}'s turn...");
            character.Do(enemies);
            Console.WriteLine("");
            // Console.WriteLine($"{character.name} did {character.Do()}.\n");
        }
    }

    public void Run()
    {
        while (rounds > 0)
        {
            _runPartyTurn(heroes, monsters);
            _runPartyTurn(monsters, heroes);
            
            rounds--;
        }
    }
}