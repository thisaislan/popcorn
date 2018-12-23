using UnityEngine;
using PersonsTags = Popcorn.Metadatas.Tags.Persons;

namespace Popcorn.GameObjects.Objects
{

    public class StartPoint : MonoBehaviour
    {

        void Awake()
        {
            GameObject player = GameObject.FindGameObjectWithTag(PersonsTags.Player.ToString());

            if (player != null) { player.transform.position = new Vector3(this.transform.position.x + 2f, this.transform.position.y, 0); }
        }

    }

}