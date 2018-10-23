using UnityEngine;
using Popcorn.Bases;
using Popcorn.Managers;
using Popcorn.Metadatas;
using Popcorn.ObjectsServices;
using Popcorn.ObjectsModifiers;
using Popcorn.GameObjects.Elementies;
using PersonsTags = Popcorn.Metadatas.Tags.Persons;
using GameStates = Popcorn.GameObjects.Elementies.GameBehavior.GameStates;

namespace Popcorn.GameObjects.Persons
{

    [RequireComponent(typeof(AudioSource))]
    public class ZombiePop : EnemyBase
    {

        enum AnimationParameters { WalkTrigger };

        [SerializeField]
        float Velocity = 1.2f;
        [SerializeField]
        bool IsWalking = false;
        [SerializeField]
        bool StartPostionToLeft = true;
        [SerializeField]
        float TimeToWalk = 1f;

        float Direction = Transforms.Direction.Right;
        float WalkClock = 0;

        Move Move = new Move();

        AudioSource DeathAudioSource;

        override protected void Awake()
        {
            base.Awake();
            DeathAudioSource = (AudioSource)Getter.ComponentInChild(this, gameObject, typeof(AudioSource), 0);
        }

        void Start()
        {
            if (StartPostionToLeft)
            {
                Direction = Transforms.Direction.Left;
            }
            else
            {
                float scaleX = ThisRigidbody2D.transform.localScale.x;

                ThisRigidbody2D.transform.localScale = new Vector3(-scaleX,
                    Transforms.Scale.Normal,
                    Transforms.Scale.Normal);
            }
        }

        void FixedUpdate()
        {
            WalkClock += Time.deltaTime;
        }

        protected override void Update()
        {
            base.Update();

            if (GameBehavior.GameState != GameStates.Runing || !IsAlive)
            {
                if (ThisAnimator.speed != 0)
                {
                    ThisAnimator.speed = 0;
                }
                if (GameBehavior.GameState == GameStates.Runing)
                {
                    ThisRigidbody2D.transform.Rotate(Vector3.forward * 2 * Direction);
                }
                return;
            }

            if (IsWalking)
            {
                Walk();
            }
        }

        void Walk()
        {
            if (WalkClock >= TimeToWalk)
            {
                Move.Execute(ThisRigidbody2D, Velocity * Direction);
                ThisAnimator.SetTrigger(AnimationParameters.WalkTrigger.ToString());
                CheckedIfNeedIversionOfPosition();
                WalkClock = 0;
            }
        }

        private void CheckedIfNeedIversionOfPosition()
        {
            if (HasHoleAhead() || HasObstacleAhead())
            {
                InvertionOfDirection();
            }
        }

        bool HasObstacleAhead()
        {
            Vector2 direction;
            Vector2 position = transform.position;

            if (Direction == Transforms.Direction.Left)
            {
                direction = Vector2.left;
            }
            else
            {
                direction = Vector2.right;
            }

            position.x += Direction;

            RaycastHit2D hit = Physics2D.Raycast(position, direction, 0.08f);

            if (hit.collider != null && hit.collider.tag != PersonsTags.Player.ToString())
            {
                return true;
            }
            return false;
        }

        bool HasHoleAhead()
        {
            Vector2 position = transform.position;
            position.x += Direction;

            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 1);

            if (hit.collider == null)
            {
                return true;
            }
            return false;
        }

        void InvertionOfDirection()
        {

            if (Direction == Transforms.Direction.Right)
            {
                Direction = Transforms.Direction.Left;
            }
            else
            {
                Direction = Transforms.Direction.Right;
            }

            float scaleX = ThisRigidbody2D.transform.localScale.x;

            ThisRigidbody2D.transform.localScale = new Vector3(-scaleX,
                Transforms.Scale.Normal,
                Transforms.Scale.Normal);
        }

        protected override void WeakPointHitted()
        {
            if (IsAlive)
            {
                IsAlive = false;
                KillAnimation();
            }
        }

        void KillAnimation()
        {
            AudioManager.Instance.PlaySoundOnce(caller: this, sound: DeathAudioSource);
            ThisAnimator.speed = 0;
            ThisRigidbody2D.velocity = Vector2.zero;
            (Getter.Component(this, gameObject, typeof(Collider2D)) as Collider2D).isTrigger = true;
            (Getter.ComponentInChild(this, gameObject, typeof(CircleCollider2D), 0) as CircleCollider2D).isTrigger = true;
            ThisSpriteRenderer.sortingOrder = (int)Layers.OrdersInDefaultLayer.Max;
            ThisRigidbody2D.AddForce(new Vector2(0, 100));
            ThisRigidbody2D.gravityScale = Transforms.Gravity.Hard;
        }

    }
}
