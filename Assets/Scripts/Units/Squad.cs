using System;
using Fakejam.Players;

namespace Fakejam.Units
{
    [Serializable]
    public class Squad
    {
        public PlayerType Owner;
        
        /// <summary>
        /// The type of unit contained in the squad
        /// </summary>
        public UnitDefinition UnitDefinition;
        /// <summary>
        /// Unit count in this squad
        /// </summary>
        public int Count;

        public Squad Clone()
        {
            return new Squad
            {
                Owner = Owner,
                UnitDefinition = UnitDefinition,
                Count = Count
            };
        }
    }
}