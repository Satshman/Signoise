using System.Collections;
using UnityEngine;
using TMPro;
using System.Globalization;

public class ArquitecturaTexto
{
    private TextMeshProUGUI tmpro_ui;
    private TextMeshProUGUI tmpro_mundo;
    public TMP_Text tmpro => tmpro_ui != null ? tmpro_ui : tmpro_mundo;

    public string textoActual => tmpro.text;
    public string targetTexto { get; private set; } = "";
    public string preTexto { get; private set; } = "";
    private int preTextoTamano = 0;
    public string targetCompletoTexto => preTexto + targetTexto;

    public enum MetodoConstruir {instant,typewriter,fade}
    public MetodoConstruir metodoConstruir=MetodoConstruir.typewriter;
    public Color colorTexto {get { return tmpro.color; }set { tmpro.color = value; }}

    public float velocidad { get { return velocidadBase * velocidadMultiplicador; }set { velocidadMultiplicador = value; } }
    private const float velocidadBase = 1;
    private float velocidadMultiplicador = 1;

    public int caracterCiclo { get { return velocidad <= 2f ? caracterMultiplicador : velocidad <= 2.5f ? caracterMultiplicador * 2 : caracterMultiplicador * 3; } }
    private int caracterMultiplicador = 1;
    public bool acelerar=false;

    public ArquitecturaTexto(TextMeshProUGUI tmpro_ui)
    {
        this.tmpro_ui = tmpro_ui;

    }

    //Build metodo
    public Coroutine Construccion(string text)
    {
        preTexto = "";
        targetTexto= text;
        Detener();
        construccionProceso = tmpro.StartCoroutine(Construccion());
        return construccionProceso;
    }
    //Append dialogo
    public Coroutine Iniciar(string text)
    {
        preTexto = tmpro.text;
        targetTexto = text;
        Detener();
        construccionProceso = tmpro.StartCoroutine(Construccion());
        return construccionProceso;
    }

    public Coroutine construccionProceso = null;
    public bool construyendo => construccionProceso != null;

    public void Detener()
    {
        if (!construyendo)
            return;

        tmpro.StopCoroutine(construccionProceso);
        construccionProceso = null;
    }

    IEnumerator Construccion()
    {
        Preparar();
        switch (metodoConstruir)
        {
            case MetodoConstruir.typewriter:
                yield return Construir_Typewriter();
                break;
            case MetodoConstruir.fade:
                yield return Construir_fade();
                break;
        }
        Finalizar();
    }

    private void Finalizar()
    {
        construccionProceso = null;
        acelerar = false;
    }
  
    public void ForzarCompletado()
    {
        switch (metodoConstruir)
        {
            case MetodoConstruir.typewriter:
                tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
                break;
            case MetodoConstruir.fade:
                tmpro.ForceMeshUpdate();
                break;
            
        }
        Detener();
        Finalizar();
    }

    private void Preparar()
    {
        switch (metodoConstruir)
        {
            case MetodoConstruir.typewriter:
                PrepararTypewriter();
                break;
            case MetodoConstruir.fade:
                PrepararFade();
                break;
            case MetodoConstruir.instant:
                PrepararInstant();
                break;
        }
    }
    private void PrepararInstant()
    {
        tmpro.color = tmpro.color;
        tmpro.text = targetCompletoTexto;
        tmpro.ForceMeshUpdate();
        tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
    }

    private void PrepararTypewriter()
    {
        tmpro.color = tmpro.color;
        tmpro.maxVisibleCharacters = 0;
        tmpro.text = preTexto;
        if (preTexto != "")
        {
            tmpro.ForceMeshUpdate();
            tmpro.maxVisibleCharacters=tmpro.textInfo.characterCount;
        }

        tmpro.text += targetTexto;
        tmpro.ForceMeshUpdate();
    }
  
    private void PrepararFade()
    {
        tmpro.text= preTexto;
        if (preTexto != "")
        {
            tmpro.ForceMeshUpdate();
            preTextoTamano = tmpro.textInfo.characterCount;
        }
        else
        {
            preTextoTamano = 0;
        }
        tmpro.text += targetTexto;
        tmpro.maxVisibleCharacters=int.MaxValue;
        tmpro.ForceMeshUpdate();

        TMP_TextInfo textoInformacion = tmpro.textInfo;
        Color colorVisible = new Color(colorTexto.r,colorTexto.g,colorTexto.b,1);
        Color colorOculto = new Color(colorTexto.r, colorTexto.g, colorTexto.b,0);

        Color32[] vectorColores = textoInformacion.meshInfo[textoInformacion.characterInfo[0].materialReferenceIndex].colors32;

        for(int i = 0;i < textoInformacion.characterCount; i++)
        {
            TMP_CharacterInfo caracterInformacion = textoInformacion.characterInfo[i];
            if (!caracterInformacion.isVisible)
                continue;
            if (i<preTextoTamano)
            {
                for (int v=0;v<4;v++)
                {
                    vectorColores[caracterInformacion.vertexIndex+v]=colorVisible;
                }
            }
            else
            {
                for (int v = 0; v < 4; v++)
                {
                    vectorColores[caracterInformacion.vertexIndex + v] = colorOculto;
                }
            }
        }

        tmpro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

    private IEnumerator Construir_Typewriter()
    {
        while (tmpro.maxVisibleCharacters < tmpro.textInfo.characterCount)
        {
            tmpro.maxVisibleCharacters += acelerar ? caracterCiclo * 5 : caracterCiclo;
            yield return new WaitForSeconds(0.015f/velocidad);
        }
    }

    private IEnumerator Construir_fade()
    {
        int minRango = preTextoTamano;
        int maxRango = minRango + 1;

        byte alphaThreshold = 15;

        TMP_TextInfo textoInformacion = tmpro.textInfo;

        Color32[] vectorColores = textoInformacion.meshInfo[textoInformacion.characterInfo[0].materialReferenceIndex].colors32;

        float[] alphas = new float[textoInformacion.characterCount];

        while (true) 
        {
            float fadeVelocidad = velocidad;

            for (int i = minRango; i < maxRango; i++)
            {
                TMP_CharacterInfo caracterInformacion = textoInformacion.characterInfo[i];
                if (!caracterInformacion.isVisible)
                    continue;
                int vertexIndex = textoInformacion.characterInfo[i].vertexIndex;
                alphas[i] = Mathf.MoveTowards(alphas[i], 255, fadeVelocidad);

                for (int v = 0; v < 4; v++)
                    vectorColores[caracterInformacion.vertexIndex + v].a = (byte)alphas[i];
                if (alphas[i] >= 255)
                    minRango++;
            }
            tmpro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            bool ultimoCaracterEsInvisible = !textoInformacion.characterInfo[maxRango - 1].isVisible;
            if (alphas[maxRango - 1] > alphaThreshold || ultimoCaracterEsInvisible) { }
            {
                if (maxRango < textoInformacion.characterCount)
                    maxRango++;
                else if (alphas[maxRango - 1] >= 255 || ultimoCaracterEsInvisible)
                    break;
            }
            yield return new WaitForEndOfFrame();
        }
    }



}
