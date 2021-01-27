using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace CalculateAge
{
    [Serializable]
    class BinarySerializationClass : IDeserializationCallback
    {
        public int yearOfBirth { get; set; }
        [NonSerialized]
        public int age;
        public BinarySerializationClass(int yearOfBirth)
        {
            this.yearOfBirth = yearOfBirth;
        }

        public void OnDeserialization(object sender)
        {
            DateTime dateTime = DateTime.Now;
            age = DateTime.Now.Year - yearOfBirth;
            

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your Birth Year :");
            int year = int.Parse(Console.ReadLine());
            BinarySerializationClass bs = new BinarySerializationClass(year);
            FileStream fs = new FileStream(@"age.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter br = new BinaryFormatter();
            br.Serialize(fs, bs);
            fs.Seek(0, SeekOrigin.Begin);
            BinarySerializationClass b = (BinarySerializationClass)br.Deserialize(fs);
            Console.WriteLine("Age of Person is : "+b.age);

        }
    }
}
