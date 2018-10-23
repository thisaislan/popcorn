using UnityEngine;
using Popcorn.Bases;
using Popcorn.Metadatas;

namespace Popcorn.GameObjects.Helpers
{

    public class PopColliderHelper : TriggerHelperBase
    {

        [HideInInspector]
        public bool IsColliding { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            IsColliding = false;
        }

        void OnTriggerEnter2D(Collider2D otherCollider2D)
        {
            if (otherCollider2D.CompareTag(Tags.Surfaces.Platform.ToString()) ||
                otherCollider2D.CompareTag(Tags.Helpers.WeakPoint.ToString()) ||
                otherCollider2D.CompareTag(Tags.Elementies.Limit.ToString()) ||
                otherCollider2D.CompareTag(Tags.Objects.Object.ToString()))
            {
                IsColliding = true;
            }
        }

        private void OnTriggerStay2D(Collider2D otherCollider2D)
        {
            if (otherCollider2D.CompareTag(Tags.Surfaces.Platform.ToString()) ||
                otherCollider2D.CompareTag(Tags.Helpers.WeakPoint.ToString()) ||
                otherCollider2D.CompareTag(Tags.Elementies.Limit.ToString()) ||
                otherCollider2D.CompareTag(Tags.Objects.Object.ToString()))
            {
                IsColliding = true;
            }
        }

        void OnTriggerExit2D(Collider2D otherCollider2D)
        {
            if (otherCollider2D.CompareTag(Tags.Surfaces.Platform.ToString()) ||
                otherCollider2D.CompareTag(Tags.Helpers.WeakPoint.ToString()) ||
                otherCollider2D.CompareTag(Tags.Elementies.Limit.ToString()) ||
                otherCollider2D.CompareTag(Tags.Objects.Object.ToString()))
            {
                IsColliding = false;
            }
        }

    }
}
