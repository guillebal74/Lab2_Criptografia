using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace LaboratorioCriptografia.Data
{
    public class Caesar
    {
        private static Caesar _instance = null;
        public static Caesar Instance
        {
            get
            {
                if (_instance == null) _instance = new Caesar();
                return _instance;
            }
        }
        public Dictionary<char, char> DiccionarioChechaMayus;
        public Dictionary<char, char> DiccionarioChechaMinus;
        public static string Alfabeto = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ .,:;¿?¡!1234567890";
        public static string AlfabetoMin = "abcdefghijklmnñopqrstuvwxyz .,:;¿?¡!1234567890";
        //public string claveChecha = "Dovakin".ToUpper() + Alfabeto.ToUpper();
        //public string claveChechaMin = "dovahkin" + AlfabetoMin;
        //_ =  new string ("");
        string mensajeEncriptado = "";
        HttpPostedFileBase File;
        string Ruta;
        void EncriptarChecha(HttpPostedFileBase postedFile, string ruta,string clave)
        {
            var file = new FileStream(File.FileName, FileMode.Open); // cambiar a dinamico
            var lectura = new BinaryReader(file);
            var Buffer = new byte[1000000];
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

            Buffer = lectura.ReadBytes(1000000);
            string Encriptado = "";
            //MensajeAEncriptar.ToCharArray();
            for (int j = 0; j < Buffer.Length; j++)
            {
                if (char.IsUpper((char)Buffer[j]))
                {
                    if (DiccionarioChechaMayus.ContainsKey((char)Buffer[j]))
                    {
                        //char joto = MensajeAEncriptar[j];
                        Encriptado = DiccionarioChechaMayus[(char)Buffer[j]].ToString();
                        mensajeEncriptado += Encriptado;
                    }
                }
                else
                {
                    if (DiccionarioChechaMinus.ContainsKey((char)Buffer[j]))
                    {
                        //char joto = MensajeAEncriptar[j];
                        Encriptado = DiccionarioChechaMinus[(char)Buffer[j]].ToString();
                        mensajeEncriptado += Encriptado;
                    }
                }
            }
        }
        public static string RemoverLetrasRepetidas(string input)
        {
            return new string(input.ToCharArray().Distinct().ToArray());
        }
        void DesencriptarChecha()
        {
            const int bufferLength = 100000;
            using (var stream = new FileStream(File.FileName, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream))
                {
                    using (var writeStream = new FileStream(File.FileName, FileMode.OpenOrCreate))
                    {
                        using (var writer = new BinaryWriter(writeStream))
                        {
                            var byteBuffer = new byte[bufferLength];
                            while (reader.BaseStream.Position != reader.BaseStream.Length)
                            {
                                byteBuffer = reader.ReadBytes(bufferLength);
                                writer.Write(byteBuffer);
                            }
                        }
                    }
                }
            }
        }
    }
}
