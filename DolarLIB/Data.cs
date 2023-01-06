using System.Net;

namespace DolarLIB
{

    /// <summary>
    /// Obtiene el Data
    /// </summary>
    public class Data
    {

        /// <summary>
        /// Obtiene los datos del Dolar
        /// </summary>
        public static (bool Connected, bool IsDown, string Value) FethData()
        {
            var data = GetData();
            return new(data.Can, !data.IsDown, data.Value);
        }



        /// <summary>
        /// Obtiene los datos
        /// </summary>
        private static (string Value, bool Can, bool IsDown) GetData()
        {

            // Url de consulta
            string url = "https://dolar.wilkinsonpc.com.co/";
            bool IsDown = false;

            // Respuesta
            var request = (HttpWebRequest)WebRequest.Create(url);

            // Configuracion
            request.UserAgent = "curl";
            request.Method = "GET";

            // Resultado
            string result = "";

            try
            {
                // Ejecucion
                using (WebResponse response = request.GetResponse())
                {
                    using var reader = new StreamReader(response.GetResponseStream());
                    result = reader.ReadToEnd();
                }


                // Dato Solicitado (Bajo | Subio)




                // Obtiene el dato solicitado
                string dto = "sube-numero";
            Again:
                int i1 = result.IndexOf(dto);
                if (i1 == -1 & !IsDown)
                {
                    dto = "baja-numero";
                    IsDown = true;
                    goto Again;
                }
                else
                {
                    IsDown = false;
                }

                // 
                string str1 = result.Remove(0, i1 + dto.Length + 3);

                i1 = str1.IndexOf('<');
                string result3 = str1[..i1];

                result3 = result3.Split(".")[0];
                return new(result3, true, IsDown);

            }
            catch
            {
                return new("", false, false);
            }


        }


    }
}
