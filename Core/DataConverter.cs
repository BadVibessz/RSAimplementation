using System.Numerics;
using System.Text;

namespace Core;

// https://www.rfc-editor.org/rfc/rfc8017#section-4.1
public class DataConverter // TODO: precalculate powers of 256
{
    private static int _mod = 256;

    public static BigInteger StringToInteger(string text, Encoding encoding) // OS2IP
    {
        var bytes = encoding.GetBytes(text);

        var hash = new BigInteger();

        for (int i = 0; i < text.Length; i++)
            hash += bytes[i] * BigInteger.Pow(_mod, i);

        return hash;
    }

    // https://stackoverflow.com/questions/18819095/how-insert-bits-into-block-in-java-cryptography/18824707#18824707
    public static string IntegerToString(BigInteger integer, Encoding encoding) // I2OSP
    {
        var bytes = new List<int>();
        while (integer != 0)
        {
            bytes.Add((int)(integer % _mod)); // todo: understand why it works
            integer /= _mod;
        }

        return encoding.GetString(bytes.Select(Convert.ToByte).ToArray());
    }
}