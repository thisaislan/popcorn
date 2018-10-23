using UnityEngine;

namespace Popcorn.ObjectsServices
{

    public static class Checker
    {

        public static void ObjectExistence(string tag, string error)
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag(tag);

            if (gameObject == null)
            {
                throw new UnityException(error);
            }
        }

        public static void SingleExistence(string tag, string errorIfNone, string errorIfMultiple)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);

            if (gameObjects.Length == 0)
            {
                throw new UnityException(errorIfNone);
            }
            else if (gameObjects.Length > 1)
            {
                throw new UnityException(errorIfMultiple);
            }
        }

        public static void SingleOrNoneExistence(string tag, string errorIfMultiple)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);

            if (gameObjects.Length > 1)
            {
                throw new UnityException(errorIfMultiple);
            }
        }

        public static void CruzedHorizontalPosition(GameObject leftGameObject, GameObject rightGameObject, string error)
        {
            if (leftGameObject.transform.position.x > rightGameObject.transform.position.x)
            {
                throw new UnityException(error);
            }
        }

        public static void CruzedVerticalPosition(GameObject upGameObject, GameObject bottomGameObject, string error)
        {
            if (upGameObject.transform.position.y < bottomGameObject.transform.position.y)
            {
                throw new UnityException(error);
            }
        }

        public static void Tag(GameObject gameObject, string expectedTag, string error)
        {
            if (gameObject.tag != expectedTag)
            {
                throw new UnityException(error);
            }
        }

    }
}