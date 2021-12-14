﻿using UnityEngine;
using TatmanGames.ScreenUI.Interfaces;
using TatmanGames.ScreenUI.UI;


namespace TatmanGames.ScreenUI.Keyboard
{
    /// <summary>
    /// As is, not sure if this class belongs here as its specific to the test scene more than something reusable
    /// </summary>
    public class GlobalKeyboardHandler : IKeyboardHandler
    {
        private GameObject dialog = null;
        private GameObject popup = null;

        public GlobalKeyboardHandler(GameObject dialog, GameObject popup)
        {
            this.dialog = dialog;
            this.popup = popup;
        }
        
        public virtual bool HandleKeyPress()
        {
            IPopupHandler popupHandler = ServiceLocator.Instance.PopupHandler;
            if (Input.GetKeyDown(KeyCode.D))
            {
                popupHandler?.ShowDialog(dialog);
                return true;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                popupHandler?.CloseDialog();
                return true;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                popupHandler?.ShowPopup(popup);
                return true;
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ServiceLocator.Instance.Logger.Log("setting resolution to 3840 x 2160");
                Screen.SetResolution(3840, 2160, true);
                return true;
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ServiceLocator.Instance.Logger.Log("setting resolution to 1920 x 1080");
                Screen.SetResolution(1920, 1080, true);
                return true;
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ServiceLocator.Instance.Logger.Log("setting resolution to 1024 x 768");
                Screen.SetResolution(1024, 768, true);
                return true;
            }
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit(0);
                return true;
            }

            return false;
        }
    }
}