using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TESTEO {
    public class TesteoArchivos : MonoBehaviour
    {

        [SerializeField] private TextAsset fileName;
        void Start()
        {
            StartCoroutine(Run());
        }

        IEnumerator Run()
        {
            List<string> lineas = ArchivoManager.ReadTextAsset(fileName, true);

            foreach (string line in lineas)
                Debug.Log(line);

            yield return null;
        }
    }
}

