using UnityEngine;
using Popcorn.ObjectsServices;
using Errors = Popcorn.Metadados.Strings.Errors;
using ErrorsAuxs = Popcorn.Metadados.Strings.ErrorsAuxs;
using CombineCharacters = Popcorn.Metadados.Strings.CombineCharacters;

namespace Popcorn.Bases
{

    [RequireComponent(typeof(Collider2D))]
    public abstract class TriggerHelperBase : MonoBehaviour
    {

        protected virtual void Awake()
        {
            Collider2D coll = (Collider2D)Getter.Component(this, gameObject, typeof(Collider2D));

            if (!coll.isTrigger)
            {

                throw new UnityException(Errors.TRIGGER_NOT_FOUND_IN_A_TRIGGER_HELPER +
                    CombineCharacters.SPACE_COLON_SPACE +
                    ErrorsAuxs.CALLER +
                    this.ToString() +
                    CombineCharacters.COMMA_SPACE +
                    ErrorsAuxs.GAME_OBJECT +
                    this.gameObject.ToString());
            }
        }

    }
}
