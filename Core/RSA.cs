using System.Numerics;
using System.Text;

namespace Core;

public class RSA : ICipher // TODO: IDisposable
{
    // private
    private BigInteger _decryptionExp;
    private BigInteger _p, _q; // p,q - big enough prime numbers (todo: randomly generate)


    // public
    public BigInteger EncryptionExp { get; init; } = 65537;
    public BigInteger Modulus { get; init; }

    public Encoding Encoding { get; init; } = new UTF8Encoding(true, true);

    // Number of digits in base B required for representing a number N is floor(log_B(N)+1).
    // Logarithm has this nice property that log(X*Y)=log(X)+log(Y),
    // which hints that the number of digits for X*Y is roughly 
    // the sum of the number of digits representing X and Y.
    public int PrimesSize { get; init; } = 1024; // recommended to use 2048 size of key since 2011


    public RSA(Encoding? encoding = null, int bitSize = 1024, bool useDefaultPublicExp = true)
    {
        if (encoding is not null) Encoding = encoding;

        // TODO: generate!
        //_p = new BigInteger(9393804643);
        //_q = new BigInteger(5730255211);
        _p = PrimeNumberGenerator.GetRandomPrimaryNumber(bitSize);
        _q = PrimeNumberGenerator.GetRandomPrimaryNumber(bitSize);

        while (_q == _p)
            _q = PrimeNumberGenerator.GetRandomPrimaryNumber(bitSize);

        Modulus = _p * _q;

        // 3, 5, 17, 257, 65537 - that are known to be prime are often favoured because they speed up calculations 
        // on one side of the operation (encrypt/decrypt, sign/verify) - and 65537 is probably 
        // the most common exponent in use at this point in time (2020/11).
        if (!useDefaultPublicExp)
        {
            // todo: calculate public exp
        }

        _decryptionExp = CalculateDecryptionExp();
    }

    private BigInteger CalculateDecryptionExp()
        => MyMath.InverseByModulus(EncryptionExp, (_p - 1) * (_q - 1));

    public BigInteger Encrypt(int m)
        => BigInteger.ModPow(m, EncryptionExp, Modulus);

    public BigInteger Decrypt(BigInteger c)
        => BigInteger.ModPow(c, _decryptionExp, Modulus);

    public string Encrypt(string text)
    {
        var encrypted = new List<BigInteger>();

        foreach (var c in text)
            encrypted.Add(Encrypt(c - '0'));

        var strBuilder = new StringBuilder();
        foreach (var i in encrypted)
        {
            var bytes = i.ToByteArray();
            strBuilder.Append(Encoding.UTF8.GetChars(bytes));
            //strBuilder.Append(GetCharByBigInteger(i));
        }


        return strBuilder.ToString();
    }


    // private string GetCharByBigInteger(BigInteger number)
    // {
    //     var str = number.ToString();
    //
    //     while (str.Length % 3 != 0)
    //         str = '0' + str;
    //
    //     string result = "";
    //     for (int i = 0; i < str.Length; i += 3)
    //         result += (char)(Int32.Parse(str.Substring(i, 3)));
    //     return result;
    // }

    public string Decrypt(string cipherText)
    {
        return "";
    }
}