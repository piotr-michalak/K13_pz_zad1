using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;

namespace Projekt1
{
    class Program
    {
        public static List<Osoba> ListaOsob = new List<Osoba>();

        private static void Opcje()
        {
            Console.WriteLine("Wybierz jedna z opcji:");
            Console.WriteLine("1 - Stworz nowego uzytkownika");
            Console.WriteLine("2 - Edytuj uzytkownika");
            Console.WriteLine("3 - Usun uzytkownika");
            Console.WriteLine("4 - Wyswietl danego uzytkownika");
            Console.WriteLine("0 - Zakoncz dzialanie programu");
        }
        private static void OpcjeWyszukiwania()
        {
            Console.WriteLine("1 - Imie");
            Console.WriteLine("2 - Nazwisko");
            Console.WriteLine("3 - Plec (K/M)");
            Console.WriteLine("4 - Kod pocztowy (xx-xxx)");
            Console.WriteLine("5 - Miasto");
            Console.WriteLine("6 - Ulica");
            Console.WriteLine("7 - Nr domu");
            Console.WriteLine("8 - Nr mieszkania");
        }
        public static string DodajKodPocztowy()
        {
            while (true)
            {
                string sWzorRegEx = @"^\d{2}-\d{3}";
                string kodPocztowy = ToolboxClass.WprowadzTekst("Podaj kod pocztowy (format xx-xxx):", false);
                if (Regex.IsMatch(kodPocztowy, sWzorRegEx))
                    return kodPocztowy;
                Console.WriteLine("Niepoprawny format pola <KodPocztowy> lub bledne dane");
            }
        }
        private static void StworzUzytkownika()
        {
            ListaOsob.Add(new Osoba
            {
                Wiek = ToolboxClass.WprowadzLiczbeZZakresu(0, 150, "Podaj wiek w przedziale"),
                Imie = ToolboxClass.WprowadzTekst("Podaj imie:", false),
                Nazwisko = ToolboxClass.WprowadzTekst("Podaj nazwisko:", false),
                Plec = ToolboxClass.WprowadzTekst("Podaj plec (K/M):", false),
                Adres = new Adres(DodajKodPocztowy(),
                ToolboxClass.WprowadzTekst("Podaj miasto:", false),
                ToolboxClass.WprowadzTekst("Podaj ulice:", false),
                ToolboxClass.WprowadzLiczbeZZakresu(0, 150, "Podaj nr domu w przedziale"),
                ToolboxClass.WprowadzLiczbeZZakresu(0, 150, "(Opcjonalne) Podaj nr mieszkania w przedziale"))
            });
            ToolboxClass.Serializacja(ListaOsob);
        }

        private static List<Osoba> WyszukajUzytkownika(string wzor, int wybor)
        {
            List<Osoba> wynikiWyszukiwania = new List<Osoba>();
            if (wzor == "*")
            {
                foreach (var osoba in ListaOsob)
                    wynikiWyszukiwania.Add(osoba);
            }
            else
            {
                switch (wybor)
                {
                    case 1:
                        wynikiWyszukiwania = ListaOsob.FindAll(osoba => osoba.Imie.Contains(wzor));
                        break;
                    case 2:
                        wynikiWyszukiwania = ListaOsob.FindAll(osoba => osoba.Nazwisko.Contains(wzor));
                        break;
                    case 3:
                        wynikiWyszukiwania = ListaOsob.FindAll(osoba => osoba.Plec.Contains(wzor));
                        break;
                    case 4:
                        wynikiWyszukiwania = ListaOsob.FindAll(osoba => osoba.Adres.KodPocztowy.Contains(wzor));
                        break;
                    case 5:
                        wynikiWyszukiwania = ListaOsob.FindAll(osoba => osoba.Adres.Miasto.Contains(wzor));
                        break;
                    case 6:
                        wynikiWyszukiwania = ListaOsob.FindAll(osoba => osoba.Adres.Ulica.Contains(wzor));
                        break;
                    case 7:
                        wynikiWyszukiwania = ListaOsob.FindAll(osoba => osoba.Adres.NrDomu.Equals(wzor));
                        break;
                    case 8:
                        wynikiWyszukiwania = ListaOsob.FindAll(osoba => osoba.Adres.NrMieszkania.Equals(wzor));
                        break;
                }
            }
            foreach (var osoba in wynikiWyszukiwania)
                Console.WriteLine(osoba);
            return wynikiWyszukiwania;
        }
        static void ApplicationRun()
        {
            bool dzialanieProgramu = true;
            ListaOsob = ToolboxClass.Deserializacja(ListaOsob);
            List<Osoba> wynikiWyszukiwania = new List<Osoba>();
            while (dzialanieProgramu)
            {
                try
                {
                    int wybor = 0;
                    int wyborRekordu = 0;
                    int wyborWyszukiwania = 0;
                    string wzor = "";
                    Opcje();
                    int.TryParse(Console.ReadLine(), out wybor);
                    switch (wybor)
                    {
                        case 1:
                            StworzUzytkownika();
                            Console.Clear();
                            break;
                        case 2:
                            OpcjeWyszukiwania();
                            int.TryParse(Console.ReadLine(), out wyborWyszukiwania);

                            Console.WriteLine("Podaj wzor wyszukiwania (* - wyswietl wszystkich uzytkownikow):");
                            wzor = Console.ReadLine();

                            Console.Clear();

                            wynikiWyszukiwania = WyszukajUzytkownika(wzor, wyborWyszukiwania);
                            Console.WriteLine("Znalezione rekordy:");

                            for (int index = 0; index < wynikiWyszukiwania.Count(); index++)
                            {
                                Console.WriteLine($"{index + 1} - {wynikiWyszukiwania[index]}");
                            }

                            while (true)
                            {
                                Console.WriteLine("Ktory rekord chcesz edytowac?");
                                int.TryParse(Console.ReadLine(), out wyborRekordu);
                                if (wyborRekordu <= wynikiWyszukiwania.Count() && wyborRekordu != 0)
                                {
                                    OpcjeWyszukiwania();
                                    Console.WriteLine("Ktore pole chcesz edytowac?");
                                    int.TryParse(Console.ReadLine(), out wybor);
                                    for (int index = 0; index < ListaOsob.Count(); index++)
                                    {
                                        if (ListaOsob[index].Equals(wynikiWyszukiwania[wyborRekordu - 1]))
                                        {
                                            switch (wybor)
                                            {
                                                case 1:
                                                    ListaOsob[index].Imie = ToolboxClass.WprowadzTekst("Podaj imie:");
                                                    break;
                                                case 2:
                                                    ListaOsob[index].Nazwisko = ToolboxClass.WprowadzTekst("Podaj nazwisko:");
                                                    break;
                                                case 3:
                                                    ListaOsob[index].Wiek = ToolboxClass.WprowadzLiczbeZZakresu(0, 150, "Podaj wiek w przedziale");
                                                    break;
                                                case 4:
                                                    ListaOsob[index].Plec = ToolboxClass.WprowadzTekst("Podaj plec (K/M):");
                                                    break;
                                                case 5:
                                                    ListaOsob[index].Adres.KodPocztowy = DodajKodPocztowy();
                                                    break;
                                                case 6:
                                                    ListaOsob[index].Adres.Miasto = ToolboxClass.WprowadzTekst("Podaj miasto:");
                                                    break;
                                                case 7:
                                                    ListaOsob[index].Adres.NrDomu = ToolboxClass.WprowadzLiczbeZZakresu(0, 150, "Podaj nr domu w przedziale");
                                                    break;
                                                case 8:
                                                    ListaOsob[index].Adres.NrMieszkania = ToolboxClass.WprowadzLiczbeZZakresu(0, 150, "(Opcjonalne) Podaj nr mieszkania w przedziale");
                                                    break;
                                                default:
                                                    Console.WriteLine("Niepoprawny argument");
                                                    Console.ReadKey();
                                                    Console.Clear();
                                                    break;
                                            }
                                        }
                                    }
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Wybrano niepoprawny rekord");
                                }
                            }
                            foreach (var osoba in ListaOsob)
                                Console.WriteLine(osoba);
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 3:
                            OpcjeWyszukiwania();
                            int.TryParse(Console.ReadLine(), out wyborWyszukiwania);

                            Console.WriteLine("Podaj wzor wyszukiwania (* - wyswietl wszystkich uzytkownikow):");
                            wzor = Console.ReadLine();

                            Console.Clear();

                            wynikiWyszukiwania = WyszukajUzytkownika(wzor, wyborWyszukiwania);
                            Console.WriteLine("Znalezione rekordy:");

                            for (int index = 0; index < wynikiWyszukiwania.Count(); index++)
                            {
                                Console.WriteLine($"{index + 1} - {wynikiWyszukiwania[index]}");
                            }

                            while (true)
                            {
                                Console.WriteLine("Ktory rekord chcesz usunac?");
                                int.TryParse(Console.ReadLine(), out wyborRekordu);
                                if (wyborRekordu <= wynikiWyszukiwania.Count())
                                {
                                    for (int index = 0; index < ListaOsob.Count(); index++)
                                    {
                                        if (ListaOsob[index].Equals(wynikiWyszukiwania[wyborRekordu - 1]))
                                        {
                                            ListaOsob.RemoveAt(index);
                                        }
                                    }
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Wybrano niepoprawny rekord");
                                }
                            }
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 4:
                            OpcjeWyszukiwania();
                            Console.WriteLine("Wedlug ktorego pola chcesz wyszukac uzytkownika?");
                            int.TryParse(Console.ReadLine(), out wyborWyszukiwania);

                            Console.WriteLine("Podaj wzor wyszukiwania (* - wyswietl wszystkich uzytkownikow):");
                            wzor = Console.ReadLine();

                            WyszukajUzytkownika(wzor, wyborWyszukiwania);

                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 0:
                            dzialanieProgramu = false;
                            break;
                        default:
                            Console.WriteLine("Niepoprawny argument");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }
                    ToolboxClass.Serializacja(ListaOsob);
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
