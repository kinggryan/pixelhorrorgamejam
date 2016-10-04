using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour {

    CharacterController characterController;
    MouseLook mouseLook = new MouseLook();
    new Camera camera;

    public float maxSpeed;
    public float acceleration;
    public float gravity = 9.8f;
    public float maxFallSpeed;

    public AudioSource footstepAudioSource;

    private float footstepsWalkingVolume;

	// Use this for initialization
	void Start () {
        camera = Camera.main;
        mouseLook.Init(transform, camera.transform);
        characterController = GetComponent<CharacterController>();

        footstepsWalkingVolume = footstepAudioSource.volume;
        footstepAudioSource.volume = 0;
    }

    Vector2 GetInput()
    {
        Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (inputVector.magnitude > 1)
            inputVector.Normalize();
        return inputVector;
    }

    // Update is called once per frame
    void Update() {
        // Update look position
        mouseLook.LookRotation(transform, camera.transform);

        Vector3 velocity = characterController.velocity;

        // Update movement speed
        if (characterController.isGrounded)
        {
            Vector2 inputVec = GetInput();
            Vector3 targetVelocity = Quaternion.LookRotation(transform.forward, Vector3.up) * new Vector3(inputVec.x, 0, inputVec.y);
            velocity = Vector3.MoveTowards(velocity, targetVelocity * maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
        }

        characterController.Move(velocity*Time.deltaTime);

        // Make movement noise
        if(characterController.isGrounded)
            MakeMovementNoise();
    }

    void MakeMovementNoise()
    {
        if(characterController.velocity.magnitude > 0.01)
        {
            MonsterAI.monster.HearSound(transform.position, characterController.velocity.magnitude / maxSpeed);
            footstepAudioSource.volume = Mathf.Lerp(footstepAudioSource.volume, footstepsWalkingVolume, 0.95f);
        }
        else
        {
            footstepAudioSource.volume = Mathf.Max(footstepAudioSource.volume - 3f * Time.deltaTime, 0);
        }
    }
}
