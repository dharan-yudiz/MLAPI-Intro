using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MLAPI.Demo
{
    public class NonIntrusivePopup : MonoBehaviour
    {
        static NonIntrusivePopup _instance;
        public static NonIntrusivePopup Instance => _instance;

        public Text PopupText;


        SimpleAnimation SimpleAnimation;
        Canvas canvas;

        private void Awake()
        {
            _instance = this;
            SimpleAnimation = GetComponentInChildren<SimpleAnimation>();
            canvas = GetComponent<Canvas>();

            SimpleAnimation.OnHideAnimationFinish.AddListener(OhScreenHidden);
        }

        private void OhScreenHidden()
        {
            canvas.enabled = false;
        }

        public void Show(string text)
        {
            if (!canvas.enabled)
            {

                PopupText.text = text;

                StartCoroutine(PopupRoutine());
         
            }
        }


        IEnumerator PopupRoutine()
        {
            canvas.enabled = true;
            SimpleAnimation.StartShowAnimation(EnableDirection.Forward);

            yield return new WaitForSeconds(3f);

            SimpleAnimation.StartHideAnimation(EnableDirection.Forward);
        }
    }
}