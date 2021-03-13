using System;
using System.Collections.Generic;

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
        static void ApplicationRun()
        {
            bool dzialanieProgramu = true;
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
                            //var wynik = ListaOsob.Find(item => item.Imie == "Jan");
                            //Console.WriteLine(wynik);
                            Console.Clear();
                            break;
                        case "q":
                            dzialanieProgramu = false;
                            break;
                    }
                    /*
                    foreach (var _oOsoba in ListaOsob)
                        Console.WriteLine(_oOsoba);
                    */
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
