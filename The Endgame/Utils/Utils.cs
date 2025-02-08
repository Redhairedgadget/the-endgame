namespace The_Endgame.Utils;

public class Utils
{
    public static T SelectEnum<T>() where T: Enum
    {
        while (true)
        {
            var enumValues = Enum.GetValues((typeof(T)));
            var enumCount = enumValues.Length;

            for (int i = 0; i < enumCount; i++)
            {
                Console.WriteLine($"{i}: {enumValues.GetValue(i)}");
            }
            
            // TODO: handle the case where user provices a non-numeric value
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice >= 0 && choice < enumCount)
            {
                return (T)enumValues.GetValue(choice);
            }
        }
    }
}