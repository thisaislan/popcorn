using UnityEngine;
using Popcorn.Bases;
using UnityEngine.UI;
using Popcorn.Managers;
using System.Collections;
using Popcorn.ObjectsServices;
using Popcorn.GameObjects.Objects;
using Times = Popcorn.Metadados.Times;
using Errors = Popcorn.Metadados.Strings.Errors;
using ObjectsTags = Popcorn.Metadados.Tags.Objects;
using PersonsTags = Popcorn.Metadados.Tags.Persons;
using ElementiesTags = Popcorn.Metadados.Tags.Elementies;
using UIElementiesTags = Popcorn.Metadados.Tags.UIElementies;

namespace Popcorn.GameObjects.Elementies
{

    [RequireComponent(typeof(CameraBehavior))]
    [RequireComponent(typeof(AudioSource))]
    public class GameBehavior : MonoBehaviour
    {

        public enum GameStates { Paused, Runing, TimeOut, Ended };

        [SerializeField]
        Times.ScenesTimes scenesTime = Times.ScenesTimes.Normal;
        [SerializeField]
        Text timeScreen;

        PlayerBase player;
        EndPoint endPoint;

        float time;
        float alertTime = 30;
        bool inAlertTime = false;

        AudioSource timeOutAudioSource;
        AudioSource backgroundMusic;

        [HideInInspector]
        public static GameStates GameState { get; protected set; }

        void Awake()
        {
            GetPlayer();
            GetAndCheckElements();
            GetAudioSources();

            time = (float)scenesTime;
            GameState = GameStates.Paused;
        }

        void GetAudioSources()
        {
            timeOutAudioSource = (AudioSource)Getter.ComponentInChild(this, gameObject, typeof(AudioSource), 0);
            backgroundMusic = (AudioSource)Getter.ComponentInChild(this, gameObject, typeof(AudioSource), 1);
        }

        void GetAndCheckElements()
        {
            GameObject endPointObject = Getter.SingleInstanceObjectWithTag(caller: this,
                tag: ObjectsTags.EndPoint.ToString(),
                errorOnNotFound: Errors.ENDPOINT_NOT_FOUND,
                multiplesInstance: Errors.MULTIPLE_ENDPOINT_FOUND);

            endPoint = (EndPoint)Getter.Component(this, endPointObject, typeof(EndPoint));

            Checker.SingleExistence(tag: ObjectsTags.StartPoint.ToString(),
                errorIfNone: Errors.START_POINT_NOT_FOUND,
                errorIfMultiple: Errors.MULTIPLE_START_POINT_FOUND);

            Checker.CruzedHorizontalPosition(leftGameObject: GameObject.FindGameObjectWithTag(ObjectsTags.StartPoint.ToString())
                , rightGameObject: endPointObject
                , error: Errors.START_AND_ENDPOINT_CRUZED);

            Checker.SingleOrNoneExistence(tag: ElementiesTags.Destructor.ToString(),
                errorIfMultiple: Errors.MULTIPLE_DESTRUCTOR_FOUND);

            Checker.SingleExistence(tag: UIElementiesTags.Time.ToString(),
                 errorIfNone: Errors.TIME_NOT_FOUND,
                 errorIfMultiple: Errors.MULTIPLE_TIME_FOUND);

            Checker.SingleExistence(tag: ElementiesTags.Camera.ToString(),
                 errorIfNone: Errors.CAMERA_NOT_FOUND,
                 errorIfMultiple: Errors.MULTIPLE_CAMERA_FOUND);

            if (timeScreen == null) throw new UnityException(Errors.TIME_NOT_SET_IN_GAME_BEHAVIOR);
        }

        void GetPlayer()
        {
            GameObject playerObject = Getter.SingleInstanceObjectWithTag(caller: this,
                tag: PersonsTags.Player.ToString(),
                errorOnNotFound: Errors.PLAYER_NOT_FOUND,
                multiplesInstance: Errors.MULTIPLE_PLAYERS_FOUND);

            player = (PlayerBase)Getter.Component(this, playerObject, typeof(PlayerBase));
        }

        void Start()
        {
            AudioManager.Instance.PlayBackgroundMusic(caller: this, music: backgroundMusic);
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
            time -= UnityEngine.Time.deltaTime;
        }

        void CheckTime()
        {
            if (time < 1) GameState = GameStates.TimeOut;
            else if (time <= alertTime && !inAlertTime) StartAlertTime();
        }

        void StartAlertTime()
        {
            inAlertTime = true;
            timeScreen.color = Color.red;
            AudioManager.Instance.PlaySoundOnce(caller: this, sound: timeOutAudioSource);
        }

        void SetTimeInScreen()
        {
            timeScreen.text = ((int)time).ToString();
        }

        bool IsGameFinished()
        {
            if (player.isAlive == false || endPoint.wasReachedTheEnd == true)
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
            yield return new WaitForSeconds(Times.Waits.MEDIUM);
            ScenesManager.Instance.CallNextScene();
        }

    }
}
