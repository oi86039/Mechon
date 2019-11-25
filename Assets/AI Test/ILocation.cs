namespace DisablerAi.Interfaces
{
    public interface ILocation
    {
        /// <summary>
        /// Get a random location around this location
        /// </summary>
        /// <param name="distanceFromPlayer">Minimum distance from IPlayer</param>
        /// <param name="distanceFromRobots">Minimum distance from IRobots</param>
        /// <returns>Location that best matches the given constraints</returns>
        ILocation RandomLocation(float distanceFromPlayer, float distanceFromRobots);

        /// <summary>
        /// Find the euclidean distance between two location
        /// </summary>
        /// <param name="location">Other location</param>
        /// <returns>Distance in meters</returns>
        float DistanceFrom(ILocation location);
    }
}