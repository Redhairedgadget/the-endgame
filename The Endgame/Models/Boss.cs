namespace The_Endgame.Models;

public class Boss: Character
{
    public Boss(): base("Boss", Role.ai, 15, new Unraveling()){}
}