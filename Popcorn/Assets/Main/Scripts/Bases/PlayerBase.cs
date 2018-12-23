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

        protected PopColliderHelper bottomColliderHelper;
        protected PopColliderHelper rightColliderHelper;
        protected PopColliderHelper leftColliderHelper;

        protected AudioSource jumpAudioSource;
        protected AudioSource winAudioSource;
        protected AudioSource deathAudioSource;

        protected float bottomLimit;

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
            jumpAudioSource = (AudioSource)Getter.ComponentInChild(this, gameObject, typeof(AudioSource), 0);
            winAudioSource = (AudioSource)Getter.ComponentInChild(this, gameObject, typeof(AudioSource), 1);
            deathAudioSource = (AudioSource)Getter.ComponentInChild(this, gameObject, typeof(AudioSource), 2);
        }

        void SetBottomLimiteToStillAlive()
        {
            bottomLimit = Getter.ObjectWithTag(this, ElementiesTags.BottomLimitView.ToString()).transform.position.y - 7;
        }

        void GetHelpers()
        {
            bottomColliderHelper = (PopColliderHelper)Getter.ComponentInChild(this, gameObject, typeof(PopColliderHelper), 0);
            rightColliderHelper = (PopColliderHelper)Getter.ComponentInChild(this, gameObject, typeof(PopColliderHelper), 1);
            leftColliderHelper = (PopColliderHelper)Getter.ComponentInChild(this, gameObject, typeof(PopColliderHelper), 2);
        }

    }
    
}