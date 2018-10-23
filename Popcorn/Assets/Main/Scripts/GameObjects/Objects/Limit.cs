using UnityEngine;
using RelativyPosiotions = Popcorn.Metadatas.Position.RelativyPosiotions;

namespace Popcorn.GameObjects.Objects
{

    public class Limit : MonoBehaviour
    {

        [SerializeField]
        RelativyPosiotions RelativyPosiotion;
        [SerializeField]
        GameObject GameObjectRelativy;
        [SerializeField]
        float Distance = 4;

        void Awake()
        {
            if (GameObjectRelativy != null)
            {
                SetPosition();
            }
        }

        void SetPosition()
        {
            Vector3 ThisPosition = this.transform.position;

            switch (RelativyPosiotion)
            {
                case RelativyPosiotions.After:
                    ThisPosition.x = GameObjectRelativy.transform.position.x + Distance;
                    break;
                case RelativyPosiotions.Before:
                    ThisPosition.x = GameObjectRelativy.transform.position.x - Distance;
                    break;
                case RelativyPosiotions.Bellow:
                    ThisPosition.y = GameObjectRelativy.transform.position.y - Distance;
                    break;
                case RelativyPosiotions.Above:
                    ThisPosition.y = GameObjectRelativy.transform.position.y + Distance;
                    break;
            }
            this.transform.position = ThisPosition;
        }

    }
}
