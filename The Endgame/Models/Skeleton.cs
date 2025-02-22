namespace The_Endgame.Models;

public class Skeleton: Character
{
    public Skeleton(Role role) : base("Skeleton", role, 5, new BoneCrunch()){}
}