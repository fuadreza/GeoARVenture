﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using Cubex.UI;

namespace Cubex
{
    public enum UIScreenInfo
    {
        LOADING_SCREEN,
        START_SCREEN,
        IN_GAME_SCREEN,
        PAUSE_SCREEN
    }

    public class UIManager : MonoBehaviour
    {
        private static UIManager m_Singleton;

        public static UIManager Singleton
        {
            get
            {
                return m_Singleton;
            }
        }

        [SerializeField]
        private List<UIScreen> m_Screens;
        private UIScreen m_ActiveScreen;
        private UIWindow m_ActiveWindow;
        [SerializeField]
        private Texture2D m_CursorDefaultTexture;
        [SerializeField]
        private Texture2D m_CursorClickTexture;
        [SerializeField]
        private float m_CursorHideDelay = 1f;

        public List<UIScreen> UISCREENS
        {
            get
            {
                return m_Screens;
            }
        }

        public UIScreen GetUIScreen(UIScreenInfo screenInfo)
        {
            return m_Screens.Find(el => el.ScreenInfo == screenInfo);
        }

        void Awake()
        {
            if (m_Singleton != null)
            {
                //Destroy(gameObject);
                return;
            }
            m_Singleton = this;
            //Cursor.SetCursor(m_CursorDefaultTexture, Vector2.zero, CursorMode.Auto);
        }

        void Start()
        {
            Init();
        }

        public void Init()
        {
            //var loadingScreen = GetUIScreen(UIScreenInfo.LOADING_SCREEN);
            //OpenScreen(loadingScreen);
            CloseAllScreens();

        }

        public void OpenWindow(UIWindow window)
        {
            window.Open();
            m_ActiveWindow = window;
        }

        public void CloseWindow(UIWindow window)
        {
            if (m_ActiveWindow == window)
            {
                m_ActiveWindow = null;
            }
            window.Close();
        }

        public void CloseActiveWindow()
        {
            if (m_ActiveWindow != null)
            {
                CloseWindow(m_ActiveWindow);
            }
        }

        public void OpenScreen(UIScreen screen)
        {
            CloseAllScreens();
            screen.UpdateScreenStatus(true);
            m_ActiveScreen = screen;
        }

        public void CloseScreen(UIScreen screen)
        {
            if (m_ActiveScreen == screen)
            {
                m_ActiveScreen = null;
            }
            screen.UpdateScreenStatus(false);
        }

        public void CloseAllScreens()
        {
            foreach (var screen in m_Screens)
                CloseScreen(screen);
        }

        bool IsAsScreenOpen()
        {
            foreach (var screen in m_Screens)
            {
                if (screen.IsOpen)
                    return true;
            }

            return false;
        }
    }

    
}
