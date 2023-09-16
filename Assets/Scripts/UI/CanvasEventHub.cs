using IndividualGames.UniPoly.Utils;
using UnityEngine;

namespace IndividualGames.UniPoly.UI
{
    /// <summary>
    /// Exposure of Canvas related events.
    /// </summary>
    public class CanvasEventHub : MonoBehaviour
    {
        public readonly BasicSignal JoinGame = new();
        public readonly BasicSignal<float> VolumeChanged = new();
        public readonly BasicSignal<bool> ItemDetected = new();


        /// <summary> Network game join successful. </summary>
        public void OnJoinGame()
        {
            JoinGame.Emit();
        }


        /// <summary> Adjust volume to change. </summary>
        public void OnVolumeChanged(float a_volume)
        {
            VolumeChanged.Emit(a_volume);
        }

        /// <summary> Item is detected. </summary>
        public void OnItemDetected(bool a_detected)
        {
            ItemDetected.Emit(a_detected);
        }
    }
}