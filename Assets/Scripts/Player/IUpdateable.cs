namespace IndividualGames.UniPoly.Player
{
    /// <summary>
    /// Able to be updated to function.
    /// </summary>
    public interface IUpdateable
    {
        /// <summary> Update every cycle to function this class. </summary>
        public void UpdateState();
    }
}