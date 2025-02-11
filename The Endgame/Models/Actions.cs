namespace The_Endgame.Models;

public abstract class Action
{
    public string actionName { get; }

    public Action (string actionName)
    {
        this.actionName = actionName;
    }
    
    public abstract void Execute(string executor);
}

public class TargetlessAction : Action
{
    public TargetlessAction (string actionName): base(actionName) { }

    public override void Execute(string executor)
    {
        Console.WriteLine($"{executor} did {actionName}");
    }
}

// Interface for Targeted Actions
public interface ITargetedAction
{
    void Execute(string executor, Character target);
}

public class TargetedActionData
{
    public int HPChange { get; set; }
    public double Probability { get; set; }
}
public class TargetedAction : Action, ITargetedAction
{
    private static Random _random = new Random();
    public TargetedActionData actionData { get; protected set; }
    // public int hpChange { get; protected set; }
    public TargetedAction(string actionName, int hpChange, double probablity) : base(actionName)
    {
        actionData = new TargetedActionData
        {
            HPChange = hpChange,
            Probability = probablity
        };
    }
    
    public override void Execute(string executor)
    {
        throw new InvalidOperationException($"TargetedAction '{actionName}' cannot be executed without a target.");
    }
    
    public void Execute(string executor, Character target)
    {
        Console.WriteLine($"{executor} used {actionName} on {target.name}.");
        var calculatedHpChange = _random.Next(100) < actionData.Probability ? actionData.HPChange : 0;

        if (calculatedHpChange != 0)
        {
            Console.WriteLine($"{target.name}'s HP changed by {calculatedHpChange}.");
            target.changeHP(calculatedHpChange);
            Console.WriteLine($"{target.name} is now at {target.currentHP}/{target.maxHP}HP");
        }
        else
        {
            Console.WriteLine($"{executor} missed!");
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
    public Attack(string attackName, int damage, double propability) : base(attackName, damage, propability)
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