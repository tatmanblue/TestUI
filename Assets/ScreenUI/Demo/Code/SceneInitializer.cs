using TatmanGames.Common.ServiceLocator;
using UnityEngine;
using TatmanGames.ScreenUI.Interfaces;
using TatmanGames.ScreenUI.Keyboard;
using TatmanGames.ScreenUI.Scene;
using TatmanGames.ScreenUI.UI;
using ILogger = TatmanGames.ScreenUI.Interfaces.ILogger;

namespace TatmanGames.ScreenUI.Demo
{
    /// <summary>
    /// Demonstrates how to set up the ScreenUI system
    /// </summary>

    public class SceneInitializer : MonoBehaviour
    {
        [SerializeField] private GameObject gameMenuDialog;
        [SerializeField] private GameObject settingsDialog;
        [SerializeField] private GameObject toolbarPopup;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip openSound;
        [SerializeField] private AudioClip closeSound;

        private void Start()
        {
            var dialogEvents = new PopupEventsManager();
            
            ServicesLocator services = GlobalServicesLocator.Instance;
            services.AddService(DialogServices.PopupHandler, new PopupHandler(dialogEvents));
            services.AddService(DialogServices.PopupEventsManager, dialogEvents);
            services.AddService(DialogServices.DialogEvents, dialogEvents);
            services.AddService("KeyboardHandler",new DemoKeyboardHandler(gameMenuDialog, toolbarPopup));
            services.AddService("Logger", new DebugLogging());
            
            IPopupHandler popupHandler = GlobalServicesLocator.Instance.GetServiceByName<IPopupHandler>("PopupHandler");
            popupHandler.Canvas = GetComponent<Canvas>();
            popupHandler.AudioSource = audioSource;
            popupHandler.OpenSound = openSound;
            popupHandler.CloseSound = closeSound;
            
            dialogEvents.OnButtonPressed += DialogEventsOnButtonPressed;
        }

        private bool DialogEventsOnButtonPressed(string dialogName, string buttonId)
        {
            if ("quit" == buttonId)
                GlobalServicesLocator.Instance.GetServiceByName<IPopupHandler>(DialogServices.PopupHandler)?.CloseDialog();
            else if ("settings" == buttonId && settingsDialog != null)
                GlobalServicesLocator.Instance.GetServiceByName<IPopupHandler>(DialogServices.PopupHandler)?.ReplaceDialog(settingsDialog);
            else
                GlobalServicesLocator.Instance.GetServiceByName<ILogger>("Logger")?.LogWarning($"dialog command {buttonId} for dialog {dialogName} not handled.");
            
            return false;
        }

        private void Update()
        {
            GlobalServicesLocator.Instance.GetServiceByName<IKeyboardHandler>("KeyboardHandler")?.HandleKeyPress();
        }
    }

}
