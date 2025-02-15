namespace The_Endgame.Models;

using Utils;

public class Character
{
    // TODO: characters should have unique ID, as name could be repeated
    public string name { get; private set; }
    private Role role;
    public int maxHP { get; private set; }
    public int currentHP { get; private set; }
    public TargetlessAction pass { get; private set; }
    public TargetedAction attack { get; private set; }
    
    public Character(string name, Role role, int hp, Attack attack)
    {
        this.name = name;
        this.role = role;
        this.maxHP = hp;
        this.currentHP = hp;
        this.pass = new Nothing();
        this.attack = attack;
    }

    private Character _chooseTarget(List<Character> enemies)
    {
        Console.WriteLine("Choose target: ");
        for (int i = 0; i < enemies.Count; i++)
        {
            Console.WriteLine($"{i}: {enemies[i].name}");
        }

        while (true)
        {
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice >= 0 && choice < enemies.Count)
            {
                return enemies[choice];
            }
            Console.WriteLine("Please enter a valid choice.");
        }
    }

    private void _playerChoosesAction(Party enemies)
    {
        Console.WriteLine("What would you like to do?");
        ActionType choice = Utils.SelectEnum<ActionType>();
        
        if (choice is ActionType.Attack)
        {
            Character target = _chooseTarget(enemies.characters);
            bool enemyDead = attack.Execute(name, target);
            if (enemyDead) enemies.Remove(target);
        }
        else
        {
            pass.Execute(name);
        }
    }
    
    // TODO: perhaps there is a better way to pass enemies (recheck when selection of enemy is implemented)
    public void Do(Party enemies)
    {
        // TODO: this will need to be outsourced in its own function during ai implementation
        
        // Choose attacks and propagate effects on targets
        if (role == Role.ai)
        {
            Character firstCharacter = enemies.characters[0];
            bool charDead = attack.Execute(name, firstCharacter);
            if (charDead) enemies.Remove(firstCharacter);
        }
        else _playerChoosesAction(enemies);
    }

    public void changeHP(int hpChange)
    {
        currentHP = Math.Max(0, currentHP + hpChange);
    }
}

public enum Role {player, ai};
public enum ActionType { Pass, Attack }