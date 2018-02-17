using UnityEngine;
using UnityEngine.UI;
using Popcorn.ObjectsServices;

namespace Popcorn.Canvas.Components
{

    public class Shadow : MonoBehaviour
    {

        [SerializeField]
        Text target;
        [SerializeField]
        float shadowInX;
        [SerializeField]
        float shadowInY;

        void Awake()
        {
            Vector3 behindPos = transform.position;

            behindPos = new Vector3(target.transform.position.x + shadowInX, target.transform.position.y + shadowInY, target.transform.position.z - 1);
            transform.position = behindPos;
        }

        void Update()
        {
            ((Text)Getter.Component(this, gameObject, typeof(Text))).text = target.text;
        }

    }
}