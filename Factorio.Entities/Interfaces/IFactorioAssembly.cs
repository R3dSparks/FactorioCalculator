using Factorio.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.Entities.Interfaces
{
    public interface IFactorioAssembly
    {

        Dictionary<FactorioItem, double> GetSummary(Dictionary<FactorioItem, double> summary = null);

        Dictionary<FactorioItem, double> Summary { get; }

        CraftingStation CraftingStation { get; set; }

        double Quantity { get; set; }

        FactorioItem AssemblyItem { get; }

        List<IFactorioAssembly> SubAssembly { get; }

        void UpdateAssembly();

        void UpdateAssembly(IFactorioAssembly topAssembly);

        double CraftingSpeed { get; }
       

    }
}
