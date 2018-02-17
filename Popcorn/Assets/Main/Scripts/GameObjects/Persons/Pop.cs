using UnityEngine;
using Popcorn.Bases;
using Popcorn.Managers;
using Popcorn.Metadados;
using System.Collections;
using Popcorn.ObjectsServices;
using Popcorn.ObjectsModifiers;
using Popcorn.GameObjects.Elementies;
using MathExt = Popcorn.Extensions.MathExt;
using HelpersTags = Popcorn.Metadados.Tags.Helpers;
using PersonsTags = Popcorn.Metadados.Tags.Persons;
using ObjectsTags = Popcorn.Metadados.Tags.Objects;
using GameStates = Popcorn.GameObjects.Elementies.GameBehavior.GameStates;

namespace Popcorn.GameObjects.Persons
{

    public class Pop : PlayerBase
    {

        [SerializeField]
        float timeToRestatIdle = 4.5f;
        [SerializeField]
        float velocity = 4f;
        [SerializeField]
        float jumpForce = 720f;
        [SerializeField]
        float hitForce = 300f;

        float timeInStandBy = 0f;
        bool isJumping = false;
        float lastDir = Transforms.Direction.RIGHT;

        Jump jump = new Jump();
        Move move = new Move();

        void FixedUpdate()
        {
            timeInStandBy += Time.deltaTime;

            if (timeInStandBy >= timeToRestatIdle)
            {
                animator.SetTrigger(AnimationParameters.IdleTrigger.ToString());
                timeInStandBy = 0;
            }
        }

        void Update()
        {

            if (CheckIfDontCanMove())
            {
                timeInStandBy = 0;
                return;
            }
            CleanVelocityX();

            if (Input.GetKey(KeyCode.A) == true && leftColliderHelper.isColliding == false)
            {
                Move(Transforms.Direction.LEFT);
            }
            else if (Input.GetKey(KeyCode.D) == true && rightColliderHelper.isColliding == false)
            {
                Move(Transforms.Direction.RIGHT);
            }

            if (Input.GetKeyDown(KeyCode.Space) == true && isJumping == false)
            {
                Jump(jumpForce);
            }

            animator.SetFloat(AnimationParameters.Velocity.ToString(), GetAbsRunVelocity());
            isJumping = !bottomColliderHelper.isColliding;
            animator.SetBool(AnimationParameters.IsJump.ToString(), isJumping);
            CheckAliveConditions();
        }

        void Move(float dir)
        {
            move.Execute(rb2D, velocity * dir);
            lastDir = dir;
            timeInStandBy = 0;
            spriteR.flipX = dir < 0;

        }

        void Jump(float force)
        {
            jump.Execute(rb2D, force);
            AudioManager.Instance.PlaySoundOnce(caller: this, sound: jumpAudioSource);
            timeInStandBy = 0;
        }

        void CleanVelocityX()
        {
            Vector2 vel = rb2D.velocity;
            vel.x = 0;
            rb2D.velocity = vel;
        }

        bool CheckIfDontCanMove()
        {
            return isAlive == false ||
                GameBehavior.GameState == GameStates.Paused ||
                animator.GetBool(AnimationParameters.Hit.ToString());
        }

        void CheckAliveConditions()
        {
            if (this.transform.position.y <= bottomLimit)
            {
                Kill(jumpForce * 2);
            }

            if (GameBehavior.GameState == GameStates.TimeOut)
            {
                Kill(jumpForce);
            }
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.CompareTag(PersonsTags.Enemy.ToString()) == true)
            {
                ContactPoint2D contactPoint2D = coll.contacts[0];

                if (contactPoint2D.collider.CompareTag(HelpersTags.WeakPoint.ToString()) == false)
                {
                    Kill(jumpForce);
                }
                else
                {
                    Jump(jumpForce - 30);
                }

            }
            else if (coll.gameObject.CompareTag(ObjectsTags.Hit.ToString()) == true)
            {
                Hit();
            }
        }

        void OnCollisionStay2D(Collision2D coll)
        {
            if (coll.gameObject.CompareTag(ObjectsTags.Hit.ToString()) == true)
            {
                Hit();
            }
        }

        void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.CompareTag(ObjectsTags.EndPoint.ToString()) == true)
            {
                Win();
            }
        }

        void Win()
        {
            AudioManager.Instance.PlaySoundOnce(caller: this, sound: winAudioSource);
            if (isJumping) animator.SetTrigger(AnimationParameters.WinTrigger.ToString());
            else StartCoroutine(WinAnimation());
        }

        IEnumerator WinAnimation()
        {
            yield return new WaitForSeconds(Times.Waits.MINIMUM_PLUS);
            animator.SetTrigger(AnimationParameters.WinTrigger.ToString());
        }

        public float GetAbsRunVelocity()
        {
            return Mathf.Abs(rb2D.velocity.x);
        }

        void Kill(float forceToUp)
        {
            if (isAlive == true)
            {
                rb2D.velocity = Vector2.zero;
                rb2D.gravityScale = Transforms.Gravity.WITHOUT;
                isAlive = false;
                animator.SetBool(AnimationParameters.IsAlive.ToString(), isAlive);
                StartCoroutine(KillAnimation(forceToUp));
            }
        }

        IEnumerator KillAnimation(float forceToUp)
        {
            yield return new WaitForSeconds(Times.Waits.MINIMUM);
            AudioManager.Instance.PlaySoundOnce(caller: this, sound: deathAudioSource);
            rb2D.gravityScale = Transforms.Gravity.HARD;
            jump.Execute(rb2D, forceToUp);
            (Getter.Component(this, gameObject, typeof(Collider2D)) as Collider2D).isTrigger = true;

            rb2D.transform.localScale = new Vector3(Transforms.Scale.NORMAL_PLUS
            , Transforms.Scale.NORMAL_PLUS
            , Transforms.Scale.NORMAL_PLUS);

            spriteR.sortingOrder = (int)Layers.OrdersInDefaultLayer.Max;
        }

        void Hit()
        {
            if (animator.GetBool(AnimationParameters.Hit.ToString()) == false)
            {
                Vector2 vectorHit = new Vector2();
                float timeInWait = Times.Waits.MINIMUM;

                animator.SetBool(AnimationParameters.Hit.ToString(), true);
                vectorHit.x = MathExt.GetInvertValue(lastDir) * hitForce;

                if (isJumping == true)
                {
                    vectorHit.x = MathExt.GetPercent(value: vectorHit.x, percent: 70);
                    vectorHit.y = MathExt.GetPercent(value: hitForce, percent: 50);
                    timeInWait = Times.Waits.MINIMUM_PLUS;
                }
                rb2D.velocity = Vector2.zero;
                rb2D.AddForce(vectorHit);
                StartCoroutine(EndHitAnimation(timeInWait));
            }
        }

        IEnumerator EndHitAnimation(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            animator.SetBool(AnimationParameters.Hit.ToString(), false);
        }

    }
}
