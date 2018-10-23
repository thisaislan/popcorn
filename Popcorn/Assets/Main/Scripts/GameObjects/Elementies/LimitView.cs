using UnityEngine;
using Errors = Popcorn.Metadatas.Strings.Errors;
using LimitSide = Popcorn.Metadatas.Position.Sides;
using ElementiesTags = Popcorn.Metadatas.Tags.Elementies;
using CombineCharacters = Popcorn.Metadatas.Strings.CombineCharacters;

namespace Popcorn.GameObjects.Elementies
{
    public class LimitView : MonoBehaviour
    {

        [SerializeField]
        LimitSide LimitSide;
        [SerializeField]
        GameObject GameObjectRelativy;
        [SerializeField]
        float Distance = 7;

        void Awake()
        {
            if (IsConfigOk())
            {
                SetPosition();
            }
        }

        bool IsConfigOk()
        {
            string error = null;

            if (this.CompareTag(ElementiesTags.RightLimitView.ToString()))
            {
                if (LimitSide != LimitSide.Right)
                {
                    error = Errors.AnyLimitViewWithTheWrongLimitSide;
                }
            }
            else if (this.CompareTag(ElementiesTags.LeftLimitView.ToString()))
            {
                if (LimitSide != LimitSide.Left)
                {
                    error = Errors.AnyLimitViewWithTheWrongLimitSide;
                }
            }
            else if (this.CompareTag(ElementiesTags.UpLimitView.ToString()))
            {
                if (LimitSide != LimitSide.Up)
                {
                    error = Errors.AnyLimitViewWithTheWrongLimitSide;
                }
            }
            else if (this.CompareTag(ElementiesTags.BottomLimitView.ToString()))
            {
                if (LimitSide != LimitSide.Bottom)
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
            if (GameObjectRelativy != null)
            {
                Vector3 pos = this.transform.position;

                switch (LimitSide)
                {
                    case LimitSide.Left:
                        pos.x = GameObjectRelativy.transform.position.x + Distance;
                        pos.y = GameObjectRelativy.transform.position.y;
                        break;
                    case LimitSide.Right:
                        pos.x = GameObjectRelativy.transform.position.x - Distance;
                        pos.y = GameObjectRelativy.transform.position.y;
                        break;
                    case LimitSide.Up:
                        pos.x = GameObjectRelativy.transform.position.x;
                        pos.y = GameObjectRelativy.transform.position.y - Distance;
                        break;
                    case LimitSide.Bottom:
                        pos.x = GameObjectRelativy.transform.position.x;
                        pos.y = GameObjectRelativy.transform.position.y + Distance;
                        break;
                }
                this.transform.position = pos;
            }
        }

    }
}
