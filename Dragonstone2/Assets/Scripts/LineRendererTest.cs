using System.Collections.Generic;
 using UnityEngine;
 
 [RequireComponent(typeof(LineRenderer))]
 public class LineRendererTest : MonoBehaviour
 {
     List<Vector3> linePoints = new List<Vector3>();
     LineRenderer lineRenderer;
     public float startWidth = 0.1f;
     public float endWidth = 0.1f;
     public float threshold = 0.001f;
     Camera thisCamera;
     int lineCount = 0;
 
     Vector3 lastPos = Vector3.one * float.MaxValue;
     
     public Player player;

     void Awake()
     {
         thisCamera = Camera.main;
         lineRenderer = GetComponent<LineRenderer>();
     }

	
 
     void Update()
     {

		 if (player.dragging)
		 {
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = thisCamera.nearClipPlane;
			Vector3 mouseWorld = thisCamera.ScreenToWorldPoint(mousePos);
			
			float dist = Vector3.Distance(lastPos, mouseWorld);
			if(dist <= threshold)
				return;
			
			lastPos = mouseWorld;
			if(linePoints == null)
				linePoints = new List<Vector3>();
			linePoints.Add(mouseWorld);
			
			UpdateLine();
		 }

		 else
		 {
		 	linePoints.Clear();
			lineRenderer.positionCount = 0;
		 }
     }
 
 
     void UpdateLine()
     {
         //lineRenderer.SetWidth(startWidth, endWidth);
		 lineRenderer.startWidth = startWidth;
		 lineRenderer.endWidth = endWidth;
		 lineRenderer.positionCount = linePoints.Count;
         //lineRenderer.SetVertexCount(linePoints.Count);
 
         for(int i = lineCount; i < linePoints.Count; i++)
         {
             lineRenderer.SetPosition(i, linePoints[i]);
         }
         lineCount = linePoints.Count;
     }
 }