using UnityEngine;
using Errors = Popcorn.Metadados.Strings.Errors;
using PersonsTags = Popcorn.Metadados.Tags.Persons;
using ErrorsAuxs = Popcorn.Metadados.Strings.ErrorsAuxs;
using CombineCharacters = Popcorn.Metadados.Strings.CombineCharacters;

namespace Popcorn.Bases
{

    [RequireComponent(typeof(Collider2D))]
    public abstract class RunByTriggerBase : MonoBehaviour
    {

        protected virtual void Awake()
        {
            CheckTrigger();
        }

        void CheckTrigger()
        {
            Component[] components = gameObject.GetComponents(typeof(Collider2D));

            foreach (var component in components)
            {
                if ((component as Collider2D).isTrigger) return;
            }

            throw new UnityException(Errors.TRIGGER_NOT_FOUND_IN_RUN_WHEN_PLAYER_IS_NEAR +
                    CombineCharacters.SPACE_COLON_SPACE +
                    ErrorsAuxs.CALLER +
                    this.ToString() +
                    CombineCharacters.COMMA_SPACE +
                    ErrorsAuxs.GAME_OBJECT +
                    this.gameObject.ToString());
        }

        protected virtual void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.CompareTag(PersonsTags.Player.ToString()) == true) StartRun();
        }

        protected abstract void StartRun();

        protected virtual void OnTriggerExit2D(Collider2D coll)
        {
            if (coll.CompareTag(PersonsTags.Player.ToString()) == true) StopRun();

        }

        protected abstract void StopRun();

    }
}
