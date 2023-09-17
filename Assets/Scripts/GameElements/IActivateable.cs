namespace IndividualGames.UniPoly.GameElements
{
    /// <summary>
    /// Has activate/deactivate functionality. Keeps it's state.
    /// </summary>
    public interface IActivateable
    {
        /// <summary> State of this activatable. </summary>
        public bool ActivationState { get; set; }

        /// <summary> Activate this object. </summary>
        public void Activate()
        {
            ActivationState = true;
        }

        /// <summary> Deactivate this object. </summary>
        public virtual void Deactivate()
        {
            ActivationState = false;
        }
    }
}