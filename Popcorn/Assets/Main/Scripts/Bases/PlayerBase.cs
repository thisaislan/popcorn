using UnityEngine;
using Popcorn.ObjectsServices;
using Popcorn.GameObjects.Helpers;
using Errors = Popcorn.Metadatas.Strings.Errors;
using PersonsTags = Popcorn.Metadatas.Tags.Persons;
using ElementiesTags = Popcorn.Metadatas.Tags.Elementies;

namespace Popcorn.Bases
{

    [RequireComponent(typeof(PopColliderHelper))]
    [RequireComponent(typeof(AudioSource))]
    abstract public class PlayerBase : PersonBase
    {

        protected enum AnimationParameters { WinTrigger, IsJump, Velocity, IsAlive, IdleTrigger, Hit };

        protected PopColliderHelper BottomColliderHelper;
        protected PopColliderHelper RightColliderHelper;
        protected PopColliderHelper LeftColliderHelper;

        protected AudioSource JumpAudioSource;
        protected AudioSource WinAudioSource;
        protected AudioSource DeathAudioSource;

        protected float BottomLimit;

        protected override void Awake()
        {
            base.Awake();
            GetHelpers();
            GetAudioSources();
            SetBottomLimiteToStillAlive();
            Checker.Tag(gameObject: gameObject, expectedTag: PersonsTags.Player.ToString(), error: Errors.WrongPlayerTag);
        }

        void GetAudioSources()
        {
            JumpAudioSource = (AudioSource)Getter.ComponentInChild(this, gameObject, typeof(AudioSource), 0);
            WinAudioSource = (AudioSource)Getter.ComponentInChild(this, gameObject, typeof(AudioSource), 1);
            DeathAudioSource = (AudioSource)Getter.ComponentInChild(this, gameObject, typeof(AudioSource), 2);
        }

        void SetBottomLimiteToStillAlive()
        {
            BottomLimit = Getter.ObjectWithTag(this, ElementiesTags.BottomLimitView.ToString()).
                transform.position.y - 7;
        }

        void GetHelpers()
        {
            BottomColliderHelper = (PopColliderHelper)Getter.ComponentInChild(this, gameObject, typeof(PopColliderHelper), 0);
            RightColliderHelper = (PopColliderHelper)Getter.ComponentInChild(this, gameObject, typeof(PopColliderHelper), 1);
            LeftColliderHelper = (PopColliderHelper)Getter.ComponentInChild(this, gameObject, typeof(PopColliderHelper), 2);
        }

    }
}