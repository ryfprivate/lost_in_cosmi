using UnityEngine;
using UnityEngine.Assertions;


namespace Activity
{
    /// <summary>
    /// Starting point for your camera follow script in the Camera Activity.
    /// 
    /// For this activity, we assume you are tracking a manually supplied transform or rb, to be
    /// set via the inspector or another script.
    /// </summary>
    /// 



    public class CameraFollow : MonoBehaviour
    {
        /*
     * Min and max float values to transform the camera FOV based on velocity
     */
        public float MaxFOV = 95f;
        public float MinFOV = 60f;
        public Camera myCamera;
        // float t is the mid point on the lerp
        public float t = 0.5f;
        public Vector3 CameraTransform;
        public float cameraSmooth;
        public float CamLead = 0.3f;
        public Vector3 centerPt;
        public float radius;

        [Tooltip("Expecting to follow a rigidbody, this is used first.")]
        [SerializeField] protected Rigidbody2D targetRBToFollow;

        [Tooltip("Optional, if targetRBToFollow is not set, position will be fetched from this.")]
        [SerializeField] protected Transform targetToFollow;

        [Tooltip("Offset applied to target position before any additional calculations. Typically used to keep camera a fixed distance 'behind' target.")]
        [SerializeField] protected Vector3 offset = new Vector3(0, 0, -10);

        private bool followOn = false;

        void Start()
        {
            GameEvents.current.onTriggerLaunch += Follow;
        }

        protected void LateUpdate()
        {
            if (!followOn) return;

            Assert.IsTrue(targetRBToFollow != null || targetToFollow != null, "Require at least 1 of our taret objects to be set.");

            var targetWorldPos = Vector3.zero;
            if (targetRBToFollow != null)
            {
                //we are using the position of the transform of the rb so that we get its interpolated position, not the raw 
                //  underlying position out of the physics engine, which would be targetRBToFollow.position.
                targetWorldPos = targetRBToFollow.transform.position;
            }
            else
            {
                targetWorldPos = targetToFollow.position;
            }


            //additional logic should go here, most likely using targetRBToFollow.velocity

            Vector3 velocity = targetRBToFollow.velocity;
            targetWorldPos += offset;

            var leadPos = targetWorldPos + (velocity * CamLead);

            var smoothLeadPos = Vector3.Lerp(transform.position, leadPos, cameraSmooth * Time.deltaTime);


            //smoothing most like to happen here between transform.Position and the adjusted calculated target position.

            //for now we just snap to final location


            // learned a little bit about something called lerp, used to handle linear interpolation - a math function that will allow for the camera FOV to be scaled based on velocity
            // might try scaling camera FOV based on if the accelerate key is held
            //  void Update()

            { // update so that the zoom is called as much as possible to keep it smooth

                // attempting to get the input of the w key as the trigger for the if statement
                if (Input.GetKey(KeyCode.W))
                    // lerp intended to increase fov, making the screen more zoomed out
                    // added time.deltatime in order to have the FOV scale linearly every second instead of every frame
                    myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, MaxFOV, t * Time.deltaTime);
                else
                    // lerp intended to make the FOV more narrow in order to zoom on the player while they are slow
                    myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, MinFOV, t * Time.deltaTime);


            }
            // attempting to change camera feild of view by holding and releasing the w key, should this be a whole new script?
            //(transform.position - NewCamPos = velocity * 0.2f)
            //(NewCamPos > (targetWorldPos - NewCamPos) / 6) ;
            // dist from player to newcampos >= 1/6th targetWorldPos then  
            //if (NewCamPos.x >=  )


            //transform.position = transform.position; 
            //else

            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            Vector3 MaxPos = transform.position + movement;
            Vector3 offsetClamp = MaxPos - centerPt;
            transform.position = centerPt + Vector3.ClampMagnitude(offsetClamp, radius);


            //FROM STEVE: not sure what you are doing up there, but you definitely don't want to read input in here
            //      some of the idea is right but I don't know why you have all these new positions. I've renamed
            //      some of the ones at the top to make the changes here easier to follow

            var diff = smoothLeadPos - targetWorldPos;
            var clampedSmoothPos = Vector3.ClampMagnitude(diff, radius) + targetWorldPos;

            transform.position = clampedSmoothPos;

        }

        void Follow(float thrust)
        {
            followOn = true;
        }
    }
}