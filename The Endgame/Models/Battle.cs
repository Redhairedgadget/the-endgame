namespace The_Endgame.Models;

public class Battle
{
    private Party heroes;
    private Party monsters;
    // private int rounds;
    private bool isOver;

    public Battle(Party heroes, Party monsters)
    {
        this.heroes = heroes;
        this.monsters = monsters;
        // this.rounds = rounds;
        isOver = false;
    }

    private void _runPartyTurn(Party party, Party enemies)
    {
        foreach (var character in party.characters)
        { 
            Console.WriteLine($"It's {character.name}'s turn...");
            character.Do(enemies);
            Console.WriteLine("");
            
            // Check if party still exists
            if (enemies.partySize == 0)
            {
                isOver = true;
                Console.WriteLine($"{enemies.partyName} has been defeated. {party.partyName} have won!");
                break;
            }
        }
    }

    public void Run()
    {
        bool heroesTurn = true;
        while (!isOver)
        {
            if (heroesTurn) _runPartyTurn(heroes, monsters);
            else _runPartyTurn(monsters, heroes);
            
            heroesTurn = !heroesTurn;
            // rounds--;
        }
    }
}