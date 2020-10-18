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



            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            Vector3 MaxPos = transform.position + movement;
            Vector3 offsetClamp = MaxPos - centerPt;
            transform.position = centerPt + Vector3.ClampMagnitude(offsetClamp, radius);

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