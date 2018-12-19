using System.Collections.Generic;
using UnityEngine;
using it.amalfi.Pearl.multitags;

namespace it.amalfi.Pearl.actionTrigger
{
    /// <summary>
    /// This class examine every trigger events and sort them in specific components
    /// </summary>
    public class ActionTrigger : MonoBehaviour
    {
        #region Inspector Fields
        /// <summary>
        /// The objects waiting for the events
        /// </summary>
        [SerializeField]
        private List<GameObject> listeners;
        /// <summary>
        /// The tags that activate the trigger class.
        /// </summary>
        [SerializeField]
        private Tags[] tagForTriggered;
        #endregion

        #region Private Fields
        private List<int> listGameobjectTriggeredActived;
        private GameObject auxGameObject;
        private DestructionElement destructionElement;
        private Informations informations;
        #endregion

        #region Unity CallBacks
        private void Awake()
        {
            listGameobjectTriggeredActived = new List<int>();
        }

        private void OnDisable()
        {
            listGameobjectTriggeredActived.Clear();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            auxGameObject = collider.gameObject;
            if (gameObject.HaveTags(Tags.Obstacle) && collider.isTrigger)
                return;

            if (auxGameObject.HaveTags(tagForTriggered) && !listGameobjectTriggeredActived.Contains(auxGameObject.GetInstanceID()))
            {
                destructionElement = auxGameObject.GetComponent<DestructionElement>();
                if (destructionElement)
                    destructionElement.OnDestruction += RemoveGameObjectActive;


                listGameobjectTriggeredActived.Add(auxGameObject.GetInstanceID());
                TriggerEvent(auxGameObject);
            }
        }


        private void OnTriggerExit2D(Collider2D collider)
        {
            auxGameObject = collider.gameObject;
            RemoveGameObjectActive(auxGameObject);
        }
        #endregion

        #region Private Methods
        private void RemoveGameObjectActive(GameObject obj)
        {
            if (listGameobjectTriggeredActived.Contains(auxGameObject.GetInstanceID()))
            {
                listGameobjectTriggeredActived.Remove(auxGameObject.GetInstanceID());
            }
        }

        private void TriggerEvent(GameObject obj)
        {
            informations = new Informations(obj.GetComponent<ComplexAction>()?.Informations);
            foreach (GameObject element in listeners)
            {
                IEvent[] events = element.GetComponents<IEvent>();
                foreach (IEvent e in events)
                {
                    e.Trigger(informations, obj.ReturnTags());
                }
            }
        }
        #endregion
    }
}
