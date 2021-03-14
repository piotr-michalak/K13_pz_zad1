using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Projekt1
{
    class Program
    {
        public static List<Osoba> ListaOsob = new List<Osoba>();

        static Adres PrzypiszAdres(Adres adres)
        {
            adres = new Adres
            {
                KodPocztowy = ToolboxClass.WprowadzTekst("Podaj kod pocztowy (format xx-xxx):", false),
                Miasto = ToolboxClass.WprowadzTekst("Podaj miasto:", false),
                Ulica = ToolboxClass.WprowadzTekst("Podaj ulice:", false),
                NrDomu = ToolboxClass.WprowadzLiczbeZZakresu(0, 150),
                NrMieszkania = ToolboxClass.WprowadzLiczbeZZakresu(0, 150)
            };
            return adres;
        }
        private static void Opcje()
        {
            Console.WriteLine("Wybierz jedna z opcji:");
            Console.WriteLine("1 - Stworz nowego uzytkownika");
            Console.WriteLine("2 - Edytuj uzytkownika");
            Console.WriteLine("3 - Usun uzytkownika");
            Console.WriteLine("4 - Wyswietl danego uzytkownika");
            Console.WriteLine("q - Zakoncz dzialanie programu");
        }
        private static void StworzUzytkownika()
        {
            Adres adres = new Adres();
            ListaOsob.Add(new Osoba
            {
                Wiek = ToolboxClass.WprowadzLiczbeZZakresu(0, 150),
                Imie = ToolboxClass.WprowadzTekst("Podaj imie:", false),
                Nazwisko = ToolboxClass.WprowadzTekst("Podaj nazwisko:", false),
                Plec = ToolboxClass.WprowadzTekst("Podaj plec (K/M):", false),
                Adres = PrzypiszAdres(adres)
            });
            ToolboxClass.Serializacja(ListaOsob);
        }
        
        private static void WyswietlUzytkownika(string wzor)
        {
            if (wzor == "*")
            {
                foreach (var osoba in ListaOsob)
                    Console.WriteLine(osoba);
            }
            else
            {
                var polaKlasy = typeof(Osoba).GetProperties().Select(pole => pole.Name).ToList();
                //foreach (var pole in polaKlasy)
                //    Console.WriteLine(pole);
            }
        }
        static void ApplicationRun()
        {
            bool dzialanieProgramu = true;
            ListaOsob = ToolboxClass.Deserializacja(ListaOsob);
            while (dzialanieProgramu)
            {
                try
                {
                    Opcje();
                    string wybor = Console.ReadLine();
                    switch (wybor)
                    {
                        case "1":
                            StworzUzytkownika();
                            Console.Clear();
                            break;
                        case "2":
                            Console.Clear();
                            break;
                        case "3":
                            Console.Clear();
                            break;
                        case "4":
                            Console.Write("Podaj wzor wyszukiwania (znak * wyswietla wszystkie osoby): ");
                            string wzor = Console.ReadLine();
                            WyswietlUzytkownika(wzor);
                            Console.Clear();
                            break;
                        case "q":
                            dzialanieProgramu = false;
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        static void Main(string[] args)
        {
            ApplicationRun();
        }
    }
}
