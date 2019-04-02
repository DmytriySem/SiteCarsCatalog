namespace DAL.Migrations
{
    using DAL.Entities;
    using DAL.InitializeDB;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.EF.CatalogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "DAL.EF.CatalogContext";
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DAL.EF.CatalogContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var init = new CatalogDBInitializer();

            for (int i = 0; i < init.brands.Length; i++)
            {
                byte[] image = System.IO.File.ReadAllBytes(init.path + "Brands" + "/" + init.brands[i] + ".png");
                Brand brand = new Brand
                {
                    Name = init.brands[i],
                    Photo = image
                };

                IList<Model> modelList = new List<Model>();
                for (int j = 0; j < init.models[i].Length; j++)
                {
                    Model model = new Model
                    {
                        Name = init.models[i][j],
                        PhotoUrl = init.models[i][j] + ".jpg",
                        Brand = brand,
                        BrandId = brand.Id
                    };
                    modelList.Add(model);

                    IList<Car> carList = new List<Car>();
                    for (int l = 0; l < init.rand.Next(CatalogDBInitializer.MIN_CARS_LIST, CatalogDBInitializer.MAX_CARS_LIST); l++)
                    {
                        Car car = new Car()
                        {
                            BrandId = i,
                            Brand = brand,
                            Color = init.getRandomColor(),
                            VolumeEngine = Math.Round(init.getRandomVolumeEngine(), 2),
                            Description = init.getRandomDescription(),
                            ModelId = j,
                            Model = model
                        };

                        IList<Cost> costList = new List<Cost>();
                        for (int m = 1; m < init.rand.Next(CatalogDBInitializer.MIN_COSTS_LIST, CatalogDBInitializer.MAX_COSTS_LIST); m++)
                        {
                            Cost cost = new Cost()
                            {
                                Date = init.getRandomDate(),
                                Price = init.getRandomPrice(),
                                CarId = l,
                                Car = car
                            };
                            costList.Add(cost);

                            car.Prices.Add(cost);
                        }
                        context.Prices.AddOrUpdate(costList.ToArray());

                        carList.Add(car);
                    }
                    context.Cars.AddOrUpdate(carList.ToArray());
                }
                context.Brands.AddOrUpdate(brand);
                context.Models.AddOrUpdate(modelList.ToArray());
            }

            context.SaveChanges();
        }
    }
}
