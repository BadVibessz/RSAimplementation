using System.Numerics;

namespace Core;

public static class MyMath
{
    public static BigInteger InverseByModulus(BigInteger a, BigInteger mod) //https://ru.wikipedia.org/wiki/%D0%A0%D0%B0%D1%81%D1%88%D0%B8%D1%80%D0%B5%D0%BD%D0%BD%D1%8B%D0%B9_%D0%B0%D0%BB%D0%B3%D0%BE%D1%80%D0%B8%D1%82%D0%BC_%D0%95%D0%B2%D0%BA%D0%BB%D0%B8%D0%B4%D0%B0#%D0%9F%D1%81%D0%B5%D0%B2%D0%B4%D0%BE%D0%BA%D0%BE%D0%B4
    {
        BigInteger t = 0;
        BigInteger r = mod;

        BigInteger newt = 1;
        BigInteger newr = a;

        while (newr != 0)
        {
            var temp = r / newr;

            (t, newt) = (newt, t - temp * newt);
            (r, newr) = (newr, r - temp * newr);
        }

        if (r > 1) throw new Exception("Irreversible");

        while (t < 0) t += mod;

        return t;
    }
    
}