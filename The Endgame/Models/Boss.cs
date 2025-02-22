namespace The_Endgame.Models;

public class Boss: Character
{
    public Boss(Role role): base("Boss", role, 15, new Unraveling()){}
}