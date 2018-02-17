using UnityEngine;
using RelativyPosiotions = Popcorn.Metadados.Position.RelativyPosiotions;

namespace Popcorn.GameObjects.Objects
{

    public class Limit : MonoBehaviour
    {

        [SerializeField]
        RelativyPosiotions relativyPosiotion;
        [SerializeField]
        GameObject gameObjectRelativy;
        [SerializeField]
        float distance = 4;

        void Awake()
        {
            if (gameObjectRelativy != null) SetPosition();
        }

        void SetPosition()
        {
            Vector3 pos = this.transform.position;

            switch (relativyPosiotion)
            {
                case RelativyPosiotions.After:
                    pos.x = gameObjectRelativy.transform.position.x + distance;
                    break;
                case RelativyPosiotions.Before:
                    pos.x = gameObjectRelativy.transform.position.x - distance;
                    break;
                case RelativyPosiotions.Bellow:
                    pos.y = gameObjectRelativy.transform.position.y - distance;
                    break;
                case RelativyPosiotions.Above:
                    pos.y = gameObjectRelativy.transform.position.y + distance;
                    break;
            }
            this.transform.position = pos;
        }

    }
}
