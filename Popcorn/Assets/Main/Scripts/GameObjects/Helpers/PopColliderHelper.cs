using UnityEngine;
using Popcorn.Bases;
using Popcorn.Metadados;

namespace Popcorn.GameObjects.Helpers
{

    public class PopColliderHelper : TriggerHelperBase
    {

        [HideInInspector]
        public bool isColliding { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            isColliding = false;
        }

        void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.CompareTag(Tags.Surfaces.Platform.ToString()) == true ||
                coll.CompareTag(Tags.Helpers.WeakPoint.ToString()) == true ||
                coll.CompareTag(Tags.Elementies.Limit.ToString()) == true ||
                coll.CompareTag(Tags.Objects.Object.ToString()) == true)
            {
                isColliding = true;
            }
        }

        private void OnTriggerStay2D(Collider2D coll)
        {
            if (coll.CompareTag(Tags.Surfaces.Platform.ToString()) == true ||
                coll.CompareTag(Tags.Helpers.WeakPoint.ToString()) == true ||
                coll.CompareTag(Tags.Elementies.Limit.ToString()) == true ||
                coll.CompareTag(Tags.Objects.Object.ToString()) == true)
            {
                isColliding = true;
            }
        }

        void OnTriggerExit2D(Collider2D coll)
        {
            if (coll.CompareTag(Tags.Surfaces.Platform.ToString()) == true ||
                coll.CompareTag(Tags.Helpers.WeakPoint.ToString()) == true ||
                coll.CompareTag(Tags.Elementies.Limit.ToString()) == true ||
                coll.CompareTag(Tags.Objects.Object.ToString()) == true)
            {
                isColliding = false;
            }
        }

    }
}
