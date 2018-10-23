using UnityEngine;
using Popcorn.Metadatas;

namespace Popcorn.GameObjects.Helpers
{

    [RequireComponent(typeof(Collider2D))]
    public class WeakPoint : MonoBehaviour
    {
        [HideInInspector]
        public bool IsColliding { get; private set; }
        private void Awake()
        {
            IsColliding = false;
        }

        void OnCollisionEnter2D(Collision2D otherCollider2D)
        {
            if (otherCollider2D.gameObject.CompareTag(Tags.Persons.Player.ToString()))
            {
                IsColliding = true;
            }
        }

        private void OnCollisionExit2D(Collision2D otherCollider2D)
        {
            if (otherCollider2D.gameObject.CompareTag(Tags.Persons.Player.ToString()))
            {
                IsColliding = false;
            }
        }

    }
}
