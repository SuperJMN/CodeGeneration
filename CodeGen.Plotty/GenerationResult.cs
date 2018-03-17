using System.Collections.Generic;
using CodeGen.Core;
using Plotty.Model;

namespace CodeGen.Plotty
{
    public class GenerationResult
    {
        public Dictionary<Reference, int> AddressMap { get; }
        public IList<Line> Code { get; }

        public GenerationResult(Dictionary<Reference, int> addressMap, IList<Line> code)
        {
            AddressMap = addressMap;
            Code = code;
        }
    }
}