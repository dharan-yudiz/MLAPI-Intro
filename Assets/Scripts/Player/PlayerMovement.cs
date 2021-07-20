using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;



namespace MLAPI.Demo
{
    public class PlayerMovement : NetworkBehaviour
    {
        [SerializeField] CharacterController controller;
        [SerializeField] Transform camaraTransform;
        [SerializeField] float speed = 5f;
        [SerializeField] float lookSpeed = 3f;

        float pitch;

        private void Awake()
        {

            if (controller == null)
                controller = GetComponent<CharacterController>();
        }

        private void Start()
        {
            if (!IsLocalPlayer)
            {
                camaraTransform.GetComponent<AudioListener>().enabled = false;
                camaraTransform.GetComponent<Camera>().enabled = false;
            }
        }

        private void Update()
        {

            if (!IsLocalPlayer) return;

            OnMovement();
            OnLook();
        }

        void OnMovement()
        {

            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            movement = Vector3.ClampMagnitude(movement, 1f);

            movement = transform.TransformDirection(movement);

            controller.SimpleMove(movement * speed);
        }

        void OnLook()
        {
            float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
            transform.Rotate(0, mouseX, 0);
            pitch -= Input.GetAxis("Mouse Y") * lookSpeed;
            pitch = Mathf.Clamp(pitch, -45f, 45f);
            camaraTransform.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
    }
}