namespace EvalFunction
{
    public class Program
    {

        // 15 * 6 + 16 - 18
        // 15 6 16 18 - + *

        // (15 * 6) + 16 - 18
        // (15 6 *) 16 18 - +

        // ((15 * 6) + 16) - 18
        // (15 6 *) 16 18 - +

        static void Main(string[] args)
        {
           Console.WriteLine(MathCore.Evauluate("((((15 * 6)) + 16) - 18)"));
            Console.ReadKey();
        }
    }
}