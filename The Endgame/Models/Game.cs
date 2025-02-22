namespace The_Endgame.Models;
using Utils;

public class Game
{
    private GameMode mode;
    private List<Party> waves = new ();
    private Party heroes = new ("Your party");
    private bool gameOver = false;
    
    private void _initializeHeroes () {
        // Create heroes party
        Console.WriteLine("What is your name? ");
        string name = Console.ReadLine();
        Role playerRole = mode == GameMode.cvc ? Role.ai : Role.player;
        Character player = new (name, playerRole, 25, new Punch());
        heroes.Add(player);
    }

    private void _addWaves()
    {
        Role wavesRole = mode == GameMode.pvp ? Role.player: Role.ai;
        // 1
        var skeletonParty = new Party("Skeleton");
        var skeleton = new Skeleton(wavesRole);
        skeletonParty.Add(skeleton);
        waves.Add(skeletonParty);

        // 2
        var skeletonsParty = new Party("Skeletons");
        var skeleton1 = new Skeleton(wavesRole);
        var skeleton2 = new Skeleton(wavesRole);
        skeletonsParty.Add(skeleton1);
        skeletonsParty.Add(skeleton2);
        waves.Add(skeletonsParty);

        // 3
        var bossParty = new Party("Boss");
        var boss = new Boss(wavesRole);
        bossParty.Add(boss);
        waves.Add(bossParty);
    }

    public Game()
    {
        Console.WriteLine("Welcome to the Endgame Game!");
        Console.WriteLine("Please select the game mode:");
        GameMode mode = Utils.SelectEnum<GameMode>();
        this.mode = mode;
        
        _initializeHeroes();
        _addWaves();
    }

    public void play()
    {
        while (!gameOver)
        {
            for (var i = 0; i < waves.Count; i++)
            {   
                Console.WriteLine($"Wave {i + 1} start!");
                var wave = waves[i];
                var battle = new Battle(heroes, wave);
                battle.Run();
                if (heroes.partySize == 0) break;
            }
            gameOver = true;
        }
    }
}


enum GameMode { pvc, cvc, pvp }