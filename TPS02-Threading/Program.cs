public class Program
{
    private static int SUM;

    public static void Main(string[] args)
    {
        for (int i = 0; i < 1_000_000; i++)
        {
            SUM += 1;
        }

        Console.WriteLine(SUM);

        SumAndPrint();

        Thread thread = new Thread(SumAndPrint);

        thread.Start();

        thread.Join();

        Thread t1 = new Thread(SumAndPrint);
        Thread t2 = new Thread(SumAndPrint);

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();
    }

    public static void SumAndPrint()
    {
        for (int i = 0; i < 1_000_000; i++)
        {
            SUM += 1;
        }

        Console.WriteLine(SUM);
    }
}

/// Mission
/// What do you think is wrong?
/// How to solve this problem?