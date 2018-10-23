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

        [SerializeField]
        Vector2 Offset = new Vector2(0, 0);
        [SerializeField]
        Vector2 LerpAmount = new Vector2(0.05f, 0.05f);

        Transform Target;
        GameObject LeftLimitView;
        GameObject RightLimitView;
        GameObject UpLimitView;
        GameObject BottomLimitView;

        void Awake()
        {
            FindTarget();
            FindLimites();
            CheckICrossLimits();
        }

        void FindTarget()
        {
            Target = Getter.ObjectWithTag(
                caller: this,
                tag: PersonsTags.Player.ToString(),
                errorOnNotFound: Errors.PlayerNotFound
                ).transform;

            Vector3 pos = this.transform.position;
            pos.x = Target.position.x + Offset.x + 3;
            pos.y = Target.position.y + Offset.y;
            this.transform.position = pos;
        }

        void FindLimites()
        {

            LeftLimitView = Getter.SingleInstanceObjectWithTag(caller: this,
                tag: ElementiesTags.LeftLimitView.ToString(),
                errorOnNotFound: Errors.AnyLimitViewNotFound,
                multiplesInstance: Errors.AnyLimitViewIsMultiplied);

            RightLimitView = Getter.SingleInstanceObjectWithTag(caller: this,
                tag: ElementiesTags.RightLimitView.ToString(),
                errorOnNotFound: Errors.AnyLimitViewNotFound,
                multiplesInstance: Errors.AnyLimitViewIsMultiplied);

            UpLimitView = Getter.SingleInstanceObjectWithTag(caller: this,
                tag: ElementiesTags.UpLimitView.ToString(),
                errorOnNotFound: Errors.AnyLimitViewNotFound,
                multiplesInstance: Errors.AnyLimitViewIsMultiplied);

            BottomLimitView = Getter.SingleInstanceObjectWithTag(caller: this,
                tag: ElementiesTags.BottomLimitView.ToString(),
                errorOnNotFound: Errors.AnyLimitViewNotFound,
                multiplesInstance: Errors.AnyLimitViewIsMultiplied);
        }

        void CheckICrossLimits()
        {
            Checker.CruzedHorizontalPosition(leftGameObject: LeftLimitView, rightGameObject: RightLimitView, error: Errors.CruzedHorizontalLimits);
            Checker.CruzedVerticalPosition(upGameObject: UpLimitView, bottomGameObject: BottomLimitView, error: Errors.CruzedVerticalLimits);
        }

        void Update()
        {
            if (Target == null)
            {
                return;
            }
            else
            {
                Follow();
            }
        }

        void Follow()
        {
            Vector3 thisPosition = this.transform.position;

            if (Target.position.x <= LeftLimitView.transform.position.x)
            {
                thisPosition.x = Mathf.Lerp(thisPosition.x, LeftLimitView.transform.position.x + Offset.x, LerpAmount.x);
            }
            else if (Target.position.x >= RightLimitView.transform.position.x)
            {
                thisPosition.x = Mathf.Lerp(thisPosition.x, RightLimitView.transform.position.x + Offset.x, LerpAmount.x);
            }
            else
            {
                thisPosition.x = Mathf.Lerp(thisPosition.x, Target.position.x + Offset.x, LerpAmount.x);
            }

            if (Target.position.y <= BottomLimitView.transform.position.y)
            {
                thisPosition.y = Mathf.Lerp(thisPosition.y, BottomLimitView.transform.position.y + Offset.y, LerpAmount.y);
            }
            else if (Target.position.y >= UpLimitView.transform.position.y)
            {
                thisPosition.y = Mathf.Lerp(thisPosition.y, UpLimitView.transform.position.y + Offset.y, LerpAmount.y);
            }
            else
            {
                thisPosition.y = Mathf.Lerp(thisPosition.y, Target.position.y + Offset.y, LerpAmount.y);
            }

            this.transform.position = thisPosition;
        }

    }
}
