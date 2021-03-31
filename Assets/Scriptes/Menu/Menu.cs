using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum StatePanel { GamePanel, SettingPanel, HelpPanel }

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _buttonGame, _buttonSetting, _buttonHelp;
    [SerializeField] private List<State> _panels;
    private State _currentState;

    private void Start()
    {
        _buttonHelp.onClick.AddListener(() => onPanelState(StatePanel.HelpPanel));
        _buttonGame.onClick.AddListener(() => onPanelState(StatePanel.GamePanel));
        _buttonSetting.onClick.AddListener(() => onPanelState(StatePanel.SettingPanel));
    }

    private void onPanelState(StatePanel _state)
    {
        for (int i = 0; i < _panels.Count; i++)
        {
            _panels[i].gameObject.SetActive(false);
            
            if (_panels[i]._isState == _state)
            {
                _panels[i].gameObject.SetActive(true);
            }
           
        }
    }
} 