using UnityEngine;
using Errors = Popcorn.Metadatas.Strings.Errors;
using LimitSide = Popcorn.Metadatas.Position.Sides;
using ElementiesTags = Popcorn.Metadatas.Tags.Elementies;
using CombineCharacters = Popcorn.Metadatas.Strings.CombineCharacters;

namespace Popcorn.GameObjects.Elementies
{
    public class LimitView : MonoBehaviour
    {

        [SerializeField] LimitSide limitSide;
        [SerializeField] GameObject gameObjectRelativy;
        [SerializeField] float distance = 7;

        void Awake()
        {
            if (IsConfigOk()) { SetPosition(); }
        }

        bool IsConfigOk()
        {
            string error = null;

            if (this.CompareTag(ElementiesTags.RightLimitView.ToString()))
            {
                if (limitSide != LimitSide.Right)
                {
                    error = Errors.AnyLimitViewWithTheWrongLimitSide;
                }
            }
            else if (this.CompareTag(ElementiesTags.LeftLimitView.ToString()))
            {
                if (limitSide != LimitSide.Left)
                {
                    error = Errors.AnyLimitViewWithTheWrongLimitSide;
                }
            }
            else if (this.CompareTag(ElementiesTags.UpLimitView.ToString()))
            {
                if (limitSide != LimitSide.Up)
                {
                    error = Errors.AnyLimitViewWithTheWrongLimitSide;
                }
            }
            else if (this.CompareTag(ElementiesTags.BottomLimitView.ToString()))
            {
                if (limitSide != LimitSide.Bottom)
                {
                    error = Errors.AnyLimitViewWithTheWrongLimitSide;
                }
            }
            else
            {
                error = Errors.AnyLimitViewWithTheWrongTag;
            }

            if (error != null)
            {
                throw new UnityException(error + CombineCharacters.SpaceColonSpace + this.gameObject.ToString());
            }
            else
            {
                return true;
            }
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