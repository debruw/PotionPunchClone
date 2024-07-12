using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class BottlesParent : MonoBehaviour
    {
        public Bottle[] bottles;
        public Color[] Colors;

        private void Start()
        {
            for (int i = 0; i < bottles.Length; i++)
            {
                bottles[i].COLOR = Colors[i];
            }
        }
    }
}