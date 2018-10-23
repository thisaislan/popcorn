using UnityEngine;
using PersonsTags = Popcorn.Metadatas.Tags.Persons;

namespace Popcorn.GameObjects.Objects
{

    public class EndPoint : MonoBehaviour
    {

        [HideInInspector]
        public bool WasReachedTheEnd { get; private set; }

        private void Awake()
        {
            WasReachedTheEnd = false;
        }

        private void OnTriggerEnter2D(Collider2D otherCollider2D)
        {
            if (otherCollider2D.CompareTag(PersonsTags.Player.ToString()))
            {
                WasReachedTheEnd = true;
            }
        }

    }
}