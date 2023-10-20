using ScreenSystem.Screens;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class GameScreen : BaseScreen
    {
        public ShootingInputComponent shootingComponent;

        private void OnEnable()
        {
            shootingComponent.Shoot += PlayerInputService.SetShootInput;
        }

        protected override void OnHide()
        {
            base.OnHide();

            shootingComponent.RemoveCallbacks();
        }
    }
}