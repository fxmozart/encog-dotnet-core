using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Encog.Util
{
    public class SerializeRoundTrip
    {
        /// <summary>
        ///  Round trip serializes to memory then de serializes.
        /// </summary>
        /// <param name="obj"> The object. </param>
        /// <returns>
        ///  .
        /// </returns>
        public static object RoundTrip(object obj)
        {
            // first serialize to memory
            var ms = new MemoryStream();
            var b = new BinaryFormatter();
            b.Serialize(ms, obj);

            // now reload
            ms.Seek(0, SeekOrigin.Begin);
            object result = b.Deserialize(ms);
            return result;
        }
    }
}