using UnityEngine;
using Popcorn.ObjectsServices;
using Popcorn.GameObjects.Helpers;
using Popcorn.GameObjects.Elementies;
using Errors = Popcorn.Metadatas.Strings.Errors;
using PersonsTags = Popcorn.Metadatas.Tags.Persons;
using ErrorsAuxs = Popcorn.Metadatas.Strings.ErrorsAuxs;
using CombineCharacters = Popcorn.Metadatas.Strings.CombineCharacters;
using GameStates = Popcorn.GameObjects.Elementies.GameBehavior.GameStates;

namespace Popcorn.Bases
{

    [RequireComponent(typeof(WeakPoint))]
    public abstract class EnemyBase : PersonBase
    {

        WeakPoint ThisWeakPoint;

        protected override void Awake()
        {
            base.Awake();

            GetWeakPoint();

            Checker.Tag(gameObject: gameObject,
                expectedTag: PersonsTags.Enemy.ToString(),
                error: Errors.WrongEnemyTag +
                CombineCharacters.CommaSpace +
                ErrorsAuxs.GameObject +
                gameObject.ToString() +
                CombineCharacters.CommaSpace +
                ErrorsAuxs.Tag +
                gameObject.tag);
        }

        void GetWeakPoint()
        {
            ThisWeakPoint = (WeakPoint)Getter.ComponentInChild(this, gameObject, typeof(WeakPoint), 0);
        }

        protected virtual void Update()
        {
            
            if (ThisWeakPoint.IsColliding && GameBehavior.GameState == GameStates.Runing)
            {                
                WeakPointHitted();
            }
        }

        protected abstract void WeakPointHitted();

    }
}