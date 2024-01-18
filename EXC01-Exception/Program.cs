/// 익셉션? 
/// 간단하게 말해서 에러
/// 런타임 에러
/// 에러 => 컴파일 에러 / 런타임 에러
/// 차이점?
/// 컴파일 에러: 돌릴때?
/// 런타임 에러: 돌리다가?
/// 맞음 ㅇㅇ
/// 코딩 >> 디버깅 >> 컴파일 >> 런타임
/// 익셉션 => 런타임 에러
/// 컴파일 에러 => 1학년 정도에서의 대부분의 에러
/// 실제 => 런타임 에러, 실행은 잘되는데 실행하고보니 뭔가 버튼을 딱 누르니까 꺼짐 < 이런거
/// C#에서는 런타임 에러를 종류별로 나눠놨음
/// 하나씩 확인
/// 

// NullReferenceException

/// 뭘까요?
/// C++, 자바 등에서는 NULLPointerException...
/// 포인터 & 객체 변수가 가르키는 실제 위치가 비어있는 상황
/// 

DirectoryInfo info = new DirectoryInfo("C:\\Users\\");
Console.WriteLine(info.FullName);

DirectoryInfo? nullInfo = null;
Console.WriteLine(nullInfo!.FullName);

