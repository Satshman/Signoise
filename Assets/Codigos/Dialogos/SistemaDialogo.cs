using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace DIALOGO {
    public class SistemaDialogo : MonoBehaviour
    {

        public ContenedorDialogo contenedorDialogo = new ContenedorDialogo();


        public static SistemaDialogo instancia;
        private void Awake()
        {
            if (instancia == null)
                instancia = this;
            else
                DestroyImmediate(gameObject);

        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

