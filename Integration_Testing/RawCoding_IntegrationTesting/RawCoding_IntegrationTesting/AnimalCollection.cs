using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RawCoding_IntegrationTesting
{
    [CollectionDefinition(nameof(AnimalCollection))]
    public class AnimalCollection: ICollectionFixture<AnimalSetupFixture>
    {
        
    }
}
