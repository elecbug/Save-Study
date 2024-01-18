string path = "C:\\temp\\temp.txt";

long tick = DateTime.Now.Ticks;

for (int i = 0; i < 10; i++)
{
    if (File.Exists(path))
    {
        Console.WriteLine(new StreamReader(path).ReadToEnd());
    }
    else
    {
        continue;
    }
}

Console.WriteLine(DateTime.Now.Ticks - tick); // 50000 ~ 60000 // 문제, 처음에는 150000이 걸림.

long tick2 = DateTime.Now.Ticks;

for (int i = 0; i < 10; i++)
{
    try
    {
        Console.WriteLine(new StreamReader(path).ReadToEnd());
    }
    catch
    {
        continue;
    }
}

Console.WriteLine(DateTime.Now.Ticks - tick2);