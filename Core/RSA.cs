using System.Numerics;
using System.Text;

namespace Core;
// TODO: ROLLBACK TO LAST COMMIT

public class RSA : ICipher
{
    private BigInteger _decryptionExp;
    private BigInteger _p, _q;
    public BigInteger EncryptionExp { get; init; } = 65537;
    public BigInteger Modulus { get; init; }
    public Encoding Encoding { get; init; } = Encoding.UTF8;
    public int PrimesSize { get; init; }


    public RSA(Encoding? encoding = null, int bitSize = 2048, bool useDefaultPublicExp = true)
    {
        // for text encoding
        if (encoding is not null) Encoding = encoding;

        PrimesSize = bitSize / 2;

        _p = PrimeNumberGenerator.GetRandomPrimaryNumber(PrimesSize);
        _q = PrimeNumberGenerator.GetRandomPrimaryNumber(PrimesSize);

        while (_q == _p)
            _q = PrimeNumberGenerator.GetRandomPrimaryNumber(PrimesSize);

        Modulus = _p * _q;

        if (!useDefaultPublicExp)
        {
            // todo: calculate public exp
        }

        _decryptionExp = MyMath.InverseByModulus(EncryptionExp, (_p - 1) * (_q - 1));
    }

    public RSA(int bitSize) : this(null, bitSize)
    {
    }

    public BigInteger Encrypt(BigInteger m)
        => BigInteger.ModPow(m, EncryptionExp, Modulus);

    public BigInteger Decrypt(BigInteger c)
        => BigInteger.ModPow(c, _decryptionExp, Modulus);

    public string Encrypt(string plainText)
    {
        var hash = DataConverter.StringToInteger(plainText, Encoding);
        var encrypted = Encrypt(hash);
        return Convert.ToBase64String(encrypted.ToByteArray());
    }

    public string Decrypt(string cipherText)
    {
        var encrypted = new BigInteger(Convert.FromBase64String(cipherText), isUnsigned: true);
        var decrypted = Decrypt(encrypted);

        return DataConverter.IntegerToString(decrypted, Encoding.ASCII);
    }
}