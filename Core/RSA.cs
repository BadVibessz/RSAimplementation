using System.Numerics;
using System.Text;

namespace Core;

public class RSA : ICipher // TODO: IDisposable
{
    // private
    private BigInteger _decryptionExp;
    private BigInteger _p, _q; 


    // public
    public BigInteger EncryptionExp { get; init; } = 65537;
    public BigInteger Modulus { get; init; }

    public Encoding Encoding { get; init; } = new UTF8Encoding(true, true);

    // Number of digits in base B required for representing a number N is floor(log_B(N)+1).
    // Logarithm has this nice property that log(X*Y)=log(X)+log(Y),
    // which hints that the number of digits for X*Y is roughly 
    // the sum of the number of digits representing X and Y.
    public int PrimesSize { get; init; } // recommended to use 2048 size of key since 2011


    public RSA(Encoding? encoding = null, int bitSize = 1024, bool useDefaultPublicExp = true)
    {
        // for text encoding
        if (encoding is not null) Encoding = encoding;

        PrimesSize = bitSize;
        
        _p = PrimeNumberGenerator.GetRandomPrimaryNumber(PrimesSize);
        _q = PrimeNumberGenerator.GetRandomPrimaryNumber(PrimesSize);

        while (_q == _p)
            _q = PrimeNumberGenerator.GetRandomPrimaryNumber(PrimesSize);

        Modulus = _p * _q;

        // 3, 5, 17, 257, 65537 - that are known to be prime are often favoured because they speed up calculations 
        // on one side of the operation (encrypt/decrypt, sign/verify) - and 65537 is probably 
        // the most common exponent in use at this point in time (2020/11).
        if (!useDefaultPublicExp)
        {
            // todo: calculate public exp
        }

        _decryptionExp = MyMath.InverseByModulus(EncryptionExp, (_p - 1) * (_q - 1));
    }
    
    public BigInteger Encrypt(int m)
        => BigInteger.ModPow(m, EncryptionExp, Modulus);

    public BigInteger Decrypt(BigInteger c)
        => BigInteger.ModPow(c, _decryptionExp, Modulus);

    // public string Encrypt(string text)
    // {
    //     var encrypted = new List<BigInteger>();
    //
    //     foreach (var c in text)
    //         encrypted.Add(Encrypt(c - '0'));
    //
    //     var strBuilder = new StringBuilder();
    //     foreach (var i in encrypted)
    //     {
    //         var bytes = i.ToByteArray();
    //         strBuilder.Append(Encoding.UTF8.GetChars(bytes));
    //         //strBuilder.Append(GetCharByBigInteger(i));
    //     }
    //
    //
    //     return strBuilder.ToString();
    // }
    //
    // public string Decrypt(string cipherText)
    // {
    //     return "";
    // }
}