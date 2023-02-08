using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;


namespace PROJEKT
{
    class Program
    {
        struct Knjiga
        {
            public string Naslov;
            public string Autor;
            public int BrojPrimjeraka;
            public int BrojPosudbi;
        }

        struct Korisnik
        {
            public string Ime;
            public string Prezime;
            public int Dob;
        }

        static void Main(string[] args)
        {
            string glavniIzbornik;
            List<Knjiga> knjige = new List<Knjiga>();
            List<Korisnik> korisnici = new List<Korisnik>();

            CitanjeBazePodatakaKorisnika(korisnici);
            CitanjeBazePodatakaKnjiga(knjige);

            while (true)
            {
                Console.Clear();

                Console.WriteLine("Glavni izbornik");
                Console.WriteLine("1. Izbornik Korisnik");
                Console.WriteLine("2. Izbornik Knjige");
                Console.WriteLine("3. Izlaz iz programa");

                glavniIzbornik = Console.ReadLine();

                switch (glavniIzbornik)
                {
                    case "1":
                        IzbornikKorisnik(korisnici);
                        break;
                    case "2":
                        IzbornikKnjige(knjige);
                        break;
                    case "3":
                        PisanjeUBazuPodatakaKorisnika(korisnici);
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\n====> Nepravilan odabir");
                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                }
            }
        }


        // METODE
        // metoda za podizbornik korisnik
        static void IzbornikKorisnik(List<Korisnik> listaKorisnika)
        {
            string izbornikKorisnik = "0";

            while (izbornikKorisnik != "4")
            {
                Console.Clear();
                Console.WriteLine("1. Dodajte novog korisnika");
                Console.WriteLine("2. Izmjenite podatke o korisniku");
                Console.WriteLine("3. Uklonite korisnika");
                Console.WriteLine("4. Povratak u glavni izbornik");

                izbornikKorisnik = Console.ReadLine();

                switch (izbornikKorisnik)
                {
                    case "1":
                        DodavanjeNovogKorisnika(listaKorisnika);
                        PisanjeUBazuPodatakaKorisnika(listaKorisnika);
                        break;
                    case "2":
                        PromjeniNazivKorisnika(listaKorisnika);
                        PisanjeUBazuPodatakaKorisnika(listaKorisnika);
                        break;
                    case "3":
                        UklanjanjeKorisnika(listaKorisnika);
                        PisanjeUBazuPodatakaKorisnika(listaKorisnika);
                        break;
                    case "4":
                        // povratak u glavni izbornik
                        break;
                    default:
                        Console.WriteLine("\n====> Nepravilan odabir");
                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                }
            }
        }


        // metoda za čitanje iz baze podataka korisnika
        static void CitanjeBazePodatakaKorisnika(List<Korisnik> listaKorisnika)
        {
            using (StreamReader sr = new StreamReader("../../../../Baza_podataka_Korisnik.txt"))
            {
                string line;
                int brojac = 0;
                line = sr.ReadLine();
                while (line != null)
                {
                    int brojElemenataBaze = 3;
                    string[] element = new string[brojElemenataBaze];
                    int polozajIme = 0;
                    int polozajPrezime = 1;
                    int polozajGodina = 2;

                    element = line.Split(';');

                    listaKorisnika.Add(new Korisnik());
                    var temp = listaKorisnika[brojac];
                    temp.Ime = element[polozajIme];
                    temp.Prezime = element[polozajPrezime];
                    temp.Dob = Convert.ToInt32(element[polozajGodina]);
                    listaKorisnika[brojac] = temp;

                    brojac = brojac + 1;
                    line = sr.ReadLine();
                }
            }
        }


        // metoda za spremanje u bazu podataka korisnika
        static void PisanjeUBazuPodatakaKorisnika(List<Korisnik> listaKorisnika)
        {
            using (StreamWriter sw = new StreamWriter("../../../../Baza_podataka_Korisnik.txt"))
            {
                for (int i = 0; i < listaKorisnika.Count; i++)
                {
                    sw.WriteLine(listaKorisnika[i].Ime + ";" + listaKorisnika[i].Prezime + ";" + listaKorisnika[i].Dob);
                }
            }
        }


        // metoda za dodavanje novog korisnika
        static void DodavanjeNovogKorisnika(List<Korisnik> listaKorisnika)
        {
            Console.Clear();

            listaKorisnika.Add(new Korisnik());
            var temp = listaKorisnika[listaKorisnika.Count - 1];
            Console.WriteLine();

            Console.WriteLine("Upišite ime novog korisnika: ");
            temp.Ime = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Upišite prezime novog korisnika: ");
            temp.Prezime = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Upišite godinu rođenja novog korisnika: ");
            temp.Dob = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            listaKorisnika[listaKorisnika.Count - 1] = temp;
        }


        // metoda za ukloniti korisnika, (indeks arraya kojeg korisnika uklonit)
        static void UklanjanjeKorisnika(List<Korisnik> listaKorisnika)
        {
            string postojeciKorisnik;
            int redniBroj, indeks;

            Console.Clear();

            Console.WriteLine("Upišite nekoliko slova imena ili prezimena korisnika kojeg želite ukloniti");
            postojeciKorisnik = Console.ReadLine();
            Console.WriteLine();

            for (int i = 0; i < listaKorisnika.Count; i++)
            {
                if ((listaKorisnika[i].Ime.Contains(postojeciKorisnik, StringComparison.OrdinalIgnoreCase)) ||
                    (listaKorisnika[i].Prezime.Contains(postojeciKorisnik, StringComparison.OrdinalIgnoreCase)))
                {
                    redniBroj = i + 1;
                    Console.WriteLine(redniBroj + ") " + listaKorisnika[i].Ime + " " + listaKorisnika[i].Prezime);
                }
            }

            Console.WriteLine("Unesite redni broj korisnika kojeg želite ukloniti");
            indeks = Convert.ToInt32(Console.ReadLine()) - 1;

            listaKorisnika.RemoveAt(indeks);
        }


        // metoda za izmjenu podataka o korisniku
        static void PromjeniNazivKorisnika(List<Korisnik> listaKorisnika)
        {
            string promjenaPodataka;
            int redniBroj, indeks;

            Console.Clear();

            Console.WriteLine("Upišite nekoliko slova imena ili prezimena korisnika kojeg želite promijeniti");
            promjenaPodataka = Console.ReadLine();
            Console.WriteLine();

            for (int i = 0; i < listaKorisnika.Count; i++)
            {
                if ((listaKorisnika[i].Ime.Contains(promjenaPodataka, StringComparison.OrdinalIgnoreCase)) ||
                    (listaKorisnika[i].Prezime.Contains(promjenaPodataka, StringComparison.OrdinalIgnoreCase)))
                {
                    redniBroj = i + 1;
                    Console.WriteLine(redniBroj + ") " + listaKorisnika[i].Ime + " " + listaKorisnika[i].Prezime);
                }
            }

            Console.WriteLine("Unesite indeks korisnika kojeg želite promijeniti");
            indeks = Convert.ToInt32(Console.ReadLine()) - 1;
            Console.WriteLine();

            if ((indeks >= 0) && (indeks < listaKorisnika.Count))
            {
                var temp = listaKorisnika[listaKorisnika.Count - 1];

                Console.WriteLine("Unesite ime korisnika:");
                temp.Ime = Console.ReadLine();
                Console.WriteLine("Unesite prezime korisnika:");
                temp.Prezime = Console.ReadLine();
                Console.WriteLine("Unesite godinu rođenja korisnika:");
                temp.Dob = Convert.ToInt32(Console.ReadLine());

                listaKorisnika[indeks] = temp;

                PisanjeUBazuPodatakaKorisnika(listaKorisnika);

            }
        }


        // metoda za podizbornik knjige
        static void IzbornikKnjige(List<Knjiga> listaKnjiga)
        {

            string izbornikKnjige = "0";

            while (izbornikKnjige != "9")
            {
                Console.Clear();
                Console.WriteLine("1. Dodajte novu knjigu");
                Console.WriteLine("2. Izmjenite podatke o knjizi");
                Console.WriteLine("3. Izbrišite knjigu");
                Console.WriteLine("4. Posuđivanje knjige");
                Console.WriteLine("5. Posuđivanje knjige");
                Console.WriteLine("6. Ispis 5 najviše posuđivanih knjiga");
                Console.WriteLine("7. Ispis 5 najmanje posuđivanih knjiga");
                Console.WriteLine("8. Ispis prosječnog broja posudbi jedne knjige");
                Console.WriteLine("9. Povratak u glavni izbornik");

                izbornikKnjige = Console.ReadLine();

                switch (izbornikKnjige)
                {
                    case "1":
                        DodajNovuKnjigu(listaKnjiga);
                        break;
                    case "2":
                        PromjeniNazivKnjige(listaKnjiga);
                        break;
                    case "3":
                        UklanjanjeKnjige(listaKnjiga);
                        break;
                    case "4":
                        PosudivanjeKnjige(listaKnjiga);
                        break;
                    case "5":
                        VracanjeKnjige(listaKnjiga);
                        break;
                    case "6":
                        NajvisePosudivanaKnjiga(listaKnjiga);
                        break;
                    case "7":
                        NajmanjePosudivanaKnjiga(listaKnjiga);
                        break;
                    case "8":
                        ProsjekPosudbiKnjiga(listaKnjiga);
                        break;
                    case "9":
                        // Izlazak iz glavnog izbornika
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("====> Pogrešan odabir");
                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                }
            }
        }


        // metoda za citanje knjiga iz baze podataka
        static void CitanjeBazePodatakaKnjiga(List<Knjiga> listaKnjiga)
        {
            using (StreamReader sr = new StreamReader("../../../../Baza_podataka_Knjiga.txt"))
            {
                string line;
                int brojac = 0;
                line = sr.ReadLine();
                while (line != null)
                {
                    int brojElemenataBaze = 3;
                    string[] element = new string[brojElemenataBaze];
                    int polozajNaslov = 0;
                    int polozajAutor = 1;
                    int polozajBrojPrimjeraka = 2;
                    int polozajBrojPosudbi = 3;

                    element = line.Split(';');

                    listaKnjiga.Add(new Knjiga());
                    var temp = listaKnjiga[brojac];
                    temp.Naslov = element[polozajNaslov];
                    temp.Autor = element[polozajAutor];
                    temp.BrojPrimjeraka = Convert.ToInt32(element[polozajBrojPrimjeraka]);
                    temp.BrojPosudbi = Convert.ToInt32(element[polozajBrojPosudbi]);
                    listaKnjiga[brojac] = temp;

                    brojac = brojac + 1;
                    line = sr.ReadLine();
                }
            }
        }


        // metoda za pisanje u bazu podataka knjiga
        static void PisanjeUBazuPodatakaKnjiga(List<Knjiga> listaKnjiga)
        {
            using (StreamWriter sw = new StreamWriter("../../../../Baza_podataka_Knjiga.txt"))
            {
                for (int i = 0; i < listaKnjiga.Count; i++)
                {
                    sw.WriteLine(listaKnjiga[i].Naslov + ";" + listaKnjiga[i].Autor + ";" + listaKnjiga[i].BrojPrimjeraka +
                        ";" + listaKnjiga[i].BrojPosudbi);
                }
            }
        }


        // metoda za dodavanje nove knjige u bazu
        static void DodajNovuKnjigu(List<Knjiga> listaKnjiga)
        {
            Console.Clear();

            listaKnjiga.Add(new Knjiga());
            var temp = listaKnjiga[listaKnjiga.Count - 1];

            Console.WriteLine("Upišite naslov nove knjige: ");
            temp.Naslov = Console.ReadLine();
            Console.WriteLine("Upišite autora nove knjige: ");
            temp.Autor = Console.ReadLine();
            Console.WriteLine("Upišite broj primjeraka nove knjige: ");
            temp.BrojPrimjeraka = Convert.ToInt32(Console.ReadLine());
            temp.BrojPosudbi = 0;

            listaKnjiga[listaKnjiga.Count - 1] = temp;

            PisanjeUBazuPodatakaKnjiga(listaKnjiga);
        }


        // remove metoda za knjige
        static void UklanjanjeKnjige(List<Knjiga> listaKnjiga)
        {
            string slovoNaslova;
            int redniBroj, indeks;

            Console.Clear();

            Console.WriteLine("Unesite nekoliko slova iz naziva knjige koju želite izbrisati");
            slovoNaslova = Console.ReadLine();

            for (int i = 0; i < listaKnjiga.Count; i++)   // s for petljom prolazimo kroz bazu podataka
            {
                if (listaKnjiga[i].Naslov.Contains(slovoNaslova, StringComparison.OrdinalIgnoreCase))
                {
                    redniBroj = i + 1;
                    Console.WriteLine(redniBroj + ") " + listaKnjiga[i].Naslov);
                }
            }

            Console.WriteLine("Unesite redni broj knjige koju želite izbrisati");
            indeks = Convert.ToInt32(Console.ReadLine()) - 1;

            listaKnjiga.RemoveAt(indeks);

            PisanjeUBazuPodatakaKnjiga(listaKnjiga);
        }


        // metoda za editiranje knjige
        static void PromjeniNazivKnjige(List<Knjiga> listaKnjiga)
        {
            string promjenaImena;
            int redniBroj, indeks;

            Console.Clear();

            Console.WriteLine("Upišite nekoliko slova naslova knjige koju želite editirati");
            promjenaImena = Console.ReadLine();

            for (int i = 0; i < listaKnjiga.Count; i++)
            {
                if (listaKnjiga[i].Naslov.Contains(promjenaImena, StringComparison.OrdinalIgnoreCase))
                {
                    redniBroj = i + 1;
                    Console.WriteLine(redniBroj + ") " + listaKnjiga[i].Naslov);
                }
            }

            Console.WriteLine("Unesite indeks knjige koju želite editirati");
            indeks = Convert.ToInt32(Console.ReadLine()) - 1;

            if ((indeks >= 0) && (indeks < listaKnjiga.Count))
            {
                var temp = listaKnjiga[listaKnjiga.Count - 1];

                Console.WriteLine("Unesite novi naslov knjige: ");
                temp.Naslov = Console.ReadLine();
                Console.WriteLine("Unesite novog autora knjige: ");
                temp.Autor = Console.ReadLine();
                Console.WriteLine("Unesite broj primjeraka knjige: ");
                temp.BrojPrimjeraka = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Unesite broj posudbi knjige: ");
                temp.BrojPosudbi = Convert.ToInt32(Console.ReadLine());

                listaKnjiga[indeks] = temp;

                PisanjeUBazuPodatakaKnjiga(listaKnjiga);
            }
        }


        // metoda za posudivanje knjige
        static void PosudivanjeKnjige(List<Knjiga> listaKnjiga)
        {
            string slovoNaslova;
            int redniBroj, indeks;

            Console.Clear();

            Console.WriteLine("Unesite nekoliko slova iz naziva knjige koju želite posuditi");
            slovoNaslova = Console.ReadLine();

            for (int i = 0; i < listaKnjiga.Count; i++)   // s for petljom prolazimo kroz bazu podataka
            {
                if (listaKnjiga[i].Naslov.Contains(slovoNaslova, StringComparison.OrdinalIgnoreCase))
                {
                    redniBroj = i + 1;
                    Console.WriteLine(redniBroj + ") " + listaKnjiga[i].Naslov);
                }
            }

            Console.WriteLine("Unesite indeks knjige koju želite posuditi");
            indeks = Convert.ToInt32(Console.ReadLine()) - 1;

            if ((indeks >= 0) && (indeks < listaKnjiga.Count))   // osiguranje da korisnik ne unese nepostojeci indeks i skrsi program
            {
                var temp = listaKnjiga[indeks];

                if (temp.BrojPrimjeraka == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Trenutno nema dostupan niti jedan primjerak te knjige");
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    temp.BrojPrimjeraka--;
                    temp.BrojPosudbi++;

                    listaKnjiga[indeks] = temp;

                    PisanjeUBazuPodatakaKnjiga(listaKnjiga);
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Unijeli ste nepostojeci indeks");
                Console.WriteLine();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }


        // metoda za vracanje knjige
        static void VracanjeKnjige(List<Knjiga> listaKnjiga)
        {
            string slovoNaslova;
            int redniBroj, indeks;

            Console.Clear();

            Console.WriteLine("Unesite nekoliko slova iz naziva knjige koju želite vratit");
            slovoNaslova = Console.ReadLine();

            for (int i = 0; i < listaKnjiga.Count; i++)   // s for petljom prolazimo kroz bazu podataka
            {
                if (listaKnjiga[i].Naslov.Contains(slovoNaslova, StringComparison.OrdinalIgnoreCase))
                {
                    redniBroj = i + 1;
                    Console.WriteLine(redniBroj + ") " + listaKnjiga[i].Naslov);
                }
            }

            Console.WriteLine("Unesite indeks knjige koju želite vratiti");
            indeks = Convert.ToInt32(Console.ReadLine()) - 1;

            if ((indeks >= 0) && (indeks < listaKnjiga.Count))   // osiguranje da korisnik ne unese nepostojeci indeks i skrsi program
            {
                var temp = listaKnjiga[indeks];

                temp.BrojPrimjeraka++;

                listaKnjiga[indeks] = temp;

                PisanjeUBazuPodatakaKnjiga(listaKnjiga);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Unijeli ste nepostojeci indeks");
                Console.WriteLine();
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
        }


        // metoda za prosjecni broj posudbi knjiga
        static void ProsjekPosudbiKnjiga(List<Knjiga> listaKnjiga)
        {
            int sumaKnjiga = 0;

            Console.Clear();

            for (int i = 0; i < listaKnjiga.Count; i++) 
            {
                sumaKnjiga += listaKnjiga[i].BrojPosudbi;
            }

            int rezultat = sumaKnjiga / listaKnjiga.Count;
            Console.WriteLine("Prosječni broj posudbi jedne knjige je: {0}", rezultat);

            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        } 


        // metoda za najmanje posudivane knjige
        static void NajmanjePosudivanaKnjiga(List<Knjiga> listaKnjiga)
        {
            List<Knjiga> privremenaLista = new List<Knjiga>();
            Knjiga temp;

            Console.Clear();

            privremenaLista = listaKnjiga;

            for (int i=0; i < privremenaLista.Count; i++)
            {
                for (int j = i + 1; j < privremenaLista.Count; j++) // 
                {
                    if (privremenaLista[i].BrojPosudbi > privremenaLista[j].BrojPosudbi)  // ako je prvi veci od drugog radi zamjenu
                    {
                        temp = privremenaLista[i];
                        privremenaLista[i] = privremenaLista[j];
                        privremenaLista[j] = temp;
                    }
                }
            }

            Console.WriteLine("Pet najmanje posuđivanih knjiga su: ");
            Console.WriteLine(privremenaLista[0].Naslov + ": " + privremenaLista[0].BrojPosudbi);
            Console.WriteLine(privremenaLista[1].Naslov + ": " + privremenaLista[1].BrojPosudbi);
            Console.WriteLine(privremenaLista[2].Naslov + ": " + privremenaLista[2].BrojPosudbi);
            Console.WriteLine(privremenaLista[3].Naslov + ": " + privremenaLista[3].BrojPosudbi);
            Console.WriteLine(privremenaLista[4].Naslov + ": " + privremenaLista[4].BrojPosudbi);

            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }


        // metoda za najcesce posudivane knjige
        static void NajvisePosudivanaKnjiga(List<Knjiga> listaKnjiga)
        {
            List<Knjiga> privremenaLista = new List<Knjiga>();
            Knjiga temp;

            Console.Clear();

            privremenaLista = listaKnjiga;

            for (int i = 0; i < privremenaLista.Count; i++)
            {
                for (int j = i + 1; j < privremenaLista.Count; j++)
                {
                    if (privremenaLista[i].BrojPosudbi < privremenaLista[j].BrojPosudbi)
                    {
                        temp = privremenaLista[i];
                        privremenaLista[i] = privremenaLista[j];
                        privremenaLista[j] = temp;
                    }
                }
            }

            Console.WriteLine("Pet najviše posuđivanih knjiga su: ");
            Console.WriteLine(privremenaLista[0].Naslov + ": " + privremenaLista[0].BrojPosudbi);
            Console.WriteLine(privremenaLista[1].Naslov + ": " + privremenaLista[1].BrojPosudbi);
            Console.WriteLine(privremenaLista[2].Naslov + ": " + privremenaLista[2].BrojPosudbi);
            Console.WriteLine(privremenaLista[3].Naslov + ": " + privremenaLista[3].BrojPosudbi);
            Console.WriteLine(privremenaLista[4].Naslov + ": " + privremenaLista[4].BrojPosudbi);

            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
