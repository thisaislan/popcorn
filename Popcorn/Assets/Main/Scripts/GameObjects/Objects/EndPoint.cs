using UnityEngine;
using PersonsTags = Popcorn.Metadatas.Tags.Persons;

namespace Popcorn.GameObjects.Objects
{

    public class EndPoint : MonoBehaviour
    {

        [HideInInspector] public bool WasReachedTheEnd { get; private set; }

        void Awake() { WasReachedTheEnd = false; }

        void OnTriggerEnter2D(Collider2D otherCollider2D)
        {
            if (otherCollider2D.CompareTag(PersonsTags.Player.ToString())) { WasReachedTheEnd = true; }
        }

    }

}