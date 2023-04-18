using System.Numerics;

namespace Core;

public interface ICipher
{
    public BigInteger Encrypt(BigInteger m);
    public BigInteger Decrypt(BigInteger n);

    public string Encrypt(string plaintext);
    public string Decrypt(string ciphertext);
}