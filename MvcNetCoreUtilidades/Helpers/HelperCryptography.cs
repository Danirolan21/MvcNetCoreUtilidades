using System;
using System.Security.Cryptography;
using System.Text;

namespace MvcNetCoreUtilidades.Helpers
{
    public class HelperCryptography
    {
        public static string Salt { get; set; }

        private static string GenerateSalt()
        {
            Random random = new Random();
            string salt = "";
            for (int i = 1; i <= 30; i++)
            {
                int aleat = random.Next(1, 255);
                char letra = Convert.ToChar(aleat);
                salt += letra;
            }
            return salt;
        }

        public static string CifrarContenido
            (string contenido, bool comparar)
        {
            if (comparar == false)
            {
                Salt = GenerateSalt();
            }
            string contenidoSalt = contenido + Salt;
            SHA256 managed = SHA256.Create();
            byte[] salida;
            UnicodeEncoding encoding = new UnicodeEncoding();
            salida = encoding.GetBytes(contenidoSalt);
            for (int i = 1; i <= 22; i++)
            {
                salida = managed.ComputeHash(salida);
            }
            //DEBEMOS LIBERAR LA MEMORIA
            managed.Clear();
            string resultado = encoding.GetString(salida);
            return resultado;
        }

        public static string EncriptarTextoBasico(string contenido)
        {
            //NECESITAMOS UN ARRAY DE
            //EL CONTENIDO DE ENTRADA
            byte[] entrada;

            //AL CIFRAR EL CONTENIDO, NOS DEVUELVE BYTES[] DE SALIDA
            byte[] salida;
            //NECESITAMOS UNA CLASE QUE NOS PERMITE CONVERTIR DE
            //STRING A BYTE[] Y VICEVERSA
            UnicodeEncoding encoding = new UnicodeEncoding();
            //NECESITAMOS UN OBJETO PARA CIFRAR EL CONTENIDO
            SHA1 managed = SHA1.Create();
            //CONVERTIMOS EL CONTENIDO DE ENTRADA A byte[]
            entrada = encoding.GetBytes(contenido);
            //LOS OBJETOS PARA CIFRAR CONTIENEN UN METODO LLAMADO 
            //ComputedHash QUE RECIBEN UN ARRAY DE BYTES E
            //INTERNAMENTE HACEN COSAS Y DEVUELVE OTRO ARRAY DE BYTES
            salida = managed.ComputeHash(entrada);
            //CONVERTIMOS SALIDA A STRING
            string resultado = encoding.GetString(salida);
            return resultado;
        }
    }
}
