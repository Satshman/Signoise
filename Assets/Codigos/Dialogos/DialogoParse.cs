using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;


namespace DIALOGO {
    public class DialogoParse : MonoBehaviour
    {
        private const string comandoRogexPatron = "\\w*[^\\s]\\(";
        public static DialogoLinea Parse(string rawLinea)
        {
            Debug.Log($"Linea Parseada - '{rawLinea}'");
            (string narrador, string dialogo, string comandos) = RipContent(rawLinea);
            Debug.Log($"Narrador='{narrador}'\nDialogo='{dialogo}'\nComando='{comandos}'");
            return new DialogoLinea(narrador,dialogo,comandos);
        }

        private static (string,string,string) RipContent(string rawLinea)
        {
            string narrador = "", dialogo = "", comandos = "";
            int dialogoInicia =-1;
            int dialogoTermina = -1;
            bool isEscaped=false;

            for (int i = 0; i < rawLinea.Length; i++)
            {
                char current = rawLinea[i];
                if (current == '\\')
                    isEscaped = !isEscaped;
                else if (current == '"' && !isEscaped)
                {
                    if (dialogoInicia == -1)
                        dialogoInicia = i;
                    else if (dialogoTermina == -1)
                        dialogoTermina = i;
                }
                else
                    isEscaped = false;                                  
            }
            //Identifica patron de comando
            Debug.Log(rawLinea.Substring(dialogoInicia+1, dialogoTermina-dialogoInicia-1));
            Regex comandoRegex = new Regex(comandoRogexPatron);
            Match match=comandoRegex.Match(rawLinea);
            int comandoInicia = -1;
            if (match.Success)
            {
                comandoInicia = match.Index;
                if(dialogoInicia==-1 && dialogoTermina==-1)
                    return ("", "", rawLinea.Trim());

            }

            //Dialogo o comando?
            if (dialogoInicia != -1 && dialogoTermina != -1 && (comandoInicia == -1 || comandoInicia > dialogoTermina))
            {
                //Se tiene un valido dialogo
                narrador = rawLinea.Substring(0, dialogoInicia).Trim();
                dialogo = rawLinea.Substring(dialogoInicia + 1, dialogoTermina - dialogoInicia - 1).Replace("\\\"", "\"");
                if (comandoInicia != -1)
                {
                    comandos = rawLinea.Substring(comandoInicia).Trim();
                }
            }
            else if (comandoInicia != -1 && dialogoInicia > comandoInicia)
                comandos = rawLinea;
            else
                narrador= rawLinea;
            return (narrador, dialogo, comandos);

        }
       
    }
}

