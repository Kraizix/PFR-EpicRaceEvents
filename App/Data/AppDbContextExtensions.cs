using App.Models;

namespace App.Data
{
    public static class AppDbContextExtensions
    {
        public static void Seed(this AppDbContext dbContext)
        {
            if (dbContext.ResultItems.Any())
            {

            }
            var Categories = new List<Category>() {
                new Category(){
                    Name ="Super Car",
                    Description ="Lorem ipsum",
                    Image ="https://upload.wikimedia.org/wikipedia/commons/0/07/The_frontview_of_McLaren_650S_Coup%C3%A9.JPG?uselang=fr",
                    Color ="#e6df0a",
                 },
                new Category(){
                    Name ="Hyper Car",
                    Description ="Lorem ipsum",
                    Image ="https://www.largus.fr/images/images/hypercar.jpg?width=712&quality=80",
                    Color ="#090255",
                },
                new Category(){
                    Name ="Japan Race Car",
                    Description ="Lorem ipsum",
                    Image ="https://www.turbo.fr/sites/default/files/styles/article_690x405/public/2020-09/Nissan-GT-R.jpg?itok=jYVNOU-N",
                    Color ="#32e60a",
                },
                new Category(){
                    Name ="German Classics",
                    Description ="Lorem ipsum",
                    Image ="https://www.largus.fr/images/images/vw-golf-gti-mk1-noir-10.jpg",
                    Color ="#62656f",
                },
                new Category(){
                    Name ="Italian Classics",
                    Description ="Lorem ipsum",
                    Image ="https://upload.wikimedia.org/wikipedia/commons/thumb/1/1a/1962_Ferrari_250_GTO_34_2.jpg/1200px-1962_Ferrari_250_GTO_34_2.jpg?20050425173242",
                    Color ="#c90808",
                },
                new Category(){
                    Name ="Muscle Car",
                    Description ="Lorem ipsum",
                    Image ="https://upload.wikimedia.org/wikipedia/commons/thumb/f/f3/%2769_Plymouth_Road_Runner_%28Auto_classique_Showtime_Muscle_Cars_%2712%29.JPG/420px-%2769_Plymouth_Road_Runner_%28Auto_classique_Showtime_Muscle_Cars_%2712%29.JPG",
                    Color ="#ea06c0",
                },
                new Category(){
                    Name ="American Legend",
                    Description ="Lorem ipsum",
                    Image ="https://upload.wikimedia.org/wikipedia/commons/thumb/d/d7/Chevrolet_Impala_1964_Lowrider.JPG/1199px-Chevrolet_Impala_1964_Lowrider.JPG?20060812105512",
                    Color ="#f79845",
                },
                new Category(){
                    Name ="Sport Car",
                    Description ="Lorem ipsum",
                    Image ="https://upload.wikimedia.org/wikipedia/commons/thumb/7/79/Audi_TTS_Coup%C3%A9_front_20100328.jpg/330px-Audi_TTS_Coup%C3%A9_front_20100328.jpg",
                    Color ="#0adbe6",
                },
                new Category(){
                    Name ="High Performance Sport Car",
                    Description ="Lorem ipsum",
                    Image ="https://www.topgear-magazine.fr/wp-content/uploads/2019/10/alpine_a110s_essai_1.jpg",
                    Color ="#0f6204",
                }
            };
            var Vehicles = new List<Vehicle>() {
                new Vehicle ()
                {
                    BuildYear = 1987,
                    Power = 7,
                    Brand = "Ferrari",
                    Model = "F40",
                    Image ="https://commons.wikimedia.org/wiki/File:F40_Ferrari_20090509.jpg?uselang=fr",
                    Categories = Categories.FindAll(x => x.Name == "Super Car" || x.Name == "Italian Classics"),

                },
                new Vehicle ()
                {
                    BuildYear = 2014,
                    Power = 7,
                    Brand = "Lamborghini",
                    Model = "Huracan",
                    Image ="https://upload.wikimedia.org/wikipedia/commons/thumb/3/31/2014-03-04_Geneva_Motor_Show_1377.JPG/420px-2014-03-04_Geneva_Motor_Show_1377.JPG",
                    Categories = Categories.FindAll(x => x.Name =="Super Car"),
                },
                new Vehicle ()
                {
                    BuildYear = 1967,
                    Power = 3,
                    Brand = "Ford",
                    Model = "Mustang",
                    Image ="https://www.gallery-aaldering.com/wp-content/uploads/gallery/27869472/27869472-60.jpg",
                    Categories = Categories.FindAll(x => x.Name =="Muscle Car" || x.Name =="American Legend"),
                },
                new Vehicle ()
                {
                    BuildYear = 2021,
                    Power = 5,
                    Brand = "Dodge",
                    Model = "Charger R/T",
                    Image ="https://www.ccarprice.com/products/Dodge-Charger-RT-2021.jpg",
                    Categories = Categories.FindAll(x => x.Name =="Muscle Car"),
                },
                new Vehicle ()
                {
                    BuildYear = 2018,
                    Power = 9,
                    Brand = "Pagani",
                    Model = "Huayra R",
                    Image= "https://cs2.gtaall.eu/screenshots/d4861/2020-06/original/75988b266d8726e1f06ab08f708d32accdb44276/807960-GTAIV-2020-06-15-13-09-19-92.jpg",
                    Categories = Categories.FindAll(x => x.Name =="Hyper Car"),
                },
                new Vehicle()
                {
                    BuildYear = 2019,
                    Power = 10,
                    Brand = "Bugatti",
                    Model = "Chiron",
                    Image = "https://i.gaw.to/vehicles/photos/09/27/092767_2019_bugatti_Chiron.jpg?640x400",
                    Categories = Categories.FindAll(x => x.Name =="Hyper Car"),
                },
                new Vehicle()
                {
                    BuildYear = 1995,
                    Power = 7,
                    Brand = "Toyota",
                    Model = "Supra Yakuza Edition",
                    Image = "https://www.turbo.fr/sites/default/files/styles/article_690x405/public/2021-08/878920%20%281%29.jpg?itok=WG_9n7w6",
                    Categories = Categories.FindAll(x => x.Name =="Japan Race Car"),
                },
                new Vehicle()
                {
                    BuildYear = 2009,
                    Power = 6,
                    Brand = "Honda",
                    Model = "S2000 Racing",
                    Image = "https://images.hgmsites.net/hug/2009-honda-s2000-driving-on-a-racetrack_100177825_h.jpg",
                    Categories = Categories.FindAll(x => x.Name =="Japan Race Car" || x.Name =="Sport Car"),
                },
                new Vehicle()
                {
                    BuildYear = 1991,
                    Power = 6,
                    Brand = "BMW",
                    Model = "E30",
                    Image = "https://www.automobile-sportive.com/guide/bmw/325ise30/bmw-325is-e30.jpg",
                    Categories = Categories.FindAll(x => x.Name =="Sport Car" || x.Name == "German Classics"),
                },
                new Vehicle()
                {
                    BuildYear = 2007,
                    Power = 7,
                    Brand = "Porsche",
                    Model = "911 GT3",
                    Image = "https://www.classicdriver.com/sites/default/files/styles/two_third_slider/public/cars_images/feed_387856/2007-porsche-911-gt3-rs",
                    Categories = Categories.FindAll(x => x.Name =="German Classics" || x.Name == "High Performance Sport Car"),
                }
            };
            dbContext.Categories.AddRange(Categories);
            dbContext.Vehicles.AddRange(Vehicles);
            dbContext.SaveChanges();
        }
    }
}