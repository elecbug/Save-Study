if (File.Exists("C:\\test\\test.txt"))
{
    StreamReader stream = new StreamReader("C:\\test\\test.txt");
    string data = stream.ReadToEnd();
    Console.WriteLine(data);
}
else
{
    File.Create("C:\\test\\test.txt");
}

/// 지금 상황은 프로그래머가 익셉션이 발생할 상황을 미리 생각해서 if로 캐치한 경우
/// 하지만 실제로는 프로그래머가 모든 상황을 예측할 수는 없음
/// try-catch라는 것을 지원해줌