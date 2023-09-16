namespace IndividualGames.UniPoly.Player
{
    /// <summary>
    /// Able to be retain states of locked and unlocked.
    /// </summary>
    public interface ILockable
    {
        /// <summary> Lock this object for changes. </summary>
        public void Lock();
        /// <summary> Unlock this object for changes. </summary>
        public void Unlock();
    }
}