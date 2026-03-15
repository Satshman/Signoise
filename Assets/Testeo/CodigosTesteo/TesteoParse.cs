using DIALOGO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TESTEO {
    public class TesteoParse : MonoBehaviour
    {
        [SerializeField] private TextAsset file;
        void Start()
        {
            MandarVideoParaParse();
        }

        void  MandarVideoParaParse()
        {
            List<string> lineas = ArchivoManager.ReadTextAsset(file, false);

            foreach (string line in lineas) 
            {
                if(line==string.Empty)
                    continue;
                DialogoLinea dl = DialogoParse.Parse(line);
            }               
        }
    }
}

