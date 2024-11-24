using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier_SqlKata_ghQL.Core.GraphQL
{
    public class SPComplexScalarGraphTypeModel
    {
        public string name { get; set; } = null!;
        public dynamic? parameters { get; set; }
    }
}
