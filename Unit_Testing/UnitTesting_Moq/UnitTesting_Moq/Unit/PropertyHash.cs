using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting_Moq.Unit
{
    public class PropertyHash
    {
        public virtual string Hash<T>(T input,params Func<T,string>[] selectors)
        {
            StringBuilder builder = new();

            foreach (var selector in selectors)
            {
                builder.Append(selector(input));
            }

            return builder.ToString();
        }


    }

    public interface IHashAlgorithmFactory
    {
        public HashAlgorithm Create();
    }

    public class AlgorithmPropertyHash : PropertyHash
    {
        private readonly HashAlgorithm _algorithm;
        private IHashAlgorithmFactory _algorithmFactory;

        public AlgorithmPropertyHash(IHashAlgorithmFactory algorithmFactory)
        {
            _algorithmFactory = algorithmFactory;
        }

        public override string Hash<T>(T input, params Func<T, string>[] selectors)
        {
            var seed = base.Hash(input, selectors);
            var seedBytes = Encoding.UTF8.GetBytes(seed);
            using var algo = _algorithmFactory.Create();
            var hashBytes = _algorithm.ComputeHash(seedBytes);
            return Convert.ToBase64String(hashBytes);
        }

    }
}
