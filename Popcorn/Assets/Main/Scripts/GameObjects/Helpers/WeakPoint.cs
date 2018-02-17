using UnityEngine;
using Popcorn.Metadados;

namespace Popcorn.GameObjects.Helpers
{

    [RequireComponent(typeof(Collider2D))]
    public class WeakPoint : MonoBehaviour
    {
        [HideInInspector]
        public bool isColliding { get; private set; }

        private void Awake()
        {
            isColliding = false;
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.CompareTag(Tags.Persons.Player.ToString()) == true) isColliding = true;
        }

        private void OnCollisionExit2D(Collision2D coll)
        {
            if (coll.gameObject.CompareTag(Tags.Persons.Player.ToString()) == true) isColliding = false;
        }

    }
}
