using UnityEngine;
using UnityEngine.UI;
using Popcorn.ObjectsServices;

namespace Popcorn.Canvas.Components
{

    public class Shadow : MonoBehaviour
    {

        [SerializeField]
        Text Target;
        [SerializeField]
        float ShadowInX;
        [SerializeField]
        float ShadowInY;

        void Awake()
        {
            Vector3 behindPos = transform.position;

            behindPos = new Vector3(Target.transform.position.x + ShadowInX, Target.transform.position.y + ShadowInY, Target.transform.position.z - 1);
            transform.position = behindPos;
        }

        void Update()
        {
            ((Text)Getter.Component(this, gameObject, typeof(Text))).text = Target.text;
        }

    }
}