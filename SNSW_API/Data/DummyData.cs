using SNSW_API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SNSW_API.Data
{
    public class DummyData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SNSWContext>();
                context.Database.EnsureCreated();
                //context.Database.Migrate();

                // Look for any insurers to check if database has been setup and populated with dummy data
                if (context.Insurers != null && context.Insurers.Any())
                    return;   // DB has already been seeded

                var insurers = GetInsurers().ToArray();
                context.Insurers.AddRange(insurers);
                context.SaveChanges();

                var vehicles = GetVehicles().ToArray();
                context.Vehicles.AddRange(vehicles);
                context.SaveChanges();

                var registrationdetails = GetRegistrationDetails().ToArray();
                context.RegistrationDetails.AddRange(registrationdetails);
                context.SaveChanges();

                var registrations = GetRegistrations(context).ToArray();
                context.Registrations.AddRange(registrations);
                context.SaveChanges();

            }
        }

        public static List<Insurer> GetInsurers()   // Insurer dummy data
        {
            List<Insurer> insurers = new List<Insurer>() {
                new Insurer {Name = "Allianz", Code = 32},
                new Insurer {Name="AAMI", Code = 17},
                new Insurer {Name="GIO", Code = 13},
                new Insurer {Name="NRMA", Code = 27}
            };
            return insurers;
        }

        public static List<Vehicle> GetVehicles()   // Vehicles dummy data
        {
            List<Vehicle> vehicles = new List<Vehicle>() {
              new Vehicle {Type="Wagon", Make = "BMW", Model = "X4 M40i", Colour = "Blue", Vin = "12389347324", Tare_weight = 1700, Gross_mass = null },
              new Vehicle {Type="Hatch", Make = "Toyota", Model = "Corolla", Colour = "Silver", Vin = "54646546313", Tare_weight = 1432, Gross_mass = 1500},
              new Vehicle {Type="Sedan", Make = "Mercedes", Model = "X4 M40i", Colour = "Blue", Vin = "87676676762", Tare_weight = 1700, Gross_mass = null},
              new Vehicle {Type="SUV", Make = "Jaguar", Model = "F pace", Colour = "Green", Vin = "65465466541", Tare_weight = 1620, Gross_mass = null}
            };
            return vehicles;
        }

        public static List<RegistrationDetail> GetRegistrationDetails()     // Registration dummy data (for "registration" child node under registrations parent node)
        {
            List<RegistrationDetail> registrationdetails = new List<RegistrationDetail>() {
              new RegistrationDetail {Expired=false, Expiry_date = new DateTime(2021, 2, 5, 23, 15, 30) },
              new RegistrationDetail {Expired=true, Expiry_date = new DateTime(2020, 3, 1, 23, 15, 30) },
              new RegistrationDetail {Expired=false, Expiry_date = new DateTime(2020, 12, 8, 23, 15, 30) },
              new RegistrationDetail {Expired=false, Expiry_date = new DateTime(2021, 7, 20, 23, 15, 30) }
            };
            return registrationdetails;
        }

        public static List<Registration> GetRegistrations(SNSWContext db)
        {

            List<Registration> registrations = new List<Registration>() {           // assign all relational nodes to the list collection which will be used to migrate into DB to populate dummy data
                new Registration {
                Plate_number = "EBF28E",
                RegistrationDetails = new List<RegistrationDetail>(db.RegistrationDetails.Skip(0).Take(1)),         // knowing the data is in order, just use Skip and Take to assign child nodes to "registrations"
                Vehicles = new List<Vehicle>(db.Vehicles.Skip(0).Take(1)),
                Insurers = new List<Insurer>(db.Insurers.Skip(0).Take(1))
                },
                new Registration {
                Plate_number = "CXD82F",
                RegistrationDetails = new List<RegistrationDetail>(db.RegistrationDetails.Skip(1).Take(1)),
                Vehicles = new List<Vehicle>(db.Vehicles.Skip(1).Take(1)),
                Insurers = new List<Insurer>(db.Insurers.Skip(1).Take(1))
                },
                new Registration {
                Plate_number = "WOP29P",
                RegistrationDetails = new List<RegistrationDetail>(db.RegistrationDetails.OrderBy(m => m.Expiry_date).Skip(2).Take(1)),
                Vehicles = new List<Vehicle>(db.Vehicles.Skip(2).Take(1)),
                Insurers = new List<Insurer>(db.Insurers.Skip(2).Take(1))
                },
                new Registration {
                Plate_number = "QWX55Z",
                RegistrationDetails = new List<RegistrationDetail>(db.RegistrationDetails.OrderBy(m => m.Expiry_date).Skip(3).Take(1)),
                Vehicles = new List<Vehicle>(db.Vehicles.Skip(3).Take(1)),
                Insurers = new List<Insurer>(db.Insurers.Skip(3).Take(1))
                }
            };
            return registrations;
        }
    }
}
