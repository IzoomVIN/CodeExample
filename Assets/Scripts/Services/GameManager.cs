using SceneControl;
using Services.Models;
using Services.Pool.Control;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public class GameManager : MonoBehaviour
    {
        [Header("Board properties")] 
        [SerializeField] private float boardXLow;
        [SerializeField] private float boardXUp;
        [SerializeField] private float boardZLow;
        [SerializeField] private float boardZUp;
        
        [Header("Start game state")]
        [SerializeField] private GameState.State startState;

        public GameData GameData { get; private set; }
        public PlayerData PlayerData { get; private set; }

        private static GameManager _instance;
        
        private void Awake()
        {
            InitGameData();
            InitPlayerData();
            _instance = this;
            
            GameData.State.ChangeGameStateEvent += StateObserve;
            GameData.State.ChangeState(startState);
        }

        private void Start()
        {
            var progressControllers = FindObjectsOfType<ProgressUIController>();
            foreach (var controller in progressControllers)
            {
                PlayerData.ChangePointsEvent += controller.UpdateCount;
            }
        }

        private void OnDisable()
        {
            _instance = null;
            
            var progressControllers = FindObjectsOfType<ProgressUIController>();
            foreach (var controller in progressControllers)
            {
                PlayerData.ChangePointsEvent -= controller.UpdateCount;
            }

            GameData.State.ChangeGameStateEvent -= StateObserve;
        }

        public static GameManager GetInstance()
        {
            return _instance;
        }

        private void InitGameData()
        {
            GameData = new GameData();
            
            var boards = new GameData.WorldBoards(boardXLow, boardXUp, boardZLow, boardZUp);
            GameData.SetBoards(boards);
        }

        private void StateObserve(GameState.State state)
        {
            switch (state)
            {
                case GameState.State.Play:
                    Time.timeScale = 1;
                    InitPlayerData();
                    break;
                case GameState.State.Pause:
                    Time.timeScale = 0;
                    break;
                case GameState.State.MainMenu:
                    Time.timeScale = 1;
                    break;
                case GameState.State.GameOver:
                    Time.timeScale = 0;
                    DestroySingletones();
                    DestroyPlayerData();
                    break;
            }
        }

        private void InitPlayerData()
        {
            PlayerData ??= new PlayerData();
        }

        private void DestroySingletones()
        {
            PoolManager.Destroy();
        }

        private void DestroyPlayerData()
        {
            PlayerData = null;
        }
        
        
    }
}