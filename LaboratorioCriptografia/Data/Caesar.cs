using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratorioCriptografia.Data
{
    public class Caesar
    {
        public Dictionary<char, char> DiccionarioChechaMayus;
        public Dictionary<char, char> DiccionarioChechaMinus;
        public static string Alfabeto = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ .,:;¿?¡!1234567890";
        public static string AlfabetoMin = "abcdefghijklmnñopqrstuvwxyz .,:;¿?¡!1234567890";
        //public string claveChecha = "Dovakin".ToUpper() + Alfabeto.ToUpper();
        //public string claveChechaMin = "dovahkin" + AlfabetoMin;
        //_ =  new string ("");
        string mensajeEncriptado = "";
        void EncriptarChecha(string clave)
        {

            string claveMayusculas;
            claveMayusculas = clave.ToUpper() + Alfabeto.ToUpper();
            clave += AlfabetoMin;
            var c = RemoverLetrasRepetidas(claveMayusculas);
            var r = RemoverLetrasRepetidas(clave);
            for (int i = 0; i < Alfabeto.Length; i++)
            {
                DiccionarioChechaMayus.Add(Alfabeto[i], c[i]);
            }
            //llenar diccionario Minusculas
            for (int k = 0; k < AlfabetoMin.Length; k++)
            {
                DiccionarioChechaMinus.Add(AlfabetoMin[k], r[k]);
            }
            string MensajeAEncriptar = Console.ReadLine();
            string Encriptado = "";
            MensajeAEncriptar.ToCharArray();
            for (int j = 0; j < MensajeAEncriptar.Length; j++)
            {
                if (char.IsUpper(MensajeAEncriptar[j]))
                {
                    if (DiccionarioChechaMayus.ContainsKey(MensajeAEncriptar[j]))
                    {
                        //char joto = MensajeAEncriptar[j];
                        Encriptado = DiccionarioChechaMayus[MensajeAEncriptar[j]].ToString();
                        mensajeEncriptado += Encriptado;
                    }
                }
                else
                {
                    if (DiccionarioChechaMinus.ContainsKey(MensajeAEncriptar[j]))
                    {
                        //char joto = MensajeAEncriptar[j];
                        Encriptado = DiccionarioChechaMinus[MensajeAEncriptar[j]].ToString();
                        mensajeEncriptado += Encriptado;
                    }
                }
            }
        }
        public static string RemoverLetrasRepetidas(string input)
        {
            return new string(input.ToCharArray().Distinct().ToArray());
        }
    }
}
