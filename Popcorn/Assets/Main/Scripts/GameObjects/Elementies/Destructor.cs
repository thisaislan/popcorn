using UnityEngine;
using Popcorn.ObjectsServices;
using PersonsTags = Popcorn.Metadados.Tags.Persons;
using ElementiesTags = Popcorn.Metadados.Tags.Elementies;

namespace Popcorn.GameObjects.Elementies
{

    [RequireComponent(typeof(Collider2D))]
    public class Destructor : MonoBehaviour
    {

        [SerializeField]
        float distanceBellowFromBottomLimitView = 10;
        [SerializeField]
        float incrementeInHorizontalSize = 60;

        void Awake()
        {
            PositionUtil positionUtil = new PositionUtil();

            GameObject bottomLimitView = Getter.ObjectWithTag(caller: this, tag: ElementiesTags.BottomLimitView.ToString());
            GameObject rightLimitView = Getter.ObjectWithTag(caller: this, tag: ElementiesTags.RightLimitView.ToString());
            GameObject leftLimitView = Getter.ObjectWithTag(caller: this, tag: ElementiesTags.LeftLimitView.ToString());

            BoxCollider2D boxCollider2D = (BoxCollider2D)Getter.Component(this, gameObject, typeof(BoxCollider2D));

            Vector2 vector2 = boxCollider2D.size;
            vector2.x = positionUtil.GetHorizontalDistanceBetweenGameObjects(leftLimitView, rightLimitView) + incrementeInHorizontalSize;
            vector2.y = 1;

            boxCollider2D.size = vector2;

            Vector3 pos = this.transform.position;
            pos.x = positionUtil.GetHorizontalMiddlePointBetweenGameObjects(leftLimitView, rightLimitView);
            pos.y = bottomLimitView.transform.position.y - distanceBellowFromBottomLimitView;
            this.transform.position = pos;
        }

        void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.CompareTag(PersonsTags.Player.ToString()) == false) Object.Destroy(coll.gameObject);
        }

    }
}