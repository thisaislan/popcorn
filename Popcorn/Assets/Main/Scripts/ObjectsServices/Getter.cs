using System;
using UnityEngine;
using Errors = Popcorn.Metadatas.Strings.Errors;
using ErrorsAuxs = Popcorn.Metadatas.Strings.ErrorsAuxs;
using CombineCharacters = Popcorn.Metadatas.Strings.CombineCharacters;

namespace Popcorn.ObjectsServices
{

    public static class Getter
    {

        public static GameObject ObjectWithTag(UnityEngine.Object caller, string tag)
        {
            return objectWithTag(tag, Errors.DefaultErrorObjectNotFound +
                CombineCharacters.SpaceColonSpace +
                ErrorsAuxs.Caller +
                caller.ToString() +
                CombineCharacters.CommaSpace +
                ErrorsAuxs.Tag +
                tag);
        }

        public static GameObject ObjectWithTag(UnityEngine.Object caller, string tag, string errorOnNotFound)
        {
            return objectWithTag(tag, errorOnNotFound +
                CombineCharacters.SpaceColonSpace +
                ErrorsAuxs.Caller +
                caller.ToString() +
                CombineCharacters.CommaSpace +
                ErrorsAuxs.Tag +
                tag);
        }

        private static GameObject objectWithTag(string tag, string errorOnNotFound)
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag(tag);

            if (gameObject == null)
            {
                throw new UnityException(errorOnNotFound);
            }
            else
            {
                return gameObject;
            }
        }

        public static GameObject SingleInstanceObjectWithTag(UnityEngine.Object caller, string tag, string errorOnNotFound, string multiplesInstance)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);

            if (gameObjects.Length == 0)
            {
                throw new UnityException(errorOnNotFound +
                    CombineCharacters.SpaceColonSpace +
                    ErrorsAuxs.Caller +
                    caller.ToString() +
                    CombineCharacters.CommaSpace +
                    ErrorsAuxs.Tag +
                    tag);
            }
            else if (gameObjects.Length > 1)
            {
                throw new UnityException(multiplesInstance +
                    CombineCharacters.SpaceColonSpace +
                    ErrorsAuxs.Caller +
                    caller.ToString() +
                    CombineCharacters.CommaSpace +
                    ErrorsAuxs.Tag +
                    tag);
            }
            else
            {
                return gameObjects[0];
            }
        }

        public static Component ComponentFromObjectWithTag(UnityEngine.Object caller, string tag, Type type)
        {
            GameObject gameObject = ObjectWithTag(caller, tag);
            return Component(caller, gameObject, type);
        }

        public static Component Component(UnityEngine.Object caller, GameObject gameObject, Type type)
        {
            Component component = gameObject.GetComponent(type.Name);

            if (component == null)
            {
                throw new UnityException(Errors.defaultErrorComponentNotFound +
                    CombineCharacters.SpaceColonSpace +
                    ErrorsAuxs.Caller +
                    caller.ToString() +
                    CombineCharacters.CommaSpace +
                    ErrorsAuxs.GameObject +
                    gameObject.ToString() +
                    CombineCharacters.CommaSpace +
                    ErrorsAuxs.Type +
                    type.ToString());
            }
            return component;
        }

        public static Component ComponentInChild(UnityEngine.Object caller, GameObject gameObject, Type type, int position)
        {
            Component component = null;
            Component[] components = gameObject.GetComponentsInChildren(type);

            if (components.Length == 0)
            {
                throw new UnityException(Errors.DefaultErrorComponentInChildNotFound +
                    CombineCharacters.SpaceColonSpace +
                    ErrorsAuxs.Caller +
                    caller.ToString() +
                    CombineCharacters.CommaSpace +
                    ErrorsAuxs.GameObject +
                    gameObject.ToString() +
                    CombineCharacters.CommaSpace +
                    ErrorsAuxs.Type +
                    type.ToString() +
                    CombineCharacters.CommaSpace +
                    ErrorsAuxs.Position +
                    position);
            }

            try
            {
                component = components[position];
            }
            catch (Exception)
            {
                throw new UnityException(Errors.DefaultErrorComponentInChildNotFoundInPosition +
                    CombineCharacters.SpaceColonSpace +
                    ErrorsAuxs.Caller +
                    caller.ToString() +
                    CombineCharacters.CommaSpace +
                    ErrorsAuxs.GameObject +
                    gameObject.ToString() +
                    CombineCharacters.CommaSpace +
                    ErrorsAuxs.Type +
                    type.ToString() +
                    CombineCharacters.CommaSpace +
                    ErrorsAuxs.Position +
                    position);
            }
            return component;
        }

    }
}
