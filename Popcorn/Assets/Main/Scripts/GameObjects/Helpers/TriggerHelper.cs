using System;
using UnityEngine;
using Popcorn.Bases;

namespace Popcorn.GameObjects.Helpers
{

    public class TriggerHelper : TriggerHelperBase
    {

        [HideInInspector] public Action<Collider2D> onTriggerEnterAction;
        [HideInInspector] public Action<Collider2D> onTriggerExitAction;
        [HideInInspector] public Action<Collider2D> onTriggerStayAction;
        [HideInInspector] public bool isActived = true;

        void OnTriggerEnter2D(Collider2D otherCollider2D)
        {
            if (isActived && onTriggerEnterAction != null) { onTriggerEnterAction(otherCollider2D); }
        }

        void OnTriggerExit2D(Collider2D otherCollider2D)
        {
            if (isActived && onTriggerExitAction != null) { onTriggerExitAction(otherCollider2D); }
        }

        void OnTriggerStay2D(Collider2D otherCollider2D)
        {
            if (isActived && onTriggerStayAction != null) { onTriggerStayAction(otherCollider2D); }
        }

    }

}