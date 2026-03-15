using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGO 
{
    public class DialogoLinea
    {
        public string narrador;
        public string dialogo;
        public string comandos;

        public DialogoLinea(string narrador, string dialogo, string comandos)
        {
            this.narrador = narrador;
            this.dialogo = dialogo;
            this.comandos = comandos;
        }
    }
}

