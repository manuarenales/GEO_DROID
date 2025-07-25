﻿using System.Runtime.Serialization;
using System.Text;

/* Cambio no fusionado mediante combinación del proyecto 'GEO_DROID (net8.0-windows10.0.22621.0)'
Antes:
using System.Xml;
Después:
using System.Xml;
using GEO_DROID.Services.SincroService;
*/
using System.Xml;


/* Cambio no fusionado mediante combinación del proyecto 'GEO_DROID (net8.0-windows10.0.22621.0)'
Antes:
namespace GEO_DROID.Services.SincroService
Después:
namespace GEO_DROID.Services.SincroService.SincroService
*/
namespace GEO_DROID.Services.SincroService
{
    public class DataContractSerialization
    {
        public static Encoding ENCODING = Encoding.UTF8; //Importante: DataContractSerializer
        public static void WriteObject<T>(Stream stream, T obj)
        {
            try
            {
                DataContractSerializer ser = new DataContractSerializer(obj.GetType());
                XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(stream, ENCODING, false);
                ser.WriteObject(writer, obj);
                writer.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //writer.Close();
        }

        public static T ReadObject<T>(Stream stream)
        {
            XmlDictionaryReaderQuotas quotas = new XmlDictionaryReaderQuotas();
            quotas.MaxArrayLength = int.MaxValue; //Establecemos que el tamaño máximo de longitud de arrays que podemos leer sea el mayor posible
            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(stream, ENCODING, quotas, null);
            return ReadObject<T>(reader);
        }

        public static T ReadObject<T>(XmlReader reader)
        {
            Console.WriteLine("Deserializing an instance of the object.");

            DataContractSerializer ser = new DataContractSerializer(typeof(T));

            T deserializedObj = default(T);

            // Deserialize the data and read it from the instance.

            try
            {
                deserializedObj = (T)ser.ReadObject(reader, true);
            }
            catch (Exception e)
            {
                throw e;
            }
            reader.Close();
            return deserializedObj;
        }
    }
}
