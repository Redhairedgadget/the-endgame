// See https://aka.ms/new-console-template for more information

using The_Endgame.Models;

Console.WriteLine("Welcome to The Endgame!");

// Create heroes party
var heroes = new List<Character>();
Console.WriteLine("What is your name? ");
var name = Console.ReadLine();
var player = new Character(name, Role.player);
heroes.Add(player);

// Create monsters party
var monsters = new List<Character>();
var skeleton = new Character("Skeleton", Role.ai);
monsters.Add(skeleton);

Console.WriteLine($"Two characters were created:  {player.name}, {skeleton.name}.");

// Create battle
Console.WriteLine("How many rounds? ");

var rounds = Convert.ToInt32(Console.ReadLine());
var battle = new Battle(heroes, monsters, rounds);
battle.Run();


