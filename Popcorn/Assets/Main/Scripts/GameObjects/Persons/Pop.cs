using UnityEngine;
using Popcorn.Bases;
using Popcorn.Managers;
using Popcorn.Metadatas;
using System.Collections;
using Popcorn.ObjectsServices;
using Popcorn.ObjectsModifiers;
using Popcorn.GameObjects.Elementies;
using MathExt = Popcorn.Extensions.MathExt;
using HelpersTags = Popcorn.Metadatas.Tags.Helpers;
using PersonsTags = Popcorn.Metadatas.Tags.Persons;
using ObjectsTags = Popcorn.Metadatas.Tags.Objects;
using GameStates = Popcorn.GameObjects.Elementies.GameBehavior.GameStates;

namespace Popcorn.GameObjects.Persons
{

    public class Pop : PlayerBase
    {

        [SerializeField]
        float TimeToRestatIdle = 4.5f;
        [SerializeField]
        float Velocity = 4f;
        [SerializeField]
        float JumpForce = 900f;
        [SerializeField]
        float HitForce = 300f;

        float TimeInStandBy = 0f;
        bool IsJumping = false;
        float LastDir = Transforms.Direction.Right;

        Jump Jump = new Jump();
        Move Move = new Move();

        void FixedUpdate()
        {
            TimeInStandBy += Time.deltaTime;

            if (TimeInStandBy >= TimeToRestatIdle)
            {
                ThisAnimator.SetTrigger(AnimationParameters.IdleTrigger.ToString());
                TimeInStandBy = 0;
            }
        }

        void Update()
        {

            if (CheckIfDontCanMove())
            {
                TimeInStandBy = 0;
                return;
            }
            CleanVelocityX();

            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) &&
                !LeftColliderHelper.IsColliding)
            {
                ExecuteMove(Transforms.Direction.Left);
            }
            else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) &&
                !RightColliderHelper.IsColliding)
            {
                ExecuteMove(Transforms.Direction.Right);
            }

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) &&
             !IsJumping)
            {
                ExecuteJump(JumpForce);
            }

            ThisAnimator.SetFloat(AnimationParameters.Velocity.ToString(), GetAbsRunVelocity());
            IsJumping = !BottomColliderHelper.IsColliding;
            ThisAnimator.SetBool(AnimationParameters.IsJump.ToString(), IsJumping);
            CheckAliveConditions();
        }

        void ExecuteMove(float dir)
        {
            Move.Execute(ThisRigidbody2D, Velocity * dir);
            LastDir = dir;
            TimeInStandBy = 0;
            ThisSpriteRenderer.flipX = dir < 0;

        }

        void ExecuteJump(float force)
        {
            Jump.Execute(ThisRigidbody2D, force);
            AudioManager.Instance.PlaySoundOnce(caller: this, sound: JumpAudioSource);
            TimeInStandBy = 0;
        }

        void CleanVelocityX()
        {
            Vector2 vel = ThisRigidbody2D.velocity;
            vel.x = 0;
            ThisRigidbody2D.velocity = vel;
        }

        bool CheckIfDontCanMove()
        {
            return !IsAlive ||
                GameBehavior.GameState == GameStates.Paused ||
                ThisAnimator.GetBool(AnimationParameters.Hit.ToString());
        }

        void CheckAliveConditions()
        {
            if (this.transform.position.y <= BottomLimit)
            {
                Kill(JumpForce * 2);
            }

            if (GameBehavior.GameState == GameStates.TimeOut)
            {
                Kill(JumpForce);
            }
        }

        void OnCollisionEnter2D(Collision2D otherCollider2D)
        {
            if (otherCollider2D.gameObject.CompareTag(PersonsTags.Enemy.ToString()))
            {
                ContactPoint2D contactPoint2D = otherCollider2D.contacts[0];

                if (!contactPoint2D.collider.CompareTag(HelpersTags.WeakPoint.ToString()))
                {
                    Kill(JumpForce);
                }
                else
                {
                    ExecuteJump(JumpForce - 50);
                }

            }
            else if (otherCollider2D.gameObject.CompareTag(ObjectsTags.Hit.ToString()))
            {
                Hit();
            }
        }

        void OnCollisionStay2D(Collision2D otherCollider2D)
        {
            if (otherCollider2D.gameObject.CompareTag(ObjectsTags.Hit.ToString()))
            {
                Hit();
            }
        }

        void OnTriggerEnter2D(Collider2D otherCollider2D)
        {
            if (otherCollider2D.gameObject.CompareTag(ObjectsTags.EndPoint.ToString()))
            {
                Win();
            }
        }

        void Win()
        {
            AudioManager.Instance.PlaySoundOnce(caller: this, sound: WinAudioSource);
            if (IsJumping)
            {
                ThisAnimator.SetTrigger(AnimationParameters.WinTrigger.ToString());
            }
            else
            {
                StartCoroutine(WinAnimation());
            }
        }

        IEnumerator WinAnimation()
        {
            yield return new WaitForSeconds(Times.Waits.MinimunPlus);
            ThisAnimator.SetTrigger(AnimationParameters.WinTrigger.ToString());
        }

        public float GetAbsRunVelocity()
        {
            return Mathf.Abs(ThisRigidbody2D.velocity.x);
        }

        void Kill(float forceToUp)
        {
            if (IsAlive)
            {
                ThisRigidbody2D.velocity = Vector2.zero;
                ThisRigidbody2D.gravityScale = Transforms.Gravity.Without;
                IsAlive = false;
                ThisAnimator.SetBool(AnimationParameters.IsAlive.ToString(), IsAlive);
                StartCoroutine(KillAnimation(forceToUp));
            }
        }

        IEnumerator KillAnimation(float forceToUp)
        {
            yield return new WaitForSeconds(Times.Waits.Minimun);
            AudioManager.Instance.PlaySoundOnce(caller: this, sound: DeathAudioSource);
            ThisRigidbody2D.gravityScale = Transforms.Gravity.Hard;
            Jump.Execute(ThisRigidbody2D, forceToUp);
            (Getter.Component(this, gameObject, typeof(Collider2D)) as Collider2D).isTrigger = true;

            ThisRigidbody2D.transform.localScale = new Vector3(Transforms.Scale.NormalPlus
            , Transforms.Scale.NormalPlus
            , Transforms.Scale.NormalPlus);

            ThisSpriteRenderer.sortingOrder = (int)Layers.OrdersInDefaultLayer.Max;
        }

        void Hit()
        {
            if (!ThisAnimator.GetBool(AnimationParameters.Hit.ToString()))
            {
                Vector2 vectorHit = new Vector2();
                float timeInWait = Times.Waits.Minimun;

                ThisAnimator.SetBool(AnimationParameters.Hit.ToString(), true);
                vectorHit.x = MathExt.GetInvertValue(LastDir) * HitForce;

                if (IsJumping)
                {
                    vectorHit.x = MathExt.GetPercent(value: vectorHit.x, percent: 70);
                    vectorHit.y = MathExt.GetPercent(value: HitForce, percent: 50);
                    timeInWait = Times.Waits.MinimunPlus;
                }
                ThisRigidbody2D.velocity = Vector2.zero;
                ThisRigidbody2D.AddForce(vectorHit);
                StartCoroutine(EndHitAnimation(timeInWait));
            }
        }

        IEnumerator EndHitAnimation(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            ThisAnimator.SetBool(AnimationParameters.Hit.ToString(), false);
        }

    }
}