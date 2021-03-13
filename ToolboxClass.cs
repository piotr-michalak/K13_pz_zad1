using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Projekt1
{
    public class ToolboxClass
    {
        public static int WprowadzLiczbeZZakresu(int a_iMin, int a_iMax)
        {
            while (true)
            {
                Console.Write($"Podaj wartosc z przedzialu {a_iMin}-{a_iMax}:");

                if (int.TryParse(Console.ReadLine(), out int _iValue) == true &&
                    _iValue >= a_iMin && _iValue <= a_iMax)
                {
                    return _iValue;
                }

                Console.WriteLine("Niepoprawna wartosc");
            }
        }

        public static string WprowadzTekst(string a_sText, bool a_bCanBeEmpty = true)
        {
            while (true)
            {
                Console.Write(a_sText);

                string _sValue = Console.ReadLine();

                if (string.IsNullOrEmpty(_sValue) && a_bCanBeEmpty == false)
                {
                    Console.WriteLine("Nie wpisano wartosci.");
                }
                else
                    return _sValue;
            }
        }

        static string plik = "dane.xml";
        public static void Serializacja(List<Osoba> ListaOsob)
        {
            try
            {
                XmlSerializer serializacja = new XmlSerializer(typeof(List<Osoba>));
                using (TextWriter zapis = new StreamWriter(plik))
                {
                    serializacja.Serialize(zapis, ListaOsob);
                    zapis.Close();
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Plik nie istnieje" + e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /*
        public static Deserializacja(List<Osoba> ListaOsob)
        {
            try
            {
                XmlSerializer deserializacja = new XmlSerializer(typeof(List<Osoba>));
                FileStream odczyt = new FileStream(plik, FileMode.Open);
                List<Osoba> os = (List<Osoba>)deserializacja.Deserialize(odczyt);
                return os;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Plik nie istnieje" + e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        */
    }
}
