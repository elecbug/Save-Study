public partial class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("[DES|AES] [ENC|DEC] [ECB|CBC|CFB] [File name] [Your password]");

            string command = Console.ReadLine() ?? "";
            string[] cmds = command.ToLower().Split(' ');

            Algorithm algorithm;
            Rotate rotate;
            Mode mode;
            FileStream stream;
            string password;

            try
            {
                switch (cmds[0])
                {
                    case "des": algorithm = Algorithm.DES; break;
                    case "aes": algorithm = Algorithm.AES; break;

                    default: throw new ArgumentException();
                }

                switch (cmds[1])
                {
                    case "enc": rotate = Rotate.Encrypt; break;
                    case "dec": rotate = Rotate.Decrypt; break;

                    default: throw new ArgumentException();
                }

                switch (cmds[2])
                {
                    case "ecb": mode = Mode.ECB; break;
                    case "cbc": mode = Mode.CBC; break;
                    case "cfb": mode = Mode.CFB; break;

                    default: throw new ArgumentException();
                }

                stream = File.OpenRead(cmds[3]);

                password = cmds[4];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                continue;
            }

            SecureService service = new SecureService(algorithm, rotate, mode, stream, password);
            service.Run();
        }
    }
}