namespace The_Endgame.Models;

public abstract class Action
{
    public string actionName { get; }

    public Action (string actionName)
    {
        this.actionName = actionName;
    }
    
    public abstract bool Execute(string executor);
}

public class TargetlessAction : Action
{
    public TargetlessAction (string actionName): base(actionName) { }

    public override bool Execute(string executor)
    {
        Console.WriteLine($"{executor} did {actionName}");
        return false;
    }
}

// Interface for Targeted Actions
public interface ITargetedAction
{
    bool Execute(string executor, Character target);
}

public class TargetedActionData
{
    public int HPChange { get; set; }
    public double Probability { get; set; }
    public bool Random { get; set; }
}
public class TargetedAction : Action, ITargetedAction
{
    private static Random _random = new Random();
    public TargetedActionData actionData { get; protected set; }
    // public int hpChange { get; protected set; }
    public TargetedAction(string actionName, int hpChange, int probablity, bool random) : base(actionName)
    {
        actionData = new TargetedActionData
        {
            HPChange = hpChange,
            Probability = probablity,
            Random = random
        };
    }
    
    public override bool Execute(string executor)
    {
        throw new InvalidOperationException($"TargetedAction '{actionName}' cannot be executed without a target.");
    }
    
    public bool Execute(string executor, Character target)
    {
        Console.WriteLine($"{executor} used {actionName} on {target.name}.");
        // Calculate HP change
        // Probability
        var calculatedHpChange = _random.Next(100) < actionData.Probability ? actionData.HPChange : 0;
        
        // Random strength
        if (actionData.Random)
        {
            calculatedHpChange = _random.Next(calculatedHpChange, 1);
        }
        
        // TODO: perhaps it is better to outsource changes to separate function (hpChange, etc.)
        if (calculatedHpChange != 0)
        {
            Console.WriteLine($"{target.name}'s HP changed by {calculatedHpChange}.");
            target.changeHP(calculatedHpChange);
            Console.WriteLine($"{target.name} is now at {target.currentHP}/{target.maxHP}HP");

            if (target.currentHP == 0)
            {
                Console.WriteLine($"{target.name} has been defeated!");
                return true;
            }
            return false;
        }
        else
        {
            Console.WriteLine($"{executor} missed!");
            return false;
        }
    }
}


// Do Nothing
public class Nothing : TargetlessAction
{
    public Nothing(): base("NOTHING") { }
}

// Attack
public class Attack : TargetedAction
{
    public Attack(string attackName, int damage, int probability, bool random = false) : base(attackName, damage, probability, random)
    {
        if (damage > 0)
        {
            throw new ArgumentException("Attack must have negative hpChange.");
        }
    }
}

// Player Attack
public class Punch : Attack
{
    public Punch(): base("PUNCH", -1, 100) { }
}

// Skeleton Attack
public class BoneCrunch : Attack
{
    public BoneCrunch() : base("BONE CRUNCH", -1, 50) { }
}

// Boss Attack
public class Unraveling : Attack
{
    public Unraveling() : base("UNRAVELING", -2, 100, true) { }
}