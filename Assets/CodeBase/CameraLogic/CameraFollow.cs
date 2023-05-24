using System;
using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        public float rotationAngleX;
        public float distance;
        public float offsetY;

        [SerializeField] private Transform following;

        private void LateUpdate()
        {
            if (following == null)
                return;
            Quaternion rotation = Quaternion.Euler(rotationAngleX, 0, 0);
            Vector3 position = rotation * new Vector3(0, 0, -distance) + FollowingPosition();

            transform.rotation = rotation;
            transform.position = position;
        }

        public void Follow(GameObject follow) => following = follow.transform;

        private Vector3 FollowingPosition()
        {
            Vector3 followingPosition = following.position;
            followingPosition.y += offsetY;
            return followingPosition;
        }
    }
}