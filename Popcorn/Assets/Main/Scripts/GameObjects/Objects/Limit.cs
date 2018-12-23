using UnityEngine;
using RelativyPosiotions = Popcorn.Metadatas.Position.RelativyPosiotions;

namespace Popcorn.GameObjects.Objects
{

    public class Limit : MonoBehaviour
    {

        [SerializeField] RelativyPosiotions relativyPosiotion;
        [SerializeField] GameObject gameObjectRelativy;
        [SerializeField] float distance = 4;

        void Awake()
        {
            if (gameObjectRelativy != null) { SetPosition(); }
        }

        void SetPosition()
        {
            Vector3 ThisPosition = this.transform.position;

            switch (relativyPosiotion)
            {
                case RelativyPosiotions.After: ThisPosition.x = gameObjectRelativy.transform.position.x + distance; break;
                case RelativyPosiotions.Before: ThisPosition.x = gameObjectRelativy.transform.position.x - distance; break;
                case RelativyPosiotions.Bellow: ThisPosition.y = gameObjectRelativy.transform.position.y - distance; break;
                case RelativyPosiotions.Above: ThisPosition.y = gameObjectRelativy.transform.position.y + distance; break;
            }
            this.transform.position = ThisPosition;
        }

    }
    
}
