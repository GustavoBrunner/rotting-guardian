using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Game.GeneralEvents{

    public class ChangeCamTarget : UnityEvent<Transform> { }

    public class CameraEvents : MonoBehaviour
    {
        public readonly static ChangeCamTarget onChangeCamTarget = new ChangeCamTarget();
    }
}