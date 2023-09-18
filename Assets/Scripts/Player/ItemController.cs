using IndividualGames.UniPoly.Multiplayer;
using Photon.Pun;
using UnityEngine;

namespace IndividualGames.UniPoly.Player
{
    /// <summary>
    /// Controls grabbing and dropping items.
    /// </summary>
    public class ItemController
    {
        private Transform m_leftHand;
        private Transform m_rightHand;
        private Transform m_dropPosition;

        private GameObject m_grabbedItemLeft;
        private GameObject m_grabbedItemRight;


        public ItemController(Transform leftHand, Transform rightHand, Transform a_dropPosition)
        {
            m_leftHand = leftHand;
            m_rightHand = rightHand;
            m_dropPosition = a_dropPosition;
        }

        public void GrabItem(GameObject a_grabbedItem)
        {
            m_grabbedItemLeft = a_grabbedItem;
            a_grabbedItem.transform.position = m_leftHand.position;
            a_grabbedItem.transform.parent = m_leftHand;
            a_grabbedItem.GetComponent<BoxCollider>().isTrigger = false;
            PhotonController.TransferOwnershipToLocal(a_grabbedItem.GetComponent<PhotonView>());
        }

        public void DropItem()
        {
            if (m_grabbedItemLeft != null)
            {
                m_grabbedItemLeft.transform.parent = null;
                m_grabbedItemLeft.transform.position = m_dropPosition.position;
                m_grabbedItemLeft.GetComponent<BoxCollider>().isTrigger = true;
                m_grabbedItemLeft = null;
            }
        }
    }
}