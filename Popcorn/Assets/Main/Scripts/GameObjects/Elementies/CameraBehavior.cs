using UnityEngine;
using Popcorn.ObjectsServices;
using Errors = Popcorn.Metadados.Strings.Errors;
using PersonsTags = Popcorn.Metadados.Tags.Persons;
using ElementiesTags = Popcorn.Metadados.Tags.Elementies;

namespace Popcorn.GameObjects.Elementies
{

    [RequireComponent(typeof(GameBehavior))]
    public class CameraBehavior : MonoBehaviour
    {

        [SerializeField]
        Vector2 offset = new Vector2(0, 0);
        [SerializeField]
        Vector2 lerpAmount = new Vector2(0.05f, 0.05f);

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
            target = Getter.ObjectWithTag(
                caller: this,
                tag: PersonsTags.Player.ToString(),
                errorOnNotFound: Errors.PLAYER_NOT_FOUND
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
                errorOnNotFound: Errors.ANY_LIMIT_VIEW_NOT_FOUND,
                multiplesInstance: Errors.ANY_LIMIT_VIEW_IS_MULTIPLIED);

            rightLimitView = Getter.SingleInstanceObjectWithTag(caller: this,
                tag: ElementiesTags.RightLimitView.ToString(),
                errorOnNotFound: Errors.ANY_LIMIT_VIEW_NOT_FOUND,
                multiplesInstance: Errors.ANY_LIMIT_VIEW_IS_MULTIPLIED);

            upLimitView = Getter.SingleInstanceObjectWithTag(caller: this,
                tag: ElementiesTags.UpLimitView.ToString(),
                errorOnNotFound: Errors.ANY_LIMIT_VIEW_NOT_FOUND,
                multiplesInstance: Errors.ANY_LIMIT_VIEW_IS_MULTIPLIED);

            bottomLimitView = Getter.SingleInstanceObjectWithTag(caller: this,
                tag: ElementiesTags.BottomLimitView.ToString(),
                errorOnNotFound: Errors.ANY_LIMIT_VIEW_NOT_FOUND,
                multiplesInstance: Errors.ANY_LIMIT_VIEW_IS_MULTIPLIED);
        }

        void CheckICrossLimits()
        {
            Checker.CruzedHorizontalPosition(leftGameObject: leftLimitView, rightGameObject: rightLimitView, error: Errors.CRUZED_HORIZONTAL_LIMITS);
            Checker.CruzedVerticalPosition(upGameObject: upLimitView, bottomGameObject: bottomLimitView, error: Errors.CRUZED_VERTICAL_LIMITS);
        }

        void Update()
        {
            if (target == null) return;
            else Follow();
        }

        void Follow()
        {
            Vector3 pos = this.transform.position;

            if (target.position.x <= leftLimitView.transform.position.x)
                pos.x = Mathf.Lerp(pos.x, leftLimitView.transform.position.x + offset.x, lerpAmount.x);
            else if (target.position.x >= rightLimitView.transform.position.x)
                pos.x = Mathf.Lerp(pos.x, rightLimitView.transform.position.x + offset.x, lerpAmount.x);
            else
                pos.x = Mathf.Lerp(pos.x, target.position.x + offset.x, lerpAmount.x);

            if (target.position.y <= bottomLimitView.transform.position.y)
                pos.y = Mathf.Lerp(pos.y, bottomLimitView.transform.position.y + offset.y, lerpAmount.y);
            else if (target.position.y >= upLimitView.transform.position.y)
                pos.y = Mathf.Lerp(pos.y, upLimitView.transform.position.y + offset.y, lerpAmount.y);
            else
                pos.y = Mathf.Lerp(pos.y, target.position.y + offset.y, lerpAmount.y);

            this.transform.position = pos;
        }

    }
}
