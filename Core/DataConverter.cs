using System.Numerics;
using System.Text;

namespace Core;

// https://www.rfc-editor.org/rfc/rfc8017#section-4.1
public class DataConverter // TODO: precalculate powers of 256
{
    private static int _mod = 256;

    public static BigInteger StringToInteger(string text,Encoding encoding) // OS2IP
    {
        //var bytes = encoding.GetBytes(text);

        // if (bytes.Length != length)
        //     throw new Exception("Size of the octet string should be precisely " + length);

        //return new BigInteger(value: bytes, isUnsigned: true /*, isBigEndian: true*/);

        var bytes = encoding.GetBytes(text);
        
        var hash = new BigInteger();
        
        for (int i = 0; i < text.Length; i++)
            hash += bytes[i] * BigInteger.Pow(_mod, i);
        
        return hash;
    }

    // https://stackoverflow.com/questions/18819095/how-insert-bits-into-block-in-java-cryptography/18824707#18824707
    public static string IntegerToString(BigInteger integer, long length, Encoding encoding) // I2OSP
    {
        //return encoding.GetString(integer.ToByteArray());

        // if (length < 1)
        //     throw new Exception("Size of the octet string should be at least 1 but is " + length);
        //
        // if (integer.Sign == -1 || integer.GetBitLength() > length * sizeof(Byte))
        //     throw new Exception("Integer should be a non-negative number, no larger than the given size");
        //
        // var bytes = integer.ToByteArray();
        //
        // if (bytes.Length == length)
        //     return encoding.GetString(bytes);
        //
        //
        // var paddedBytes = new byte[length];
        // if (bytes.Length < length)
        // {
        //     // bytes array is too small, pad with '00' bytes at the left
        //     Array.Copy(bytes, 0, paddedBytes, length - bytes.Length, bytes.Length);
        //     return encoding.GetString(paddedBytes);
        // }
        //
        // // todo: ??
        // // todo: maybe remove all unnecessary leading '00' bytes?
        // // bytes array too large, remove leading '00' bytes
        // Array.Copy(bytes, 1, paddedBytes, 0, length);
        // return encoding.GetString(paddedBytes);

        if (length != -1 && integer >= BigInteger.Pow(_mod, (int)length)) // integer too large
            return null;
        
        var bytes = new List<int>();
        
        int i = 0;
        bool condition = integer > 0;
        if (length != -1) condition = i < length;
        
        
        var refreshCondition = () =>
        {
            if (length != -1) condition = i < length;
            else condition = integer > 0;
        };
        
        while (condition)
        {
            bytes.Add((int)(integer % _mod)); // todo: understand why it works
            integer /= _mod;
            i++;
        
            refreshCondition();
        }
        
        return encoding.GetString(bytes.Select(Convert.ToByte).ToArray());
    }
}