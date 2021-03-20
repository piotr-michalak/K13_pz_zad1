using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Projekt1
{
    public class ToolboxClass
    {
        //zajęcia 28.02.2021
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

        //zajęcia 28.02.2021
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

        static readonly string plik = "dane.json";
        public static void Serializacja(List<Osoba> ListaOsob)
        {
            try
            {
                string json = JsonSerializer.Serialize(ListaOsob);
                File.WriteAllText(plik, json);
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
        
        public static List<Osoba> Deserializacja(List<Osoba> lista)
        {
            try
            {
                string jsonPlik = File.ReadAllText(plik);
                lista = JsonSerializer.Deserialize<List<Osoba>>(jsonPlik);
                return lista;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Plik nie istnieje" + e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return lista;
        }
        
    }
}
