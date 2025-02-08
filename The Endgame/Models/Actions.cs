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
    void Execute(string executor, string target);
}
public class TargetedAction : Action, ITargetedAction
{
    public int hpChange { get; protected set; }
    public TargetedAction (string actionName, int hpChange) : base(actionName) { this.hpChange = hpChange; }
    
    public override void Execute(string executor)
    {
        throw new InvalidOperationException($"TargetedAction '{actionName}' cannot be executed without a target.");
    }
    
    public void Execute(string executor, string target)
    {
        Console.WriteLine($"{executor} used {actionName} on {target}.");
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
    public Attack(string attackName, int damage) : base(attackName, damage)
    {
        if (damage > 0)
        {
            throw new ArgumentException("Attack must have negative hpChange.");
        }
    }
    
    public void Execute(string executor, string target)
    {
        Console.WriteLine($"{executor} used {actionName} on {target}.");
    }
}

// Player Attack
public class Punch : Attack
{
    public Punch(): base("PUNCH", -5) { }
}

// Skeleton Attack
public class BoneCrunch : Attack
{
    public BoneCrunch() : base("BONE CRUNCH", -5) { }
}