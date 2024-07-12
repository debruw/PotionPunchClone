using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class Plate : MonoBehaviour
    {
        public Renderer rend;
        public LayerMask bottlesLayer;
        private Vector3 startPos;

        private void Start()
        {
            startPos = transform.position;
        }

        public void SetColor(Color color)
        {
            rend.material.color = color;
        }

        #region Movement

        //used for getting mouse pos
        private Vector3 mouseOffset;
        private float mouseZCoord;

        private void OnMouseDown()
        {
            //add z coord
            mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mouseOffset = gameObject.transform.position - GetMouseWorldPos();
        }

        private void OnMouseDrag()
        {
            transform.position = GetMouseWorldPos() + mouseOffset;
            Vector3 y = new Vector3(transform.position.x, transform.position.y, startPos.z);
            transform.position = y;
        }

        private void OnMouseUp()
        {
            //TODO bottle a çarpıyor mu kontrol et
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction);

            if (Physics.Raycast(ray, out hit, 100, bottlesLayer))
            {
                Bottle hitBottle = hit.transform.GetComponent<Bottle>();
                Debug.LogError("does hit : ", hit.transform.gameObject);
                transform.position = hitBottle.platePos.position;
            }
            else
            {
                Debug.LogError("not hit");
                transform.position = startPos;
            }
        }

        private Vector3 GetMouseWorldPos()
        {
            //pixel coordinates (x,y)
            Vector3 mousePoint = Input.mousePosition;

            //z coordinate of game object on screen
            mousePoint.z = mouseZCoord;

            return Camera.main.ScreenToWorldPoint(mousePoint);
        }

        #endregion
    }
}