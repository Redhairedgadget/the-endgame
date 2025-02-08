namespace The_Endgame.Models;

using Utils;

public class Character
{
    public string name { get; private set; }
    private Role role;
    public TargetlessAction pass { get; private set; }
    public TargetedAction attack { get; private set; }
    
    // TODO: actions should be separate and depend on character's status
    private Dictionary<int, ActionType> ACTION_DICT = new Dictionary<int, ActionType>()
    {
        {0, ActionType.Pass},
        {1, ActionType.Attack}
    };

    private void _populateSkeletonActions()
    {
        pass = new Nothing();
        attack = new BoneCrunch();
        // ACTION_DICT = new Dictionary<int, Action>()
        // {
        //     { 0, new Nothing() },
        //     { 1, new BoneCrunch() },
        // };
    }

    private void _populatePlayerActions()
    {
        pass = new Nothing();
        attack = new Punch();
        // ACTION_DICT = new Dictionary<int, Action>()
        // {
        //     { 0, new Nothing() },
        //     { 1, new Punch() },
        // };
    }
    public Character(string name, Role role)
    {
        this.name = name;
        this.role = role;

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

    private void _playerChoosesAction(List<Character> enemies)
    {
        Console.WriteLine("What would you like to do?");
        ActionType choice = Utils.SelectEnum<ActionType>();
        
        if (choice is ActionType.Attack)
        {
            // TODO: implement selecting a specific enemy
            attack.Execute(name, enemies[0].name);
        }
        else
        {
            pass.Execute(name);
        }

    }
    
    // TODO: perhaps there is a better way to pass enemies (recheck when selection of enemy is implemented)
    public void Do(List<Character> enemies)
    {
        if (role == Role.ai) attack.Execute(name, enemies[0].name);
        else _playerChoosesAction(enemies);
    }
}

public enum Role {player, ai};
public enum ActionType { Pass, Attack }