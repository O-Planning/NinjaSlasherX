using UnityEngine;
using System.Collections;

public class StageTrigger_EventObject : MonoBehaviour {


	// === 外部パラメータ（インスペクタ表示） =====================
	[System.Serializable]
	public class Rigidbody2DParam {
		public bool						enabled 					= true;
		public float 					mass 						= 1.0f;
		public float	 				linearDrag 					= 0.0f;
		public float 					angularDrag 				= 0.05f;
		public float 					gravityScale 				= 1.0f;

//		public bool  					fixedAngle 					= false; // Unity Ver 4.x
		public bool						freezeRotation				= true;
		public float					inertia						= 0.0f;
		public RigidbodyConstraints2D	constraints					= RigidbodyConstraints2D.FreezeRotation;

		public bool  					isKinematic 				= false;
		public RigidbodyInterpolation2D interpolation				= RigidbodyInterpolation2D.None;
		public RigidbodySleepMode2D 	sleepMode					= RigidbodySleepMode2D.StartAwake;
		public CollisionDetectionMode2D collisionDetection 			= CollisionDetectionMode2D.Discrete;
		
		[Header("-------------------")]
		public Vector2 					centerOfMass 				= new Vector2(0.0f,0.0f);
		public Vector2 					velocity					= new Vector2(0.0f,0.0f);
		public float 					angularVelocity 			= 0.0f;

		[Header("-------------------")]
		public bool 					addForceEnabled 			= false;
		public Vector2					addForcePower 				= new Vector2 (0.0f, 0.0f);
		public bool 					addForceAtPositionEnabled 	= false;
		public GameObject				addForceAtPositionObject;
		public Vector2					addForceAtPositionPower	 	= new Vector2 (0.0f, 0.0f);
		public bool 					addRelativeForceEnabled 	= false;
		public Vector2					addRelativeForcePower	 	= new Vector2 (0.0f, 0.0f);
		public bool 					addTorqueEnabled 			= false;
		public float					addTorquePower				= 0.0f;
		public bool 					movePositionEnabled 		= false;
		public Vector2					movePosition			 	= new Vector2 (0.0f, 0.0f);
		public bool 					moveRotationEnabled 		= false;
		public float					moveRotation				= 0.0f;
	}

	public float 					runTime 	= 0.0f;
	public float 					destoryTime = 0.0f;

	[Space(10)]
	public bool						sendMesseageObjectEnabled 		= false;
	public string					sendMesseageString 				= "OnTriggerEnter2D_PlayerEvent";
	public GameObject[] 			sendMesseageObjectList;
	public bool						instantiateGameObjectEnabled 	= false;
	public GameObject[] 			instantiateGameObjectList;

	[Space(10)]
	public Rigidbody2DParam 		rigidbody2DParam;

	// === 外部パラメータ ======================================
	[System.NonSerialized] public bool triggerOn = false;

	// === キャッシュ ==========================================
	[System.NonSerialized] public Rigidbody2D	_rigidbody2D;


	// === コード（Monobehaviour基本機能の実装） ================
	void OnTriggerEnter2D_PlayerEvent (GameObject go) {
		Invoke ("runTriggerWork",runTime);
	}

	void runTriggerWork() {

		if (rigidbody2DParam.enabled) {
			_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
			if (_rigidbody2D == null) {
				_rigidbody2D = gameObject.AddComponent<Rigidbody2D> ();
			}

			_rigidbody2D.mass 				= rigidbody2DParam.mass;
			_rigidbody2D.drag 				= rigidbody2DParam.linearDrag;
			_rigidbody2D.angularDrag 		= rigidbody2DParam.angularDrag;
			_rigidbody2D.gravityScale 		= rigidbody2DParam.gravityScale;

//			_rigidbody2D.fixedAngle 		= rigidbody2DParam.fixedAngle;	// Unity Ver 4.x
			_rigidbody2D.freezeRotation 	= rigidbody2DParam.freezeRotation;
			_rigidbody2D.inertia 			= rigidbody2DParam.inertia;
			_rigidbody2D.constraints 		= rigidbody2DParam.constraints;

			_rigidbody2D.isKinematic 		= rigidbody2DParam.isKinematic;
			_rigidbody2D.interpolation 		= rigidbody2DParam.interpolation;
			_rigidbody2D.sleepMode 			= rigidbody2DParam.sleepMode;
			_rigidbody2D.collisionDetectionMode 	= rigidbody2DParam.collisionDetection;

			_rigidbody2D.centerOfMass 		= rigidbody2DParam.centerOfMass;
			_rigidbody2D.velocity			= rigidbody2DParam.velocity;
			_rigidbody2D.angularVelocity 	= rigidbody2DParam.angularVelocity;

			if (rigidbody2DParam.addForceEnabled) {
				gameObject.GetComponent<Rigidbody2D>().AddForce(rigidbody2DParam.addForcePower);
			}
			if (rigidbody2DParam.addForceAtPositionEnabled) {
				_rigidbody2D.AddForceAtPosition(
					rigidbody2DParam.addForceAtPositionPower,
					rigidbody2DParam.addForceAtPositionObject.transform.position);
			}
			if (rigidbody2DParam.addRelativeForceEnabled) {
				_rigidbody2D.AddRelativeForce(rigidbody2DParam.addRelativeForcePower);
			}
			if (rigidbody2DParam.addTorqueEnabled) {
				_rigidbody2D.AddTorque(rigidbody2DParam.addTorquePower);
			}
			if (rigidbody2DParam.movePositionEnabled) {
				_rigidbody2D.MovePosition(rigidbody2DParam.movePosition);
			}
			if (rigidbody2DParam.moveRotationEnabled) {
				_rigidbody2D.MoveRotation(rigidbody2DParam.moveRotation);
			}
		}

		if (sendMesseageObjectEnabled && sendMesseageObjectList != null) {
			foreach(GameObject go in sendMesseageObjectList) {
				go.SendMessage(sendMesseageString,gameObject);
			}
		}
		if (instantiateGameObjectEnabled && instantiateGameObjectList != null) {
			foreach(GameObject go in instantiateGameObjectList) {
				Instantiate(go);
			}
		}

		if (destoryTime > 0.0f) {
			Destroy(gameObject,destoryTime);
		}
		
		triggerOn = true;
	}
}
