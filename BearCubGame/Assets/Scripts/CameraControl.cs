using UnityEngine;
using System;
using System.Collections.Generic;

public class CameraControl : MonoBehaviour
{
	PlayerController playerController;
	public bool groupFollowMode = false;

	public List<Transform> layerList;

	//Transform Trees_FGrnd01;
	//Transform Trees_FGrnd02;
	ScrollingScript scrollScript;

	public float m_DampTime = 0.2f;                 
	public float m_ScreenEdgeBuffer = 4f;           
	public float m_MinSize = 6.5f;                  
	public Transform[] m_Targets; 


	private Camera m_Camera;                        
	private float m_ZoomSpeed;                      
	private Vector3 m_MoveVelocity;                 
	private Vector3 m_DesiredPosition;              


	private void Awake()
	{
		playerController = GameObject.Find ("PlayerController").gameObject.transform.GetComponent<PlayerController>();
		m_Camera = GetComponentInChildren<Camera>();
	}


	private void FixedUpdate()
	{
		Move ();
		Zoom ();

		// Moving trees layers //
		for (int i = 0; i < layerList.Count; i++) {

			scrollScript = layerList [i].gameObject.transform.GetComponent<ScrollingScript> ();
			scrollScript.MoveLayer (this.transform);

		}
	}

	private void Move()
	{
		if (groupFollowMode) {
			
			FindAveragePosition ();

		} else {
			
			FindCharacterPosition ();
		}

		transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
	}



	private void FindCharacterPosition()
	{
		Vector3 averagePos = new Vector3 ();
		int numTargets = 0;

		if (playerController.bearCubSelected) {
			if (m_Targets [0].gameObject.activeSelf) {
				averagePos += m_Targets [0].position;
			}
		}

		if (playerController.rabbitBabySelected) {
			if (m_Targets [1].gameObject.activeSelf) {
				averagePos += m_Targets [1].position;
			}
		}

		if (playerController.bisonCalfSelected) {
			if (m_Targets [2].gameObject.activeSelf) {
				averagePos += m_Targets [2].position;
			}
		}
		m_DesiredPosition = averagePos;
	}
		

	private void FindAveragePosition()
	{
		Vector3 averagePos = new Vector3();
		int numTargets = 0;

		for (int i = 0; i < m_Targets.Length; i++)
		{
			if (!m_Targets[i].gameObject.activeSelf)
				continue;
			
			averagePos += m_Targets[i].position;
			numTargets++;
		}

		if (numTargets > 0)
			averagePos /= numTargets;

		m_DesiredPosition = averagePos;
	}


	private void Zoom()
	{
		float requiredSize = FindRequiredSize();
		m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime);
	}


	private float FindRequiredSize() {
		
		Vector3 desiredLocalPos = transform.InverseTransformPoint(m_DesiredPosition);
		float size = 0f;

		if (playerController.bearCubSelected) {
			if (m_Targets [0].gameObject.activeSelf) {
				Vector3 targetLocalPos = transform.InverseTransformPoint(m_Targets[0].position);
				Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;
				size = Mathf.Max (size, Mathf.Abs (desiredPosToTarget.y));
				size = Mathf.Max (size, Mathf.Abs (desiredPosToTarget.x) / m_Camera.aspect);
			}
		}

		if (playerController.rabbitBabySelected) {
			if (m_Targets [1].gameObject.activeSelf) {
				Vector3 targetLocalPos = transform.InverseTransformPoint (m_Targets [1].position);
				Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;
				size = Mathf.Max (size, Mathf.Abs (desiredPosToTarget.y));
				size = Mathf.Max (size, Mathf.Abs (desiredPosToTarget.x) / m_Camera.aspect);
			}
		}

		if (playerController.bisonCalfSelected) {
			if (m_Targets [2].gameObject.activeSelf) {
				Vector3 targetLocalPos = transform.InverseTransformPoint (m_Targets [2].position);
				Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;
				size = Mathf.Max (size, Mathf.Abs (desiredPosToTarget.y));
				size = Mathf.Max (size, Mathf.Abs (desiredPosToTarget.x) / m_Camera.aspect);
			}
		}

		size += m_ScreenEdgeBuffer;
		size = Mathf.Max(size, m_MinSize);

		return size;
	}
}