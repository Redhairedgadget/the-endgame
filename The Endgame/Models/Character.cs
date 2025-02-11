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
    
    // TODO: actions should be separate and depend on character's type
    private void _populateSkeletonActions()
    {
        pass = new Nothing();
        attack = new BoneCrunch();
    }

    private void _populatePlayerActions()
    {
        pass = new Nothing();
        attack = new Punch();
    }
    public Character(string name, Role role, int hp)
    {
        this.name = name;
        this.role = role;
        this.maxHP = hp;
        this.currentHP = hp;

        switch (role)
        {
            case Role.player:
                _populatePlayerActions();
                break;
            case Role.ai:
                _populateSkeletonActions();
                break;
        }
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

    private void _playerChoosesAction(List<Character> enemies)
    {
        Console.WriteLine("What would you like to do?");
        ActionType choice = Utils.SelectEnum<ActionType>();
        
        if (choice is ActionType.Attack)
        {
            Character target = _chooseTarget(enemies);
            attack.Execute(name, target);
        }
        else
        {
            pass.Execute(name);
        }

    }
    
    // TODO: perhaps there is a better way to pass enemies (recheck when selection of enemy is implemented)
    public void Do(List<Character> enemies)
    {
        if (role == Role.ai) attack.Execute(name, enemies[0]);
        else _playerChoosesAction(enemies);
    }

    public void changeHP(int hpChange)
    {
        currentHP = Math.Max(0, currentHP + hpChange);
    }
}

public enum Role {player, ai};
public enum ActionType { Pass, Attack }