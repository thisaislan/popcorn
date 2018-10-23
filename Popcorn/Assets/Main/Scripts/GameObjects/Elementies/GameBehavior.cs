using UnityEngine;
using Popcorn.Bases;
using UnityEngine.UI;
using Popcorn.Managers;
using System.Collections;
using Popcorn.ObjectsServices;
using Popcorn.GameObjects.Objects;
using Times = Popcorn.Metadatas.Times;
using Errors = Popcorn.Metadatas.Strings.Errors;
using ObjectsTags = Popcorn.Metadatas.Tags.Objects;
using PersonsTags = Popcorn.Metadatas.Tags.Persons;
using ElementiesTags = Popcorn.Metadatas.Tags.Elementies;
using UIElementiesTags = Popcorn.Metadatas.Tags.UIElementies;

namespace Popcorn.GameObjects.Elementies
{

    [RequireComponent(typeof(CameraBehavior))]
    [RequireComponent(typeof(AudioSource))]
    public class GameBehavior : MonoBehaviour
    {

        public enum GameStates { Paused, Runing, TimeOut, Ended };

        [SerializeField]
        Times.ScenesTimes ScenesTime = Times.ScenesTimes.Normal;
        [SerializeField]
        Text TimeScreen;

        PlayerBase Player;
        EndPoint EndPoint;

        float Time;
        float AlertTime = 30;
        bool InAlertTime = false;

        AudioSource TimeOutAudioSource;
        AudioSource BackgroundMusic;

        [HideInInspector]
        public static GameStates GameState { get; protected set; }

        void Awake()
        {
            GetPlayer();
            GetAndCheckElements();
            GetAudioSources();

            Time = (float)ScenesTime;
            GameState = GameStates.Paused;
        }

        void GetAudioSources()
        {
            TimeOutAudioSource = (AudioSource)Getter.ComponentInChild(this, gameObject, typeof(AudioSource), 0);
            BackgroundMusic = (AudioSource)Getter.ComponentInChild(this, gameObject, typeof(AudioSource), 1);
        }

        void GetAndCheckElements()
        {
            GameObject endPointObject = Getter.SingleInstanceObjectWithTag(caller: this,
                tag: ObjectsTags.EndPoint.ToString(),
                errorOnNotFound: Errors.EndpointNotFound,
                multiplesInstance: Errors.MultipleEndpointsFound);

            EndPoint = (EndPoint)Getter.Component(this, endPointObject, typeof(EndPoint));

            Checker.SingleExistence(tag: ObjectsTags.StartPoint.ToString(),
                errorIfNone: Errors.StartPointNotFound,
                errorIfMultiple: Errors.MultipleStartPointsFound);

            Checker.CruzedHorizontalPosition(leftGameObject: GameObject.FindGameObjectWithTag(ObjectsTags.StartPoint.ToString())
                , rightGameObject: endPointObject
                , error: Errors.StartAndEndpointCruzed);

            Checker.SingleOrNoneExistence(tag: ElementiesTags.Destructor.ToString(),
                errorIfMultiple: Errors.MultipleDestructorsFound);

            Checker.SingleExistence(tag: UIElementiesTags.Time.ToString(),
                 errorIfNone: Errors.TimeNotFound,
                 errorIfMultiple: Errors.MultipleTimesFound);

            Checker.SingleExistence(tag: ElementiesTags.Camera.ToString(),
                 errorIfNone: Errors.CameraNotFound,
                 errorIfMultiple: Errors.MultipleCamerasFound);

            if (TimeScreen == null)
            {
                throw new UnityException(Errors.TimeNotSetInGameBehavior);
            }
        }

        void GetPlayer()
        {
            GameObject playerObject = Getter.SingleInstanceObjectWithTag(caller: this,
                tag: PersonsTags.Player.ToString(),
                errorOnNotFound: Errors.PlayerNotFound,
                multiplesInstance: Errors.MultiplePlayersFound);

            Player = (PlayerBase)Getter.Component(this, playerObject, typeof(PlayerBase));
        }

        void Start()
        {
            AudioManager.Instance.PlayBackgroundMusic(caller: this, music: BackgroundMusic);
            GameState = GameStates.Runing;
        }

        void Update()
        {

            switch (GameState)
            {
                case GameStates.Runing:

                    if (!IsGameFinished())
                    {
                        CountingTime();
                        SetTimeInScreen();
                        CheckTime();
                    }
                    break;

                case GameStates.Ended:
                    AudioManager.Instance.StopBackgroundMusic(caller: this);
                    GameIsOver();
                    break;

                case GameStates.TimeOut:
                    GameState = GameStates.Ended;
                    break;
            }
        }

        void CountingTime()
        {
            Time -= UnityEngine.Time.deltaTime;
        }

        void CheckTime()
        {
            if (Time < 1)
            {
                GameState = GameStates.TimeOut;
            }
            else if (Time <= AlertTime && !InAlertTime)
            {
                StartAlertTime();
            }
        }

        void StartAlertTime()
        {
            InAlertTime = true;
            TimeScreen.color = Color.red;
            AudioManager.Instance.PlaySoundOnce(caller: this, sound: TimeOutAudioSource);
        }

        void SetTimeInScreen()
        {
            TimeScreen.text = ((int)Time).ToString();
        }

        bool IsGameFinished()
        {
            if (!Player.IsAlive || EndPoint.WasReachedTheEnd)
            {
                GameState = GameStates.Ended;
                return true;
            }
            return false;
        }

        void GameIsOver()
        {
            if (GameState == GameStates.Ended)
            {
                GameState = GameStates.Paused;
                StartCoroutine(CallNextScene());
            }
        }

        IEnumerator CallNextScene()
        {
            yield return new WaitForSeconds(Times.Waits.Medium);
            ScenesManager.Instance.CallNextScene();
        }

    }
}
