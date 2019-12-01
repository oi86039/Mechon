using System.Collections.Generic;

namespace DisablerAi.Interfaces
{
    public interface IPlayer
    {
        /// <summary>
        /// Instance of this Player's IDisabler
        /// </summary>
        IDisabler Disabler { get; set; }

        /// <summary>
        /// Location of thi player
        /// </summary>
        ILocation Location { get; set; }

        /// <summary>
        /// Find the nearest robots to this player 
        /// </summary>
        /// <returns></returns>
        List<IRobot> NearestRobots();

        /// <summary>
        /// Find the nearest items to this IPlayer
        /// </summary>
        /// <returns>List of items near the player</returns>
        List<IItem> NearestItems();
    }
}