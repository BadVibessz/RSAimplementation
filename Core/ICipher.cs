using System.Numerics;

namespace Core;

public interface ICipher
{
    public BigInteger Encrypt(int m);
    public BigInteger Decrypt(BigInteger n);
}