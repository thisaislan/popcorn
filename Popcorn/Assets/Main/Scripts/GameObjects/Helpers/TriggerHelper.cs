using System;
using UnityEngine;
using Popcorn.Bases;

namespace Popcorn.GameObjects.Helpers
{

    public class TriggerHelper : TriggerHelperBase
    {

        [HideInInspector]
        public Action<Collider2D> OnTriggerEnterAction;
        [HideInInspector]
        public Action<Collider2D> OnTriggerExitAction;
        [HideInInspector]
        public Action<Collider2D> OnTriggerStayAction;
        [HideInInspector]
        public bool IsActived = true;

        void OnTriggerEnter2D(Collider2D otherCollider2D)
        {
            if (IsActived && OnTriggerEnterAction != null)
            {
                OnTriggerEnterAction(otherCollider2D);
            }
        }

        void OnTriggerExit2D(Collider2D otherCollider2D)
        {
            if (IsActived && OnTriggerExitAction != null)
            {
                OnTriggerExitAction(otherCollider2D);
            }
        }

        private void OnTriggerStay2D(Collider2D otherCollider2D)
        {
            if (IsActived && OnTriggerStayAction != null)
            {
                OnTriggerStayAction(otherCollider2D);
            }
        }

    }
}
