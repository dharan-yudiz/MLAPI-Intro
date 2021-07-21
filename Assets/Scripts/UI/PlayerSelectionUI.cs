using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MLAPI.Demo
{

    public class PlayerSelectionUI : ScreenView
    {
        [SerializeField]
        GameObject SelectionEnvironment;

        [SerializeField]
        Camera PlayerSelectionCamera;

        [SerializeField]
        TextMeshPro PlayerText;

        [SerializeField]
        MeshRenderer meshRenderer;

        [SerializeField]
        List<Color> Colors;

        int currentColorIndex = 0;

        [SerializeField] InputField NameField;


        public override void OnScreenShowCalled()
        {
            base.OnScreenShowCalled();

            PlayerSelectionCamera.enabled = true;
            SelectionEnvironment.SetActive(true);
            NameField.enabled = true;
            NameField.text = "";

            SetColor(currentColorIndex);

            NameField.onValueChanged.AddListener(OnFieldEdited);

            NameField.text = "Player";
        }

        public override void OnScreenHidden()
        {
            base.OnScreenHidden();
            NameField.enabled = false;
            PlayerSelectionCamera.enabled = false;
            SelectionEnvironment.SetActive(false);
        }

        private void OnFieldEdited(string arg0)
        {
            PlayerText.text = arg0;
        }

        public void OnNext()
        {

            GameManager.Instance.currentPlayerData = new PlayerData(NameField.text,Colors[currentColorIndex]);

            NameField.DeactivateInputField();

            UIController.instance.HideCurrentScreen();
            UIController.instance.ShowThisScreen(ScreenType.StartGame, EnableDirection.Forward);
        }

        public void SetColor(int index)
        {
            currentColorIndex = index;
            meshRenderer.material.color = Colors[index];
        }
    }

}