// See https://aka.ms/new-console-template for more information

using The_Endgame.Models;

Console.WriteLine("Welcome to The Endgame!");

// Create heroes party
var heroes = new Party("Your party");
Console.WriteLine("What is your name? ");
var name = Console.ReadLine();
var player = new Character(name, Role.player, 25, new Punch());
heroes.Add(player);

// Create monster waves
List<Party> monsterWaves = new List<Party>();

// 1
var skeletonParty = new Party("Skeleton");
var skeleton = new Skeleton();
skeletonParty.Add(skeleton);
// monsterWaves.Add(skeletonParty);

// 2
var skeletonsParty = new Party("Skeletons");
var skeleton1 = new Skeleton();
var skeleton2 = new Skeleton();
skeletonsParty.Add(skeleton1);
skeletonsParty.Add(skeleton2);

// monsterWaves.Add(skeletonsParty);

// 3
var bossParty = new Party("Boss");
var boss = new Boss();
bossParty.Add(boss);
monsterWaves.Add(bossParty);

// Create and run battles
bool canBattle = true;

while (canBattle)
{
    for (var i = 0; i < monsterWaves.Count; i++)
    {   
        Console.WriteLine($"Wave {i + 1} start!");
        var wave = monsterWaves[i];
        var battle = new Battle(heroes, wave);
        battle.Run();
        if (heroes.partySize == 0) break;
    }
    canBattle = false;
}



