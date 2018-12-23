using UnityEngine;
using Popcorn.ObjectsServices;
using Errors = Popcorn.Metadatas.Strings.Errors;
using PersonsTags = Popcorn.Metadatas.Tags.Persons;
using ElementiesTags = Popcorn.Metadatas.Tags.Elementies;

namespace Popcorn.GameObjects.Elementies
{

    [RequireComponent(typeof(GameBehavior))]
    public class CameraBehavior : MonoBehaviour
    {

        [SerializeField] Vector2 offset = new Vector2(0, 0);
        [SerializeField] Vector2 lerpAmount = new Vector2(0.05f, 0.05f);

        Transform target;
        GameObject leftLimitView;
        GameObject rightLimitView;
        GameObject upLimitView;
        GameObject bottomLimitView;

        void Awake()
        {
            FindTarget();
            FindLimites();
            CheckICrossLimits();
        }

        void FindTarget()
        {
            target = Getter.ObjectWithTag(caller: this,
                tag: PersonsTags.Player.ToString(),
                errorOnNotFound: Errors.PlayerNotFound
                ).transform;

            Vector3 pos = this.transform.position;
            pos.x = target.position.x + offset.x + 3;
            pos.y = target.position.y + offset.y;
            this.transform.position = pos;
        }

        void FindLimites()
        {
            leftLimitView = Getter.SingleInstanceObjectWithTag(caller: this,
                tag: ElementiesTags.LeftLimitView.ToString(),
                errorOnNotFound: Errors.AnyLimitViewNotFound,
                multiplesInstance: Errors.AnyLimitViewIsMultiplied);

            rightLimitView = Getter.SingleInstanceObjectWithTag(caller: this,
                tag: ElementiesTags.RightLimitView.ToString(),
                errorOnNotFound: Errors.AnyLimitViewNotFound,
                multiplesInstance: Errors.AnyLimitViewIsMultiplied);

            upLimitView = Getter.SingleInstanceObjectWithTag(caller: this,
                tag: ElementiesTags.UpLimitView.ToString(),
                errorOnNotFound: Errors.AnyLimitViewNotFound,
                multiplesInstance: Errors.AnyLimitViewIsMultiplied);

            bottomLimitView = Getter.SingleInstanceObjectWithTag(caller: this,
                tag: ElementiesTags.BottomLimitView.ToString(),
                errorOnNotFound: Errors.AnyLimitViewNotFound,
                multiplesInstance: Errors.AnyLimitViewIsMultiplied);
        }

        void CheckICrossLimits()
        {
            Checker.CruzedHorizontalPosition(leftGameObject: leftLimitView, rightGameObject: rightLimitView, error: Errors.CruzedHorizontalLimits);
            Checker.CruzedVerticalPosition(upGameObject: upLimitView, bottomGameObject: bottomLimitView, error: Errors.CruzedVerticalLimits);
        }

        void Update()
        {
            if (target == null) { return; }
            else { Follow(); }
        }

        void Follow()
        {
            Vector3 thisPosition = this.transform.position;

            if (target.position.x <= leftLimitView.transform.position.x)
            {
                thisPosition.x = Mathf.Lerp(thisPosition.x, leftLimitView.transform.position.x + offset.x, lerpAmount.x);
            }
            else if (target.position.x >= rightLimitView.transform.position.x)
            {
                thisPosition.x = Mathf.Lerp(thisPosition.x, rightLimitView.transform.position.x + offset.x, lerpAmount.x);
            }
            else
            {
                thisPosition.x = Mathf.Lerp(thisPosition.x, target.position.x + offset.x, lerpAmount.x);
            }

            if (target.position.y <= bottomLimitView.transform.position.y)
            {
                thisPosition.y = Mathf.Lerp(thisPosition.y, bottomLimitView.transform.position.y + offset.y, lerpAmount.y);
            }
            else if (target.position.y >= upLimitView.transform.position.y)
            {
                thisPosition.y = Mathf.Lerp(thisPosition.y, upLimitView.transform.position.y + offset.y, lerpAmount.y);
            }
            else
            {
                thisPosition.y = Mathf.Lerp(thisPosition.y, target.position.y + offset.y, lerpAmount.y);
            }

            this.transform.position = thisPosition;
        }

    }

}