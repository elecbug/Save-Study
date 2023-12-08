using System.IO;
using System.Security.Cryptography;
using System.Text;
using static Program;

public partial class Program
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
    public enum Result
    {
        OK,
        Error,
    }

    public class SecureService
    {
        Algorithm Algorithm;
        Rotate Rotate;
        Mode Mode;
        byte[] HashedPass;
        byte[] PlainText;
        FileStream Stream;

        public SecureService(Algorithm algorithm, Rotate rotate, Mode mode, FileStream stream, string password)
        {
            using BinaryReader reader = new BinaryReader(stream);
            
            Algorithm = algorithm;
            Rotate = rotate;
            Mode = mode;
            Stream = stream;
            HashedPass = SHA512.HashData(Encoding.UTF8.GetBytes(password));
            PlainText = reader.ReadBytes((int)stream.Length);
        }

        public Result Run()
        {
            byte[] result;

            switch (Algorithm)
            {
                case Algorithm.DES:
                    {
                        TripleDES des = TripleDES.Create();
                        des.Key = HashedPass[0..24];

                        if (Rotate == Rotate.Encrypt && Mode == Mode.ECB)
                        {
                            result = des.EncryptEcb(PlainText, PaddingMode.PKCS7);
                        }
                        else if (Rotate == Rotate.Decrypt && Mode == Mode.ECB)
                        {
                            result = des.DecryptEcb(PlainText, PaddingMode.PKCS7);
                        }
                        else if (Rotate == Rotate.Encrypt && Mode == Mode.CBC)
                        {
                            result = des.EncryptCbc(PlainText, HashedPass[(64 - 8)..64]);
                        }
                        else if (Rotate == Rotate.Decrypt && Mode == Mode.CBC)
                        {
                            result = des.DecryptCbc(PlainText, HashedPass[(64 - 8)..64]);
                        }
                        else if (Rotate == Rotate.Encrypt && Mode == Mode.CFB)
                        {
                            result = des.EncryptCfb(PlainText, HashedPass[(64 - 8)..64]);
                        }
                        else if (Rotate == Rotate.Decrypt && Mode == Mode.CFB)
                        {
                            result = des.DecryptCfb(PlainText, HashedPass[(64 - 8)..64]);
                        }
                        else
                        {
                            return Result.Error;
                        }

                        string name = Stream.Name.Split('.').Last() == "enc" ?
                        Stream.Name[0..(Stream.Name.Length - 4)] : Stream.Name + ".enc";

                        using BinaryWriter writer = new BinaryWriter(File.Create(name));
                        writer.Write(result);

                        return Result.OK;
                    }
                case Algorithm.AES:
                    {
                        Aes aes = Aes.Create();
                        aes.Key = HashedPass[0..32];

                        if (Rotate == Rotate.Encrypt && Mode == Mode.ECB)
                        {
                            result = aes.EncryptEcb(PlainText, PaddingMode.PKCS7);
                        }
                        else if (Rotate == Rotate.Decrypt && Mode == Mode.ECB)
                        {
                            result = aes.DecryptEcb(PlainText, PaddingMode.PKCS7);
                        }
                        else if (Rotate == Rotate.Encrypt && Mode == Mode.CBC)
                        {
                            result = aes.EncryptCbc(PlainText, HashedPass[(64 - 16)..64]);
                        }
                        else if (Rotate == Rotate.Decrypt && Mode == Mode.CBC)
                        {
                            result = aes.DecryptCbc(PlainText, HashedPass[(64 - 16)..64]);
                        }
                        else if (Rotate == Rotate.Encrypt && Mode == Mode.CFB)
                        {
                            result = aes.EncryptCfb(PlainText, HashedPass[(64 - 16)..64]);
                        }
                        else if (Rotate == Rotate.Decrypt && Mode == Mode.CFB)
                        {
                            result = aes.DecryptCfb(PlainText, HashedPass[(64 - 16)..64]);
                        }
                        else
                        {
                            return Result.Error;
                        }

                        string name = Stream.Name.Split('.').Last() == "enc" ?
                        Stream.Name[0..(Stream.Name.Length - 4)] : Stream.Name + ".enc";

                        using BinaryWriter writer = new BinaryWriter(File.Create(name));
                        writer.Write(result);

                        return Result.OK;
                    }
            }

            return Result.Error;
        }
    }
}