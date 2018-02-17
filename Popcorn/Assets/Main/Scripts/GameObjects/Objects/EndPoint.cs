using UnityEngine;
using PersonsTags = Popcorn.Metadados.Tags.Persons;

namespace Popcorn.GameObjects.Objects
{

    public class EndPoint : MonoBehaviour
    {

        [HideInInspector]
        public bool wasReachedTheEnd { get; private set; }

        private void Awake()
        {
            wasReachedTheEnd = false;
        }

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.CompareTag(PersonsTags.Player.ToString()) == true) wasReachedTheEnd = true;
        }

    }
}