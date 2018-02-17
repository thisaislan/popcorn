using UnityEngine;
using Popcorn.ObjectsServices;
using Popcorn.GameObjects.Helpers;
using Errors = Popcorn.Metadados.Strings.Errors;
using PersonsTags = Popcorn.Metadados.Tags.Persons;
using ErrorsAuxs = Popcorn.Metadados.Strings.ErrorsAuxs;
using CombineCharacters = Popcorn.Metadados.Strings.CombineCharacters;

namespace Popcorn.Bases
{

    [RequireComponent(typeof(WeakPoint))]
    public abstract class EnemyBase : PersonBase
    {

        WeakPoint weakPoint;

        protected override void Awake()
        {
            base.Awake();

            GetWeakPoint();

            Checker.Tag(gameObject: gameObject,
                expectedTag: PersonsTags.Enemy.ToString(),
                error: Errors.WRONG_ENEMY_TAG +
                CombineCharacters.COMMA_SPACE +
                ErrorsAuxs.GAME_OBJECT +
                gameObject.ToString() +
                CombineCharacters.COMMA_SPACE +
                ErrorsAuxs.TAG +
                gameObject.tag);
        }

        void GetWeakPoint()
        {
            weakPoint = (WeakPoint)Getter.ComponentInChild(this, gameObject, typeof(WeakPoint), 0);
        }

        protected virtual void Update()
        {
            if (weakPoint.isColliding)
            {
                WeakPointHitted();
            }
        }

        protected abstract void WeakPointHitted();

    }
}