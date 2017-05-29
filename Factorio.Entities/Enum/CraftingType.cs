
namespace Factorio.Entities
{
    /// <summary>
    /// Used to define where the item is crafted in
    /// </summary>
    public enum CraftingType
    {
        /// <summary>
        /// Crafted in an assembling machine
        /// </summary>
        AssemblingMachine,
        /// <summary>
        /// Crafted in a furnace
        /// </summary>
        Furnace,
        /// <summary>
        /// Crafted in a chemical plant
        /// </summary>
        ChemicalPlant,
        /// <summary>
        /// Crafted in a refinery
        /// </summary>
        Refinery,
        /// <summary>
        /// Mined by a drill
        /// </summary>
        Drill
    }
}
