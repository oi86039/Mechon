namespace DisablerAi.Interfaces
{
    public interface IRobotHead
    {
        /// <summary>
        /// Location of the IRobot's had
        /// </summary>
        ILocation Location { get; set; }

        /// <summary>
        /// Track if this IRobotHead has been shots
        /// </summary>
        bool Shot { get; set; }
    }
}