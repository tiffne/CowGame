using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;

namespace _Scripts.Customer
{
    [CreateAssetMenu(fileName = "New Customer Species", menuName = "Scriptable Objects/Customer Species")]
    public class AnimalScriptableObject : ScriptableObject
    {
        [SerializeField] private Sprite patientSprite;
        public Sprite PatientSprite => patientSprite;
        
        [SerializeField] private Sprite neutralSprite;
        public Sprite NeutralSprite => neutralSprite;
        
        [SerializeField] private Sprite irritatedSprite;
        public Sprite IrritatedSprite => irritatedSprite;
    }
}