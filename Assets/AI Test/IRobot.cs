using System.Collections.Generic;

namespace DisablerAi.Interfaces
{
    public interface IRobot
    {
        /// <summary>
        /// Current ILocation of this entity.
        /// </summary>
        ILocation Location { get; set; }

        /// <summary>
        /// Where this IRobot is meant to be walking towards. When null do not walk/move anywhere.  
        /// </summary>
        ILocation Target { get; set; }

        /// <summary>
        /// The beginning position of this IRobot's patrol
        /// </summary>
        ILocation PatrolStart { get; set; }

        /// <summary>
        /// The end position of this IRobot's patrol
        /// </summary>
        ILocation PatrolEnd { get; set; }

        /// <summary>
        /// Pre-chosen points of interest on the map
        /// </summary>
        List<ILocation> PointsOfInterest { get; set; }

        // Acting

        /// <summary>
        /// The animation this robot should be playing out
        /// </summary>
        RobotAnimation PlayingAnimation { get; set; }

        // Vision configuration

        /// <summary>
        /// Flag et to true if this IRobot can see
        /// </summary>
        bool DetectionLineOfSight { get; set; }

        /// <summary>
        /// Flag set to true if this IRobot can hear audios
        /// </summary>
        bool DetectionAudio { get; set; }

        /// <summary>
        /// Flag set to true if this IRobot has been shot within this tick
        /// </summary>
        bool Shot { get; set; }

        /// <summary>
        /// Flag set to true if this IRobot has been hit within this tick
        /// </summary>
        bool HitWithItem { get; set; }

        /// <summary>
        /// Head of this robot. 
        /// </summary>
        IRobotHead Head { get; }

        /// <summary>
        /// Number of hits this robot can still take
        /// </summary>
        int Health { get; set; }

        // Visibility Checks

        /// <summary>
        /// Check to see if this robot can see a given player
        /// </summary>
        /// <param name="player">IPlayer to check to see if is in the vision path</param>
        /// <returns>True of this IPlayer is within the vision-cone of the given player</returns>
        bool CanSee(IPlayer player);

        /// <summary>
        /// Check to see if this robot can hear a given player
        /// </summary>
        /// <param name="player">IPlayer to check to see if is making noise and the robot should hear it</param>
        /// <returns>True if the IPlayer is within earshot and is making noise</returns>
        bool CanHear(IPlayer player);

        /// <summary>
        /// Check to see if this robot has reached it's target
        /// </summary>
        /// <param name="distanceForgiveness">How far, in meters, from the target before this returns true</param>
        /// <returns>true when the robot is within distanceForgiveness from it's target</returns>
        bool ReachedTarget(float distanceForgiveness = 0.5f);

        /// <summary>
        /// Mark the item as visible for  the player
        /// </summary>
        void MarkForPlayer();
    }
}