using UnityEngine;
using DIALOGO;
namespace TESTEO {
    public class TesteoArquitectura : MonoBehaviour
    {
        SistemaDialogo ds;
        ArquitecturaTexto arquitectura;
        public ArquitecturaTexto.MetodoConstruir bm = ArquitecturaTexto.MetodoConstruir.instant;

        string[] lineas = new string[5]
        {
            "¡Cuac-cuac! ¿Sabías que el agua no solo moja, también cuenta secretos si escuchas bien?",
            "No necesito volar alto, ¡mis sueños ya están en las nubes!",
            "¡Ay! Me resbalé… pero al menos hice una entrada espectacular, ¿verdad?",
            "si te sientes triste, solo chapotea un rato. Nadie puede estar triste mientras salpica",
            "Si ves pan, ¡avísame! Es por ciencia… digo, por curiosidad científica, jeje"
        };

        void Start()
        {
            ds = SistemaDialogo.instancia;
            arquitectura = new ArquitecturaTexto(ds.contenedorDialogo.dialogoTexto);
            arquitectura.metodoConstruir = ArquitecturaTexto.MetodoConstruir.fade;
            arquitectura.velocidad = 0.5f;
        }

        // Update is called once per frame
        void Update()
        {
            if (bm != arquitectura.metodoConstruir)
            {
                arquitectura.metodoConstruir = bm;
                arquitectura.Detener();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                arquitectura.Detener();
            }
            string lineaLarga = "¡Cuac-cuac! ¿Sabías que el agua no solo moja, también cuenta secretos si escuchas bien?No necesito volar alto, ¡mis sueños ya están en las nubes!¡Ay! Me resbalé… pero al menos hice una entrada espectacular, ¿verdad?si te sientes triste, solo chapotea un rato. Nadie puede estar triste mientras salpica";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (arquitectura.construyendo)
                {
                    if (!arquitectura.acelerar)
                    {
                        arquitectura.acelerar = true;
                    }
                    else
                    {
                        arquitectura.ForzarCompletado();
                    }
                }
                else
                {
                    arquitectura.Construccion(lineas[Random.Range(0, lineas.Length)]);
                }

            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                arquitectura.Iniciar(lineaLarga);
            }

        }
    }
}
