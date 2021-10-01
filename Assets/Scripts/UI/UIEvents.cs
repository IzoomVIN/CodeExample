using System.Collections;
using System.Collections.Generic;
using Services;
using Services.Models;
using UnityEngine;

namespace UI
{
    public class UIEvents : MonoBehaviour
    {

        [Header("Panel list")]
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private float gameOverDelay;

        private List<GameObject> _panels;
        private GameState _gameState;

        private void Awake()
        {
            InitPanelList();
        }

        private void Start()
        {
            _gameState = GameManager.GetInstance().GameData.State;
        }

        public void MainMenuAction()
        {
            _gameState.ChangeState(GameState.State.MainMenu);
            DeactivatePanels();
            mainMenuPanel.SetActive(true);
        }
        
        public void PlayAction()
        {
            _gameState.ChangeState(GameState.State.Play);
            DeactivatePanels();
            gamePanel.SetActive(true);
        }
        
        public void PauseAction()
        {
            _gameState.ChangeState(GameState.State.Pause);
            DeactivatePanels();
            pausePanel.SetActive(true);
        }

        public void RestartAction()
        {
            _gameState.ChangeState(GameState.State.GameOver);
            PlayAction();
        }

        public void GameOverAction()
        {
            StartCoroutine(GameOver());
        }

        public void SettingsAction()
        {
        }

        private void InitPanelList()
        {
            _panels = new List<GameObject>
            {
                gamePanel, 
                pausePanel, 
                gameOverPanel, 
                mainMenuPanel
            };

        }

        private void DeactivatePanels()
        {
            for (int i = 0; i < _panels.Count; i++)
            {
                _panels[i].SetActive(false);
            }
        }

        private IEnumerator GameOver()
        {
            yield return new WaitForSeconds(gameOverDelay);

            _gameState.ChangeState(GameState.State.GameOver);
            DeactivatePanels();
            gameOverPanel.SetActive(true);
        }
    }
}