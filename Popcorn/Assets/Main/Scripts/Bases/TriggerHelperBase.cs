using UnityEngine;
using Popcorn.ObjectsServices;
using Errors = Popcorn.Metadatas.Strings.Errors;
using ErrorsAuxs = Popcorn.Metadatas.Strings.ErrorsAuxs;
using CombineCharacters = Popcorn.Metadatas.Strings.CombineCharacters;

namespace Popcorn.Bases
{

    [RequireComponent(typeof(Collider2D))]
    public abstract class TriggerHelperBase : MonoBehaviour
    {

        protected virtual void Awake()
        {
            Collider2D ThisCollider2D = (Collider2D)Getter.Component(this, gameObject, typeof(Collider2D));

            if (!ThisCollider2D.isTrigger)
            {

                throw new UnityException(Errors.TriggerNotFoundInATriggerHelper +
                    CombineCharacters.SpaceColonSpace +
                    ErrorsAuxs.Caller +
                    this.ToString() +
                    CombineCharacters.CommaSpace +
                    ErrorsAuxs.GameObject +
                    this.gameObject.ToString());
            }
        }

    }
}
