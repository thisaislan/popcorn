using UnityEngine;
using Popcorn.ObjectsServices;
using PersonsTags = Popcorn.Metadatas.Tags.Persons;
using ElementiesTags = Popcorn.Metadatas.Tags.Elementies;

namespace Popcorn.GameObjects.Elementies
{

    [RequireComponent(typeof(Collider2D))]
    public class Destructor : MonoBehaviour
    {

        [SerializeField]
        float DistanceBellowFromBottomLimitView = 10;
        [SerializeField]
        float IncrementeInHorizontalSize = 60;

        void Awake()
        {
            PositionUtil positionUtil = new PositionUtil();

            GameObject bottomLimitView = Getter.ObjectWithTag(caller: this, tag: ElementiesTags.BottomLimitView.ToString());
            GameObject rightLimitView = Getter.ObjectWithTag(caller: this, tag: ElementiesTags.RightLimitView.ToString());
            GameObject leftLimitView = Getter.ObjectWithTag(caller: this, tag: ElementiesTags.LeftLimitView.ToString());

            BoxCollider2D boxCollider2D = (BoxCollider2D)Getter.Component(this, gameObject, typeof(BoxCollider2D));

            Vector2 vector2 = boxCollider2D.size;
            vector2.x = positionUtil.GetHorizontalDistanceBetweenGameObjects(leftLimitView, rightLimitView) + IncrementeInHorizontalSize;
            vector2.y = 1;

            boxCollider2D.size = vector2;

            Vector3 thisPosition = this.transform.position;
            thisPosition.x = positionUtil.GetHorizontalMiddlePointBetweenGameObjects(leftLimitView, rightLimitView);
            thisPosition.y = bottomLimitView.transform.position.y - DistanceBellowFromBottomLimitView;
            this.transform.position = thisPosition;
        }

        void OnTriggerEnter2D(Collider2D otherCollider2D)
        {
            if (!otherCollider2D.CompareTag(PersonsTags.Player.ToString()))
            {
                Object.Destroy(otherCollider2D.gameObject);
            }
        }

    }
}