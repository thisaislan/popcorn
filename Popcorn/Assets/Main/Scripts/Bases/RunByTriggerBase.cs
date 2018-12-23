using UnityEngine;
using Errors = Popcorn.Metadatas.Strings.Errors;
using PersonsTags = Popcorn.Metadatas.Tags.Persons;
using ErrorsAuxs = Popcorn.Metadatas.Strings.ErrorsAuxs;
using CombineCharacters = Popcorn.Metadatas.Strings.CombineCharacters;

namespace Popcorn.Bases
{

    [RequireComponent(typeof(Collider2D))]
    public abstract class RunByTriggerBase : MonoBehaviour
    {

        protected virtual void Awake() { CheckTrigger(); }

        void CheckTrigger()
        {
            Component[] components = gameObject.GetComponents(typeof(Collider2D));

            foreach (var component in components)
            {
                if ((component as Collider2D).isTrigger) { return; }
            }

            throw new UnityException(Errors.TriggerNotFoundInRunWhenPlayerIsNear +
                    CombineCharacters.SpaceColonSpace +
                    ErrorsAuxs.Caller +
                    this.ToString() +
                    CombineCharacters.CommaSpace +
                    ErrorsAuxs.GameObject +
                    this.gameObject.ToString());
        }

        protected virtual void OnTriggerEnter2D(Collider2D otherCollider2D)
        {
            if (otherCollider2D.CompareTag(PersonsTags.Player.ToString())) { StartRun(); }
        }

        protected abstract void StartRun();

        protected virtual void OnTriggerExit2D(Collider2D otherCollider2D)
        {
            if (otherCollider2D.CompareTag(PersonsTags.Player.ToString())) { StopRun(); }

        }

        protected abstract void StopRun();

    }

}
