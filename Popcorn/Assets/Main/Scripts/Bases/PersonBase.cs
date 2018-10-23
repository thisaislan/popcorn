using UnityEngine;
using Popcorn.ObjectsServices;

namespace Popcorn.Bases
{

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Animator))]
    public class PersonBase : MonoBehaviour
    {

        protected Rigidbody2D ThisRigidbody2D;
        protected Animator ThisAnimator;
        protected SpriteRenderer ThisSpriteRenderer;

        [HideInInspector]
        public bool IsAlive { get; protected set; }

        protected virtual void Awake()
        {
            IsAlive = true;

            GetComponents();
        }

        void GetComponents()
        {
            ThisRigidbody2D = (Rigidbody2D)Getter.Component(this, gameObject, typeof(Rigidbody2D));
            ThisAnimator = (Animator)Getter.Component(this, gameObject, typeof(Animator));
            ThisSpriteRenderer = (SpriteRenderer)Getter.ComponentInChild(this, gameObject, typeof(SpriteRenderer), 0);
        }

    }
}