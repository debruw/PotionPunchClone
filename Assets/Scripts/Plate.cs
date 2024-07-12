using System;
using DG.Tweening;
using Scrtwpns.Mixbox;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class Plate : MonoBehaviour
    {
        public Renderer rend;
        public LayerMask bottlesLayer, MugsLayer;
        
        private Vector3 startPos;
        private bool isFilling;
        private Color? _color;

        private void Start()
        {
            startPos = transform.position;
        }

        #region Movement

        //used for getting mouse pos
        private Vector3 mouseOffset;
        private float mouseZCoord;

        private void OnMouseDown()
        {
            if (isFilling) return;
            
            //add z coord
            mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mouseOffset = gameObject.transform.position - GetMouseWorldPos();
        }

        private void OnMouseDrag()
        {
            if (isFilling) return;
            
            transform.position = GetMouseWorldPos() + mouseOffset;
            Vector3 y = new Vector3(transform.position.x, transform.position.y, startPos.z);
            transform.position = y;
        }

        private void OnMouseUp()
        {
            if (isFilling) return;
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction);

            if (Physics.Raycast(ray, out hit, 100, bottlesLayer))
            {
                Bottle hitBottle = hit.transform.GetComponent<Bottle>();
                transform.DOMove(hitBottle.platePos.position, .1f);
                isFilling = true;
                
                Fill(hitBottle);
            }
            else if (Physics.Raycast(ray, out hit, 100, MugsLayer) && _color.HasValue)
            {
                Mug hitMug = hit.transform.GetComponent<Mug>();
                transform.DOMove(hitMug.PlatePos.position, .1f);
                transform.DORotate(hitMug.PlatePos.eulerAngles, .1f);
                Discharge(hitMug);
            }
            else
            {
                transform.DOMove(startPos, .1f);
            }
        }

        public void Fill(Bottle bottle)
        {
            bottle.StartFill();
            // calculate color
            if (_color.HasValue)
            {
                _color = Mixbox.Lerp(_color.Value, bottle.COLOR, .5f);
                rend.material.DOColor(_color.Value, GLOBALVARIABLES.PlateFillDuration).OnComplete(() =>
                {
                    ProcessOver();
                    bottle.StopFill();
                });
            }
            else
            {
                _color = bottle.COLOR;
                rend.material.color = _color.Value;
                rend.material.DOFloat(1, "_Fill", GLOBALVARIABLES.PlateFillDuration).OnComplete(() =>
                {
                    ProcessOver();
                    bottle.StopFill();
                });
            }
        }

        public void Discharge(Mug hitMug)
        {
            hitMug.COLOR = _color.Value;
            hitMug.rend.material.DOFloat(1, "_Fill", GLOBALVARIABLES.PlateFillDuration);
            rend.material.DOFloat(0, "_Fill", GLOBALVARIABLES.PlateFillDuration).OnComplete(() =>
            {
                //ProcessOver();
            });
        }
        
        void ProcessOver()
        {
            isFilling = false;
            transform.DOMove(startPos, .1f);
            transform.DORotate(Vector3.zero, .1f);
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