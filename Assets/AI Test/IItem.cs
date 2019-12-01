namespace DisablerAi.Interfaces
{
    public interface IItem
    {
        /// <summary>
        /// Location an Item is placed
        /// </summary>
        ILocation Location { get; set; }

        /// <summary>
        /// Mark the item as visible for  the player
        /// </summary>
        void MarkForPlayer();
    }
}