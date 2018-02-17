using System;
using UnityEngine;
using Errors = Popcorn.Metadados.Strings.Errors;
using ErrorsAuxs = Popcorn.Metadados.Strings.ErrorsAuxs;
using CombineCharacters = Popcorn.Metadados.Strings.CombineCharacters;

namespace Popcorn.ObjectsServices
{

    public static class Getter
    {

        public static GameObject ObjectWithTag(UnityEngine.Object caller, string tag)
        {
            return objectWithTag(tag, Errors.DEFAULT_ERROR_OBJECT_NOT_FOUND +
                CombineCharacters.SPACE_COLON_SPACE +
                ErrorsAuxs.CALLER +
                caller.ToString() +
                CombineCharacters.COMMA_SPACE +
                ErrorsAuxs.TAG +
                tag);
        }

        public static GameObject ObjectWithTag(UnityEngine.Object caller, string tag, string errorOnNotFound)
        {
            return objectWithTag(tag, errorOnNotFound +
                CombineCharacters.SPACE_COLON_SPACE +
                ErrorsAuxs.CALLER +
                caller.ToString() +
                CombineCharacters.COMMA_SPACE +
                ErrorsAuxs.TAG +
                tag);
        }

        private static GameObject objectWithTag(string tag, string errorOnNotFound)
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag(tag);

            if (gameObject == null) throw new UnityException(errorOnNotFound);
            else return gameObject;
        }

        public static GameObject SingleInstanceObjectWithTag(UnityEngine.Object caller, string tag, string errorOnNotFound, string multiplesInstance)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);

            if (gameObjects.Length == 0)
            {
                throw new UnityException(errorOnNotFound +
                    CombineCharacters.SPACE_COLON_SPACE +
                    ErrorsAuxs.CALLER +
                    caller.ToString() +
                    CombineCharacters.COMMA_SPACE +
                    ErrorsAuxs.TAG +
                    tag);
            }
            else if (gameObjects.Length > 1)
            {
                throw new UnityException(multiplesInstance +
                    CombineCharacters.SPACE_COLON_SPACE +
                    ErrorsAuxs.CALLER +
                    caller.ToString() +
                    CombineCharacters.COMMA_SPACE +
                    ErrorsAuxs.TAG +
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
                throw new UnityException(Errors.DEFAULT_ERROR_COMPONENT_NOT_FOUND +
                    CombineCharacters.SPACE_COLON_SPACE +
                    ErrorsAuxs.CALLER +
                    caller.ToString() +
                    CombineCharacters.COMMA_SPACE +
                    ErrorsAuxs.GAME_OBJECT +
                    gameObject.ToString() +
                    CombineCharacters.COMMA_SPACE +
                    ErrorsAuxs.TYPE +
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
                throw new UnityException(Errors.DEFAULT_ERROR_COMPONENT_IN_CHILD_NOT_FOUND +
                    CombineCharacters.SPACE_COLON_SPACE +
                    ErrorsAuxs.CALLER +
                    caller.ToString() +
                    CombineCharacters.COMMA_SPACE +
                    ErrorsAuxs.GAME_OBJECT +
                    gameObject.ToString() +
                    CombineCharacters.COMMA_SPACE +
                    ErrorsAuxs.TYPE +
                    type.ToString() +
                    CombineCharacters.COMMA_SPACE +
                    ErrorsAuxs.POSITION +
                    position);
            }

            try
            {
                component = components[position];
            }
            catch (Exception)
            {
                throw new UnityException(Errors.DEFAULT_ERROR_COMPONENT_IN_CHILD_NOT_FOUND_IN_POSITION +
                    CombineCharacters.SPACE_COLON_SPACE +
                    ErrorsAuxs.CALLER +
                    caller.ToString() +
                    CombineCharacters.COMMA_SPACE +
                    ErrorsAuxs.GAME_OBJECT +
                    gameObject.ToString() +
                    CombineCharacters.COMMA_SPACE +
                    ErrorsAuxs.TYPE +
                    type.ToString() +
                    CombineCharacters.COMMA_SPACE +
                    ErrorsAuxs.POSITION +
                    position);
            }
            return component;
        }

    }
}
