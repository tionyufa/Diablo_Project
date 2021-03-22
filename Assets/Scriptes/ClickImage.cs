using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickImage : MonoBehaviour
{
   [SerializeField] private Image _image;
   private Camera _camera;

   private void Awake()
   {
      _camera = Camera.main;
   }

   private void Update()
   {
      
   }
}
