using System.Numerics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Core;

// https://crypto.stackexchange.com/questions/1970/how-are-primes-generated-for-rsa
// https://www.geeksforgeeks.org/how-to-generate-large-prime-numbers-for-rsa-algorithm/

public static class PrimeNumberGenerator
{
    private static readonly List<Int32> _lowLevelPrimes = Regex.Matches(
            File.ReadAllText("low_level_primes.txt"),
            @"(\d+)")
        .Cast<Match>()
        .Select(i => Convert.ToInt32(i.Value))
        .ToList();

    public static BigInteger GetRandomPrimaryNumber(int bitSize)
    {
        var candidates = Generate10RandomOddNumbers(bitSize);

        for (int i = 0; i < 10; i++)
        {
            var candidate = candidates[i];
            if (HasPassedLowLevelPrimaryTest(candidate) && HasPassedMillerRabinTest(candidate, 10))
                return candidate;
        }

        return GetRandomPrimaryNumber(bitSize);
    }

    private static BigInteger GetRandomOddNumber(int bitSize)
    {
        var result = BigInteger.Abs(new BigInteger(RandomNumberGenerator.GetBytes(bitSize / 8)));
        return result.IsEven ? result + 1 : result;
    }
    
    private static BigInteger[] Generate10RandomOddNumbers(int bitSize)
    {
        BigInteger[] array = new BigInteger[10];
        for (int i = 0; i < 10; i++)
            array[i] = GetRandomOddNumber(bitSize);
        return array;
    }


    private static bool HasPassedLowLevelPrimaryTest(BigInteger candidate)
    {
        foreach (int divisor in _lowLevelPrimes)
        {
            if (candidate % divisor == 0
                && divisor * divisor <= candidate) break;

            return true;
        }

        return false;
    }

    // source: https://stackoverflow.com/questions/33895713/millerrabin-primality-test-in-c-sharp
    private static bool HasPassedMillerRabinTest(BigInteger candidate, int k) // todo: understand
    {
        var generateRandom = () =>
            new ThreadLocal<Random>(
                () => { return new Random(); }).Value;

        if (candidate <= 1)
            return false;

        if (k <= 0)
            k = 10;

        BigInteger d = candidate - 1;
        int s = 0;

        while (d % 2 == 0)
        {
            d /= 2;
            s += 1;
        }

        var bytes = new Byte[candidate.GetByteCount()];
        BigInteger a;
        for (int i = 0; i < k; i++)
        {
            do
            {
                var z = generateRandom.Invoke();
                if (z is null) throw new Exception("RAND IS NULL");
                z.NextBytes(bytes);
                a = new BigInteger(bytes);
                
            } while (a < 2 || a >= candidate - 2);

            BigInteger x = BigInteger.ModPow(a, d, candidate);
            if (x == 1 || x == candidate - 1)
                continue;

            for (int r = 1; r < s; r++)
            {
                x = BigInteger.ModPow(x, 2, candidate);

                if (x == 1)
                    return false;
                if (x == candidate - 1)
                    break;
            }

            if (x != candidate - 1)
                return false;
        }

        return true;
    }
}