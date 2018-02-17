using UnityEngine;

namespace Popcorn.ObjectsServices
{

    public sealed class PositionUtil
    {

        public float GetHorizontalDistanceBetweenGameObjects(GameObject firstGameOject, GameObject secondGameOject)
        {
            if (firstGameOject.transform.position.x > secondGameOject.transform.position.x)
                return firstGameOject.transform.position.x - secondGameOject.transform.position.x;
            else
                return secondGameOject.transform.position.x - firstGameOject.transform.position.x;
        }

        public float GetHorizontalMiddlePointBetweenGameObjects(GameObject firstGameOject, GameObject secondGameOject)
        {
            return (firstGameOject.transform.position.x + secondGameOject.transform.position.x) / 2;
        }

    }
}
