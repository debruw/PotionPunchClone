using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class Mug : MonoBehaviour
    {
        public Transform PlatePos;
        public Renderer rend;
        public Color COLOR
        {
            get => _color;
            set
            {
                _color = value;
                rend.material.color = _color;
            }
        }

        private Color _color;

    }
}