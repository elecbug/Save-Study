
using System.Diagnostics;

/// Throw
/// 란?
/// 고의로 오류를 만드는 거 => 프로그래머가 의도하여 익셉션을 발생시키는 구문
/// 
class Program
{
    public static void Main(string[] args)
    {
        int grade;

        while (true)
        {
            string gr = Console.ReadLine()!;

            try
            {
                grade = int.Parse(gr);

                if (grade < 0 || grade > 100)
                {
                    //Console.WriteLine("점수가 올바르지 않습니다.");
                    //continue;

                    throw new Exception();
                }

                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine("점수가 올바르지 않습니다.");
                Debug.WriteLine(ex);
            }
        }

        switch (grade / 10)
        {
            case 10: Console.WriteLine("A"); break;
            case 9: Console.WriteLine("A"); break;
            case 8: Console.WriteLine("B"); break;
            case 7: Console.WriteLine("C"); break;
            default: Console.WriteLine("D"); break;
        }

        Debug.WriteLine("Debug 창");
        throw new Exception("테스트 프로그램 끝날때 발생");
    }
}