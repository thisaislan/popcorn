using System;
using UnityEngine;
using Popcorn.Bases;

namespace Popcorn.GameObjects.Helpers
{

    public class TriggerHelper : TriggerHelperBase
    {

        [HideInInspector]
        public Action<Collider2D> onTriggerEnterAction;
        [HideInInspector]
        public Action<Collider2D> onTriggerExitAction;
        [HideInInspector]
        public Action<Collider2D> onTriggerStayAction;
        [HideInInspector]
        public bool isActived = true;

        void OnTriggerEnter2D(Collider2D coll)
        {
            if (isActived && onTriggerEnterAction != null) onTriggerEnterAction(coll);
        }

        void OnTriggerExit2D(Collider2D coll)
        {
            if (isActived && onTriggerExitAction != null) onTriggerExitAction(coll);
        }

        private void OnTriggerStay2D(Collider2D coll)
        {
            if (isActived && onTriggerStayAction != null) onTriggerStayAction(coll);
        }

    }
}
