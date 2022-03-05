using System;
using System.Linq;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Identity;

namespace LibApp.Models
{
    public static class SeedData
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            await using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            using var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!roleManager.Roles.Any())
            {
                var roles = new[]
                {
                    Roles.Owner,
                    Roles.StoreManager,
                    Roles.User
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            if (context.MembershipTypes.Any())
                {
                    Console.WriteLine("Database already seeded");
                    return;
                }

                context.MembershipTypes.AddRange(
                    new MembershipType
                    {
                        Id = 1,
                        SignUpFee = 0,
                        DurationInMonths = 0,
                        DiscountRate = 0
                    },
                    new MembershipType
                    {
                        Id = 2,
                        SignUpFee = 30,
                        DurationInMonths = 1,
                        DiscountRate = 10
                    },
                    new MembershipType
                    {
                        Id = 3,
                        SignUpFee = 90,
                        DurationInMonths = 3,
                        DiscountRate = 15
                    },
                    new MembershipType
                    {
                        Id = 4,
                        SignUpFee = 300,
                        DurationInMonths = 12,
                        DiscountRate = 20
                    });

                context.Rentals.AddRange(
                new Rental
                {
                    Customer = new Customer
                    {
                        Birthdate = DateTime.Now.AddYears(-17),
                        HasNewsletterSubscribed = true,
                        MembershipTypeId = 1,
                        Name = "Adam Nowak"
                    },
                    Book = new Book
                    {
                        AuthorName = "Maria Kowal",
                        DateAdded = DateTime.Now.AddDays(-30),
                        GenreId = 3,
                        Name = "Gotuj z nami",
                        NumberAvailable = 18,
                        NumberInStock = 18,
                        ReleaseDate = DateTime.Now.AddDays(-60)
                    },
                    DateRented = DateTime.Now.AddDays(-1)
                },
                new Rental
                {
                    Customer = new Customer
                    {
                        Birthdate = DateTime.Now.AddYears(-27),
                        HasNewsletterSubscribed = false,
                        MembershipTypeId = 1,
                        Name = "Piotr Nowak"
                    },
                    Book = new Book
                    {
                        AuthorName = "Grzegorz Piotrowski",
                        DateAdded = DateTime.Now.AddDays(-250),
                        GenreId = 2,
                        Name = "Bardzo fajna ksiazka",
                        NumberAvailable = 10,
                        NumberInStock = 10,
                        ReleaseDate = DateTime.Now.AddDays(-300)
                    },
                    DateRented = DateTime.Now.AddDays(-21)
                }
                );

                await context.SaveChangesAsync();
            }
        }
}