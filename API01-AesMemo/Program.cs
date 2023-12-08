using System.Security.Cryptography;
using System.Text;

public class Program
{
    public enum Algorithm
    {
        DES,
        AES,
    }
    public enum Rotate
    {
        Encrypt,
        Decrypt,
    }
    public enum Mode
    {
        ECB,
        CBC,
        CFB,
    }

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

            while (true)
            {
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

                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    continue;
                }
            }

            SecureService service = new SecureService(algorithm, rotate, mode, stream, password);
        }
    }

    public class SecureService
    {
        public SecureService(Algorithm algorithm, Rotate rotate, Mode mode, FileStream stream, string password)
        {
            byte[] hashedPass = SHA512.HashData(Encoding.UTF8.GetBytes(password));
            using BinaryReader reader = new BinaryReader(stream);
            byte[] plain = reader.ReadBytes((int)stream.Length);
            byte[] result;

            switch (algorithm)
            {
                case Algorithm.DES:
                    {
                        TripleDES des = TripleDES.Create();
                        des.Key = hashedPass[0..24];

                        if (rotate == Rotate.Encrypt && mode == Mode.ECB)
                        {
                            result = des.EncryptEcb(plain, PaddingMode.PKCS7);
                        }
                        else if (rotate == Rotate.Decrypt && mode == Mode.ECB)
                        {
                            result = des.DecryptEcb(plain, PaddingMode.PKCS7);
                        }
                        else if (rotate == Rotate.Encrypt && mode == Mode.CBC)
                        {
                            result = des.EncryptCbc(plain, hashedPass[(64 - 8)..64]);
                        }
                        else if (rotate == Rotate.Decrypt && mode == Mode.CBC)
                        {
                            result = des.DecryptCbc(plain, hashedPass[(64 - 8)..64]);
                        }
                        else if (rotate == Rotate.Encrypt && mode == Mode.CFB)
                        {
                            result = des.EncryptCfb(plain, hashedPass[(64 - 8)..64]);
                        }
                        else if (rotate == Rotate.Decrypt && mode == Mode.CFB)
                        {
                            result = des.DecryptCfb(plain, hashedPass[(64 - 8)..64]);
                        }
                        else
                        {
                            return;
                        }

                        string name = stream.Name.Split('.').Last() == "enc" ?
                            stream.Name[0..(stream.Name.Length - 4)] : stream.Name + ".enc";

                        using BinaryWriter writer = new BinaryWriter(File.Create(name));
                        writer.Write(result);
                    }
                    break;
                case Algorithm.AES:
                    {
                        Aes aes = Aes.Create();
                        aes.Key = hashedPass[0..32];

                        if (rotate == Rotate.Encrypt && mode == Mode.ECB)
                        {
                            result = aes.EncryptEcb(plain, PaddingMode.PKCS7);
                        }
                        else if (rotate == Rotate.Decrypt && mode == Mode.ECB)
                        {
                            result = aes.DecryptEcb(plain, PaddingMode.PKCS7);
                        }
                        else if (rotate == Rotate.Encrypt && mode == Mode.CBC)
                        {
                            result = aes.EncryptCbc(plain, hashedPass[(64 - 16)..64]);
                        }
                        else if (rotate == Rotate.Decrypt && mode == Mode.CBC)
                        {
                            result = aes.DecryptCbc(plain, hashedPass[(64 - 16)..64]);
                        }
                        else if (rotate == Rotate.Encrypt && mode == Mode.CFB)
                        {
                            result = aes.EncryptCfb(plain, hashedPass[(64 - 16)..64]);
                        }
                        else if (rotate == Rotate.Decrypt && mode == Mode.CFB)
                        {
                            result = aes.DecryptCfb(plain, hashedPass[(64 - 16)..64]);
                        }
                        else
                        {
                            return;
                        }

                        string name = stream.Name.Split('.').Last() == "enc" ?
                            stream.Name[0..(stream.Name.Length - 4)] : stream.Name + ".enc";

                        using BinaryWriter writer = new BinaryWriter(File.Create(name));
                        writer.Write(result);
                    }
                    break;
            }
        }
    }
}