using EasyTrainTickets.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTrainTickets.Domain.Data
{
    public class EasyTrainTicketsDBInitializer : CreateDatabaseIfNotExists<EasyTrainTicketsDbEntities>
    {
        Random rand = new Random();
        IUnitOfWorkFactory unitOfWork = new UnitOfWorkFactory();
        string seats = @"11;12;13;14;15;16;17;18;21;22;23;24;25;26;27;28;31;32;33;34;35;36;37;38;41;42;43;44;45;46;47;48;51;52;53;54;55;56;57;58;61;62;63;64;65;66;67;68;71;72;73;74;75;76;77;78;81;82;83;84;85;86;87;88;91;92;93;94;95;96;97;98;101;102;103;104;105;106;107;108";
        string expressSeats = @"11;12;13;14;15;16;21;22;23;24;25;26;31;32;33;34;35;36;41;42;43;44;45;46;51;52;53;54;55;56;61;62;63;64;65;66;71;72;73;74;75;76;81;82;83;84;85;86;91;92;93;94;95;96;101;102;103;104;105;106";
        protected override void Seed(EasyTrainTicketsDbEntities context)
        {
            User admin = new User()
            {
                Login = "Admin",
                Password = "6NgzabR+BeSAT8ps06HWgA==",
                IsAdmin = true,
            };
            context.Users.Add(admin);

            Train train = new Train()
            {
                Type = "Pośpieszny",
                PricePerKilometer = 0.2M,
            };
            context.Trains.Add(train);

            Train express = new Train()
            {
                Type = "Ekspres",
                PricePerKilometer = 0.35M,
            };
            context.Trains.Add(express);

            Discount discount1 = new Discount()
            {
                Type = "Normalny",
                Percent = 1.0
            };
            context.Discounts.Add(discount1);

            Discount discount2 = new Discount()
            {
                Type = "Student",
                Percent = 0.49
            };
            context.Discounts.Add(discount2);

            Discount discount3 = new Discount()
            {
                Type = "Uczeń",
                Percent = 0.63
            };
            context.Discounts.Add(discount3);

            Discount discount4 = new Discount()
            {
                Type = "Senior",
                Percent = 0.7
            };
            context.Discounts.Add(discount4);

            List<Route> routes = new List<Route>();
            routes.Add(new Route() { From = "Białystok", To = "Olsztyn", Distance = 270, BestTime = 210 });
            routes.Add(new Route() { From = "Białystok", To = "Suwałki", Distance = 140, BestTime = 105 });
            routes.Add(new Route() { From = "Białystok", To = "Warszawa", Distance = 184, BestTime = 125 });
            routes.Add(new Route() { From = "Bielsko-Biała", To = "Katowice", Distance = 55, BestTime = 50 });
            routes.Add(new Route() { From = "Bydgoszcz", To = "Gdańsk", Distance = 160, BestTime = 85 });
            routes.Add(new Route() { From = "Bydgoszcz", To = "Piła", Distance = 87, BestTime = 70 });
            routes.Add(new Route() { From = "Bydgoszcz", To = "Poznań", Distance = 152, BestTime = 95 });
            routes.Add(new Route() { From = "Bydgoszcz", To = "Toruń", Distance = 51, BestTime = 40 });
            routes.Add(new Route() { From = "Częstochowa", To = "Katowice", Distance = 89, BestTime = 70 });
            routes.Add(new Route() { From = "Częstochowa", To = "Kraków", Distance = 147, BestTime = 95 });
            routes.Add(new Route() { From = "Częstochowa", To = "Opole", Distance = 93, BestTime = 60 });
            routes.Add(new Route() { From = "Częstochowa", To = "Piotrków Tryb.", Distance = 86, BestTime = 50 });
            routes.Add(new Route() { From = "Częstochowa", To = "Warszawa", Distance = 230, BestTime = 125 });
            routes.Add(new Route() { From = "Gdańsk", To = "Bydgoszcz", Distance = 160, BestTime = 85 });
            routes.Add(new Route() { From = "Gdańsk", To = "Olsztyn", Distance = 176, BestTime = 140 });
            routes.Add(new Route() { From = "Gdańsk", To = "Słupsk", Distance = 131, BestTime = 115 });
            routes.Add(new Route() { From = "Gdańsk", To = "Warszawa", Distance = 328, BestTime = 160 });
            routes.Add(new Route() { From = "Jelenia Góra", To = "Wrocław", Distance = 127, BestTime = 115 });
            routes.Add(new Route() { From = "Katowice", To = "Bielsko-Biała", Distance = 55, BestTime = 50 });
            routes.Add(new Route() { From = "Katowice", To = "Częstochowa", Distance = 89, BestTime = 70 });
            routes.Add(new Route() { From = "Katowice", To = "Kraków", Distance = 77, BestTime = 120 });
            routes.Add(new Route() { From = "Katowice", To = "Opole", Distance = 97, BestTime = 75 });
            routes.Add(new Route() { From = "Katowice", To = "Włoszczowa", Distance = 114, BestTime = 60 });
            routes.Add(new Route() { From = "Kielce", To = "Kraków", Distance = 132, BestTime = 90 });
            routes.Add(new Route() { From = "Kielce", To = "Radom", Distance = 85, BestTime = 60 });
            routes.Add(new Route() { From = "Kielce", To = "Włoszczowa", Distance = 68, BestTime = 55 });
            routes.Add(new Route() { From = "Kraków", To = "Częstochowa", Distance = 147, BestTime = 95 });
            routes.Add(new Route() { From = "Kraków", To = "Katowice", Distance = 77, BestTime = 120 });
            routes.Add(new Route() { From = "Kraków", To = "Kielce", Distance = 132, BestTime = 90 });
            routes.Add(new Route() { From = "Kraków", To = "Rzeszów", Distance = 157, BestTime = 145 });
            routes.Add(new Route() { From = "Kraków", To = "Warszawa", Distance = 293, BestTime = 140 });
            routes.Add(new Route() { From = "Kraków", To = "Włoszczowa", Distance = 109, BestTime = 60 });
            routes.Add(new Route() { From = "Kutno", To = "Łódź", Distance = 110, BestTime = 80 });
            routes.Add(new Route() { From = "Kutno", To = "Poznań", Distance = 179, BestTime = 95 });
            routes.Add(new Route() { From = "Kutno", To = "Toruń", Distance = 109, BestTime = 80 });
            routes.Add(new Route() { From = "Kutno", To = "Warszawa", Distance = 126, BestTime = 75 });
            routes.Add(new Route() { From = "Lublin", To = "Radom", Distance = 122, BestTime = 75 });
            routes.Add(new Route() { From = "Lublin", To = "Rzeszów", Distance = 203, BestTime = 150 });
            routes.Add(new Route() { From = "Lublin", To = "Warszawa", Distance = 185, BestTime = 130 });
            routes.Add(new Route() { From = "Łódź", To = "Kutno", Distance = 110, BestTime = 80 });
            routes.Add(new Route() { From = "Łódź", To = "Piotrków Tryb.", Distance = 57, BestTime = 33 });
            routes.Add(new Route() { From = "Łódź", To = "Warszawa", Distance = 126, BestTime = 70 });
            routes.Add(new Route() { From = "Łódź", To = "Wrocław", Distance = 250, BestTime = 215 });
            routes.Add(new Route() { From = "Olsztyn", To = "Białystok", Distance = 270, BestTime = 210 });
            routes.Add(new Route() { From = "Olsztyn", To = "Gdańsk", Distance = 176, BestTime = 140 });
            routes.Add(new Route() { From = "Olsztyn", To = "Warszawa", Distance = 231, BestTime = 155 });
            routes.Add(new Route() { From = "Opole", To = "Częstochowa", Distance = 93, BestTime = 60 });
            routes.Add(new Route() { From = "Opole", To = "Katowice", Distance = 97, BestTime = 75 });
            routes.Add(new Route() { From = "Opole", To = "Wrocław", Distance = 82, BestTime = 55 });
            routes.Add(new Route() { From = "Poznań", To = "Bydgoszcz", Distance = 152, BestTime = 95 });
            routes.Add(new Route() { From = "Poznań", To = "Szczecin", Distance = 213, BestTime = 140 });
            routes.Add(new Route() { From = "Poznań", To = "Kutno", Distance = 179, BestTime = 95 });
            routes.Add(new Route() { From = "Poznań", To = "Piła", Distance = 95, BestTime = 100 });
            routes.Add(new Route() { From = "Poznań", To = "Wrocław", Distance = 164, BestTime = 135 });
            routes.Add(new Route() { From = "Poznań", To = "Zielona Góra", Distance = 134, BestTime = 95 });
            routes.Add(new Route() { From = "Piła", To = "Bydgoszcz", Distance = 87, BestTime = 70 });
            routes.Add(new Route() { From = "Piła", To = "Poznań", Distance = 95, BestTime = 100 });
            routes.Add(new Route() { From = "Piła", To = "Słupsk", Distance = 226, BestTime = 180 });
            routes.Add(new Route() { From = "Piła", To = "Szczecin", Distance = 173, BestTime = 170 });
            routes.Add(new Route() { From = "Piotrków Tryb.", To = "Częstochowa", Distance = 86, BestTime = 50 });
            routes.Add(new Route() { From = "Piotrków Tryb.", To = "Łódź", Distance = 57, BestTime = 33 });
            routes.Add(new Route() { From = "Piotrków Tryb.", To = "Warszawa", Distance = 144, BestTime = 80 });
            routes.Add(new Route() { From = "Przemyśl", To = "Rzeszów", Distance = 87, BestTime = 70 });
            routes.Add(new Route() { From = "Radom", To = "Kielce", Distance = 85, BestTime = 60 });
            routes.Add(new Route() { From = "Radom", To = "Lublin", Distance = 122, BestTime = 75 });
            routes.Add(new Route() { From = "Radom", To = "Warszawa", Distance = 103, BestTime = 120 });
            routes.Add(new Route() { From = "Rzeszów", To = "Kraków", Distance = 157, BestTime = 145 });
            routes.Add(new Route() { From = "Rzeszów", To = "Lublin", Distance = 203, BestTime = 150 });
            routes.Add(new Route() { From = "Rzeszów", To = "Przemyśl", Distance = 87, BestTime = 70 });
            routes.Add(new Route() { From = "Siedlce", To = "Terespol", Distance = 117, BestTime = 75 });
            routes.Add(new Route() { From = "Siedlce", To = "Warszawa", Distance = 93, BestTime = 60 });
            routes.Add(new Route() { From = "Słupsk", To = "Gdańsk", Distance = 131, BestTime = 115 });
            routes.Add(new Route() { From = "Słupsk", To = "Piła", Distance = 226, BestTime = 180 });
            routes.Add(new Route() { From = "Słupsk", To = "Szczecin", Distance = 245, BestTime = 165 });
            routes.Add(new Route() { From = "Suwałki", To = "Białystok", Distance = 140, BestTime = 105 });
            routes.Add(new Route() { From = "Szczecin", To = "Słupsk", Distance = 245, BestTime = 165 });
            routes.Add(new Route() { From = "Szczecin", To = "Piła", Distance = 173, BestTime = 170 });
            routes.Add(new Route() { From = "Szczecin", To = "Poznań", Distance = 213, BestTime = 140 });
            routes.Add(new Route() { From = "Szczecin", To = "Zielona Góra", Distance = 202, BestTime = 165 });
            routes.Add(new Route() { From = "Terespol", To = "Siedlce", Distance = 117, BestTime = 75 });
            routes.Add(new Route() { From = "Toruń", To = "Bydgoszcz", Distance = 51, BestTime = 40 });
            routes.Add(new Route() { From = "Toruń", To = "Kutno", Distance = 109, BestTime = 80 });
            routes.Add(new Route() { From = "Warszawa", To = "Białystok", Distance = 184, BestTime = 125 });
            routes.Add(new Route() { From = "Warszawa", To = "Częstochowa", Distance = 230, BestTime = 125 });
            routes.Add(new Route() { From = "Warszawa", To = "Gdańsk", Distance = 328, BestTime = 160 });
            routes.Add(new Route() { From = "Warszawa", To = "Radom", Distance = 103, BestTime = 120 });
            routes.Add(new Route() { From = "Warszawa", To = "Kraków", Distance = 293, BestTime = 140 });
            routes.Add(new Route() { From = "Warszawa", To = "Lublin", Distance = 185, BestTime = 130 });
            routes.Add(new Route() { From = "Warszawa", To = "Łódź", Distance = 126, BestTime = 70 });
            routes.Add(new Route() { From = "Warszawa", To = "Olsztyn", Distance = 231, BestTime = 155 });
            routes.Add(new Route() { From = "Warszawa", To = "Piotrków Tryb.", Distance = 144, BestTime = 80 });
            routes.Add(new Route() { From = "Warszawa", To = "Włoszczowa", Distance = 184, BestTime = 90 });
            routes.Add(new Route() { From = "Warszawa", To = "Siedlce", Distance = 93, BestTime = 60 });
            routes.Add(new Route() { From = "Warszawa", To = "Kutno", Distance = 126, BestTime = 75 });
            routes.Add(new Route() { From = "Włoszczowa", To = "Katowice", Distance = 114, BestTime = 60 });
            routes.Add(new Route() { From = "Włoszczowa", To = "Kielce", Distance = 68, BestTime = 55 });
            routes.Add(new Route() { From = "Włoszczowa", To = "Kraków", Distance = 109, BestTime = 60 });
            routes.Add(new Route() { From = "Włoszczowa", To = "Warszawa", Distance = 184, BestTime = 90 });
            routes.Add(new Route() { From = "Wrocław", To = "Jelenia Góra", Distance = 127, BestTime = 115 });
            routes.Add(new Route() { From = "Wrocław", To = "Łódź", Distance = 250, BestTime = 215 });
            routes.Add(new Route() { From = "Wrocław", To = "Opole", Distance = 82, BestTime = 55 });
            routes.Add(new Route() { From = "Wrocław", To = "Poznań", Distance = 164, BestTime = 135 });
            routes.Add(new Route() { From = "Wrocław", To = "Zielona Góra", Distance = 154, BestTime = 135 });
            routes.Add(new Route() { From = "Zielona Góra", To = "Poznań", Distance = 134, BestTime = 95 });
            routes.Add(new Route() { From = "Zielona Góra", To = "Szczecin", Distance = 202, BestTime = 165 });
            routes.Add(new Route() { From = "Zielona Góra", To = "Wrocław", Distance = 154, BestTime = 135 });

            foreach (var route in routes)
                context.Routes.Add(route);
            context.SaveChanges();
            List<Connection> connections = new List<Connection>();
            for (int i = 1; i < 11; i++)
            {
                connections.Clear();
                connections.Add(CreateConnection(new string[] { "Białystok", "Warszawa", "Łódź", "Wrocław", "Jelenia Góra" }, new int[] { 0, 15, 10, 5, 0 }, DateTime.Now.AddDays(i), 5, 25, routes, train, "Orzeszkowa"));
                connections.Add(CreateConnection(new string[] { "Białystok", "Warszawa" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 7, 43, routes, train, "Branicki"));
                connections.Add(CreateConnection(new string[] { "Białystok", "Warszawa", "Łódź" }, new int[] { 0, 15, 0 }, DateTime.Now.AddDays(i), 6, 54, routes, train, "Zamenhof"));
                connections.Add(CreateConnection(new string[] { "Białystok", "Warszawa", "Łódź", "Wrocław" }, new int[] { 0, 10, 5, 0 }, DateTime.Now.AddDays(i), 14, 11, routes, train, "Konopnicka"));
                connections.Add(CreateConnection(new string[] { "Białystok", "Warszawa", "Piotrków Tryb.", "Częstochowa", "Katowice", "Bielsko-Biała" }, new int[] { 0, 30, 1, 1, 5, 0 }, DateTime.Now.AddDays(i), 9, 47, routes, train, "Ondraszek"));
                connections.Add(CreateConnection(new string[] { "Białystok", "Warszawa", "Łódź" }, new int[] { 0, 10, 0 }, DateTime.Now.AddDays(i), 16, 10, routes, train, "Słonimski"));
                connections.Add(CreateConnection(new string[] { "Białystok", "Warszawa", "Łódź" }, new int[] { 0, 10, 0 }, DateTime.Now.AddDays(i), 20, 12, routes, train, "Leśmian"));
                connections.Add(CreateConnection(new string[] { "Białystok", "Warszawa", "Łódź" }, new int[] { 0, 10, 0 }, DateTime.Now.AddDays(i), 11, 43, routes, train, "Nałkowska"));
                connections.Add(CreateConnection(new string[] { "Białystok", "Olsztyn", "Gdańsk", "Słupsk", "Szczecin" }, new int[] { 0, 25, 15, 5, 0 }, DateTime.Now.AddDays(i), 5, 50, routes, train, "Rybak"));
                connections.Add(CreateConnection(new string[] { "Białystok", "Olsztyn", "Gdańsk" }, new int[] { 0, 25, 0 }, DateTime.Now.AddDays(i), 13, 31, routes, train, "Biebrza"));

                connections.Add(CreateConnection(new string[] { "Bielsko-Biała", "Katowice", "Częstochowa", "Piotrków Tryb.", "Warszawa", "Białystok" }, new int[] { 0, 10, 3, 1, 10, 0 }, DateTime.Now.AddDays(i), 11, 31, routes, train, "Ondraszek"));
                connections.Add(CreateConnection(new string[] { "Bielsko-Biała", "Katowice", "Częstochowa", "Piotrków Tryb.", "Warszawa", "Olsztyn" }, new int[] { 0, 10, 4, 1, 10, 0 }, DateTime.Now.AddDays(i), 12, 54, routes, train, "Kormoran"));
                connections.Add(CreateConnection(new string[] { "Bielsko-Biała", "Katowice", "Włoszczowa", "Warszawa", "Gdańsk" }, new int[] { 0, 5, 2, 5, 0 }, DateTime.Now.AddDays(i), 15, 56, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Bielsko-Biała", "Katowice", "Włoszczowa", "Warszawa", "Gdańsk" }, new int[] { 0, 4, 1, 5, 0 }, DateTime.Now.AddDays(i), 8, 0, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Bielsko-Biała", "Katowice", "Włoszczowa", "Warszawa" }, new int[] { 0, 8, 2, 0 }, DateTime.Now.AddDays(i), 5, 15, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Bielsko-Biała", "Katowice", "Częstochowa", "Piotrków Tryb.", "Warszawa", "Lublin" }, new int[] { 0, 10, 6, 2, 15, 0 }, DateTime.Now.AddDays(i), 14, 58, routes, train, "Pilecki"));
                connections.Add(CreateConnection(new string[] { "Bielsko-Biała", "Katowice", "Częstochowa", "Piotrków Tryb.", "Warszawa" }, new int[] { 0, 10, 6, 1, 0 }, DateTime.Now.AddDays(i), 17, 7, routes, train, "Korfanty"));
                connections.Add(CreateConnection(new string[] { "Bielsko-Biała", "Katowice", "Częstochowa", "Piotrków Tryb.", "Warszawa" }, new int[] { 0, 15, 6, 2, 0 }, DateTime.Now.AddDays(i), 4, 36, routes, train, "Górnik"));

                connections.Add(CreateConnection(new string[] { "Bydgoszcz", "Toruń", "Kutno", "Warszawa" }, new int[] { 0, 7, 3, 0 }, DateTime.Now.AddDays(i), 3, 23, routes, train, "Ogiński"));
                connections.Add(CreateConnection(new string[] { "Bydgoszcz", "Toruń", "Kutno", "Warszawa" }, new int[] { 0, 8, 4, 0 }, DateTime.Now.AddDays(i), 8, 18, routes, train, "Kujawiak"));
                connections.Add(CreateConnection(new string[] { "Bydgoszcz", "Toruń", "Kutno", "Warszawa" }, new int[] { 0, 7, 2, 0 }, DateTime.Now.AddDays(i), 16, 3, routes, train, "Kopernik"));
                connections.Add(CreateConnection(new string[] { "Bydgoszcz", "Toruń", "Kutno", "Warszawa", "Lublin", "Rzeszów" }, new int[] { 0, 7, 4, 10, 15, 0 }, DateTime.Now.AddDays(i), 5, 46, routes, train, "Staszic"));

                connections.Add(CreateConnection(new string[] { "Częstochowa", "Kraków" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 4, 59, routes, train, "Pogoria"));
                connections.Add(CreateConnection(new string[] { "Częstochowa", "Piotrków Tryb.", "Warszawa", "Lublin" }, new int[] { 0, 1, 24, 0 }, DateTime.Now.AddDays(i), 5, 10, routes, train, "Czartoryski"));

                connections.Add(CreateConnection(new string[] { "Gdańsk", "Olsztyn", "Białystok" }, new int[] { 0, 15, 0 }, DateTime.Now.AddDays(i), 8, 0, routes, train, "Biebrza"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Bydgoszcz", "Poznań", "Zielona Góra" }, new int[] { 0, 10, 10, 0 }, DateTime.Now.AddDays(i), 14, 5, routes, train, "Bachus"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Bydgoszcz", "Poznań" }, new int[] { 0, 10, 0 }, DateTime.Now.AddDays(i), 18, 36, routes, train, "Bałtyk"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Bydgoszcz", "Poznań", "Wrocław" }, new int[] { 0, 3, 3, 0 }, DateTime.Now.AddDays(i), 6, 30, routes, train, "Piast"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Bydgoszcz", "Poznań", "Wrocław" }, new int[] { 0, 3, 13, 0 }, DateTime.Now.AddDays(i), 16, 33, routes, train, "Mieszko"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 4, 50, routes, train, "Rataj"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Włoszczowa", "Katowice", "Bielsko-Biała" }, new int[] { 0, 5, 2, 4, 0 }, DateTime.Now.AddDays(i), 6, 1, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Włoszczowa", "Katowice", "Bielsko-Biała" }, new int[] { 0, 5, 1, 4, 0 }, DateTime.Now.AddDays(i), 15, 53, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Włoszczowa", "Katowice" }, new int[] { 0, 5, 2, 0 }, DateTime.Now.AddDays(i), 9, 48, routes, express, "Ex Sobieski"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Włoszczowa", "Katowice" }, new int[] { 0, 5, 2, 0 }, DateTime.Now.AddDays(i), 7, 53, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Włoszczowa", "Katowice" }, new int[] { 0, 5, 2, 0 }, DateTime.Now.AddDays(i), 13, 54, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Kraków" }, new int[] { 0, 5, 0 }, DateTime.Now.AddDays(i), 6, 54, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Częstochowa", "Opole", "Wrocław" }, new int[] { 0, 5, 2, 3, 0 }, DateTime.Now.AddDays(i), 8, 0, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Bydgoszcz", "Toruń", "Kutno", "Warszawa", "Lublin", "Rzeszów", "Przemyśl" }, new int[] { 0, 16, 5, 1, 10, 17, 26, 0 }, DateTime.Now.AddDays(i), 11, 50, routes, train, "Kochanowski"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Kraków" }, new int[] { 0, 5, 0 }, DateTime.Now.AddDays(i), 10, 54, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Kraków" }, new int[] { 0, 5, 0 }, DateTime.Now.AddDays(i), 12, 54, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Częstochowa", "Opole", "Wrocław" }, new int[] { 0, 5, 4, 2, 0 }, DateTime.Now.AddDays(i), 14, 0, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Kraków", "Rzeszów" }, new int[] { 0, 5, 10, 0 }, DateTime.Now.AddDays(i), 14, 54, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Kraków" }, new int[] { 0, 5, 0 }, DateTime.Now.AddDays(i), 16, 54, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Lublin" }, new int[] { 0, 10, 0 }, DateTime.Now.AddDays(i), 17, 45, routes, train, "Neptun"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Bydgoszcz", "Poznań", "Wrocław" }, new int[] { 0, 3, 14, 0 }, DateTime.Now.AddDays(i), 8, 18, routes, train, "Pomorzanin"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Warszawa", "Włoszczowa", "Kraków" }, new int[] { 0, 10, 2, 0 }, DateTime.Now.AddDays(i), 19, 49, routes, train, "Karpaty"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Słupsk", "Szczecin" }, new int[] { 0, 5, 0 }, DateTime.Now.AddDays(i), 5, 55, routes, train, "Albatros"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Bydgoszcz", "Poznań", "Wrocław", "Jelenia Góra" }, new int[] { 0, 10, 25, 15, 0 }, DateTime.Now.AddDays(i), 23, 10, routes, train, "Sudety"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Bydgoszcz", "Toruń", "Kutno", "Warszawa" }, new int[] { 0, 7, 2, 4, 0 }, DateTime.Now.AddDays(i), 17, 36, routes, train, "Doker"));
                connections.Add(CreateConnection(new string[] { "Gdańsk", "Bydgoszcz", "Toruń", "Kutno", "Łódź", "Piotrków Tryb.", "Częstochowa", "Katowice" }, new int[] { 0, 8, 2, 2, 2, 1, 2, 0 }, DateTime.Now.AddDays(i), 13, 30, routes, train, "Hutnik"));

                connections.Add(CreateConnection(new string[] { "Jelenia Góra", "Wrocław", "Łódź", "Warszawa", "Białystok" }, new int[] { 0, 10, 10, 15, 0 }, DateTime.Now.AddDays(i), 13, 5, routes, train, "Orzeszkowa"));
                connections.Add(CreateConnection(new string[] { "Jelenia Góra", "Wrocław", "Opole", "Częstochowa", "Warszawa" }, new int[] { 0, 10, 2, 5, 0 }, DateTime.Now.AddDays(i), 7, 9, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Jelenia Góra", "Wrocław", "Opole", "Katowice", "Kraków", "Rzeszów", "Przemyśl" }, new int[] { 0, 10, 2, 5, 12, 25, 0 }, DateTime.Now.AddDays(i), 8, 34, routes, train, "Mehoffer"));
                connections.Add(CreateConnection(new string[] { "Jelenia Góra", "Wrocław", "Łódź", "Warszawa" }, new int[] { 0, 10, 15, 0 }, DateTime.Now.AddDays(i), 16, 30, routes, train, "Asnyk"));
                connections.Add(CreateConnection(new string[] { "Jelenia Góra", "Wrocław", "Poznań", "Bydgoszcz", "Gdańsk" }, new int[] { 0, 10, 15, 12, 0 }, DateTime.Now.AddDays(i), 20, 32, routes, train, "Sudety"));

                connections.Add(CreateConnection(new string[] { "Lublin", "Warszawa", "Gdańsk" }, new int[] { 0, 10, 0 }, DateTime.Now.AddDays(i), 12, 15, routes, train, "Neptun"));
                connections.Add(CreateConnection(new string[] { "Lublin", "Warszawa", "Kutno", "Poznań", "Szczecin" }, new int[] { 0, 15, 5, 15, 0 }, DateTime.Now.AddDays(i), 5, 0, routes, train, "Gałczyński"));
                connections.Add(CreateConnection(new string[] { "Lublin", "Warszawa", "Piotrków Tryb.", "Częstochowa", "Katowice", "Bielsko-Biała" }, new int[] { 0, 10, 2, 5, 15, 0 }, DateTime.Now.AddDays(i), 5, 48, routes, train, "Pilecki"));
                connections.Add(CreateConnection(new string[] { "Lublin", "Warszawa", "Łódź", "Wrocław" }, new int[] { 0, 15, 10, 0 }, DateTime.Now.AddDays(i), 9, 50, routes, train, "Słowacki"));
                connections.Add(CreateConnection(new string[] { "Lublin", "Radom", "Kielce", "Kraków" }, new int[] { 0, 5, 6, 0 }, DateTime.Now.AddDays(i), 18, 46, routes, train, "Jagiełło"));
                connections.Add(CreateConnection(new string[] { "Lublin", "Radom", "Kielce", "Kraków" }, new int[] { 0, 2, 3, 0 }, DateTime.Now.AddDays(i), 6, 43, routes, train, "Długosz"));
                connections.Add(CreateConnection(new string[] { "Lublin", "Radom", "Kielce", "Włoszczowa", "Katowice", "Opole", "Wrocław" }, new int[] { 0, 2, 3, 4, 10, 4, 0 }, DateTime.Now.AddDays(i), 8, 20, routes, train, "Bolko"));
                connections.Add(CreateConnection(new string[] { "Lublin", "Radom", "Kielce", "Włoszczowa", "Katowice", "Opole", "Wrocław" }, new int[] { 0, 2, 3, 4, 10, 4, 0 }, DateTime.Now.AddDays(i), 12, 23, routes, train, "Sztygar"));
                connections.Add(CreateConnection(new string[] { "Lublin", "Radom", "Kielce", "Włoszczowa", "Katowice" }, new int[] { 0, 2, 3, 4, 0 }, DateTime.Now.AddDays(i), 16, 25, routes, train, "Godula"));
                connections.Add(CreateConnection(new string[] { "Lublin", "Warszawa", "Piotrków Tryb.", "Częstochowa" }, new int[] { 0, 10, 1, 0 }, DateTime.Now.AddDays(i), 17, 50, routes, train, "Czartoryski"));

                connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 20, 32, routes, train, "Wokulski"));
                connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 17, 32, routes, train, "Prząśniczka"));
                connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 15, 32, routes, train, "Tuwim"));
                connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 11, 31, routes, train, "Rzecki"));
                connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 11, 30, routes, train, "Jagna"));
                connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 6, 47, routes, train, "Łęcka"));
                connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 6, 40, routes, train, "Łodzianin"));
                connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 5, 44, routes, train, "Zosia"));
                connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 9, 32, routes, train, "Telimena"));
                connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 5, 12, routes, train, "Soplica"));
                connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa", "Białystok" }, new int[] { 0, 15, 0 }, DateTime.Now.AddDays(i), 16, 22, routes, train, "Zamenhof"));
                connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa", "Białystok" }, new int[] { 0, 15, 0 }, DateTime.Now.AddDays(i), 8, 32, routes, train, "Słonimski"));
                connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa", "Białystok" }, new int[] { 0, 15, 0 }, DateTime.Now.AddDays(i), 5, 6, routes, train, "Leśmian"));
                connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa", "Białystok" }, new int[] { 0, 15, 0 }, DateTime.Now.AddDays(i), 12, 22, routes, train, "Nałkowska"));
                connections.Add(CreateConnection(new string[] { "Łódź", "Warszawa", "Olsztyn" }, new int[] { 0, 15, 0 }, DateTime.Now.AddDays(i), 6, 29, routes, train, "Mazury"));
                connections.Add(CreateConnection(new string[] { "Łódź", "Kutno", "Poznań", "Szczecin" }, new int[] { 0, 5, 10, 0 }, DateTime.Now.AddDays(i), 6, 46, routes, train, "Włókniarz"));
                connections.Add(CreateConnection(new string[] { "Łódź", "Piotrków Tryb.", "Częstochowa", "Kraków" }, new int[] { 0, 1, 5, 0 }, DateTime.Now.AddDays(i), 6, 0, routes, train, "Reymont"));

                connections.Add(CreateConnection(new string[] { "Katowice", "Włoszczowa", "Warszawa", "Gdańsk" }, new int[] { 0, 2, 5, 0 }, DateTime.Now.AddDays(i), 12, 33, routes, express, "Ex Sobieski"));
                connections.Add(CreateConnection(new string[] { "Katowice", "Włoszczowa", "Warszawa", "Gdańsk" }, new int[] { 0, 2, 5, 0 }, DateTime.Now.AddDays(i), 6, 55, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Katowice", "Włoszczowa", "Warszawa", "Gdańsk" }, new int[] { 0, 2, 5, 0 }, DateTime.Now.AddDays(i), 14, 48, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Katowice", "Włoszczowa", "Warszawa" }, new int[] { 0, 2, 0 }, DateTime.Now.AddDays(i), 18, 34, routes, express, "Ex Polonia"));
                connections.Add(CreateConnection(new string[] { "Katowice", "Włoszczowa", "Warszawa" }, new int[] { 0, 2, 0 }, DateTime.Now.AddDays(i), 15, 33, routes, train, "Varsovia"));
                connections.Add(CreateConnection(new string[] { "Katowice", "Włoszczowa", "Warszawa" }, new int[] { 0, 2, 0 }, DateTime.Now.AddDays(i), 11, 33, routes, train, "Porta Moravica"));
                connections.Add(CreateConnection(new string[] { "Katowice", "Włoszczowa", "Warszawa" }, new int[] { 0, 2, 0 }, DateTime.Now.AddDays(i), 7, 37, routes, express, "Ex Comenius"));
                connections.Add(CreateConnection(new string[] { "Katowice", "Włoszczowa", "Warszawa" }, new int[] { 0, 2, 0 }, DateTime.Now.AddDays(i), 4, 31, routes, train, "Chopin"));
                connections.Add(CreateConnection(new string[] { "Katowice", "Częstochowa", "Piotrków Tryb.", "Warszawa" }, new int[] { 0, 5, 1, 0 }, DateTime.Now.AddDays(i), 8, 10, routes, train, "Wysocki"));
                connections.Add(CreateConnection(new string[] { "Katowice", "Opole", "Wrocław", "Poznań", "Szczecin" }, new int[] { 0, 2, 15, 5, 0 }, DateTime.Now.AddDays(i), 15, 0, routes, train, "Dobrawa"));
                connections.Add(CreateConnection(new string[] { "Katowice", "Opole", "Wrocław", "Poznań", "Szczecin" }, new int[] { 0, 4, 15, 15, 0 }, DateTime.Now.AddDays(i), 7, 30, routes, train, "Barnim"));
                connections.Add(CreateConnection(new string[] { "Katowice", "Opole", "Wrocław", "Zielona Góra", "Szczecin" }, new int[] { 0, 4, 15, 15, 0 }, DateTime.Now.AddDays(i), 8, 57, routes, train, "Światowid"));
                connections.Add(CreateConnection(new string[] { "Katowice", "Opole", "Wrocław" }, new int[] { 0, 3, 0 }, DateTime.Now.AddDays(i), 18, 20, routes, train, "Panorama"));
                connections.Add(CreateConnection(new string[] { "Katowice", "Częstochowa", "Piotrków Tryb.", "Łódź", "Kutno", "Poznań", "Szczecin" }, new int[] { 0, 5, 1, 5, 4, 10, 0 }, DateTime.Now.AddDays(i), 6, 46, routes, train, "Portowiec"));
                connections.Add(CreateConnection(new string[] { "Katowice", "Włoszczowa", "Kielce", "Radom", "Lublin" }, new int[] { 0, 2, 17, 2, 0 }, DateTime.Now.AddDays(i), 6, 45, routes, train, "Godula"));
                connections.Add(CreateConnection(new string[] { "Katowice", "Opole", "Wrocław", "Poznań", "Piła", "Słupsk" }, new int[] { 0, 2, 6, 16, 3, 0 }, DateTime.Now.AddDays(i), 5, 5, routes, train, "Gwarek"));
                connections.Add(CreateConnection(new string[] { "Katowice", "Częstochowa", "Piotrków Tryb.", "Łódź", "Kutno", "Toruń", "Bydgoszcz", "Gdańsk" }, new int[] { 0, 2, 1, 2, 2, 2, 10, 0 }, DateTime.Now.AddDays(i), 7, 0, routes, train, "Hutnik"));

                connections.Add(CreateConnection(new string[] { "Kielce", "Radom", "Warszawa" }, new int[] { 0, 2, 0 }, DateTime.Now.AddDays(i), 5, 24, routes, train, "Nida"));

                connections.Add(CreateConnection(new string[] { "Kraków", "Włoszczowa", "Warszawa", "Białystok", "Suwałki" }, new int[] { 0, 2, 15, 20, 0 }, DateTime.Now.AddDays(i), 5, 0, routes, train, "Hańcza"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Kielce", "Włoszczowa", "Warszawa", "Olsztyn" }, new int[] { 0, 3, 2, 15, 0 }, DateTime.Now.AddDays(i), 15, 22, routes, train, "Orłowicz"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Kielce", "Włoszczowa", "Warszawa", "Olsztyn" }, new int[] { 0, 3, 2, 15, 0 }, DateTime.Now.AddDays(i), 11, 32, routes, train, "Kolberg"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Kielce", "Włoszczowa", "Warszawa", "Olsztyn" }, new int[] { 0, 6, 2, 15, 0 }, DateTime.Now.AddDays(i), 7, 20, routes, train, "Żeromski"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Włoszczowa", "Warszawa" }, new int[] { 0, 2, 0 }, DateTime.Now.AddDays(i), 19, 24, routes, train, "Malinowski"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Włoszczowa", "Warszawa" }, new int[] { 0, 2, 0 }, DateTime.Now.AddDays(i), 4, 50, routes, train, "Korczak"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 16, 53, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 17, 53, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 19, 59, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 18, 43, routes, express, "Ex Norwid"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 14, 38, routes, express, "Ex Sawa"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 6, 38, routes, express, "Ex Krakus"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 12, 0, routes, express, "Ex Lwów"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Włoszczowa", "Warszawa", "Siedlce", "Terespol" }, new int[] { 0, 2, 5, 2, 0 }, DateTime.Now.AddDays(i), 5, 59, routes, train, "Wit Stwosz"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa", "Gdańsk" }, new int[] { 0, 5, 0 }, DateTime.Now.AddDays(i), 15, 53, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa", "Gdańsk", "Słupsk" }, new int[] { 0, 5, 4, 0 }, DateTime.Now.AddDays(i), 13, 53, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa", "Gdańsk" }, new int[] { 0, 5, 0 }, DateTime.Now.AddDays(i), 5, 53, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa", "Gdańsk" }, new int[] { 0, 5, 0 }, DateTime.Now.AddDays(i), 9, 53, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Warszawa", "Gdańsk" }, new int[] { 0, 5, 0 }, DateTime.Now.AddDays(i), 11, 53, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Włoszczowa", "Warszawa", "Gdańsk" }, new int[] { 0, 3, 25, 0 }, DateTime.Now.AddDays(i), 2, 35, routes, train, "Karpaty"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Kielce", "Radom", "Warszawa", "Gdańsk", "Słupsk" }, new int[] { 0, 2, 2, 5, 15, 0 }, DateTime.Now.AddDays(i), 19, 13, routes, train, "Ustronie"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Włoszczowa", "Warszawa", "Gdańsk", "Słupsk" }, new int[] { 0, 2, 5, 15, 0 }, DateTime.Now.AddDays(i), 9, 24, routes, train, "Pobrzeże"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Kielce", "Radom", "Lublin" }, new int[] { 0, 3, 2, 0 }, DateTime.Now.AddDays(i), 5, 19, routes, train, "Jagiełło"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Kielce", "Radom", "Lublin" }, new int[] { 0, 2, 3, 0 }, DateTime.Now.AddDays(i), 16, 41, routes, train, "Długosz"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Częstochowa", "Piotrków Tryb.", "Łódź", "Kutno", "Poznań", "Szczecin" }, new int[] { 0, 5, 1, 5, 4, 10, 0 }, DateTime.Now.AddDays(i), 14, 52, routes, train, "Wawel"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Częstochowa", "Piotrków Tryb.", "Łódź", "Kutno", "Poznań", "Szczecin" }, new int[] { 0, 5, 1, 5, 4, 10, 0 }, DateTime.Now.AddDays(i), 13, 27, routes, train, "Osterwa"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Częstochowa", "Piotrków Tryb.", "Łódź", "Kutno", "Poznań", "Szczecin" }, new int[] { 0, 5, 1, 5, 4, 10, 0 }, DateTime.Now.AddDays(i), 6, 52, routes, train, "Barbakan"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Katowice", "Opole", "Wrocław", "Poznań" }, new int[] { 0, 15, 5, 15, 0 }, DateTime.Now.AddDays(i), 19, 0, routes, train, "Uznam"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Częstochowa", "Piotrków Tryb.", "Łódź" }, new int[] { 0, 1, 5, 0 }, DateTime.Now.AddDays(i), 18, 29, routes, train, "Reymont"));
                connections.Add(CreateConnection(new string[] { "Kraków", "Częstochowa" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 22, 50, routes, train, "Pogoria"));

                connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 6, 35, routes, train, "Wokulski"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 10, 15, routes, train, "Prząśniczka"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 13, 20, routes, train, "Tuwim"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 15, 15, routes, train, "Rzecki"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 15, 44, routes, train, "Jagna"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 16, 45, routes, train, "Łęcka"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 17, 5, routes, train, "Łodzianin"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 17, 15, routes, train, "Zosia"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 17, 45, routes, train, "Telimena"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 23, 35, routes, train, "Soplica"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Białystok" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 15, 10, routes, train, "Branicki"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Poznań", "Zielona Góra" }, new int[] { 0, 4, 4, 0 }, DateTime.Now.AddDays(i), 16, 0, routes, express, "Ex Lech"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Poznań", "Zielona Góra" }, new int[] { 0, 5, 10, 0 }, DateTime.Now.AddDays(i), 20, 0, routes, train, "Warta"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Olsztyn" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 5, 55, routes, train, "Warmia"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Gdańsk" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 7, 55, routes, train, "Rataj"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Włoszczowa", "Kraków" }, new int[] { 0, 3, 0 }, DateTime.Now.AddDays(i), 5, 35, routes, train, "Malinowski"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Włoszczowa", "Kraków" }, new int[] { 0, 3, 0 }, DateTime.Now.AddDays(i), 15, 10, routes, train, "Korczak"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Kraków" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 6, 50, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Kraków" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 7, 50, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Kraków" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 12, 50, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Kraków" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 9, 0, routes, express, "Ex Norwid"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Kraków" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 10, 55, routes, express, "Ex Sawa"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Kraków" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 16, 55, routes, express, "Ex Krakus"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Kraków" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 18, 55, routes, express, "Ex Lwów"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Włoszczowa", "Katowice" }, new int[] { 0, 3, 0 }, DateTime.Now.AddDays(i), 6, 55, routes, express, "Ex Polonia"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Włoszczowa", "Katowice" }, new int[] { 0, 3, 0 }, DateTime.Now.AddDays(i), 9, 55, routes, train, "Varsovia"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Włoszczowa", "Katowice" }, new int[] { 0, 3, 0 }, DateTime.Now.AddDays(i), 13, 55, routes, train, "Porta Moravica"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Włoszczowa", "Katowice", "Bielsko-Biała" }, new int[] { 0, 3, 5, 0 }, DateTime.Now.AddDays(i), 15, 55, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Włoszczowa", "Katowice" }, new int[] { 0, 3, 0 }, DateTime.Now.AddDays(i), 17, 54, routes, express, "Ex Comenius"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Włoszczowa", "Katowice" }, new int[] { 0, 3, 0 }, DateTime.Now.AddDays(i), 21, 20, routes, train, "Chopin"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Piotrków Tryb.", "Częstochowa", "Katowice" }, new int[] { 0, 1, 6, 0 }, DateTime.Now.AddDays(i), 14, 47, routes, train, "Wysocki"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Poznań", "Szczecin" }, new int[] { 0, 3, 5, 0 }, DateTime.Now.AddDays(i), 17, 58, routes, express, "Ex Chrobry"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Poznań", "Szczecin" }, new int[] { 0, 3, 10, 0 }, DateTime.Now.AddDays(i), 14, 57, routes, express, "Ex Mewa"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Poznań", "Szczecin" }, new int[] { 0, 5, 5, 0 }, DateTime.Now.AddDays(i), 6, 59, routes, express, "Ex Podkowiński"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Poznań" }, new int[] { 0, 6, 0 }, DateTime.Now.AddDays(i), 19, 0, routes, express, "Ex Krzywousty"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Poznań" }, new int[] { 0, 6, 0 }, DateTime.Now.AddDays(i), 9, 59, routes, express, "Ex BWE"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Lublin", "Rzeszów" }, new int[] { 0, 10, 0 }, DateTime.Now.AddDays(i), 5, 50, routes, train, "Pomarańczarka"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Piotrków Tryb.", "Częstochowa", "Katowice", "Bielsko-Biała" }, new int[] { 0, 1, 5, 15, 0 }, DateTime.Now.AddDays(i), 6, 15, routes, train, "Korfanty"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Piotrków Tryb.", "Częstochowa", "Katowice", "Bielsko-Biała" }, new int[] { 0, 2, 5, 15, 0 }, DateTime.Now.AddDays(i), 16, 14, routes, train, "Górnik"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Częstochowa", "Opole", "Wrocław", "Jelenia Góra" }, new int[] { 0, 4, 3, 10, 0 }, DateTime.Now.AddDays(i), 16, 27, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Częstochowa", "Opole", "Wrocław" }, new int[] { 0, 4, 3, 0 }, DateTime.Now.AddDays(i), 6, 10, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Łódź", "Wrocław", "Jelenia Góra" }, new int[] { 0, 10, 25, 0 }, DateTime.Now.AddDays(i), 5, 5, routes, train, "Asnyk"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Toruń", "Bydgoszcz" }, new int[] { 0, 2, 5, 0 }, DateTime.Now.AddDays(i), 14, 20, routes, train, "Ogiński"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Toruń", "Bydgoszcz", "Gdańsk" }, new int[] { 0, 2, 5, 15, 0 }, DateTime.Now.AddDays(i), 5, 25, routes, train, "Doker"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Toruń", "Bydgoszcz" }, new int[] { 0, 1, 5, 0 }, DateTime.Now.AddDays(i), 16, 31, routes, train, "Kujawiak"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Toruń", "Bydgoszcz" }, new int[] { 0, 1, 5, 0 }, DateTime.Now.AddDays(i), 8, 30, routes, train, "Kopernik"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Siedlce", "Terespol" }, new int[] { 0, 1, 0 }, DateTime.Now.AddDays(i), 19, 40, routes, train, "Polonez"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Siedlce", "Terespol" }, new int[] { 0, 3, 0 }, DateTime.Now.AddDays(i), 18, 10, routes, train, "Kraszewski"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Siedlce", "Terespol" }, new int[] { 0, 2, 0 }, DateTime.Now.AddDays(i), 16, 12, routes, train, "Niemcewicz"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Siedlce", "Terespol" }, new int[] { 0, 1, 0 }, DateTime.Now.AddDays(i), 7, 0, routes, train, "Skaryna"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Radom", "Kielce" }, new int[] { 0, 3, 0 }, DateTime.Now.AddDays(i), 19, 18, routes, train, "Nida"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Toruń", "Bydgoszcz", "Piła", "Szczecin" }, new int[] { 0, 2, 2, 5, 17, 0 }, DateTime.Now.AddDays(i), 15, 30, routes, train, "Moniuszko"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Kutno", "Toruń", "Bydgoszcz", "Piła" }, new int[] { 0, 2, 5, 19, 0 }, DateTime.Now.AddDays(i), 6, 30, routes, train, "Noteć"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Piotrków Tryb.", "Częstochowa", "Opole", "Wrocław" }, new int[] { 0, 2, 2, 2, 0 }, DateTime.Now.AddDays(i), 7, 35, routes, express, "Ex Odra"));
                connections.Add(CreateConnection(new string[] { "Warszawa", "Piotrków Tryb.", "Częstochowa", "Opole", "Wrocław" }, new int[] { 0, 2, 2, 2, 0 }, DateTime.Now.AddDays(i), 11, 35, routes, train, "Fredro"));

                connections.Add(CreateConnection(new string[] { "Olsztyn", "Gdańsk", "Słupsk", "Szczecin" }, new int[] { 0, 5, 4, 0 }, DateTime.Now.AddDays(i), 6, 41, routes, train, "Żuławy"));
                connections.Add(CreateConnection(new string[] { "Olsztyn", "Gdańsk", "Bydgoszcz", "Poznań", "Zielona Góra" }, new int[] { 0, 10, 5, 10, 0 }, DateTime.Now.AddDays(i), 9, 25, routes, train, "Ukiel"));
                connections.Add(CreateConnection(new string[] { "Olsztyn", "Gdańsk", "Słupsk", "Szczecin" }, new int[] { 0, 10, 4, 0 }, DateTime.Now.AddDays(i), 13, 30, routes, train, "Gryf"));
                connections.Add(CreateConnection(new string[] { "Olsztyn", "Warszawa", "Włoszczowa", "Kielce", "Kraków" }, new int[] { 0, 10, 1, 3, 0 }, DateTime.Now.AddDays(i), 5, 18, routes, train, "Orłowicz"));
                connections.Add(CreateConnection(new string[] { "Olsztyn", "Warszawa", "Piotrków Tryb.", "Częstochowa", "Katowice", "Bielsko-Biała" }, new int[] { 0, 15, 1, 2, 13, 0 }, DateTime.Now.AddDays(i), 7, 30, routes, train, "Kormoran"));
                connections.Add(CreateConnection(new string[] { "Olsztyn", "Warszawa", "Włoszczowa", "Kielce", "Kraków" }, new int[] { 0, 5, 2, 3, 0 }, DateTime.Now.AddDays(i), 9, 41, routes, train, "Kolberg"));
                connections.Add(CreateConnection(new string[] { "Olsztyn", "Warszawa", "Włoszczowa", "Kielce", "Kraków" }, new int[] { 0, 5, 2, 3, 0 }, DateTime.Now.AddDays(i), 13, 36, routes, train, "Żeromski"));
                connections.Add(CreateConnection(new string[] { "Olsztyn", "Warszawa", "Łódź" }, new int[] { 0, 5, 0 }, DateTime.Now.AddDays(i), 17, 30, routes, train, "Mazury"));
                connections.Add(CreateConnection(new string[] { "Olsztyn", "Warszawa" }, new int[] { 0, 0 }, DateTime.Now.AddDays(i), 19, 30, routes, train, "Warmia"));

                connections.Add(CreateConnection(new string[] { "Piła", "Bydgoszcz", "Toruń", "Kutno", "Warszawa" }, new int[] { 0, 17, 2, 1, 0 }, DateTime.Now.AddDays(i), 16, 45, routes, train, "Noteć"));

                connections.Add(CreateConnection(new string[] { "Poznań", "Bydgoszcz", "Gdańsk" }, new int[] { 0, 15, 0 }, DateTime.Now.AddDays(i), 6, 31, routes, train, "Bałtyk"));
                connections.Add(CreateConnection(new string[] { "Poznań", "Kutno", "Warszawa" }, new int[] { 0, 3, 0 }, DateTime.Now.AddDays(i), 6, 24, routes, express, "Ex Krzywousty"));
                connections.Add(CreateConnection(new string[] { "Poznań", "Kutno", "Warszawa" }, new int[] { 0, 3, 0 }, DateTime.Now.AddDays(i), 15, 26, routes, express, "Ex BWE"));
                connections.Add(CreateConnection(new string[] { "Poznań", "Kutno", "Warszawa", "Lublin", "Rzeszów", "Przemyśl" }, new int[] { 0, 4, 10, 25, 15, 0 }, DateTime.Now.AddDays(i), 9, 30, routes, train, "Gombrowicz"));
                connections.Add(CreateConnection(new string[] { "Poznań", "Wrocław", "Opole", "Katowice", "Kraków", "Rzeszów", "Przemyśl" }, new int[] { 0, 10, 4, 15, 5, 25, 0 }, DateTime.Now.AddDays(i), 6, 0, routes, train, "Siemiradzki"));
                connections.Add(CreateConnection(new string[] { "Poznań", "Wrocław", "Opole", "Katowice", "Kraków" }, new int[] { 0, 10, 6, 15, 0 }, DateTime.Now.AddDays(i), 2, 0, routes, train, "Uznam"));

                connections.Add(CreateConnection(new string[] { "Przemyśl", "Rzeszów", "Lublin", "Warszawa", "Kutno", "Toruń", "Bydgoszcz", "Gdańsk" }, new int[] { 0, 20, 24, 15, 1, 2, 15, 0 }, DateTime.Now.AddDays(i), 3, 33, routes, train, "Kochanowski"));
                connections.Add(CreateConnection(new string[] { "Przemyśl", "Rzeszów", "Kraków", "Częstochowa", "Opole", "Wrocław", "Poznań", "Szczecin" }, new int[] { 0, 8, 15, 15, 3, 5, 10, 0 }, DateTime.Now.AddDays(i), 6, 27, routes, train, "Matejko"));
                connections.Add(CreateConnection(new string[] { "Przemyśl", "Rzeszów", "Kraków", "Częstochowa", "Opole", "Wrocław", "Poznań", "Szczecin" }, new int[] { 0, 3, 35, 2, 5, 5, 15, 0 }, DateTime.Now.AddDays(i), 4, 31, routes, train, "Chełmoński"));
                connections.Add(CreateConnection(new string[] { "Przemyśl", "Rzeszów", "Kraków", "Katowice", "Opole", "Wrocław", "Poznań", "Szczecin" }, new int[] { 0, 10, 15, 15, 3, 5, 10, 0 }, DateTime.Now.AddDays(i), 18, 26, routes, train, "Przemyślanin"));
                connections.Add(CreateConnection(new string[] { "Przemyśl", "Rzeszów", "Lublin", "Warszawa", "Kutno", "Poznań" }, new int[] { 0, 33, 25, 15, 5, 0 }, DateTime.Now.AddDays(i), 7, 27, routes, train, "Gombrowicz"));
                connections.Add(CreateConnection(new string[] { "Przemyśl", "Rzeszów", "Kraków", "Katowice", "Opole", "Wrocław", "Poznań" }, new int[] { 0, 25, 15, 15, 5, 10, 0 }, DateTime.Now.AddDays(i), 12, 26, routes, train, "Siemiradzki"));
                connections.Add(CreateConnection(new string[] { "Przemyśl", "Rzeszów", "Kraków", "Katowice", "Opole", "Wrocław", "Jelenia Góra" }, new int[] { 0, 20, 10, 5, 2, 15, 0 }, DateTime.Now.AddDays(i), 11, 16, routes, train, "Mehoffer"));
                connections.Add(CreateConnection(new string[] { "Przemyśl", "Rzeszów", "Kraków", "Katowice", "Opole", "Wrocław" }, new int[] { 0, 20, 15, 15, 3, 0 }, DateTime.Now.AddDays(i), 14, 29, routes, train, "Wyspiański"));
                connections.Add(CreateConnection(new string[] { "Przemyśl", "Rzeszów", "Kraków", "Częstochowa", "Opole", "Wrocław", "Poznań", "Piła", "Słupsk" }, new int[] { 0, 2, 5, 3, 2, 12, 14, 1, 0 }, DateTime.Now.AddDays(i), 8, 49, routes, train, "Malczewski"));

                connections.Add(CreateConnection(new string[] { "Rzeszów", "Lublin", "Warszawa" }, new int[] { 0, 15, 0 }, DateTime.Now.AddDays(i), 16, 55, routes, train, "Pomarańczarka"));
                connections.Add(CreateConnection(new string[] { "Rzeszów", "Kraków", "Warszawa", "Gdańsk" }, new int[] { 0, 15, 5, 0 }, DateTime.Now.AddDays(i), 6, 0, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Rzeszów", "Lublin", "Warszawa", "Kutno", "Toruń", "Bydgoszcz" }, new int[] { 0, 17, 5, 1, 4, 0 }, DateTime.Now.AddDays(i), 12, 20, routes, train, "Staszic"));

                connections.Add(CreateConnection(new string[] { "Słupsk", "Gdańsk", "Warszawa", "Kraków" }, new int[] { 0, 3, 10, 0 }, DateTime.Now.AddDays(i), 7, 54, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Słupsk", "Gdańsk", "Warszawa", "Radom", "Kielce", "Kraków" }, new int[] { 0, 5, 5, 1, 2, 0 }, DateTime.Now.AddDays(i), 22, 25, routes, train, "Ustronie"));
                connections.Add(CreateConnection(new string[] { "Słupsk", "Gdańsk", "Warszawa", "Włoszczowa", "Kraków" }, new int[] { 0, 5, 5, 2, 0 }, DateTime.Now.AddDays(i), 8, 57, routes, train, "Pobrzeże"));
                connections.Add(CreateConnection(new string[] { "Słupsk", "Piła", "Poznań", "Wrocław", "Opole", "Częstochowa", "Kraków", "Rzeszów", "Przemyśl" }, new int[] { 0, 1, 19, 6, 2, 1, 7, 2, 0 }, DateTime.Now.AddDays(i), 5, 22, routes, train, "Malczewski"));
                connections.Add(CreateConnection(new string[] { "Słupsk", "Piła", "Poznań", "Wrocław", "Opole", "Katowice" }, new int[] { 0, 2, 17, 5, 3, 0 }, DateTime.Now.AddDays(i), 13, 26, routes, train, "Gwarek"));

                connections.Add(CreateConnection(new string[] { "Suwałki", "Białystok", "Warszawa", "Kutno", "Poznań", "Szczecin" }, new int[] { 0, 25, 10, 2, 5, 0 }, DateTime.Now.AddDays(i), 6, 46, routes, train, "Podlasiak"));
                connections.Add(CreateConnection(new string[] { "Suwałki", "Białystok", "Warszawa", "Włoszczowa", "Kraków" }, new int[] { 0, 20, 10, 2, 0 }, DateTime.Now.AddDays(i), 15, 37, routes, train, "Hańcza"));

                connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Kutno", "Warszawa", "Białystok", "Suwałki" }, new int[] { 0, 15, 3, 15, 20, 0 }, DateTime.Now.AddDays(i), 10, 41, routes, train, "Podlasiak"));
                connections.Add(CreateConnection(new string[] { "Szczecin", "Słupsk", "Gdańsk", "Olsztyn", "Białystok" }, new int[] { 0, 3, 15, 20, 0 }, DateTime.Now.AddDays(i), 11, 9, routes, train, "Rybak"));
                connections.Add(CreateConnection(new string[] { "Szczecin", "Słupsk", "Gdańsk", "Olsztyn" }, new int[] { 0, 4, 15, 0 }, DateTime.Now.AddDays(i), 14, 20, routes, train, "Żuławy"));
                connections.Add(CreateConnection(new string[] { "Szczecin", "Słupsk", "Gdańsk", "Olsztyn" }, new int[] { 0, 5, 5, 0 }, DateTime.Now.AddDays(i), 7, 18, routes, train, "Gryf"));
                connections.Add(CreateConnection(new string[] { "Szczecin", "Słupsk", "Gdańsk" }, new int[] { 0, 6, 0 }, DateTime.Now.AddDays(i), 17, 17, routes, train, "Albatros"));
                connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Wrocław", "Opole", "Częstochowa", "Kraków", "Rzeszów", "Przemyśl" }, new int[] { 0, 5, 10, 2, 15, 20, 11, 0 }, DateTime.Now.AddDays(i), 9, 52, routes, train, "Matejko"));
                connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Wrocław", "Opole", "Częstochowa", "Kraków", "Rzeszów", "Przemyśl" }, new int[] { 0, 5, 10, 2, 15, 20, 11, 0 }, DateTime.Now.AddDays(i), 11, 49, routes, train, "Chełmoński"));
                connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Wrocław", "Opole", "Katowice", "Kraków", "Rzeszów", "Przemyśl" }, new int[] { 0, 5, 10, 2, 15, 20, 10, 0 }, DateTime.Now.AddDays(i), 19, 35, routes, train, "Przemyślanin"));
                connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Wrocław", "Opole", "Katowice" }, new int[] { 0, 10, 15, 3, 0 }, DateTime.Now.AddDays(i), 5, 44, routes, train, "Dobrawa"));
                connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Wrocław", "Opole", "Katowice" }, new int[] { 0, 5, 15, 4, 0 }, DateTime.Now.AddDays(i), 13, 50, routes, train, "Barnim"));
                connections.Add(CreateConnection(new string[] { "Szczecin", "Zielona Góra", "Wrocław", "Opole", "Katowice" }, new int[] { 0, 5, 15, 4, 0 }, DateTime.Now.AddDays(i), 11, 35, routes, train, "Światowid"));
                connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Kutno", "Warszawa", "Lublin" }, new int[] { 0, 10, 4, 15, 0 }, DateTime.Now.AddDays(i), 14, 43, routes, train, "Gałczyński"));
                connections.Add(CreateConnection(new string[] { "Szczecin", "Zielona Góra", "Wrocław" }, new int[] { 0, 15, 0 }, DateTime.Now.AddDays(i), 7, 6, routes, train, "Swarożyc"));
                connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Kutno", "Warszawa" }, new int[] { 0, 10, 3, 0 }, DateTime.Now.AddDays(i), 5, 56, routes, express, "Ex Chrobry"));
                connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Kutno", "Warszawa" }, new int[] { 0, 5, 3, 0 }, DateTime.Now.AddDays(i), 7, 53, routes, express, "Ex Mewa"));
                connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Kutno", "Warszawa" }, new int[] { 0, 10, 4, 0 }, DateTime.Now.AddDays(i), 15, 59, routes, express, "Ex Podkowiński"));
                connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Kutno", "Łódź", "Piotrków Tryb.", "Częstochowa", "Kraków" }, new int[] { 0, 15, 3, 15, 2, 2, 0 }, DateTime.Now.AddDays(i), 4, 50, routes, train, "Wawel"));
                connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Kutno", "Łódź", "Piotrków Tryb.", "Częstochowa", "Kraków" }, new int[] { 0, 15, 3, 15, 2, 2, 0 }, DateTime.Now.AddDays(i), 6, 58, routes, train, "Osterwa"));
                connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Kutno", "Łódź", "Piotrków Tryb.", "Częstochowa", "Katowice" }, new int[] { 0, 15, 3, 15, 2, 2, 0 }, DateTime.Now.AddDays(i), 8, 49, routes, train, "Portowiec"));
                connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Kutno", "Łódź", "Piotrków Tryb.", "Częstochowa", "Kraków" }, new int[] { 0, 15, 3, 15, 2, 2, 0 }, DateTime.Now.AddDays(i), 12, 54, routes, train, "Barbakan"));
                connections.Add(CreateConnection(new string[] { "Szczecin", "Poznań", "Kutno", "Łódź" }, new int[] { 0, 15, 3, 0 }, DateTime.Now.AddDays(i), 16, 54, routes, train, "Włókniarz"));
                connections.Add(CreateConnection(new string[] { "Szczecin", "Piła", "Bydgoszcz", "Toruń", "Kutno", "Warszawa" }, new int[] { 0, 20, 5, 2, 2, 0 }, DateTime.Now.AddDays(i), 5, 49, routes, train, "Moniuszko"));

                connections.Add(CreateConnection(new string[] { "Terespol", "Siedlce", "Warszawa", "Włoszczowa", "Kraków" }, new int[] { 0, 3, 5, 2, 0 }, DateTime.Now.AddDays(i), 16, 23, routes, train, "Wit Stwosz"));
                connections.Add(CreateConnection(new string[] { "Terespol", "Siedlce", "Warszawa" }, new int[] { 0, 1, 0 }, DateTime.Now.AddDays(i), 5, 18, routes, train, "Polonez"));
                connections.Add(CreateConnection(new string[] { "Terespol", "Siedlce", "Warszawa" }, new int[] { 0, 3, 0 }, DateTime.Now.AddDays(i), 7, 23, routes, train, "Kraszewski"));
                connections.Add(CreateConnection(new string[] { "Terespol", "Siedlce", "Warszawa" }, new int[] { 0, 2, 0 }, DateTime.Now.AddDays(i), 10, 38, routes, train, "Niemcewicz"));
                connections.Add(CreateConnection(new string[] { "Terespol", "Siedlce", "Warszawa" }, new int[] { 0, 1, 0 }, DateTime.Now.AddDays(i), 19, 18, routes, train, "Skaryna"));

                connections.Add(CreateConnection(new string[] { "Wrocław", "Łódź", "Warszawa", "Białystok" }, new int[] { 0, 5, 15, 0 }, DateTime.Now.AddDays(i), 7, 0, routes, train, "Konopnicka"));
                connections.Add(CreateConnection(new string[] { "Wrocław", "Poznań", "Bydgoszcz", "Gdańsk" }, new int[] { 0, 7, 5, 0 }, DateTime.Now.AddDays(i), 16, 30, routes, train, "Piast"));
                connections.Add(CreateConnection(new string[] { "Wrocław", "Poznań", "Bydgoszcz", "Gdańsk" }, new int[] { 0, 17, 3, 0 }, DateTime.Now.AddDays(i), 13, 27, routes, train, "Pomorzanin"));
                connections.Add(CreateConnection(new string[] { "Wrocław", "Poznań", "Bydgoszcz", "Gdańsk" }, new int[] { 0, 14, 6, 0 }, DateTime.Now.AddDays(i), 6, 15, routes, train, "Mieszko"));
                connections.Add(CreateConnection(new string[] { "Wrocław", "Opole", "Częstochowa", "Warszawa", "Gdańsk" }, new int[] { 0, 1, 2, 5, 0 }, DateTime.Now.AddDays(i), 14, 16, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Wrocław", "Zielona Góra", "Szczecin" }, new int[] { 0, 15, 0 }, DateTime.Now.AddDays(i), 15, 46, routes, train, "Swarożyc"));
                connections.Add(CreateConnection(new string[] { "Wrocław", "Łódź", "Warszawa", "Lublin" }, new int[] { 0, 10, 10, 0 }, DateTime.Now.AddDays(i), 11, 0, routes, train, "Słowacki"));
                connections.Add(CreateConnection(new string[] { "Wrocław", "Opole", "Częstochowa", "Warszawa" }, new int[] { 0, 4, 5, 0 }, DateTime.Now.AddDays(i), 5, 3, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Wrocław", "Opole", "Katowice" }, new int[] { 0, 6, 0 }, DateTime.Now.AddDays(i), 7, 55, routes, train, "Panorama"));
                connections.Add(CreateConnection(new string[] { "Wrocław", "Opole", "Katowice", "Kraków", "Rzeszów", "Przemyśl" }, new int[] { 0, 2, 15, 15, 23, 0 }, DateTime.Now.AddDays(i), 6, 16, routes, train, "Wyspiański"));
                connections.Add(CreateConnection(new string[] { "Wrocław", "Opole", "Częstochowa", "Warszawa", "Gdańsk" }, new int[] { 0, 2, 5, 10, 0 }, DateTime.Now.AddDays(i), 10, 17, routes, express, "Ex Premium"));
                connections.Add(CreateConnection(new string[] { "Wrocław", "Opole", "Katowice", "Włoszczowa", "Kielce", "Radom", "Lublin" }, new int[] { 0, 2, 13, 2, 10, 3, 0 }, DateTime.Now.AddDays(i), 12, 52, routes, train, "Bolko"));
                connections.Add(CreateConnection(new string[] { "Wrocław", "Opole", "Katowice", "Włoszczowa", "Kielce", "Radom", "Lublin" }, new int[] { 0, 2, 11, 3, 12, 2, 0 }, DateTime.Now.AddDays(i), 8, 40, routes, train, "Sztygar"));
                connections.Add(CreateConnection(new string[] { "Wrocław", "Opole", "Częstochowa", "Piotrków Tryb.", "Warszawa"}, new int[] { 0, 2, 2, 2, 0 }, DateTime.Now.AddDays(i), 18, 33, routes, express, "Ex Odra"));
                connections.Add(CreateConnection(new string[] { "Wrocław", "Opole", "Częstochowa", "Piotrków Tryb.", "Warszawa" }, new int[] { 0, 2, 2, 2, 0 }, DateTime.Now.AddDays(i), 19, 6, routes, train, "Fredro"));

                connections.Add(CreateConnection(new string[] { "Zielona Góra", "Poznań", "Kutno", "Warszawa" }, new int[] { 0, 6, 3, 0 }, DateTime.Now.AddDays(i), 5, 52, routes, express, "Ex Lech"));
                connections.Add(CreateConnection(new string[] { "Zielona Góra", "Poznań", "Bydgoszcz", "Gdańsk", "Olsztyn" }, new int[] { 0, 15, 10, 15, 0 }, DateTime.Now.AddDays(i), 12, 55, routes, train, "Ukiel"));
                connections.Add(CreateConnection(new string[] { "Zielona Góra", "Poznań", "Bydgoszcz", "Gdańsk" }, new int[] { 0, 15, 15, 0 }, DateTime.Now.AddDays(i), 8, 46, routes, train, "Bachus"));
                connections.Add(CreateConnection(new string[] { "Zielona Góra", "Poznań", "Kutno", "Warszawa" }, new int[] { 0, 17, 6, 0 }, DateTime.Now.AddDays(i), 2, 53, routes, train, "Warta"));

                foreach (var connection in connections)
                {
                    context.Connections.Add(connection);
                }
                context.SaveChanges();
            }
            context.SaveChanges();
            base.Seed(context);
        }

        private Connection CreateConnection(string[] stations, int[] offsets, DateTime datetime, int hour, int minutes, List<Route> routes, Train train, string name)
        {
            ConnectionPart[] conParts = new ConnectionPart[stations.Length - 1];
            int offset = 0;
            string seat = seats;
            int numberOfSeats = 80;
            if (train.Type == "Ekspres")
            {
                seat = expressSeats;
                numberOfSeats = 60;
            }
            for (int i = 0; i < conParts.Length; i++)
            {
                var query = routes.Where(r => r.From == stations[i] && r.To == stations[i + 1]).First();
                int reserveTime = rand.Next(query.BestTime / 10);
                if (train.Type == "Ekspres")
                {
                    reserveTime = -reserveTime;
                }
                conParts[i] = new ConnectionPart()
                {
                    StartTime = new DateTime(datetime.Year, datetime.Month, datetime.Day, hour, minutes, 0).AddMinutes(offset + offsets[i]),
                    EndTime = new DateTime(datetime.Year, datetime.Month, datetime.Day, hour, minutes, 0).AddMinutes(offset + offsets[i] + query.BestTime + reserveTime),
                    Route = query,
                    FreeSeats = numberOfSeats,
                    Seats = seat
                };
                offset += offsets[i];
                offset += query.BestTime + reserveTime;
            }
            Connection con = new Connection()
            {
                StartPlace = stations[0],
                EndPlace = stations[stations.Length - 1],
                Train = train,
                Name = name,
                Parts = conParts.ToList()
            };
            return con;
        }

    }
}
