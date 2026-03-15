using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ArchivoManager 
{
    public static List<string> ReadTextFile(string archivoPath, bool includeBlankLines = true)

    {
        if (!archivoPath.StartsWith('/'))
            archivoPath = ArchivoPath.root+archivoPath;

        List<string>lineas=new List<string>();
        try
        {
            using (StreamReader sr = new StreamReader(archivoPath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (includeBlankLines || !string.IsNullOrWhiteSpace(line))
                        lineas.Add(line);
                }
            }
        }
        catch(FileNotFoundException ex)
        {
            Debug.LogError($"Archivo no encontrado: '{ex.FileName}'");
        }

        return lineas;
    }

    public static List<string> ReadTextAsset(string archivoPath, bool includeBlankLines = true)
    {
        TextAsset asset=Resources.Load<TextAsset>(archivoPath);
        if (asset==null)
        {
            Debug.LogError($"Archivo no encontrado: '{archivoPath}'");
            return null;
        }
        return ReadTextAsset(asset, includeBlankLines);
    }

    public static List<string> ReadTextAsset(TextAsset asset, bool includeBlankLines = true)
    {
        List<string> lines = new List<string>();
        using (StringReader sr = new StringReader(asset.text))
        {
            while (sr.Peek()>-1)
            {
                string line = sr.ReadLine();
                if(includeBlankLines||!string.IsNullOrWhiteSpace(line))
                    lines.Add(line);
            }
        }
        return lines;
    }
}
