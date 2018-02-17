using UnityEngine;
using Errors = Popcorn.Metadados.Strings.Errors;
using LimitSide = Popcorn.Metadados.Position.Sides;
using ElementiesTags = Popcorn.Metadados.Tags.Elementies;
using CombineCharacters = Popcorn.Metadados.Strings.CombineCharacters;

namespace Popcorn.GameObjects.Elementies
{
    public class LimitView : MonoBehaviour
    {

        [SerializeField]
        LimitSide limitSide;
        [SerializeField]
        GameObject gameObjectRelativy;
        [SerializeField]
        float distance = 7;

        void Awake()
        {
            if (IsConfigOk()) SetPosition();
        }

        bool IsConfigOk()
        {
            string error = null;

            if (this.CompareTag(ElementiesTags.RightLimitView.ToString()) == true)
            {
                if (limitSide != LimitSide.Right) error = Errors.ANY_LIMIT_VIEW_WITH_THE_WRONG_LIMIT_SIDE;
            }
            else if (this.CompareTag(ElementiesTags.LeftLimitView.ToString()) == true)
            {
                if (limitSide != LimitSide.Left) error = Errors.ANY_LIMIT_VIEW_WITH_THE_WRONG_LIMIT_SIDE;
            }
            else if (this.CompareTag(ElementiesTags.UpLimitView.ToString()) == true)
            {
                if (limitSide != LimitSide.Up) error = Errors.ANY_LIMIT_VIEW_WITH_THE_WRONG_LIMIT_SIDE;
            }
            else if (this.CompareTag(ElementiesTags.BottomLimitView.ToString()) == true)
            {
                if (limitSide != LimitSide.Bottom) error = Errors.ANY_LIMIT_VIEW_WITH_THE_WRONG_LIMIT_SIDE;
            }
            else
            {
                error = Errors.ANY_LIMIT_VIEW_WITH_THE_WRONG_TAG;
            }

            if (error != null) throw new UnityException(error + CombineCharacters.SPACE_COLON_SPACE + this.gameObject.ToString());
            else return true;
        }

        void SetPosition()
        {
            if (gameObjectRelativy != null)
            {
                Vector3 pos = this.transform.position;

                switch (limitSide)
                {
                    case LimitSide.Left:
                        pos.x = gameObjectRelativy.transform.position.x + distance;
                        pos.y = gameObjectRelativy.transform.position.y;
                        break;
                    case LimitSide.Right:
                        pos.x = gameObjectRelativy.transform.position.x - distance;
                        pos.y = gameObjectRelativy.transform.position.y;
                        break;
                    case LimitSide.Up:
                        pos.x = gameObjectRelativy.transform.position.x;
                        pos.y = gameObjectRelativy.transform.position.y - distance;
                        break;
                    case LimitSide.Bottom:
                        pos.x = gameObjectRelativy.transform.position.x;
                        pos.y = gameObjectRelativy.transform.position.y + distance;
                        break;
                }
                this.transform.position = pos;
            }
        }

    }
}
